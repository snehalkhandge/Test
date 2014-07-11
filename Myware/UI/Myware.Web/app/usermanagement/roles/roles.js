(function () {
    'use strict';

    angular
        .module('app.usermanagement')
        .controller('Roles', Roles);

    Roles.$inject = ['common'];

    function Roles(common, dataservice) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var vm = this;

        vm.news = {
            title: 'Marvel Avengers',
            description: 'Marvel Avengers 2 is now in production!'
        };
        vm.avengerCount = 0;
        vm.avengers = [];
        vm.title = 'Roles';

        activate();

        function activate() {
                log('Activated Roles View');
        
        }

    }
})();