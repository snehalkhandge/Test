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
                url: '/presales/personal/edit/:personalId',
                config: {
                    templateUrl: '/app/presales/personal/editPersonal.html',
                    title: 'Edit Personal',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Add Personal'
                    }
                }
            }
        ];
    }
})();