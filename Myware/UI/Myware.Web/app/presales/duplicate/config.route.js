(function () {
    'use strict';

    angular
        .module('app.presales')
        .run(['routehelper', function (routehelper) {
            routehelper.configureRoutes(getRoutes());
        }]);

    function getRoutes() {
        return [
            
            {
                url: '/presales/duplicate/importCustomers',
                config: {
                    templateUrl: '/app/presales/duplicate/importCustomers.html',
                    title: 'Import Duplicate Customers',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Import Duplicate Customers'
                    }
                }
            }
        ];
    }
})();