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
                url: '/presales/customer/edit/:customerId',
                config: {
                    templateUrl: '/app/presales/customers/listCustomer.html',
                    title: 'Edit Customer',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Add Customer'
                    }
                }
            }
        ];
    }
})();