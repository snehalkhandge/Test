(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('EditCompany', EditCompany);

    EditCompany.$inject = ['$scope', '$timeout', '$routeParams', 'common','authService', 'companyFactory', 'localityFactory'];

    function EditCompany($scope, $timeout, $routeParams, common,authService, companyFactory, localityFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;                 
       
        var companyId = ($routeParams.companyId) ? parseInt($routeParams.companyId) : 0;
        var defaultForm = {
            Id: companyId,
            Name: '',
            Address: '',
            Pin: '',
            FaxNumber: '',
            ReceiptFormat: '',
            LocalityId: '',
            ContactNumbers: []
        };


        $scope.company = defaultForm;

        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        $scope.title = (companyId > 0) ? 'Edit Company' : 'Add Company';
        $scope.buttonText = (companyId > 0) ? 'Update Company' : 'Add New Company';              
        $scope.Localities = {};
        $scope.SelectedLocalities = {};


        $scope.isClean = function () {
            return angular.equals(original, $scope.customer);
        }

        
        $scope.saveCompany = function (company) {
            
            angular.extend(company, { UserId: authService.authentication.userId });
            delete company.Location;
            delete company.Locality;
            
            companyFactory.saveCompany(company)
                .then(function () {
                    $scope.buttonText = "Update Company"
                    common.logger.success("Successfully saved the item");
                    $scope.alerts.push({ type: 'success', msg: "Successfully saved the item" });
                });
        };

        $scope.resetForm = function () {
            $scope.myForm.$setPristine();
            $scope.company = defaultForm;
        }

        var newDate = new Date();
        $scope.addContactNumber = function () {

            $scope.inserted = {
                $id: $scope.company.ContactNumbers.length + 1,
                PhoneNumber: '',
                Type: ''
                
            };
            $scope.company.ContactNumbers.push($scope.inserted);
        };

        $scope.removeContactNumber = function($index)
        {
            $scope.company.ContactNumbers.splice($index,1);
        }

        activate();

        function activate() {

            if (companyId != 0)
            {

                companyFactory.getCompanyById(companyId)
                    .then(function (result) {                        
                        $scope.company = result;

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
            
        }

        


    }
})();