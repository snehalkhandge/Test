(function () {
    'use strict';

    angular
        .module('app.presales')
        .controller('EditPersonal', EditPersonal);

    EditPersonal.$inject = ['$scope', '$timeout', '$location', '$http',
                            '$routeParams', 'common', 'authService', 'personalFactory',
                            'localityFactory', 'sourceFactory', 'customerTypeFactory', 'locationFactory'];

    function EditPersonal($scope, $timeout, $location, $http,
                          $routeParams, common, authService, personalFactory,
                          localityFactory, sourceFactory, customerTypeFactory, locationFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;                 
       
        var personalId = ($routeParams.personalId) ? parseInt($routeParams.personalId) : 0;
        var defaultForm = {
            Id: personalId,
            FirstName: '',
            LastName: '',
            Email: '',
            Address: '',
            Locality: '',
            City: '',
            PinCode: '',
            DateOfBirth: '',
            Remarks: '',
            AnniversaryDate: '',
            Campaign: '',
            SubCampaign: '',
            ContactType: '',
            ContactNumbers: [],
            BusinessInformation:[]
        };

        $scope.personal = defaultForm;


        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        $scope.title = (personalId > 0) ? 'Edit Personal Information' : 'Add Personal Information';
        $scope.buttonText = (personalId > 0) ? 'Update Personal Information' : 'Add New Personal Information';

        $scope.Cities = {};
        $scope.Localities = {};
        $scope.CustomerTypes = {};
        $scope.Campaigns = {};
        $scope.SubCampaigns = {};
        $scope.SelectedLocalities = {};
        $scope.showUpdateButton = false;

        $scope.isClean = function () {
            return angular.equals(original, $scope.personal);
        }

        
        $scope.savePersonal = function (personal) {
            
            angular.extend(personal, { UserId: authService.authentication.userId });
                        
            personalFactory.savePersonal(personal)
                .then(function (result) {
                    $scope.buttonText = "Update Personal Information"
                    common.logger.success("Successfully saved the item");
                    $scope.alerts.push({ type: 'success', msg: "Successfully saved the item" });

                    $scope.personal.Id = result.Id;

                    var businessId = 0;

                    if(result.BusinessInformation.length > 0)
                    {
                        businessId = result.BusinessInformation[0].Id;
                    }

                    $timeout(function () {
                        $location.path("/presales/business/edit/" + result.Id + "/" + result.ContactType + "/"+businessId);
                    }, 500);

                });
        };

        $scope.resetForm = function () {
            $scope.myForm.$setPristine();
            $scope.personal = defaultForm;
        }

        var newDate = new Date();
        $scope.addContactNumber = function () {

            $scope.inserted = {
                $id: $scope.personal.ContactNumbers.length + 1,
                PhoneNumber: '',
                Type: ''
                
            };
            $scope.personal.ContactNumbers.push($scope.inserted);
        };

        $scope.removeContactNumber = function ($index) {
            $scope.personal.ContactNumbers.splice($index, 1);
        };

        $scope.dataToUpdate = {};

        $scope.updateData = function () {
            $scope.personal = $scope.dataToUpdate;
        };

        $scope.isUniqueEmail = function (email) {

            if (email.length < 10) {
                return;
            }
            $http({
                url: common.apiUrl + '/customerEmailIsUnique',
                method: "POST",
                data: {
                    email: email
                },

            }).success(function (data) {
                        
                        if(!data.IsUnique)
                        {
                            $scope.dataToUpdate = data.PersonalInformation;
                            common.logger.error("Error,  There is account associated with it.");
                            $scope.alerts.push({ type: 'danger', msg: "Error, There is account associated with it." });
                            $scope.showUpdateButton = true;

                        }
           }).error(function (data, status, headers, config) {
                        $scope.alerts.push({ type: 'danger', msg: "Error, Unable to contact the serve." });
           });
        };

        $scope.isUniqueContactNumber = function (number) {

            if (number.length < 8)
            {
                return;
            }
            
            $http.get(common.apiUrl + '/customerContactNumberIsUnique/' + number)
                    .success(function (data) {

                        if (!data.IsUnique) {
                            $scope.dataToUpdate = data.PersonalInformation;
                            common.logger.error("Error,  There is account associated with it.");
                            $scope.alerts.push({ type: 'danger', msg: "Error, There is account associated with it." });
                            $scope.showUpdateButton = true;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        $scope.alerts.push({ type: 'danger', msg: "Error, Unable to contact the serve." });
                    });

        };

        activate();

        function activate() {

            if (personalId != 0)
            {

                personalFactory.getPersonalById(personalId)
                    .then(function (result) {                        
                        $scope.personal = result;

                        if(result.ContactNumbers.length == 0)
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


            localityFactory.getAllLocality()
                         .then(function (result) {

                             $scope.Localities = result.Results;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });



            locationFactory.getAllLocations()
                         .then(function (result) {

                             $scope.Cities = result.Results;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });


            customerTypeFactory.getAllCustomerTypes()
                         .then(function (result) {

                             $scope.CustomerTypes = result.Results;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });


            sourceFactory.getAllSource()
                         .then(function (result) {

                             $scope.SubCampaigns = result.Results;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });


            sourceFactory.getAllParentSource()
                         .then(function (result) {

                             $scope.Campaigns = result.Results;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });

            
        }

        $scope.initDate = new Date('2016-15-20');
        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];

        $scope.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        };

        $scope.openDOB = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.openedDOB = true;
        };
        $scope.today = function () {
            $scope.dt = new Date();
        };
        $scope.today();

        $scope.minDate = new Date('1930-15-20');

    }
})();