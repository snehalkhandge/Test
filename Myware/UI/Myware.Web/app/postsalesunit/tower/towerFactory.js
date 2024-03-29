﻿(function() {
    'use strict';

    var serviceId = 'towerFactory';

    angular.module('app.postsalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http', 'DSCacheFactory', 'common'];

    function dataservice($http, DSCacheFactory, common) {

        var $q = common.$q;

        

        var personalCache = DSCacheFactory('towerCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('towerCache');

        
        var getTowers = function(searchData) {
            var deferred = $q.defer(),
                start = new Date().getTime();
                
            $http.post(common.apiUrl + '/listTowers', searchData)
                    .success(function (data) {
                        data = data || {};                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };
                
        var saveTower = function(tower) {

           var deferred = $q.defer();
           if (tower.Id == '') {
               tower.Id = 0;
           }
                       
            $http.post(common.apiUrl + '/saveProjectTower/' + tower.Id, tower)
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
                
        var getTowerById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/getTowerById/' + id)
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        var getWingNumbersFromBuildingById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/getWingNumbersFromBuildingById/' + id)
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        var getAllTowers = function() {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/getAllTowers')
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        var getBuildingNamesByProjectId = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/getBuildingNamesByProjectId/' + id)
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
            getTowers: getTowers,
            getTowerById: getTowerById,
            saveTower: saveTower,
            getAllTowers: getAllTowers,
            getWingNumbersFromBuildingById: getWingNumbersFromBuildingById,
            getBuildingNamesByProjectId: getBuildingNamesByProjectId
                        
        };

    }
})();