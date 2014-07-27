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
                url: '/presales/import/customers',
                config: {
                    templateUrl: '/app/presales/import/importCustomers.html',
                    title: 'Import Customers',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Import Customers'
                    }
                }
            }
        ];
    }
})();