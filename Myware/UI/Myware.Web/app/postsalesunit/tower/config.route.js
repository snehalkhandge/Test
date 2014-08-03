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
                url: '/postsalesunit/tower/edit/:towerId',
                config: {
                    templateUrl: '/app/postsalesunit/tower/editTower.html',
                    title: 'Edit Tower',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Add Tower'
                    }
                }
            },
            {
                url: '/postsalesunit/tower/detail/:towerId',
                config: {
                    templateUrl: '/app/postsalesunit/tower/detailTower.html',
                    title: 'Edit Tower',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Detail Tower'
                    }
                }
            },
            {
                url: '/postsalesunit/towers',
                config: {
                    templateUrl: '/app/postsalesunit/tower/listTower.html',
                    title: 'List Projects',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> List Towers'
                    }
                }
            }
        ];
    }
})();