(function () {
    'use strict';

    angular
        .module('app.avengers')
        .controller('Avengers', Avengers);

    Avengers.$inject = ['common', 'roledataservice'];

    function Avengers(common, roledataservice) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var vm = this;
        vm.avengers = [];
        vm.title = 'Avengers';

        activate();

        function activate() {
            
            log('Activated Avengers View');
            
        }

    }
})();