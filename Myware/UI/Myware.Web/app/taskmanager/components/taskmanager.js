(function () {
    'use strict';

    angular
        .module('app.taskmanager')
        .controller('Tasks', Task);

    Task.$inject = ['$scope', '$routeParams', 'common', 'authService', 'taskmanagerFactory'];

    function Task($scope, $routeParams, common, authService, taskFactory) {
        var log = common.logger.info;

        var taskKind = ($routeParams.taskKind) ? parseInt($routeParams.taskKind) : 0;

        /*jshint validthis: true */
        var $q = common.$q;
        $scope.taskKind = taskKind;
        $scope.title = 'Task';
        $scope.results = {};
        $scope.totalItems = 0;
        $scope.itemsPerPage = 7;
        $scope.page = 1;
        $scope.searchQuery = "all";
        $scope.setPage = function (pageNo) {
            $scope.page = pageNo;
        };
        $scope.pageChanged = function () {
            activate();
        };
        $scope.hidePagination = false;

        $scope.taskStatuses = {};

        activate();

        function activate() {

            if (taskKind != 1)
            {
                taskFactory.getTaskmanagerAssignedByMe(authService.authentication.userId, $scope.page, $scope.itemsPerPage, $scope.searchQuery)
                    .then(function (result) {

                        $scope.taskStatuses = taskFactory.getAllTaskStatus();
                        $scope.results = result.Results;
                        $scope.totalItems = result.TotalItems;

                        if ($scope.totalItems <= $scope.itemsPerPage) {
                            $scope.hidePagination = true;
                        }

                    }, function (reason) {
                        common.logger.error(reason);

                    });
            }
            else
            {
                taskFactory.getTaskmanagerAssignedToMe(authService.authentication.userId, $scope.page, $scope.itemsPerPage, $scope.searchQuery)
                    .then(function (result) {

                        $scope.taskStatuses = taskFactory.getAllTaskStatus();

                        $scope.results = result.Results;
                        $scope.totalItems = result.TotalItems;

                        if ($scope.totalItems <= $scope.itemsPerPage) {
                            $scope.hidePagination = true;
                        }

                    }, function (reason) {
                        common.logger.error(reason);

                    });
            }
            
        }



    }
})();