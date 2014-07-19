(function () {
    'use strict';

    angular
        .module('app.taskmanager')
        .controller('DetailTaskmanager', DetailTask);

    DetailTask.$inject = ['$scope', '$routeParams', 'common', '$upload', 'authService', 'taskmanagerFactory', 'userFactory'];

    function DetailTask($scope, $routeParams, common, $upload, authService, taskmanagerFactory, userFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;                 
       
        var taskId = ($routeParams.taskId) ? parseInt($routeParams.taskId) : 0;
        var defaultForm = {
            Id: taskId,
            Title: '',
            Description: '',
            TaskStatus: '',
            AssignedToId: '',
            TasksRelatedFiles: []
        };

        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };


        $scope.task = defaultForm;

        $scope.children = {};

        $scope.title = 'Task Details';
        
        $scope.onFileSelect = function ($files) {

            //$files: an array of files selected, each file has name, size, and type.
            for (var i = 0; i < $files.length; i++) {
                var file = $files[i];
                $scope.upload = $upload.upload({
                    url: common.apiUrl + '/saveTaskFile', //upload.php script, node.js route, or servlet url
                    method: 'POST',
                    // headers: {'header-key': 'header-value'},
                    // withCredentials: true,
                    data: { taskObject: { id: taskId } },
                    file: file,
                }).progress(function (evt) {
                    $scope.progress = parseInt(100.0 * evt.loaded / evt.total);

                }).success(function (data, status, headers, config) {
                    // file is uploaded successfully
                    $scope.inserted = {
                        $id: $scope.task.TasksRelatedFiles.length + 1,
                        FileUrl: data                                     
                    };

                    $scope.task.TasksRelatedFiles.push($scope.inserted);
                    
                    common.logger.success("Successfully saved the file.");
                    $scope.alerts.push({ type: 'success', msg: "Successfully saved the file." });
                })
                .error(function () {
                    
                    common.logger.error("Error, while saving the file.");
                    $scope.alerts.push({ type: 'danger', msg: "Error, while saving the file." });
                });
            }
        };


        $scope.isClean = function () {
            return angular.equals(original, $scope.task);
        }

        

        activate();

        function activate() {

            if (taskId != 0)
            {

                taskmanagerFactory.getTaskmanagerById(taskId)
                    .then(function (result) {                        
                        $scope.task = result;
                        
                        taskmanagerFactory.getTaskChildrenByParentId(taskId)
                          .then(function (result) {                             
                              $scope.children = result;
                          }, function (reason) {
                              common.logger.error(reason);
                              $scope.alerts.push({ type: 'danger', msg: reason });

                          }); 

                    }, function (reason) {
                        common.logger.error(reason);
                        $scope.alerts.push({ type: 'danger', msg: reason });

                    });

            }                       
        }

        





    }
})();