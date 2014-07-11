(function () {
    'use strict';

    angular
        .module('app.usermanagement')
        .controller('Users', Users);

    Users.$inject = ['common'];

    function Users(common, dataservice) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var vm = this;

        vm.news = {
            title: 'Marvel Avengers',
            description: 'Marvel Avengers 2 is now in production!'
        };
        vm.avengerCount = 0;
        vm.avengers = [];
        vm.title = 'Users';

        activate();

        function activate() {
             log('Activated Dashboard View');
            
        }

    }
})();