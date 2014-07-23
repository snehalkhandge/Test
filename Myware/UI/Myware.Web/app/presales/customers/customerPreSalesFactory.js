(function() {
    'use strict';

    var serviceId = 'customerPreSalesFactory';

    angular.module('app.presales')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http', 'DSCacheFactory', 'common'];

    function dataservice($http, DSCacheFactory, common) {

        var $q = common.$q;

        

        var customerPreSalesCache = DSCacheFactory('customerPreSalesCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('customerPreSalesCache');

        
        var getAllCustomerNames = function (queryParams) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "customerPreSales-AllCustomerNames";

            if (dataCache.get(cacheId)) {
                deferred.resolve(dataCache.get(cacheId));
            } else {
                $http.post(common.apiUrl + '/customerNames/all', queryParams.data)
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

        var getAllBudgetFrom = function (queryParams) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "customerPreSales-AllBudgetFrom";

            if (dataCache.get(cacheId)) {
                deferred.resolve(dataCache.get(cacheId));
            } else {
                $http.post(common.apiUrl + '/customersBudgetFrom/all', queryParams.data)
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

        var getAllBudgetTo = function (queryParams) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "customerPreSales-AllBudgetTo";

            if (dataCache.get(cacheId)) {
                deferred.resolve(dataCache.get(cacheId));
            } else {
                $http.post(common.apiUrl + '/customersBudgetTo/all', queryParams.data)
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

        var getAllContactNumbers = function (queryParams) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "customerPreSales-ContactNumbers";

            if (dataCache.get(cacheId)) {
                deferred.resolve(dataCache.get(cacheId));
            } else {
                $http.post(common.apiUrl + '/customersContactNumbers/all', queryParams.data)
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

        var getCustomers = function (contactLeadFilters) {
            var deferred = $q.defer(),
                start = new Date().getTime(),
                cacheId = "customerPreSales-" + contactLeadFilters.page + contactLeadFilters.pageSize + contactLeadFilters.toString();



            if (dataCache.get(cacheId)) {
                deferred.resolve(dataCache.get(cacheId));
            } else {
                $http.post(common.apiUrl + '/customersPreSales/', contactLeadFilters)
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

        return {
            getAllContactNumbers: getAllContactNumbers,
            getAllBudgetTo: getAllBudgetTo,
            getAllBudgetFrom: getAllBudgetFrom,
            getAllCustomerNames: getAllCustomerNames,
            getCustomers: getCustomers
        };

    }
})();