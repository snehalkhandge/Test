(function() {
    'use strict';

    var serviceId = 'permissionFactory';

    angular.module('app.usermanagement')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common', 'authService'];

    function dataservice($http, $timeout, DSCacheFactory, common, authService) {

        var $q = common.$q;

        

        var permissionCache = DSCacheFactory('permissionCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

       var dataCache = DSCacheFactory.get('permissionCache');

       var getPermissions = function(page,pageSize,searchQuery) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "Permissions" + page + pageSize + searchQuery;

            

                if (dataCache.get(cacheId)) {
                    deferred.resolve(dataCache.get(cacheId));
                } else {
                    $http.get(common.apiUrl + '/permissions/' + page + '/size/' + pageSize + '/search/' + searchQuery)
                        .success(function (data) {
                            data = data || {};
                            if (data.Messages == null) {
                                dataCache.put(cacheId, data);
                            }

                            deferred.resolve(data);
                        })
                        .error(function (data, status, headers, config) {                            
                            deferred.reject(new Error(angular.toJson(data)));
                        });

                }


                return deferred.promise;

           
       };

       var savePermission = function(permission) {

           var deferred = $q.defer();
           if (permission.Id == '') {
               permission.Id = 0;
           }

           
           $http.post(common.apiUrl + '/savePermission/' + permission.Id, permission)
                    .success(function (data) {

                        if (dataCache.info()) {
                            dataCache.removeAll();
                        }
                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        common.logger.error(data);
                        deferred.reject({});
                    });
           return deferred.promise;

       };

       var uniquePermission = function (name) {

           var deferred = $q.defer();
           $http.get(common.apiUrl + '/permissionsIsUnique/' +name)
                    .success(function (data) {
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        common.logger.error(data);
                        deferred.reject({});
                    });

           return deferred.promise;
       };
 


        return {
            getPermissions: getPermissions,
            savePermission: savePermission,
            uniquePermission: uniquePermission
            
        };

    }
})();