(function () {
    'use strict';

    angular
        .module('app.taskmanager')
        .run(['routehelper', function (routehelper) {
            routehelper.configureRoutes(getRoutes());
        }]);

    function getRoutes() {
        return [
            
            {
                url: '/taskmanager/tasks/:taskKind',
                config: {
                    templateUrl: '/app/taskmanager/components/taskmanager.html',
                    title: 'Manage Tasks',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Tasks'
                    }
                }
            },
            {
                url: '/taskmanager/task/edit/:taskId/:parentId',
                config: {
                    templateUrl: '/app/taskmanager/components/editTaskmanager.html',
                    title: 'Edit Broker',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Edit Tasks'
                    }
                }
            },
            {
                url: '/taskmanager/task/:taskId',
                config: {
                    templateUrl: '/app/taskmanager/components/detailTaskmanager.html',
                    title: 'Detail Company',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Detail Task'
                    }
                }
            }

        ];
    }
})();