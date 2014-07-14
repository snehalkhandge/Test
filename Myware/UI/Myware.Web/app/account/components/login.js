(function () {
    'use strict';

    angular
        .module('app.account')
        .controller('Login', Login);

    Login.$inject = ['$scope', '$location', 'common', 'authService'];

    function Login($scope, $location, common, authService) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var vm = this;
        vm.disableSubmit = false;
        vm.title = 'Login';
        
        vm.loginData = {
            userName: "",
            password: ""
        };

        vm.login = function (isValid) {

            if (isValid) {
                vm.disableSubmit = true;
                vm.showSplash = true;
                authService.login(vm.loginData).then(function (response) {

                    $location.path('/admin');

                },
                 function (err) {
                     common.logger.error(err.error_description, "", "Authentication service", true);

                 });

                vm.disableSubmit = false;
                vm.showSplash = false;
            }
            
        };



        activate();


        function activate() {
            log('Activated Login View');
           
        }

    }
})();

