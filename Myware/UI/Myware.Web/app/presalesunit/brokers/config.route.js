(function () {
    'use strict';

    angular
        .module('app.usermanagement')
        .run(['routehelper', function (routehelper) {
            routehelper.configureRoutes(getRoutes());
        }]);

    function getRoutes() {
        return [
            {
                url: '/usermanagement/users',
                config: {
                    templateUrl: '/app/usermanagement/components/users.html',
                    title: 'Manage Users',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Users'
                    }
                }
            },
            {
                url: '/usermanagement/roles',
                config: {
                    templateUrl: '/app/usermanagement/components/roles.html',
                    title: 'Manage Roles',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Roles'
                    }
                }
            },
            {
                url: '/usermanagement/permissions',
                config: {
                    templateUrl: '/app/usermanagement/components/permissions.html',
                    title: 'Manage Roles',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Permissions'
                    }
                }
            }
        ];
    }
})();