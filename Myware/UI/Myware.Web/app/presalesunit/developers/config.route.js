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
                url: '/presalesunit/developers',
                config: {
                    templateUrl: '/app/presalesunit/developers/developers.html',
                    title: 'Manage Developers',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Developers'
                    }
                }
            }
        ];
    }
})();