(function() {
    'use strict';

    var serviceId = 'brokerFactory';

    angular.module('app.presalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common'];

    function dataservice($http, $timeout, DSCacheFactory, common) {

        var $q = common.$q;

        

        var brokerCache = DSCacheFactory('brokerCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('brokerCache');

        var getAllBroker = function () {
           var deferred = $q.defer(),
               start = new Date().getTime(),
               cacheId = "broker-All";



           if (dataCache.get(cacheId)) {
               deferred.resolve(dataCache.get(cacheId));
           } else {
               $http.get(common.apiUrl + '/brokers/all')
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

        var getBroker = function (page, pageSize, searchQuery) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "brokers" + page + pageSize + searchQuery;

            

                if (dataCache.get(cacheId)) {
                    deferred.resolve(dataCache.get(cacheId));
                } else {
                    $http.get(common.apiUrl + '/brokers/' + page + '/size/' + pageSize + '/search/' + searchQuery)
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

        var getBrokerById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();
                
            $http.get(common.apiUrl + '/brokerById/' +id)
                    .success(function (data) {
                        data = data || {};                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(new Error(angular.toJson(data)));
                    });

            return deferred.promise;

        };



        var saveBroker = function (broker) {

           var deferred = $q.defer();
           if (broker.Id == '') {
               broker.Id = 0;
           }

           
            $http.post(common.apiUrl + '/saveBroker/' + broker.Id, broker)
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

        var uniqueBroker = function (name) {

           var deferred = $q.defer();
            $http.get(common.apiUrl + '/brokerIsUnique/' + name)
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
            getBroker: getBroker,
            saveBroker: saveBroker,
            uniqueBroker: uniqueBroker,
            getAllBroker: getAllBroker,
            getBrokerById: getBrokerById
            
        };

    }
})();