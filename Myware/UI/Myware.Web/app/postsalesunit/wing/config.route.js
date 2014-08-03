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
                url: '/postsalesunit/wing/edit/:wingId',
                config: {
                    templateUrl: '/app/postsalesunit/wing/editWing.html',
                    title: 'Edit Wing',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Add Wing'
                    }
                }
            },
            {
                url: '/postsalesunit/wing/detail/:wingId',
                config: {
                    templateUrl: '/app/postsalesunit/wing/detailWing.html',
                    title: 'Edit Wing',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Detail Wing'
                    }
                }
            },
            {
                url: '/postsalesunit/wings',
                config: {
                    templateUrl: '/app/postsalesunit/wing/listWing.html',
                    title: 'List Wings',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> List Wings'
                    }
                }
            }
        ];
    }
})();