(function () {
    'use strict';

    angular
        .module('app.usermanagement')
        .controller('Roles', Roles);

    Roles.$inject = ['$scope', '$timeout', '$interval', 'common', 'roleFactory', 'permissionFactory'];

    function Roles($scope, $timeout, $interval, common, rolesFactory, permissionFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Roles';
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

            rolesFactory.getRoles($scope.page, $scope.itemsPerPage, $scope.searchQuery)
                .then(function (result) {


                    permissionFactory.getAllPermissions()
                                 .then(function (result) {

                                     $scope.permissions = result.Results;

                                 }, function (reason) {
                                     common.logger.error(reason);

                                 });


                    $scope.results = result.Results;
                    $scope.totalItems = result.TotalItems;

                    if ($scope.totalItems <= $scope.itemsPerPage) {
                        $scope.hidePagination = true;
                    }

                }, function (reason) {
                        common.logger.error(reason);

                });            
        }


        $scope.addRole = function () {

            $scope.inserted = {
                $id: $scope.results.length + 1,
                Id:'',
                Name: '',
                SelectedPermissions: [],

            };
            $scope.results.push($scope.inserted);
            
        };

        $scope.checkRoleName = function (roleName) {
            /*/if (data !== 'awesome') {
                return "Username 2 should be `awesome`";
            }*/

            if (roleName === '') {
                return "Role name is a required field";
            }

            var deferred = $q.defer();
            deferred.resolve(

                rolesFactory.uniqueRole(roleName).then(function (result) {                    
                    if (result == 'true') {
                        return "All role name has to be unique";
                    }
                    
                })
            );
            
            return deferred.promise;
        };

        $scope.deleteRole = function (role) {
            /*common.logger.error("Permission Id : " + permission.Id);
            common.logger.error("Index : " + permission.$id); */
            if (role.Id != '') {
                common.logger.error("Sorry! you cannot delete a role.");
            } else {
                $scope.results.splice(role.$id - 1, 1);
            }
            

        };

        $scope.saveRole = function (role,Id) {

            angular.extend(role, { Id: Id });            

            rolesFactory.saveRole(role)
                .then(function () {                    
                    $scope.page = 1;                    
                    common.logger.success("Successfully updated the item");
                    activate();
                });
                        
        };

        
        $scope.permissions = [];

        $scope.showPermissions = function (role) {
            var selected = [];

            if (role.SelectedPermissions != null) {
                angular.forEach(role.SelectedPermissions, function (x) {

                    angular.forEach($scope.permissions, function (s) {
                        if (x == s.Id) {
                            selected.push(s.Name);
                        }

                    });

                });
            } else if (role.RolePermissions.length > 0) {
                    
                angular.forEach(role.RolePermissions, function (x) {

                   angular.forEach($scope.permissions, function (s) {
                       if (x.PermissionId == s.Id) {
                                selected.push(s.Name);
                            }

                   });
                        
               });
            }

            return selected.length ? selected.join(', ') : 'Not set';
        };

    }
})();