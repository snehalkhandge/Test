(function() {
    'use strict';

    var serviceId = 'companyFactory';

    angular.module('app.presalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common'];

    function dataservice($http, $timeout, DSCacheFactory, common) {

        var $q = common.$q;

        

        var companyCache = DSCacheFactory('companyCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('companyCache');

        var getAllCompany = function () {
           var deferred = $q.defer(),
               start = new Date().getTime(),
               cacheId = "company-All";



           if (dataCache.get(cacheId)) {
               deferred.resolve(dataCache.get(cacheId));
           } else {
               $http.get(common.apiUrl + '/companies/all')
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

        var getCompany = function (page, pageSize, searchQuery) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "companies" + page + pageSize + searchQuery;

            

                if (dataCache.get(cacheId)) {
                    deferred.resolve(dataCache.get(cacheId));
                } else {
                    $http.get(common.apiUrl + '/companies/' + page + '/size/' + pageSize + '/search/' + searchQuery)
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

        var getCompanyById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();
                
            $http.get(common.apiUrl + '/companyById/' +id)
                    .success(function (data) {
                        data = data || {};                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(new Error(angular.toJson(data)));
                    });

            return deferred.promise;

        };

        var saveCompany = function (company) {

           var deferred = $q.defer();
           if (company.Id == '') {
               company.Id = 0;
           }

           
            $http.post(common.apiUrl + '/saveCompany/' + company.Id, company)
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

        var uniqueCompany = function (name) {

           var deferred = $q.defer();
            $http.get(common.apiUrl + '/companyIsUnique/' + name)
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
            getCompany: getCompany,
            saveCompany: saveCompany,
            uniqueCompany: uniqueCompany,
            getAllCompany: getAllCompany,
            getCompanyById: getCompanyById
            
        };

    }
})();