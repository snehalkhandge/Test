(function () {
    'use strict';

    angular
        .module('app.presales')
        .controller('ImportCustomers', ImportCustomers);

    ImportCustomers.$inject = ['$scope', '$timeout', 'common', 'fileReaderFactory', 'personalFactory', 'businessFactory', 'contactEnquiryFactory', 'ngTableParams', '$sce', 'authService'];

    function ImportCustomers($scope, $timeout, common, fileReaderFactory, personalFactory, businessFactory, contactEnquiryFactory, ngTableParams, $sce, authService) {
        var log = common.logger.info;
        var $q = common.$q;
        
        $scope.title = 'Import Customer Lead';        
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

        
        var data = [];
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
                    params.total(data.length);
                    $scope.csv.result = data.slice((params.page() - 1) * params.count(), params.page() * params.count());
                    // set new data
                    $defer.resolve(data);
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

        function processHeader(temp_headers) {

            var headers = [];
            var i = 0;
            angular.forEach(temp_headers, function (value, key) {
                var processString = '';
                
                switch (value) {
                    case 'Contact Type': processString = 'ContactType';
                        break;
                    case 'FirstName': processString = 'FirstName';
                        break;
                    case 'LastName': processString = 'LastName';
                        break;
                    case 'MobileNo': processString = 'ContactNumbers';
                        break;
                    case 'EmailID': processString = 'Email';
                        break;
                    case 'Address': processString = 'Address';
                        break;
                    case 'City': processString = 'City';
                        break;
                    case 'Locality': processString = 'Locality';
                        break;                    
                    case 'PinCode': processString = 'PinCode';
                        break;
                    case 'DOB': processString = 'DateOfBirth';
                        break;
                    case 'AnniversaryDate': processString = 'AnniversaryDate';
                        break;
                    case 'Source': processString = 'Campaign';
                        break;
                    case 'SubSource': processString = 'SubCampaign';
                        break;
                    case 'Company Name': processString = 'CompanyName';
                        break;
                    case 'Designation': processString = 'Designation';
                        break;
                    case 'Business Industry': processString = 'BusinessOrIndustry';
                        break;
                    case 'Business City': processString = 'BusinessCity';
                        break;
                    case 'Business Locality': processString = 'BusinessLocality';
                        break;
                    case 'Invest Capicity': processString = 'InvestmentCapacity';
                        break;
                    case 'Other Mobile': processString = 'BusinessContactNumbers';
                        break;
                    case 'Fax': processString = 'Fax';
                        break;
                    case 'Website': processString = 'Website';
                        break;
                    case 'Enq. Date': processString = 'EnquiryDate';
                        break;
                    case 'Looking For': processString = 'LookingForType';
                        break;
                    case 'Unit Type': processString = 'PreferredUnitTypes';
                        break;
                    case 'Trasaction Type': processString = 'TransactionType';
                        break;
                    case 'Property Age': processString = 'PropertyAge';
                        break;
                    case 'Budget From': processString = 'BudgetFrom';
                        break;
                    case 'Budget To': processString = 'BudgetTo';
                        break;
                    case 'Furnising': processString = 'IsFurnished';
                        break;
                    case 'Sale Area From': processString = 'SaleAreaFrom';
                        break;
                    case 'Sale Area  To': processString = 'SaleAreaTo';
                        break;
                    case 'Offered Rate': processString = 'OfferedRate';
                        break;
                    case 'Carpet Area From': processString = 'CarpetAreaFrom';
                        break;
                    case 'Carpet Area  To': processString = 'CarpetAreaTo';
                        break;
                    case 'Lead Status': processString = 'LeadStatus';
                        break;
                    case 'Remark': processString = 'Remarks';
                        break;
                }
                
                if (processString != '')
                {
                    this.push({
                        Index: i,
                        OrginalString: value,
                        UpdateString: processString
                    });
                }
                    
                i++;

            }, headers);

            $scope.csv.headers = headers;
            return headers;
        };

        
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
            if ($scope.csv.header) {
                
                 headers = processHeader(lines[0].split(","));
                start = 1;
            }

            $scope.progress = 0;
            
            for (var i = start; i < lines.length; i++) {
                var obj = {};
                var currentline = lines[i].split($scope.csv.separator);
                if (currentline.length === columnCount)
                {
                    if ($scope.csv.header) {
                        for (var j = 0; j < headers.length; j++) {
                            if (headers[j].UpdateString == 'Email' || headers[j].UpdateString.indexOf("Date") > -1)
                            {
                                obj[headers[j].UpdateString] = currentline[j + 1];
                            }
                            else {
                                obj[headers[j].UpdateString] = currentline[j + 1].replace(/[^\w\s]/gi, '');
                            }
                            
                        }
                    } else {
                        for (var k = 0; k < currentline.length; k++) {
                            obj[k] = currentline[k];
                        }
                    }

                    obj["hasError"] = false;
                    obj["errorMessage"] = "";
                    obj["id"] = i;
                    $scope.progress = (i/lines.length)*100;

                    result.push(obj);

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
            data = result;
            $scope.tableParams.reload();
            //saveAllRowsToServer(result);
        };

        $scope.saveAllRowsToServer = function() {

            $scope.alerts.splice(0, 1);
            $scope.alerts.push({ type: 'success', msg: "Saving data to server started." });
            $scope.progress = 0;
           var rows = data.slice(1, 20);
            var errors = [];
            angular.forEach(rows, function (item, key) {
                var result = saveDataToServer(item);
                if (!result.hasError) {
                    data.splice(key, 1);
                }
                else {
                    //obj["errorMessage"] = "";
                    this.push(result);
                }
                $scope.progress = (key / rows.length) * 100;
            }, errors);

            //data = errors;
            $scope.tableParams.reload();
            $scope.alerts.splice(0, 1);
            $scope.alerts.push({ type: 'success', msg: "Saving data to server completed." });
        };
        
        function saveDataToServer(item) {

            var deferred = $q.defer();
            var personal = {
                Id: '',
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
                ContactNumbers: [{
                    PhoneNumber: item.ContactNumbers,
                    Type: 'Secondary'
                }],
                BusinessInformation: [],
                UserId: authService.authentication.userId

            };



            personalFactory.savePersonal(personal)
                .then(function (result) {
                    item.Id = result.Id;
                    //Save Business
                    //Save Enquiry


                    return item;
                    
                }, function (error) {
                    item.hasError = true;
                    item.errorMessage = error.data.Message;
                    if(error.data.Message.indexOf("Duplicate") > -1) {
                        var res = error.data.Message.split("==");
                        if(res.length == 2)
                        {
                            item.Id = res[1];
                            item.PersonalInformationId = res[1];

                            $scope.alerts.push({ type: 'danger', msg: error.data.Message + "-- Personal Info Id--" + item.PersonalInformationId });
                        }
                    }

                    
                    return item;
                });

            deferred.resolve(item);
            return deferred.promise;
        }

        function saveBusinessInformation(item)
        {
            var businessInfo = {
                Id: '',
                PersonalInformationId: '',
                CompanyName: '',
                Designation: '',
                BusinessOrIndustry: '',
                InvestmentCapacity: '',
                Fax: '',
                Website: '',
                Locality: '',
                City: '',
                BusinessContactNumbers: [],
                ImageUrl: '',
                Type: ''
            };
        }

        function saveEnquiryInformation(item)
        {


            var enquiryInfo = {
                Id: '',
                PersonalInformationId: '',
                Remarks: '',
                AssignedDate: '',
                LeadStatus: '',
                TransactionType: '',
                LookingForType: '',
                BudgetFrom: '',
                BudgetTo: '',
                SaleAreaFrom: '',
                SaleAreaTo: '',
                CarpetAreaFrom: '',
                CarpetAreaTo: '',
                PropertyAge: '',
                IsFurnished: '',
                OfferedRate: '',
                EnquiryDate: '',
                FacingType: '',
                ContactStatus: '',
                PreferredUnitTypes: [],
                PreferredLocations: [],
            };
        }
    }
})();





