'use strict';

angular.module('ngService', []).config(function () { }).factory('authService', ['$http', '$q', 'localStorageService', 'CFG', 'toastr', '$rootScope', '$httpService', function ($http, $q, localStorageService, CFG, toastr, $rootScope, $httpService) {

    var serviceBase = CFG.rest.baseURI;
    var clientId = CFG.rest.clientId;
    var authServiceFactory = {};

    var _externalAuthData = {
        provider: "",
        userName: "",
        externalAccessToken: "",
        first_name: "",
        last_name: "",
        email: ""
    };

    $rootScope.authData = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false,
        firstName: "",
        id: "",
        expire: '',
        roles: '',
        img: '',
        lastLogin: ''
    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            $rootScope.authData = {
                isAuth: true,
                id: authData.id,
                firstName: authData.firstName,
                userName: authData.userName,
                useRefreshTokens: authData.useRefreshTokens,
                expire: authData.expire,
                roles: authData.roles,
                img: authData.img,
                lastLogin: authData.lastLogin
            };
        }
        else {
            $rootScope.authData = {
                isAuth: false,
                id: "",
                firstName: "",
                userName: "",
                useRefreshTokens: false,
                expire: "",
                roles: "",
                img: "",
                lastLogin: ""
            };
        }
    };

    var _signUp = function (signUp) {
        _signOut();
        var Obj = { postData: signUp, url: '/Account/Register', successToast: 'Your registration completed successfully...Please login to continue.', errorToast: 'Error while adding new account' };
        return $httpService.$post(Obj);
    };

    var _signIn = function (signIn) {

        var loginData = {
            userName: signIn.Email,
            password: signIn.Password,
            staySignIn: signIn.Stay
        };

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password + "&scope=" + loginData.staySignIn;

        loginData.useRefreshTokens = false;
        if (loginData.useRefreshTokens) {
            data = data + "&client_id=" + clientId;
        }

        var deferred = $q.defer();

        $http.post(serviceBase + '/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            if (loginData.useRefreshTokens) {
                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: response.refresh_token, useRefreshTokens: true, firstName: response.firstName, id: response.id, expire: response['.expires'], roles: response.roles, img: response.img, lastLogin: response.lastLogin });
            }
            else {
                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false, firstName: response.firstName, id: response.id, expire: response['.expires'], roles: response.roles, img: response.img, lastLogin: response.lastLogin });
            }

            _fillAuthData();

            deferred.resolve(response);
        })
            .error(function (err, status) {
                $rootScope.showNotify(err.error, 'Error', 'e');
                deferred.reject(err);
            });

        return deferred.promise;

    };

    var _signOut = function () {

        localStorageService.remove('authorizationData');

        $rootScope.authData = {
            isAuth: false,
            id: "",
            firstName: "",
            userName: "",
            useRefreshTokens: false,
            expire: "",
            roles: "",
            img: "",
            lastLogin: ""
        };
    };

    var _refreshToken = function () {
        var deferred = $q.defer();

        var authData = localStorageService.get('authorizationData');

        if (authData) {

            if (authData.useRefreshTokens) {

                var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + clientId;

                localStorageService.remove('authorizationData');

                $http.post(serviceBase + '/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                    localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true, firstName: response.firstName, id: response.id, expire: response['.expires'], roles: response.roles });

                    deferred.resolve(response);

                }).error(function (err, status) {
                    _signOut();
                    deferred.reject(err);
                    return $rootScope.showNotify(err.error, 'Error', 'e');
                });
            }
        }

        return deferred.promise;
    };

    var _obtainAccessToken = function (externalData) {

        var deferred = $q.defer();
        $http.get(serviceBase + '/Account/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken, email: externalData.email } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false, firstName: response.firstName, id: response.id, expire: response['.expires'], roles: response.roles, img: response.img, lastLogin: response.lastLogin });

            _fillAuthData();

            deferred.resolve(response);

        }).error(function (err, status) {
            _signOut();
            deferred.reject(err);
            return $rootScope.showNotify(err.error, 'Error', 'e');
        });

        return deferred.promise;

    };

    var _registerExternal = function (registerExternalData) {

        var deferred = $q.defer();

        $http.post(serviceBase + '/Account/RegisterExternal', registerExternalData).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false, firstName: response.firstName, id: response.id, expire: response['.expires'], roles: response.roles, img: response.img, lastLogin: response.lastLogin });

            _fillAuthData();

            deferred.resolve(response);

        }).error(function (err, status) {
            _signOut();
            deferred.reject(err);
            return $rootScope.showNotify(err.error, 'Error', 'e');
            //return toastr.error(err.error, 'Error');
        });

        return deferred.promise;

    };

    var _getVerified = function (email, hash) {
        var Obj = { params: { email: email, hash: hash }, url: '/Account/VerifyEmail' };
        return $httpService.$get(Obj);
    };

    var _sendForgotPasswordLink = function (email) {
        var Obj = { params: { email: email }, url: '/Account/SendForgotPasswordLink' };
        return $httpService.$get(Obj);
    };

    var _resetPassword = function (User) {
        var Obj = { postData: User, url: '/Account/ResetPassword', successToast: 'Your password has been successfully reseted... You can login with new password.', errorToast: 'Error while reseting your password' };
        return $httpService.$post(Obj);
    };

    authServiceFactory.signUp = _signUp;
    authServiceFactory.signIn = _signIn;
    authServiceFactory.signOut = _signOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.refreshToken = _refreshToken;

    authServiceFactory.obtainAccessToken = _obtainAccessToken;
    authServiceFactory.externalAuthData = _externalAuthData;
    authServiceFactory.registerExternal = _registerExternal;
    authServiceFactory.getVerified = _getVerified;
    authServiceFactory.sendForgotPasswordLink = _sendForgotPasswordLink;
    authServiceFactory.resetPassword = _resetPassword;

    return authServiceFactory;
}])

.factory('authInterceptorService', ['$q', '$injector', '$location', 'localStorageService', function ($q, $injector, $location, localStorageService) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            var authService = $injector.get('authService');
            var authData = localStorageService.get('authorizationData');

            if (authData) {
                if (authData.useRefreshTokens) {
                    $location.path('/refresh');
                    return $q.reject(rejection);
                }
            }
            authService.signOut();
            $location.path('/');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    //authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}])

.factory('ErrorHttpInterceptor', function ($q) {
    var errorCounter = 0;
    function notifyError(rejection) {
        $.bigBox({
            title: rejection.status + ' ' + rejection.statusText,
            content: rejection.data,
            color: "#C46A69",
            icon: "fa fa-warning shake animated",
            number: ++errorCounter,
            timeout: 6000
        });
    }

    return {
        // On request failure
        requestError: function (rejection) {
            // show notification
            notifyError(rejection);

            // Return the promise rejection.
            return $q.reject(rejection);
        },

        // On response failure
        responseError: function (rejection) {
            // show notification
            notifyError(rejection);
            // Return the promise rejection.
            return $q.reject(rejection);
        }
    };
});