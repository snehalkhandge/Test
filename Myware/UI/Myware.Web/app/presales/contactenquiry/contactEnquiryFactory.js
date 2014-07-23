(function() {
    'use strict';

    var serviceId = 'contactEnquiryFactory';

    angular.module('app.presales')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http', 'DSCacheFactory', 'common'];

    function dataservice($http, DSCacheFactory, common) {

        var $q = common.$q;

        

        var contactEnquiryCache = DSCacheFactory('contactEnquiryCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('contactEnquiryCache');

        
        var getcontactEnquiryById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();
                
            $http.get(common.apiUrl + '/contactEnquiryById/' +id)
                    .success(function (data) {
                        data = data || {};                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(new Error(angular.toJson(data)));
                    });

            return deferred.promise;

        };



        var savecontactEnquiry = function (contactEnquiry) {

           var deferred = $q.defer();
           if (contactEnquiry.Id == '') {
               contactEnquiry.Id = 0;
           }

           
            $http.post(common.apiUrl + '/savecontactEnquiry/' + contactEnquiry.Id, contactEnquiry)
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
            savecontactEnquiry: savecontactEnquiry,
            getcontactEnquiryById: getcontactEnquiryById
            
        };

    }
})();