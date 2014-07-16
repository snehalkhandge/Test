(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .run(['routehelper', function (routehelper) {
            routehelper.configureRoutes(getRoutes());
        }]);

    function getRoutes() {
        return [
            
            {
                url: '/presalesunit/customertypes',
                config: {
                    templateUrl: '/app/presalesunit/customertypes/customerType.html',
                    title: 'Manage Customer Types',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Customer Types'
                    }
                }
            }
        ];
    }
})();