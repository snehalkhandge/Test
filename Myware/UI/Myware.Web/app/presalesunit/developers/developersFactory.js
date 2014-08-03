(function() {
    'use strict';

    var serviceId = 'developersFactory';

    angular.module('app.presalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common', 'authService'];

    function dataservice($http, $timeout, DSCacheFactory, common, authService) {

        var $q = common.$q;

        

        var roleCache = DSCacheFactory('developersCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('developersCache');

        var getDevelopers = function (page, pageSize, searchQuery) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "Developers" + page + pageSize + searchQuery;

            

                if (dataCache.get(cacheId)) {
                    deferred.resolve(dataCache.get(cacheId));
                } else {
                    $http.get(common.apiUrl + '/developers/' + page + '/size/' + pageSize + '/search/' + searchQuery)
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

       var saveDeveloper = function (developer) {

           var deferred = $q.defer();
           if (developer.Id == '') {
               developer.Id = 0;
           }

           
           $http.post(common.apiUrl + '/saveDeveloper/' + developer.Id, developer)
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

       var uniqueDeveloper = function (name) {

           var deferred = $q.defer();
           $http.get(common.apiUrl + '/developerIsUnique/' + name)
                    .success(function (data) {
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        common.logger.error(data);
                        deferred.reject({});
                    });

           return deferred.promise;
       };
 
       var getDeveloperByCompanyId = function (companyId) {

           var deferred = $q.defer();
           $http.get(common.apiUrl + '/developersByCompanyId/' + companyId)
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
            getDevelopers: getDevelopers,
            saveDeveloper: saveDeveloper,
            uniqueDeveloper: uniqueDeveloper,
            getDeveloperByCompanyId: getDeveloperByCompanyId
            
        };

    }
})();