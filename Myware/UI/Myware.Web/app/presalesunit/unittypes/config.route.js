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
                url: '/presalesunit/unittypes',
                config: {
                    templateUrl: '/app/presalesunit/unittypes/unitType.html',
                    title: 'Manage Unit Types',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Unit Types'
                    }
                }
            }
        ];
    }
})();