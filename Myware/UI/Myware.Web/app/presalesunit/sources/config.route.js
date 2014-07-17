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
                url: '/presalesunit/sources',
                config: {
                    templateUrl: '/app/presalesunit/sources/sources.html',
                    title: 'Manage Sources',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Sources'
                    }
                }
            }
        ];
    }
})();