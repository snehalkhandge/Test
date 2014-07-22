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
                url: '/presales/business/edit/:personalId',
                config: {
                    templateUrl: '/app/presales/business/editBusiness.html',
                    title: 'Edit Business',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Add Business'
                    }
                }
            }
        ];
    }
})();