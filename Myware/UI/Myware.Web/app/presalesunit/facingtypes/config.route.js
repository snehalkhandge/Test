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
                url: '/presalesunit/facingtypes',
                config: {
                    templateUrl: '/app/presalesunit/facingtypes/facingType.html',
                    title: 'Manage Facing Types',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Facing Types'
                    }
                }
            }
        ];
    }
})();