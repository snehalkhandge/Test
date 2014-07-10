﻿(function () {
    'use strict';

    angular
        .module('app.avengers')
        .controller('Avengers', Avengers);

    Avengers.$inject = ['common', 'dataservice'];

    function Avengers(common, dataservice) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var vm = this;
        vm.avengers = [];
        vm.title = 'Avengers';

        activate();

        function activate() {
            var promises = [getAvengers()];
            return dataservice.ready(promises).then(function () {
                log('Activated Avengers View');
            });
        }

        function getAvengers() {
            dataservice.getAvengers().then(function (data) {
                vm.avengers = data;
                return vm.avengers;
            });
        }
    }
})();