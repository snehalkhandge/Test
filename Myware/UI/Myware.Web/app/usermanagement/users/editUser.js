(function () {
    'use strict';

    angular
        .module('app.usermanagement')
        .controller('EditUser', EditUser);

    EditUser.$inject = ['$scope', '$routeParams', 'common', 'authService', 'userFactory', 'roleFactory'];

    function EditUser($scope, $routeParams, common, authService, userFactory, roleFactory) {
        var log = common.logger.info;        
        var $q = common.$q;                 
        var userId = ($routeParams.userId) ? parseInt($routeParams.userId) : 0;

       
        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };
        $scope.title = (userId > 0) ? 'Edit User Information' : 'Add User Information';
        $scope.buttonText = (userId > 0) ? 'Update User Information' : 'Add New User Information';
        $scope.showUpdateButton = false;
        $scope.roles = {};
        $scope.user = {
            Id: '',
            FirstName: '',
            LastName: '',
            Email: '',
            UserName: '',
            RoleName: '',
            Password:''
        };

        $scope.saveUser = function (user) {
                        
            userFactory.saveUser(user)
                .then(function (result) {
                    $scope.buttonText = "Update User Information"
                    common.logger.success("Successfully saved the item");
                    $scope.alerts.push({ type: 'success', msg: "Successfully saved the item" });

                    $scope.user.Id = result.Id;

                });
        };

        
        $scope.isUniqueEmail = function (email) {

            if (email.length < 10) {
                return;
            }

            userFactory.uniqueUserByEmail(email)
                .then(function (result) {
                    if (!result.IsUnique) {
                     
                        common.logger.error("Error,  There is account associated with it.");
                        $scope.alerts.push({ type: 'danger', msg: "Error, There is account associated with it." });
                     }

                }, function() {
                    $scope.alerts.push({ type: 'danger', msg: "Error, Unable to contact the serve." });
                });

        };

        $scope.isUniqueUserName = function(name) {

            if (name.length < 8)
            {
                return;
            }
            
            userFactory.uniqueUserByUserName(name)
                .then(function (result) {
                    if (!result.IsUnique) {
                        $scope.alerts.push({ type: 'danger', msg: "Error, There is account associated with it." });
                    }

                }, function () {
                    $scope.alerts.push({ type: 'danger', msg: "Error, Unable to contact the serve." });
                });

        };

        activate();

        function activate() {

            if (userId != 0)
            {

                userFactory.getUserById(userId)
                    .then(function (result) {                        
                        $scope.user = result;

                    }, function (reason) {
                        
                        $scope.alerts.push({ type: 'danger', msg: reason });

                    });

            }

            roleFactory.getRoles(1, 100, 'all')
                       .then(function (result) {
                           $scope.roles = result.Results;
                       }, function(reason) {
                           $scope.alerts.push({ type: 'danger', msg: reason });
                       });
        }

    }
})();