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
                url: '/presalesunit/locations',
                config: {
                    templateUrl: '/app/presalesunit/locations/locations.html',
                    title: 'Manage Locations',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Locations'
                    }
                }
            }
        ];
    }
})();