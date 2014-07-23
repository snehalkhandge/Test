(function () {
    'use strict';

    angular
        .module('app.presales')
        .controller('EditBusiness', EditBusiness);

    EditBusiness.$inject = ['$scope', '$timeout', '$location', '$routeParams', '$upload', 'common', 'authService', 'personalFactory', 'localityFactory', 'businessFactory'];

    function EditBusiness($scope, $timeout, $location, $routeParams, $upload, common, authService, personalFactory, localityFactory, businessFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;                 
       
        var personalId = ($routeParams.personalId) ? parseInt($routeParams.personalId) : 0;
        var businessId = ($routeParams.businessId) ? parseInt($routeParams.businessId) : 0;
        var contactType = ($routeParams.contactType) ? $routeParams.contactType : "";

        var defaultForm = {
            Id: businessId,
            PersonalInformationId : personalId,
            CompanyName: '',
            Designation: '',
            BusinessOrIndustry: '',
            InvestmentCapacity: '',
            Fax: '',
            Website: '',
            Locality: '',
            City:'',
            BusinessContactNumbers: [],
            ImageUrl: '',
            Type:''
        };


        $scope.business = defaultForm;


        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        $scope.title = (businessId > 0) ? 'Edit Business Information' : 'Add Business Information';
        $scope.buttonText = (businessId > 0) ? 'Update Business Information' : 'Add New Business Information';

        $scope.showEnquiry = false;
        $scope.showOther = false;

        $scope.Localities = {};
        


        $scope.isClean = function () {
            return angular.equals(original, $scope.business);
        }

        
        $scope.saveBusiness = function (business) {
            
            angular.extend(business, { UserId: authService.authentication.userId });
                        
            businessFactory.saveBusiness(business)
                .then(function (result) {

                    $scope.business.Id = result.Id;
                    $scope.buttonText = "Update Business Information"
                    common.logger.success("Successfully saved the item");
                    $scope.alerts.push({ type: 'success', msg: "Successfully saved the item" });

                    if ($scope.showEnquiry) {

                      $timeout(function () {
                            $location.path("/presales/contactenquiry/edit/" +personalId + "/0");
                      }, 500);
                    }


                });
        };

        $scope.resetForm = function () {
            $scope.myForm.$setPristine();
            $scope.business = defaultForm;
        }

        
        $scope.addContactNumber = function () {

            $scope.inserted = {
                $id: $scope.business.BusinessContactNumbers.length + 1,
                PhoneNumber: '',
                Type: ''
                
            };
            $scope.business.BusinessContactNumbers.push($scope.inserted);
        };

        $scope.removeContactNumber = function($index)
        {
            $scope.business.BusinessContactNumbers.splice($index, 1);
        }

        $scope.onFileSelect = function ($files) {

            //$files: an array of files selected, each file has name, size, and type.
            for (var i = 0; i < $files.length; i++) {
                var file = $files[i];
                $scope.upload = $upload.upload({
                    url: common.apiUrl + '/savePersonalInfoImage',
                    method: 'POST',
                    // headers: {'header-key': 'header-value'},
                    // withCredentials: true,
                    data: { businessObject: { id: personalId } },
                    file: file,
                }).progress(function (evt) {
                    $scope.progress = parseInt(100.0 * evt.loaded / evt.total);

                }).success(function (data, status, headers, config) {
                    // file is uploaded successfully
                    $scope.business.ImageUrl = data.replace(/"/g, "");
                    common.logger.success("Successfully saved the image.");
                    $scope.alerts.push({ type: 'success', msg: "Successfully saved the image." });
                })
                .error(function () {
                    $scope.business.ImageUrl = "http://placehold.it/150x150.png&text=Error upload image";
                    common.logger.error("Error, while saving the image.");
                    $scope.alerts.push({ type: 'danger', msg: "Error, while saving the image." });
                });
            }
        };


        activate();

        function activate() {

            if (businessId != 0)
            {

                businessFactory.getbusinessById(businessId)
                    .then(function (result) {                        
                        $scope.business = result;

                        if (result.BusinessContactNumbers.length == 0)
                        {
                            $scope.addContactNumber();
                        }

                    }, function (reason) {
                        common.logger.error(reason);
                        $scope.alerts.push({ type: 'danger', msg: reason });

                    });

            }
            else {
                $scope.addContactNumber();
            }



            personalFactory.getPersonalById(personalId)
                .then(function (result) {
                    $scope.business.ImageUrl = result.ImageUrl;

                    if (result.ContactType == "Enquiry")
                    {
                        $scope.showEnquiry = true;
                    }
                    else
                    {
                        $scope.showOther = true;
                    }

                }, function (reason) {
                    common.logger.error(reason);
                    $scope.alerts.push({ type: 'danger', msg: reason });

                });

            localityFactory.getAllLocality()
                         .then(function (result) {

                             $scope.Localities = result.Results;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });

            
        }

    }
})();