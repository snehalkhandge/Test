(function () {
    'use strict';

    var serviceId = 'roleFactory';

    angular.module('app.usermanagement')
            .factory(serviceId, dataservice);
            

    dataservice.$inject = ['$resource'];

    function dataservice($resource) {
        
        return $resource('/api/ManageRoles/:id',
            {
                id: '@id' 
                
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