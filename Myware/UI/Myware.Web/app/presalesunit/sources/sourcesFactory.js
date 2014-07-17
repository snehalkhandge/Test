(function() {
    'use strict';

    var serviceId = 'sourceFactory';

    angular.module('app.presalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common'];

    function dataservice($http, $timeout, DSCacheFactory, common) {

        var $q = common.$q;

        

        var SourceCache = DSCacheFactory('sourceCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('sourceCache');

        var getAllSource = function () {
           var deferred = $q.defer(),
               start = new Date().getTime(),
               cacheId = "source-All";



           if (dataCache.get(cacheId)) {
               deferred.resolve(dataCache.get(cacheId));
           } else {
               $http.get(common.apiUrl + '/source/all')
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


        var getAllParentSource = function () {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "source-All-Parent";



            if (dataCache.get(cacheId)) {
                deferred.resolve(dataCache.get(cacheId));
            } else {
                $http.get(common.apiUrl + '/source/parent/all')
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

        var getSource = function (page, pageSize, searchQuery) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "source" + page + pageSize + searchQuery;

            

                if (dataCache.get(cacheId)) {
                    deferred.resolve(dataCache.get(cacheId));
                } else {
                    $http.get(common.apiUrl + '/source/' + page + '/size/' + pageSize + '/search/' + searchQuery)
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

        var saveSource = function (source) {

           var deferred = $q.defer();
           if (source.Id == '') {
               source.Id = 0;
           }

           
            $http.post(common.apiUrl + '/saveSource/' + source.Id, source)
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



        return {
            getSource: getSource,
            saveSource: saveSource,        
            getAllSource: getAllSource,
            getAllParentSource: getAllParentSource,

            
        };

    }
})();