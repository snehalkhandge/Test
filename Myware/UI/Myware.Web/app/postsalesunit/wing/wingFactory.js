(function() {
    'use strict';

    var serviceId = 'wingFactory';

    angular.module('app.postsalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http', 'DSCacheFactory', 'common'];

    function dataservice($http, DSCacheFactory, common) {

        var $q = common.$q;

        

        var personalCache = DSCacheFactory('wingCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('wingCache');

        
        var getWings = function(searchData) {
            var deferred = $q.defer(),
                start = new Date().getTime();
                
            $http.post(common.apiUrl + '/listWings', searchData)
                    .success(function (data) {
                        data = data || {};                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };
                
        var saveWing = function(wing) {

           var deferred = $q.defer();
           if (wing.Id == '') {
               wing.Id = 0;
           }
                       
            $http.post(common.apiUrl + '/saveProjectWing/' + wing.Id, wing)
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
                
        var getWingById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/getWingById/' + id)
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        var getAllWingsByBuildingId = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/getAllWingsByBuildingId/' + id)
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
            getWings: getWings,
            getWingById: getWingById,
            saveWing: saveWing,            
            getAllWingsByBuildingId: getAllWingsByBuildingId
                        
        };

    }
})();