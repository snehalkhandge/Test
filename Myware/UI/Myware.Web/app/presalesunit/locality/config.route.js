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
                url: '/presalesunit/locality',
                config: {
                    templateUrl: '/app/presalesunit/locality/locality.html',
                    title: 'Manage Locations',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Locality'
                    }
                }
            }
        ];
    }
})();