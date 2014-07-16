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
                url: '/presalesunit/contactstatustypes',
                config: {
                    templateUrl: '/app/presalesunit/contactstatustypes/contactStatusType.html',
                    title: 'Manage Contact Status Types',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Contact Status Types'
                    }
                }
            }
        ];
    }
})();