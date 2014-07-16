(function() {
    'use strict';

    var serviceId = 'unitTypeFactory';

    angular.module('app.presalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common', 'authService'];

    function dataservice($http, $timeout, DSCacheFactory, common, authService) {

        var $q = common.$q;

        

        var unitTypeCache = DSCacheFactory('unitTypeCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('unitTypeCache');

        var getAllUnitTypes = function () {
           var deferred = $q.defer(),
               start = new Date().getTime(),
               cacheId = "unitType-All";



           if (dataCache.get(cacheId)) {
               deferred.resolve(dataCache.get(cacheId));
           } else {
               $http.get(common.apiUrl + '/unitTypes/all')
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

        var getUnitTypes = function (page, pageSize, searchQuery) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "unitTypes" + page + pageSize + searchQuery;

            

                if (dataCache.get(cacheId)) {
                    deferred.resolve(dataCache.get(cacheId));
                } else {
                    $http.get(common.apiUrl + '/unitTypes/' + page + '/size/' + pageSize + '/search/' + searchQuery)
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

        var saveUnitType = function (unitType) {

           var deferred = $q.defer();
           if (unitType.Id == '') {
               unitType.Id = 0;
           }

           
            $http.post(common.apiUrl + '/saveUnitType/' + unitType.Id, unitType)
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

        var uniqueUnitType = function (name) {

           var deferred = $q.defer();
            $http.get(common.apiUrl + '/unitTypeIsUnique/' + name)
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
            getUnitTypes: getUnitTypes,
            saveUnitType: saveUnitType,
            uniqueUnitType: uniqueUnitType,
            getAllUnitTypes: getAllUnitTypes
            
        };

    }
})();