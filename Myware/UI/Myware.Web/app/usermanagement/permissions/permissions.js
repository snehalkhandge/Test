(function () {
    'use strict';

    angular
        .module('app.usermanagement')
        .controller('Permissions', Permissions);

    Permissions.$inject = ['$scope', '$timeout', '$interval', 'common', 'permissionFactory'];

    function Permissions($scope, $timeout, $interval, common, permissionFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Permissions';
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
            
            permissionFactory.getPermissions($scope.page, $scope.itemsPerPage, $scope.searchQuery)
                .then(function (result) {

                    $scope.results = result[0].Results;
                    $scope.totalItems = result[0].TotalItems;

                    if ($scope.totalItems <= $scope.itemsPerPage) {
                        $scope.hidePagination = true;
                    }

                }, function (reason) {
                        common.logger.error(reason);

                });            
        }


        $scope.addPermission = function () {

            $scope.inserted = {
                $id: $scope.results.length + 1,
                Id:'',
                Name: ''
            };
            $scope.results.push($scope.inserted);
            
        };

        $scope.checkPermissionName = function (permissionName) {
            /*/if (data !== 'awesome') {
                return "Username 2 should be `awesome`";
            }*/

            if (permissionName === '') {
                return "Permission name is a required field";
            }

            var deferred = $q.defer();
            deferred.resolve(

                permissionFactory.uniquePermission(permissionName).then(function (result) {                    
                    if (result == 'true') {
                        return "All permission name has to be unique";
                    }
                    
                })
            );
            
            return deferred.promise;
        };

        $scope.deletePermission = function (permission) {
            /*common.logger.error("Permission Id : " + permission.Id);
            common.logger.error("Index : " + permission.$id); */
            if (permission.Id != '') {
                common.logger.error("Sorry! you cannot delete a permission.");
            } else {
                $scope.results.splice(permission.$id - 1, 1);
            }
            

        };

        $scope.savePermission = function (permission,Id) {

            angular.extend(permission, { Id: Id });            

            permissionFactory.savePermission(permission)
                .then(function () {                    
                    $scope.page = 1;                    
                    common.logger.success("Successfully updated the item");
                    activate();
                });
                        
        };

    }
})();