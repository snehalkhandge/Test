(function () {
    'use strict';

    angular
        .module('app.usermanagement')
        .controller('Roles', Roles);

    Roles.$inject = ['common', 'dataservice'];

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
            var promises = [getAvengerCount(), getAvengersCast()];
            return dataservice.ready(promises).then(function () {
                log('Activated Roles View');
            });
        }

        function getAvengerCount() {
            return dataservice.getAvengerCount().then(function (data) {
                vm.avengerCount = data;
                return vm.avengerCount;
            });
        }

        function getAvengersCast() {
            return dataservice.getAvengersCast().then(function (data) {
                vm.avengers = data;
                return vm.avengers;
            });
        }
    }
})();