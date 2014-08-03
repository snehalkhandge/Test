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
                url: '/postsalesunit/project/edit/:projectId',
                config: {
                    templateUrl: '/app/postsalesunit/project/editProject.html',
                    title: 'Edit Project',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Add Project'
                    }
                }
            },
            {
                url: '/postsalesunit/project/detail/:projectId',
                config: {
                    templateUrl: '/app/postsalesunit/project/detailProject.html',
                    title: 'Detail Project',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Detail Project'
                    }
                }
            },
            {
                url: '/postsalesunit/projects',
                config: {
                    templateUrl: '/app/postsalesunit/project/listProject.html',
                    title: 'List Projects',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> List Projects'
                    }
                }
            }
        ];
    }
})();