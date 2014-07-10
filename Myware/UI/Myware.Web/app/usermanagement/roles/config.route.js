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
                url: '/usermanagement/roles',
                config: {
                    templateUrl: '/app/usermanagement/roles/roles.html',
                    title: 'Manage Roles',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Roles'
                    }
                }
            }
        ];
    }
})();