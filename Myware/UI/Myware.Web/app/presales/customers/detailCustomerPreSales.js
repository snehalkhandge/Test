(function () {
    'use strict';

    angular
        .module('app.presales')
        .controller('DetailCustomerPreSales', DetailCustomerPreSales);

    DetailCustomerPreSales.$inject = ['$scope', 'common', '$routeParams', 'personalFactory', 'businessFactory', 'contactEnquiryFactory'];

    function DetailCustomerPreSales($scope, common, $routeParams, personalFactory, businessFactory, contactEnquiryFactory) {
        var log = common.logger.info;
        var $q = common.$q;
        var personalId = ($routeParams.personalId) ? parseInt($routeParams.personalId) : 0;

        

        $scope.title = 'Customer Detail';                
        $scope.personal = {};        
        $scope.ContactEnquiries = [];
        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];

        $scope.oneAtATime = true;

        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        activate();

        function activate() {
            getPersonalInformation();
        };

        function getPersonalInformation() {

            if (personalId != 0) {

                personalFactory.getPersonalById(personalId)
                        .then(function (result) {
                            $scope.personal = result;

                        }, function (reason) {
                            common.logger.error(reason);
                            $scope.alerts.push({ type: 'danger', msg: reason });

                        });
            } else {
                $scope.alerts.push({ type: 'danger', msg: "Error, Invalid personal information id." });
            }

        };
        

        $scope.getEnquiries = function () {

            if (personalId != 0) {

                if ($scope.ContactEnquiries.length == 0) {

                    contactEnquiryFactory.getAllContactEnquiriesByPersonalId(personalId)
                        .then(function (result) {
                            $scope.ContactEnquiries = result;

                        }, function (reason) {
                                    common.logger.error(reason);
                                    $scope.alerts.push({ type: 'danger', msg: reason });

                        });

                }
                

            } else {
                $scope.alerts.push({ type: 'danger', msg: "Error, Invalid personal information id." });
            }
        };


    }
})();