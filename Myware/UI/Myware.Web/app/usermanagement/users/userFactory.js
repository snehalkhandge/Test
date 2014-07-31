(function() {
    'use strict';

    var serviceId = 'userFactory';

    angular.module('app.usermanagement')
        .factory(serviceId, dataservice);


    dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common', 'authService'];

    function dataservice($http, $timeout, DSCacheFactory, common, authService) {

        var $q = common.$q;

        

        var userCache = DSCacheFactory('userCache', {
            maxAge: 3600000,
            capacity: 100,
            deleteOnExpire: 'aggressive',
            storageMode: 'localStorage',
            onExpire: function(key, value) {

            }
        });

       var dataCache = DSCacheFactory.get('userCache');

       var getUsers = function (page, pageSize, searchQuery) {
            var deferred = $q.defer();

            $http.get(common.apiUrl + '/getUsers/' + page + '/size/' + pageSize + '/search/' + searchQuery)
                 .success(function (data) {
                         deferred.resolve(data);
                  })
                 .error(function (data, status, headers, config) {                            
                       deferred.reject(new Error(angular.toJson(data)));
                 });

            return deferred.promise;
           
       };

       var getUserById = function (id) {
           var deferred = $q.defer();
           
           $http.get(common.apiUrl + '/getUserById/' + id)
                   .success(function (data) {
                       
                       deferred.resolve(data);
                   })
                   .error(function (data, status, headers, config) {
                       deferred.reject(new Error(angular.toJson(data)));
                   });

           


           return deferred.promise;


       };

       var saveUser = function (user) {

           var deferred = $q.defer();
           if (user.Id == '') {
               user.Id = 0;
           }

           
           $http.post(common.apiUrl + '/saveUser/' + user.Id, user)
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

       var uniqueUserByUserName = function (name) {

           var deferred = $q.defer();
           $http.get(common.apiUrl + '/userIsUniqueByUserName', { email: name })
                    .success(function (data) {
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        common.logger.error(data);
                        deferred.reject(data);
                    });

           return deferred.promise;
       };
 
       var uniqueUserByEmail = function (email) {

           var deferred = $q.defer();
           $http.get(common.apiUrl + '/userIsUniqueByEmail', { email: email })
                    .success(function (data) {
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        common.logger.error(data);
                        deferred.reject(data);
                    });

           return deferred.promise;
       };

       var getAllUsers = function () {
           var deferred = $q.defer(),
               start = new Date().getTime(),
               cacheId = "Users-All";

           if (dataCache.get(cacheId)) {
               deferred.resolve(dataCache.get(cacheId));
           } else {
               $http.get(common.apiUrl + '/getAllUsers/all')
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
            getUsers: getUsers,
            saveUser: saveUser,
            uniqueUserByUserName: uniqueUserByUserName,
            uniqueUserByEmail: uniqueUserByEmail,
            getAllUsers: getAllUsers,            
            getUserById : getUserById 
        };

    }
})();