(function () {
    'use strict';

var serviceId = 'authService';


angular.module('app.core')
        .factory(serviceId, authService);

authService.$inject = ['$http', 'common', '$q', 'localStorageService', 'permissionService'];


function authService($http, common, $q, localStorageService, permissionService) {
    var cookieName = "authorizationData";
    var serviceBase = common.apiUrl+'/';
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: "",
        name: "",
        userId: "",
        permissions:[],
        roles:[],
    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response)
        {
            $http.post(common.apiUrl + '/getRolesByUserId/' + response.userId)
                 .success(function (result) {

                     $http.post(common.apiUrl + '/getRoleByName/' + result.replace(/[^\w\s]/gi, ''))
                             .success(function (res) {

                                 _authentication.isAuth = true;
                                 _authentication.userName = loginData.userName;
                                 _authentication.name = response.Name;
                                 _authentication.userId = response.userId;
                                 _authentication.roles.push(res.Name);

                                 angular.forEach(res.RolePermissions, function (value, key) {
                                     this.push(value.Permission.Name);
                                 }, _authentication.permissions);
                                  

                                 permissionService.setPermissions(_authentication.permissions);

                                 localStorageService.set(cookieName, {
                                     token: response.access_token,
                                     userName: loginData.userName,
                                     name: response.Name,
                                     userId: response.userId,
                                     permissions: _authentication.permissions,
                                     roles: _authentication.roles

                                 });

                                 deferred.resolve(res);

                             }).error(function (err, status) {
                                 _logOut();
                                 common.logger.error(err);
                                 deferred.reject(err);
                             });                     

                 }).error(function (err, status) {
                     _logOut();
                     common.logger.error(err);
                     deferred.reject(err);
                 });



        }).error(function (err, status) {
            _logOut();
            common.logger.error(err);
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove(cookieName);

        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.name = "";
        _authentication.userId = "";
        permissionService.setPermissions([]);
    };

    var _fillAuthData = function () {

        var authData = localStorageService.get(cookieName);
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.name = authData.name;
            _authentication.userId = authData.userId;
            _authentication.roles = authData.roles;
            _authentication.permissions = authData.permissions;
            permissionService.setPermissions(authData.permissions);
        }
        else
        {
            permissionService.setPermissions([]);
        }
        

    }

   // authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
}

})();
