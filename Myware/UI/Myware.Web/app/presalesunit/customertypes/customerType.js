(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('CustomerTypes', CustomerTypes);

    CustomerTypes.$inject = ['$scope', '$timeout', '$interval', 'common', 'authService', 'customerTypeFactory'];

    function CustomerTypes($scope, $timeout, $interval, common, authService, customerTypeFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Customer Types';
        $scope.results = {};
        $scope.totalItems = 0;
        $scope.itemsPerPage = 7;
        $scope.page = 1;
        $scope.searchQuery = "all";
        $scope.setPage = function (pageNo) {
            $scope.page = pageNo;
        };
        $scope.pageChanged = function () {
            activate();
        };
        $scope.hidePagination = false;

        activate();

        function activate() {
            
            customerTypeFactory.getCustomerTypes($scope.page, $scope.itemsPerPage, $scope.searchQuery)
                .then(function (result) {

                    $scope.results = result.Results;
                    $scope.totalItems = result.TotalItems;

                    if ($scope.totalItems <= $scope.itemsPerPage) {
                        $scope.hidePagination = true;
                    }

                }, function (reason) {
                        common.logger.error(reason);

                });            
        }


        $scope.addCustomerType = function () {

            $scope.inserted = {
                $id: $scope.results.length + 1,
                Id:'',
                Name: ''
            };
            $scope.results.push($scope.inserted);
            
        };

        $scope.checkCustomerTypeName = function (customerTypeName) {
            /*/if (data !== 'awesome') {
                return "Username 2 should be `awesome`";
            }*/

            if (customerTypeName === '') {
                return "Customer type name is a required field";
            }

            var deferred = $q.defer();
            deferred.resolve(

                customerTypeFactory.uniqueCustomerType(customerTypeName).then(function (result) {
                    if (result == 'true') {
                        return "All customer type name has to be unique";
                    }
                    
                })
            );
            
            return deferred.promise;
        };

        $scope.deleteCustomerType = function (customerType) {
            
            if (customerType.Id != '') {
                common.logger.error("Sorry! you cannot delete a customer type.");
            } else {
                $scope.results.splice(customerType.$id - 1, 1);
            }
            

        };

        $scope.saveCustomerType = function (customerType, Id) {

            angular.extend(customerType, { Id: Id});
            angular.extend(customerType, { UserId: authService.authentication.userId });

            customerTypeFactory.saveCustomerType(customerType)
                .then(function () {                    
                    $scope.page = 1;                    
                    common.logger.success("Successfully updated the item");
                    activate();
                });
                        
        };

    }
})();