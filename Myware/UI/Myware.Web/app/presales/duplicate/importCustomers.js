(function () {
    'use strict';

    angular
        .module('app.presales')
        .controller('ImportDuplicateCustomers', ImportDuplicateCustomers);

    ImportDuplicateCustomers.$inject = ['$scope', '$http', '$timeout', 'common', 'fileReaderFactory', 'personalFactory', 'businessFactory', 'contactEnquiryFactory', 'ngTableParams', '$sce', 'authService'];

    function ImportDuplicateCustomers($scope, $http, $timeout, common, fileReaderFactory, personalFactory, businessFactory, contactEnquiryFactory, ngTableParams, $sce, authService) {
        var log = common.logger.info;
        var $q = common.$q;
        
        $scope.title = 'Import Duplicate Customers';
        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        $scope.csv = {
            invalidContent: [],
            header: true,
            separator: ',',            
            result: [],
            headers:[]
        };

        
        $scope.data = [];
        $scope.errors = [];
        $scope.progress = 0;

        
        

        activate();

        function activate() {
        
        };

        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10           // count per page
        }, {
            counts:[10,25,50],
            total: 0, // length of data
            getData: function ($defer, params) {

                $timeout(function () {
                    // update table params
                    params.total($scope.data.length);
                    $scope.csv.result = $scope.data.slice((params.page() - 1) * params.count(), params.page() * params.count());
                    // set new data
                    $defer.resolve($scope.data);
                    $scope.alerts.splice(0, 1);
                }, 500);

                
            }
        });




        $scope.getFile = function ($files) {
            $scope.alerts.push({ type: 'success', msg: "File reading in progress." });
            
            $scope.progress = 0;
            fileReaderFactory.readAsText($files[0], $scope)
                          .then(function (result) {
                              $scope.alerts.splice(0, 1);
                              $scope.alerts.push({ type: 'success', msg: "Successfully read the file." });
                              processData(result);

                          });
        };

        $scope.$on("fileProgress", function (e, progress) {
            $scope.progress = (progress.loaded / progress.total)*100;
        });
                
        function processData(content) {

            $scope.alerts.splice(0, 1);
            $scope.alerts.push({ type: 'success', msg: "Processing Data." });
            var lines = [];
            var lines = content.split(/\r\n|\n/);
            var result = [];
            var invalidData = [];
            var start = 0;

            var columnCount = lines[0].split(",").length;

            var headers = [];
                headers = lines[0].split(",");
                start = 1;
            

            $scope.progress = 0;
            
            for (var i = start; i < lines.length; i++) {
                var obj = {};
                var currentline = lines[i].split($scope.csv.separator);
                if (currentline.length === columnCount)
                {
                    
                    for (var j = 0; j < headers.length; j++) {
                         obj[headers[j]] = currentline[j];                            
                    }
                    
                    obj["hasError"] = false;
                    obj["errorMessage"] = "";
                    obj["id"] = i;
                    $scope.progress = (i/lines.length)*100;

                    if (obj.Email != '' || obj.ContactNumbers != '')
                    {
                        result.push(obj);
                    }
                    else {
                        invalidData.push(obj);
                    }
                    

                }
                else
                {
                    var invalidObject = [];
                    for (var k = 0; k < currentline.length; k++) {
                        invalidObject[k] = currentline[k];
                    }

                    invalidData.push(invalidObject);
                }
            }

            $scope.csv.invalidContent = invalidData;

            $scope.alerts.splice(0, 1);
            $scope.alerts.push({ type: 'success', msg: "Processing completed." });
            
            $scope.data = result.slice(0, 10);
            $scope.tableParams.reload();
            $scope.progress = 0;
            
        };

        $scope.saveAllRowsToServer = function() {

           $scope.alerts.splice(0, 1);
           $scope.alerts.push({ type: 'success', msg: "Saving data to server started." });
           $scope.progress = 0;
           
           var totalBatches = Math.ceil($scope.data.length / 10);
           var i = 0;
           
           function synchronizeDataInsert(rows) {
               var defer = $q.defer();
               var processData;               

               for (i = 0; i <= totalBatches; i++) {

                   processData = processDeferSaveData(rows.slice((i * 10), (i + 1) * 10));

                    defer.resolve(processData);
               }
               
               return defer.promise;
               
           };

           synchronizeDataInsert($scope.data);

        };
        

        function processDeferSaveData(rows) {
            var promises = [];
            var promise;
            var i = 0;

            for (i = 0; i < rows.length; i++) {

                promise = saveDataToServer(rows[i])
                            .then(function (result) {
                                if (result.hasError) {                                    
                                    $scope.errors.push(result);
                                }
                                $scope.data.splice(0, 1);
                                $scope.progress = (i / (2*rows.length)) * 100;
                            });

                promises.push(promise);
            };

            $q.all(promises).then(function () {              
                
                if ($scope.data.length == 0)
                {
                    $scope.data = $scope.errors;
                    $scope.tableParams.reload();
                    $scope.progress = 100;
                    $scope.alerts.splice(0, 1);
                    $scope.alerts.push({ type: 'success', msg: "Saving data to server completed." });
                }

                
                return {};
            });
        };

        function saveDataToServer(item) {

            var deferred = $q.defer();
            var personal = {
                Id: item.PersonalInformationId,
                FirstName: item.FirstName,
                LastName: item.LastName,
                Email: item.Email,
                Address: item.Address,
                Locality: item.Locality,
                City: item.City,
                PinCode: item.PinCode,
                DateOfBirth: item.DateOfBirth,
                Remarks: item.Remarks,
                AnniversaryDate: item.AnniversaryDate,
                Campaign: item.Campaign,
                SubCampaign: item.SubCampaign,
                ContactType: item.ContactType,
                ContactNumbers: [],
                BusinessInformation: [],
                UserId: authService.authentication.userId

            };

            if (item.ContactNumbers != '') {
                personal.ContactNumbers.push({
                    PhoneNumber: item.ContactNumbers,
                    Type: 'Secondary'
                });
            }

            
          var savePersonal =  personalFactory.savePersonal(personal)
                                .then(function (result) {
                                    item.Id = result.Id;
                                    item.PersonalInformationId = result.Id;
                                    //Save Business
                                    saveBusinessInformation(item);
                                    
                                    if(item.ContactType.toUpperCase() == 'ENQUIRY')
                                    {   
                                        //Save Enquiry
                                        saveEnquiryInformation(item);
                                    }
                                    
                                    $http.post(common.apiUrl + '/deleteDuplicateData/' + personal.Id)
                                         .success(function (data) {

                                          })
                                         .error(function (data, status, headers, config) {
                                                
                                         });



                                    return item;

                                }, function (error) {
                                    item.hasError = true;
                                    item.errorMessage = error.data.Message;
                                    if (error.data.Message.indexOf("Duplicate") > -1) {
                                        var res = error.data.Message.split("==");
                                        if (res.length == 2) {
                                            item.Id = res[1];
                                            item.PersonalInformationId = res[1];
                                        }
                                    }


                                    return item;
                                });

          deferred.resolve(savePersonal);
          return deferred.promise;
        };

        function saveBusinessInformation(item) {


            var business = {
                Id: item.BusinessId,
                PersonalInformationId: item.PersonalInformationId,
                CompanyName: item.CompanyName,
                Designation: item.Designation,
                BusinessOrIndustry: item.BusinessOrIndustry,
                InvestmentCapacity: item.InvestmentCapacity,
                Fax: item.Fax,
                Website: item.Website,
                Locality: item.Locality,
                City: item.City,
                BusinessContactNumbers: [],
                ImageUrl: item.ImageUrl,
                Type: '',
                UserId: authService.authentication.userId
            };

            if (item.BusinessContactNumbers != '') {

                var partsOfStr = item.BusinessContactNumbers.split(',');

                if (partsOfStr.length > 1)
                {
                    angular.forEach(values, function (value, key) {
                        this.push({
                            PhoneNumber: value,
                            Type: 'Secondary'
                        });
                    }, business.BusinessContactNumbers);
                }
                else {

                    business.BusinessContactNumbers.push({
                        PhoneNumber: item.BusinessContactNumbers,
                        Type: 'Secondary'
                    });

                }
            }


            businessFactory.saveBusiness(business)
                .then(function (result) {
                                        
                });
            
        };

        function saveEnquiryInformation(item) {


            var enquiry = {
                Id: item.ContactEnquiryId,
                PersonalInformationId: item.PersonalInformationId,
                Remarks: item.Remarks,
                AssignedDate: item.AssignedDate,
                LeadStatus: item.LeadStatus,
                TransactionType: item.TransactionType,
                LookingForType: item.LookingForType,
                BudgetFrom: item.BudgetFrom,
                BudgetTo: item.BudgetTo,
                SaleAreaFrom: item.SaleAreaFrom,
                SaleAreaTo: item.SaleAreaTo,
                CarpetAreaFrom: item.CarpetAreaFrom,
                CarpetAreaTo: item.CarpetAreaTo,
                PropertyAge: item.PropertyAge,
                IsFurnished: item.IsFurnished,
                OfferedRate: item.OfferedRate,
                EnquiryDate: item.EnquiryDate,
                FacingType: item.FacingType,
                ContactStatus: '',
                PreferredUnitTypes: [],
                PreferredLocations: [],
                UserId: authService.authentication.userId
            };

            if (item.PreferredUnitTypes != '') {

                enquiry.PreferredUnitTypes.push({
                    UnitType: item.PreferredUnitTypes
                    
                });
            }

            if (item.EnquiryDate != '') {
                item.EnquiryDate = new Date(item.EnquiryDate);
            } else {
                item.EnquiryDate = new Date("05/05/1980");
            }

            contactEnquiryFactory.savecontactEnquiry(enquiry)
                .then(function (result) {

                });


        };


        
    }
})();





