(function() {
    'use strict';

    var serviceId = 'customerTypeFactory';

    angular.module('app.presalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common', 'authService'];

    function dataservice($http, $timeout, DSCacheFactory, common, authService) {

        var $q = common.$q;

        

        var customerTypeCache = DSCacheFactory('customerTypeCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('customerTypeCache');

        var getAllCustomerTypes = function () {
           var deferred = $q.defer(),
               start = new Date().getTime(),
               cacheId = "customerType-All";



           if (dataCache.get(cacheId)) {
               deferred.resolve(dataCache.get(cacheId));
           } else {
               $http.get(common.apiUrl + '/customerTypes/all')
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

        var getCustomerTypes = function (page, pageSize, searchQuery) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "customerTypes" + page + pageSize + searchQuery;

            

                if (dataCache.get(cacheId)) {
                    deferred.resolve(dataCache.get(cacheId));
                } else {
                    $http.get(common.apiUrl + '/customerTypes/' + page + '/size/' + pageSize + '/search/' + searchQuery)
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

        var saveCustomerType = function (customerType) {

           var deferred = $q.defer();
           if (customerType.Id == '') {
               customerType.Id = 0;
           }

           
            $http.post(common.apiUrl + '/saveCustomerType/' + customerType.Id, customerType)
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

        var uniqueCustomerType = function (name) {

           var deferred = $q.defer();
           $http.get(common.apiUrl + '/customerTypeIsUnique/' + name)
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
            getCustomerTypes: getCustomerTypes,
            saveCustomerType: saveCustomerType,
            uniqueCustomerType: uniqueCustomerType,
            getAllCustomerTypes: getAllCustomerTypes
            
        };

    }
})();