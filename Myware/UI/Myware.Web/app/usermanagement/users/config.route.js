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
                    templateUrl: '/app/usermanagement/users/users.html',
                    title: 'Manage Users',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Users'
                    }
                }
            },
            {
                url: '/usermanagement/user/edit/:userId',
                config: {
                    templateUrl: '/app/usermanagement/users/editUser.html',
                    title: 'Edit User'
                }
            }
        ];
    }
})();