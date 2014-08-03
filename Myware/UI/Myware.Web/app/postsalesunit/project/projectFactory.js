(function() {
    'use strict';

    var serviceId = 'projectFactory';

    angular.module('app.postsalesunit')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http', 'DSCacheFactory', 'common'];

    function dataservice($http, DSCacheFactory, common) {

        var $q = common.$q;

        

        var personalCache = DSCacheFactory('projectCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

        var dataCache = DSCacheFactory.get('projectCache');

        
        var getProjects = function (searchData) {
            var deferred = $q.defer(),
                start = new Date().getTime();
                
            $http.post(common.apiUrl + '/listProjects', searchData)
                    .success(function (data) {
                        data = data || {};                        
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        var getProjectsRelatedCompanies = function() {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/relatedCompanies')
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };
        
        var getProjectsRelatedCities = function () {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/relatedCities')
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        var getProjectsRelatedLocalities = function () {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/relatedLocalities')
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        var getProjectTypes = function () {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/getProjectTypes')
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        var saveProjectBase = function(projectBase) {

           var deferred = $q.defer();
           if (projectBase.Id == '') {
               projectBase.Id = 0;
           }
                       
            $http.post(common.apiUrl + '/saveProjectBase/' + projectBase.Id, projectBase)
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

        var saveProjectOtherInformation = function(projectInformation) {

            var deferred = $q.defer();
            if (projectInformation.Id == '') {
                projectInformation.Id = 0;
            }

            $http.post(common.apiUrl + '/saveProjectOtherInformation/' + projectInformation.Id, projectInformation)
                    .success(function (data) {

                        if (dataCache.info()) {
                            dataCache.removeAll();
                        }

                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject({ data: data, status: status, headers: headers });
                    });
            return deferred.promise;

        };

        var saveProjectBankDetail = function(bankDetail) {

            var deferred = $q.defer();
            if (bankDetail.Id == '') {
                bankDetail.Id = 0;
            }

            $http.post(common.apiUrl + '/saveProjectBankDetails', bankDetail)
                    .success(function (data) {

                        if (dataCache.info()) {
                            dataCache.removeAll();
                        }

                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject({ data: data, status: status, headers: headers });
                    });
            return deferred.promise;

        };

        var saveProjectPropertyCharge = function (propertyCharge) {

            var deferred = $q.defer();
            if (propertyCharge.Id == '') {
                propertyCharge.Id = 0;
            }

            $http.post(common.apiUrl + '/saveProjectPropertyCharges/' + propertyCharge.Id, propertyCharge)
                    .success(function (data) {

                        if (dataCache.info()) {
                            dataCache.removeAll();
                        }

                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject({ data: data, status: status, headers: headers });
                    });
            return deferred.promise;

        };

        var getProjectById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/getProjectById/'+id)
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        var getAllProjects = function() {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/getAllProjects')
                    .success(function (data) {
                        data = data || {};
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(data);
                    });

            return deferred.promise;

        };

        var getBuildingNumbersFromProjectById = function (id) {
            var deferred = $q.defer(),
                start = new Date().getTime();

            $http.get(common.apiUrl + '/projects/getBuildingNumbersFromProjectById/' + id)
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
            getProjects: getProjects,
            getProjectsRelatedCompanies: getProjectsRelatedCompanies,
            getProjectsRelatedCities: getProjectsRelatedCities,
            getProjectsRelatedLocalities: getProjectsRelatedLocalities,
            getProjectTypes: getProjectTypes,
            saveProjectBase: saveProjectBase,
            saveProjectOtherInformation: saveProjectOtherInformation,
            saveProjectPropertyCharge:saveProjectPropertyCharge,
            saveProjectBankDetail: saveProjectBankDetail,
            getProjectById: getProjectById,
            getAllProjects: getAllProjects,
            getBuildingNumbersFromProjectById: getBuildingNumbersFromProjectById
                        
        };

    }
})();