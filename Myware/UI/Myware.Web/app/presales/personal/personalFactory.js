(function() {
    'use strict';

    var serviceId = 'personalFactory';

    angular.module('app.presales')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http', 'DSCacheFactory', 'common'];

    function dataservice($http, DSCacheFactory, common) {

        var $q = common.$q;

        

        var personalCache = DSCacheFactory('personalCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('personalCache');

        
        var getPersonalById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();
                
            $http.get(common.apiUrl + '/personalInformationById/' + id)
                    .success(function (data) {
                        data = data || {};                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(new Error(angular.toJson(data)));
                    });

            return deferred.promise;

        };



        var savePersonal = function (personal) {

           var deferred = $q.defer();
           if (personal.Id == '') {
               personal.Id = 0;
           }

           
            $http.post(common.apiUrl + '/savePersonalInformation/' + personal.Id, personal)
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
            savePersonal: savePersonal,
            getPersonalById: getPersonalById
            
        };

    }
})();