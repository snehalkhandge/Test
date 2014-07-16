(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('ContactStatusTypes', ContactStatusTypes);

    ContactStatusTypes.$inject = ['$scope', '$timeout', '$interval', 'common', 'authService', 'contactStatusTypeFactory'];

    function ContactStatusTypes($scope, $timeout, $interval, common, authService, contactStatusTypeFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Contact Status Types';
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
            
            contactStatusTypeFactory.getContactStatusTypes($scope.page, $scope.itemsPerPage, $scope.searchQuery)
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


        $scope.addContactStatusType = function () {

            $scope.inserted = {
                $id: $scope.results.length + 1,
                Id:'',
                Name: ''
            };
            $scope.results.push($scope.inserted);
            
        };

        $scope.checkContactStatusTypeName = function (contactStatusTypeName) {
            /*/if (data !== 'awesome') {
                return "Username 2 should be `awesome`";
            }*/

            if (contactStatusTypeName === '') {
                return "Contact status type name is a required field";
            }

            var deferred = $q.defer();
            deferred.resolve(

                contactStatusTypeFactory.uniqueContactStatusType(contactStatusTypeName).then(function (result) {
                    if (result == 'true') {
                        return "All contact status type name has to be unique";
                    }
                    
                })
            );
            
            return deferred.promise;
        };

        $scope.deleteCustomerType = function (contactStatusType) {
            
            if (contactStatusType.Id != '') {
                common.logger.error("Sorry! you cannot delete a contact status type.");
            } else {
                $scope.results.splice(contactStatusType.$id - 1, 1);
            }
            

        };

        $scope.saveContactStatusType = function (contactStatusType, Id) {

            angular.extend(contactStatusType, { Id: Id});
            angular.extend(contactStatusType, { UserId: authService.authentication.userId });

            contactStatusTypeFactory.saveContactStatusType(contactStatusType)
                .then(function () {                    
                    $scope.page = 1;                    
                    common.logger.success("Successfully updated the item");
                    activate();
                });
                        
        };

    }
})();