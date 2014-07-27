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
                url: '/presales/customers',
                config: {
                    templateUrl: '/app/presales/customers/listCustomers.html',
                    title: 'Customer Contact List',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Customer Contact List'
                    }
                }
            },

            {
                url: '/presales/customer/detail/:personalId',
                config: {
                    templateUrl: '/app/presales/customers/detailCustomerPreSales.html',
                    title: 'Customer Detail',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Customer Detail'
                    }
                }
            }
        ];
    }
})();