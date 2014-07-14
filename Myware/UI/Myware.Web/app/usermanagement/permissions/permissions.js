(function () {
    'use strict';

    angular
        .module('app.usermanagement')
        .controller('Permissions', Permissions);

    Permissions.$inject = ['common', 'permissionFactory'];

    function Permissions(common, permissionFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;
        var vm = this;
        vm.permissions = {};
        vm.permission = {};
        vm.totalPermissions = 0;
        vm.title = 'Permissions';

        activate();

        function activate() {

            $q.all([getPermissions()])
                .then(function() {
                    log('Activated Permissions View');
            });
        }

        vm.addMode = false;

        vm.toggleAddMode = function () {
            vm.addMode = !vm.addMode;
        };

        vm.toggleEditMode = function (permission) {
            permission.editMode = !permission.editMode;
        };

        var successCallback = function (e) {
            common.logger.success("Successfully, executed the query");
            getPermissions();
        };

        var successPostCallback = function (e) {
            vm.toggleAddMode();
            vm.permission = {};
            successCallback(e);
        };

        var errorCallback = function (e) {
            common.logger.error("Sorry! something went wrong.");
        };

        vm.addPermissions = function () {
            permissionFactory.save(vm.permission, successPostCallback, errorCallback);
        };

        vm.deletePermissions = function (permission) {
            permissionFactory.delete({ id: permission.Id }, successCallback, errorCallback);
        };

        vm.updatePermissions = function (permission) {
            permissionFactory.update({ id: permission.Id }, permission, successCallback, errorCallback);
        };

        
        function getPermissions() {

            var promise = permissionFactory.query().$promise;
            promise.then(function (res) {
                vm.permissions = res[0].Permissions;
                vm.totalPermissions = res[0].Total;
            });
            promise.catch(function (res) {
                common.logger.error(res);
            });

        };

    }
})();