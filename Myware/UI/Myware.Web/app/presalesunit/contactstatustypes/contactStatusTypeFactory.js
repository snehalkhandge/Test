(function() {
    'use strict';

    var serviceId = 'contactStatusTypeFactory';

    angular.module('app.presalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common', 'authService'];

    function dataservice($http, $timeout, DSCacheFactory, common, authService) {

        var $q = common.$q;

        

        var contactStatusTypeCache = DSCacheFactory('contactStatusTypeCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('contactStatusTypeCache');

        var getAllContactStatusTypes = function () {
           var deferred = $q.defer(),
               start = new Date().getTime(),
               cacheId = "contactStatusType-All";



           if (dataCache.get(cacheId)) {
               deferred.resolve(dataCache.get(cacheId));
           } else {
               $http.get(common.apiUrl + '/contactStatusTypes/all')
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

        var getContactStatusTypes = function (page, pageSize, searchQuery) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "contactStatusTypes" + page + pageSize + searchQuery;

            

                if (dataCache.get(cacheId)) {
                    deferred.resolve(dataCache.get(cacheId));
                } else {
                    $http.get(common.apiUrl + '/contactStatusTypes/' + page + '/size/' + pageSize + '/search/' + searchQuery)
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

        var saveContactStatusType = function (contactStatusType) {

           var deferred = $q.defer();
           if (contactStatusType.Id == '') {
               contactStatusType.Id = 0;
           }

           
            $http.post(common.apiUrl + '/saveContactStatusType/' + contactStatusType.Id, contactStatusType)
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

        var uniqueContactStatusType = function (name) {

           var deferred = $q.defer();
           $http.get(common.apiUrl + '/contactStatusTypeIsUnique/' + name)
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
            getContactStatusTypes: getContactStatusTypes,
            saveContactStatusType: saveContactStatusType,
            uniqueContactStatusType: uniqueContactStatusType,
            getAllContactStatusTypes: getAllContactStatusTypes
            
        };

    }
})();