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
                url: '/usermanagement/permissions',
                config: {
                    templateUrl: '/app/usermanagement/permissions/permissions.html',
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