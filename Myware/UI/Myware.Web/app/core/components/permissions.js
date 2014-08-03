(function () {
    'use strict';

    var serviceId = 'permissionService';

    angular.module('app.core')
            .factory(serviceId, permissionService);

    permissionService.$inject = ['$rootScope'];

    function permissionService($rootScope) {

        var permissionList;
        return {
            setPermissions: function (permissions) {
                permissionList = permissions;
                $rootScope.$broadcast('permissionsChanged')
            },
            hasPermission: function (permission) {
                permission = permission.trim();                
                var isPermissionExist = false;
                angular.forEach(permissionList, function (value, key) {
                    if (value.trim() == permission) {
                        isPermissionExist = true;
                    }
                        
                });

                return isPermissionExist;
           }
        };

    }

})();


(function () {
    'use strict';
    angular
        .module('app.core')
        .directive('hasPermission', hasPermission);

    function hasPermission(permissionService) {
        return {
            link: function (scope, element, attrs) {
                if (!angular.isString(attrs.hasPermission))
                    throw "hasPermission value must be a string";

                var value = attrs.hasPermission.trim();
                var notPermissionFlag = value[0] === '!';
                if (notPermissionFlag) {
                    value = value.slice(1).trim();
                }

                function toggleVisibilityBasedOnPermission() {
                    var hasPermission = permissionService.hasPermission(value);

                    if (hasPermission && !notPermissionFlag || !hasPermission && notPermissionFlag)
                        element.show();
                    else
                        element.hide();
                }
                toggleVisibilityBasedOnPermission();
                scope.$on('permissionsChanged', toggleVisibilityBasedOnPermission);
            }
        };
    }

})();

