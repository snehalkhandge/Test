(function() {
    'use strict';

    var serviceId = 'unitFactory';

    angular.module('app.postsalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http', 'DSCacheFactory', 'common'];

    function dataservice($http, DSCacheFactory, common) {

        var $q = common.$q;

        

        var personalCache = DSCacheFactory('unitCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('unitCache');

        
        var getUnits = function (searchData) {
            var deferred = $q.defer(),
                start = new Date().getTime();
                
            $http.post(common.apiUrl + '/listUnits', searchData)
                    .success(function (data) {
                        data = data || {};                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };
                
        var saveUnit = function (unit) {

           var deferred = $q.defer();
           if (unit.Id == '') {
               unit.Id = 0;
           }
                       
            $http.post(common.apiUrl + '/saveProjectUnit/' + unit.Id, unit)
                    .success(function (data) {

                        if (dataCache.info()) {
                            dataCache.removeAll();
                        }
                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {                        
                        deferred.reject({ data:data,status:status, headers:headers });
                    });
           return deferred.promise;

       };
                
        var getUnitById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/getUnitById/' + id)
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        
        return {
            getUnits: getUnits,
            getUnitById: getUnitById,
            saveUnit: saveUnit                                       
        };

    }
})();