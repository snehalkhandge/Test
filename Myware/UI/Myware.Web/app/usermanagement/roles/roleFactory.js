﻿(function() {
    'use strict';

    var serviceId = 'roleFactory';

    angular.module('app.usermanagement')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common', 'authService'];

    function dataservice($http, $timeout, DSCacheFactory, common, authService) {

        var $q = common.$q;

        

        var roleCache = DSCacheFactory('roleCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

       var dataCache = DSCacheFactory.get('roleCache');

       var getRoles = function (page, pageSize, searchQuery) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "Roles" + page + pageSize + searchQuery;

            

                if (dataCache.get(cacheId)) {
                    deferred.resolve(dataCache.get(cacheId));
                } else {
                    $http.get(common.apiUrl + '/roles/' + page + '/size/' + pageSize + '/search/' + searchQuery)
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

       var saveRole = function (role) {

           var deferred = $q.defer();
           if (role.Id == '') {
               role.Id = 0;
           }

           
           $http.post(common.apiUrl + '/saveRole/' + role.Id, role)
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

       var uniqueRole = function (name) {

           var deferred = $q.defer();
           $http.get(common.apiUrl + '/roleIsUnique/' +name)
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
            getRoles: getRoles,
            saveRole: saveRole,
            uniqueRole: uniqueRole
            
        };

    }
})();