(function () {
    'use strict';

    angular
        .module('app.account')
        .controller('Logout', Logout);

    Logout.$inject = ['$scope', '$location', 'common', 'authService'];

    function Logout($scope, $location, common, authService) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var vm = this;

        
        vm.title = 'Logout';

        activate();


        function activate() {

            authService.logOut();

            log("Successfully, logged out of the system");
            $location.path('/');
            
        }

    }
})();

