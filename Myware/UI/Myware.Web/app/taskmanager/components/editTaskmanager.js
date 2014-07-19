(function () {
    'use strict';

    angular
        .module('app.taskmanager')
        .controller('EditTaskmanager', EditTaskmanager);

    EditTaskmanager.$inject = ['$scope', '$routeParams', '$upload', 'common', 'authService', 'taskmanagerFactory', 'userFactory'];

    function EditTaskmanager($scope, $routeParams, $upload, common, authService, taskmanagerFactory, userFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;                 
       
        var taskmanagerId = ($routeParams.taskId) ? parseInt($routeParams.taskId) : 0;
        var parentId = ($routeParams.parentId) ? parseInt($routeParams.parentId) : 0;
        var defaultForm = {
            Id: taskmanagerId,
            Title: '',
            Description: '',
            TaskStatus: '',
            TaskType: '',
            AssignedToId: '',            
            TaskFiles: []
        };

        $scope.parentId = parentId;

        $scope.taskmanager = defaultForm;
        $scope.users = [];
        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        $scope.title = (taskmanagerId > 0) ? 'Edit Taskmanager' : 'Add Taskmanager';
        $scope.buttonText = (taskmanagerId > 0) ? 'Update Taskmanager' : 'Add New Taskmanager';              
        $scope.Localities = {};
        $scope.SelectedLocalities = {};
        $scope.taskTypes = {};

        $scope.isClean = function () {
            return angular.equals(original, $scope.taskmanager);
        }

        
        $scope.saveTaskmanager = function (taskmanager) {
            
            angular.extend(taskmanager, { AssignedFromId: authService.authentication.userId });
            if (parentId != 0)
            {
                angular.extend(taskmanager, { ParentTaskId: parentId });
            }
            

            taskmanagerFactory.saveTaskmanager(taskmanager)
                .then(function () {
                    $scope.buttonText = "Update Taskmanager"
                    common.logger.success("Successfully saved the item");
                    $scope.alerts.push({ type: 'success', msg: "Successfully saved the item" });
                });
        };

        $scope.resetForm = function () {
            $scope.myForm.$setPristine();
            $scope.taskmanager = defaultForm;
        }

        var newDate = new Date();
        
        $scope.onFileSelect = function ($files) {

                //$files: an array of files selected, each file has name, size, and type.
                for (var i = 0; i < $files.length; i++) {
                    var file = $files[i];
                    $scope.upload = $upload.upload({
                        url: common.apiUrl + '/saveTaskFile', //upload.php script, node.js route, or servlet url
                        method: 'POST',
                        // headers: {'header-key': 'header-value'},
                        // withCredentials: true,
                        data: { taskmanagerObject: { id: taskmanagerId } },
                        file: file,
                    }).progress(function (evt) {
                        $scope.progress = parseInt(100.0 * evt.loaded / evt.total);

                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        $scope.taskmanager.ImageUrl = data;
                        common.logger.success("Successfully saved the image.");
                        $scope.alerts.push({ type: 'success', msg: "Successfully saved the image." });
                    })
                    .error(function () {
                        $scope.taskmanager.ImageUrl = "http://placehold.it/150x150.png&text=Error upload image";
                        common.logger.error("Error, while saving the image.");
                        $scope.alerts.push({ type: 'danger', msg: "Error, while saving the image." });
                    });                    
                }
            };


        activate();

        function activate() {

            if (taskmanagerId != 0)
            {

                taskmanagerFactory.getTaskmanagerById(taskmanagerId)
                    .then(function (result) {                        
                        $scope.taskmanager = result;
                        
                    }, function (reason) {
                        common.logger.error(reason);
                        $scope.alerts.push({ type: 'danger', msg: reason });

                    });

            }


            userFactory.getAllUsers()
                       .then(function (result) {
                             $scope.users = result;
                       }, function (reason) {
                            common.logger.error(reason);
                            $scope.alerts.push({ type: 'danger', msg: reason });

                       });


            $scope.taskStatuses = taskmanagerFactory.getAllTaskStatus();
            
            $scope.taskTypes = taskmanagerFactory.getAllTaskType();
        }

        


    }
})();