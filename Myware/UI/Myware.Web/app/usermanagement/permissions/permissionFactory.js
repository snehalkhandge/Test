(function () {
    'use strict';

    var serviceId = 'permissionFactory';

    angular.module('app.usermanagement')
            .factory(serviceId, dataservice);
            

    dataservice.$inject = ['$resource'];

    function dataservice($resource) {
        
        return $resource('/api/ManagePermissions',
            {
                
            },
            {
               'update':
                   { method: 'PUT' }
            },
            {
               'query': {
                    method: 'GET',
                    isArray: false,
                    cache: true
                   }
           }
         );

    }
})();