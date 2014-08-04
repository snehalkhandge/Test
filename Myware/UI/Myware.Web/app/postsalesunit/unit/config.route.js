(function () {
    'use strict';

    angular
        .module('app.postsalesunit')
        .run(['routehelper', function (routehelper) {
            routehelper.configureRoutes(getRoutes());
        }]);

    function getRoutes() {
        return [
            
            {
                url: '/postsalesunit/unit/edit/:unitId',
                config: {
                    templateUrl: '/app/postsalesunit/unit/editUnit.html',
                    title: 'Edit Unit',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Add Unit'
                    }
                }
            },
            {
                url: '/postsalesunit/unit/detail/:unitId',
                config: {
                    templateUrl: '/app/postsalesunit/unit/detailUnit.html',
                    title: 'Detail Unit',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Detail Unit'
                    }
                }
            },
            {
                url: '/postsalesunit/units',
                config: {
                    templateUrl: '/app/postsalesunit/unit/listUnit.html',
                    title: 'List Units',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> List Units'
                    }
                }
            }
        ];
    }
})();