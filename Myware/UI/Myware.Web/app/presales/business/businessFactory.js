(function() {
    'use strict';

    var serviceId = 'businessFactory';

    angular.module('app.presales')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http', 'DSCacheFactory', 'common'];

    function dataservice($http, DSCacheFactory, common) {

        var $q = common.$q;

        

        var businessCache = DSCacheFactory('businessCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('businessCache');

        
        var getbusinessById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();
                
            $http.get(common.apiUrl + '/businessById/' +id)
                    .success(function (data) {
                        data = data || {};                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(new Error(angular.toJson(data)));
                    });

            return deferred.promise;

        };



        var savebusiness = function (business) {

           var deferred = $q.defer();
           if (business.Id == '') {
               business.Id = 0;
           }

           
            $http.post(common.apiUrl + '/savebusiness/' + business.Id, business)
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
            savebusiness: savebusiness,
            getbusinessById: getbusinessById
            
        };

    }
})();