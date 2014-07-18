(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('DetailCompany', DetailCompany);

    DetailCompany.$inject = ['$scope', '$timeout', '$routeParams', 'common', 'companyFactory', 'localityFactory'];

    function DetailCompany($scope, $timeout, $routeParams, common, companyFactory, localityFactory) {
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

        $scope.title = 'Company Details';
        


        $scope.isClean = function () {
            return angular.equals(original, $scope.customer);
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
            
        }

        


    }
})();