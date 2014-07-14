(function () {
    'use strict';

    angular
        .module('app.usermanagement')
        .controller('Roles', Roles);

    Roles.$inject = ['common', 'roleFactory'];

    function Roles(common, roleFactory) {
        var log = common.logger.info;
        
        /*jshint validthis: true */
        var vm = this;

        vm.title = 'Roles';
        vm.rolesCount = 0;
        vm.role = {};
        vm.role.Id = "";
        vm.role.Name = "";
        vm.role.Permissions = [];
        vm.removePermissionFromRole = function(rolePermissionId)
        {
            
        };
       vm.getRole = function (cb) {

           var promise = roleFactory.query().$promise;
           promise.then(function (res) {
               vm.roles = res;
               if (cb) cb();
           });
           promise.catch(function (res) {
               common.logger.error(res);

           });
            
        };

        activate();

        function activate() {
            vm.getRole();
           log('Activated Roles View');
        
        }

    }
})();


