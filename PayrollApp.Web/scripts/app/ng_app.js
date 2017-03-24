'use strict';

angular.module('app', [
    'ngSanitize',
    'ngAnimate',
    'restangular',
    'ui.router',
    'ui.bootstrap',
    'ngAppState',

    'LocalStorageModule',
    'toastr',
    'ngService',
    'ngHttp',
    'ngDataTable',
    'ds.clock',
    'ncy-angular-breadcrumb',
    'ngIntlTelInput',
    'ngPagination',

    'SmartAdmin',

    'app.auth',
    'app.dashboard',
    'app.account',
    'app.role',
    'app.user',
    'app.error',
    'app.exception',
    'app.employee',
    'app.employeetype',
    'app.country',
    'app.state',
    'app.city',
    'app.payfrequency',
    'app.labourclassification',
    'app.customer',
    'app.certification',
    'app.title',
    'app.skill',
    'app.paymentterm',
    'app.salesrep',
    'app.order',
    'app.equipment',
    'app.image',
    'app.report',
    'app.rollover',
    'app.timeslip',
    'app.preference',
    'app.device'
])

.constant('APP_CONFIG', window.appConfig)

.constant('CFG', {
    rest: {
        baseURI: 'http://localhost:60586',
        //baseURI: 'http://rest.rsvpweb.ca',
        clientId: 'Payroll'
    },
    settings: {
        mainCountry: 1,
        mainState: 3,
        mainCity: 38445,
        gMapKey: 'AIzaSyC4Jj0dFfFmAg1FcNAAdZ-kf_Vx0Q7VPjY',
        defaultLabourRate: 11.00,
        defaultInvoiceRate: 18.57,
        oneRolePerUser: false,
        defaultPaytermForCustomer: 4,
        dayOfWeek: 'SATURDAY',
        defaultRollOverDate: '1900-01-01'
    }
})

.config(function ($provide, $httpProvider, $urlRouterProvider, $urlMatcherFactoryProvider, $locationProvider, RestangularProvider, $breadcrumbProvider, ngIntlTelInputProvider) {

    $urlMatcherFactoryProvider.caseInsensitive(true);
    $locationProvider.html5Mode({ enabled: true, requireBase: false });

    //$urlRouterProvider.when('', '/login');
    $urlRouterProvider.otherwise('/dashboard');

    //$httpProvider.interceptors.push('ErrorHttpInterceptor');
    $httpProvider.interceptors.push('authInterceptorService');

    RestangularProvider.setBaseUrl(location.pathname.replace(/[^\/]+?$/, ''));

    $breadcrumbProvider.setOptions({
        prefixStateName: 'app.dashboard',
        //template: 'bootstrap2'
    });

    ngIntlTelInputProvider.set({
        defaultCountry: 'ca',
        preferredCountries : ['ca', 'us'],
        utilsScript: 'script/ang/utils.js'
    });
})


.run(function ($rootScope, $state, $stateParams, CFG, $location, authService, toastr, $timeout) {
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;

    $rootScope.photoURI = CFG.rest.baseURI;

    $rootScope.pageNumber = 1;
    $rootScope.IsRefreshed = false;

    authService.fillAuthData();

    var expire = new Date($rootScope.authData.expire);
    var current = new Date();
    console.log('expire: ' + expire);
    console.log('current: ' + current);

    if (current > expire) {
        authService.signOut();
        $state.go('login');
        authService.fillAuthData();
    }

    $rootScope.IsLogined = function () {
        return $rootScope.authData.isAuth; //return true;
    };

    $rootScope.IsProfileComplete = function () {
        return true;
    };

    $rootScope.IsMenuActive = function (menuItem) {
        if (menuItem === $rootScope.menuItemActive) return 'active';
        return '';
    };

    $rootScope.IsMenuShow = function (menuItemShow) {
        for (var i = 0; i < menuItemShow.length; i++) {
            if ($rootScope.authData.roles.includes(menuItemShow[i])) {
                return true;
            }
        }
        return false;
    };

    var $html = $('html');

    $rootScope.$on('$stateChangeStart', function (event, nextState, nextParams, currentState, currentParams, options) {
        $rootScope.IsLoading = true;

        if (!angular.isUndefined($rootScope.menuItemActive)) delete $rootScope.menuItemActive;

        if (!angular.isUndefined(nextState.data.requireLogin) && nextState.data.requireLogin == true && !$rootScope.IsLogined()) {
            event.preventDefault();
            $state.go('login');
            return;
        };

        if (!angular.isUndefined(nextState.data.requireFilledProfile) && nextState.data.requireFilledProfile == true && !$rootScope.IsProfileComplete()) {
            event.preventDefault();
            $state.go('app.account.profile');
            return;
        }

        if (!angular.isUndefined(nextState.data.activateMainMenuItem))
            $rootScope.menuItemActive = nextState.data.activateMainMenuItem;

        if (!angular.isUndefined(nextState.data.activateTemplate))
            $rootScope.templateActive = nextState.data.activateTemplate;
        else
            $rootScope.templateActive = 'main';

        if (!angular.isUndefined(nextState.data.htmlId)) {
            $html.attr('id', nextState.data.htmlId);
            $rootScope.extrpage = nextState.data.htmlId;
        }
        else {
            $html.removeAttr('id');
            $rootScope.extrpage = null;
        }
    });

    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        $rootScope.IsLoading = false;
    })

    $rootScope.showNotify = function (title, msg, type) {
        switch (type) {
            case 's':
                toastr.success(title, msg);
                break;
            case 'e':
                toastr.error(title, msg);
                break;
            case 'i':
                toastr.info(title, msg);
                break;
            case 'w':
                toastr.warning(title, msg);
                break;
        }
    };

    $rootScope.clearNotify = function () {
        toastr.clear();
    };

    $rootScope.handleErrors = function (status) {
        $rootScope.IsLoading = false;
        if (status) {
            switch (status) {
                case 400:
                    {
                        var msg = 'The request you send to server is in bad fromat';
                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                    }
                    break;
                case 401:
                    {
                        var msg = 'You are unautorized to use this resource';
                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                        //$state.go('app.error.error401');
                    }
                    break;
                case 403:
                    {
                        var msg = 'This resource is not accessible for you';
                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                    }
                    break;
                case 404:
                    {
                        var msg = 'The resource you are looking is not found';
                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                        $state.go('app.error.error404');
                    }
                    break;
                case 405:
                    {
                        var msg = 'Bad action performed by user';
                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                    }
                    break;
                case 409:
                    {
                        var msg = 'Please sign up.';
                        var title = 'Error';
                        $state.go('login');
                        //$rootScope.showNotify(msg, title, 'e');
                    }
                    break;
                case 500:
                    {
                        var msg = 'Internal server error occured...';
                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                        $state.go('app.error.error500');
                    }
                    break;
                default:
                    {
                    }
            }
        }
    };

    $rootScope.handleErrors = function (err, status) {
        $rootScope.IsLoading = false;
        if (status) {
            switch (status) {
                case 400:
                    {
                        var msg = '';
                        if (err == '')
                            msg = 'The request you send to server is in bad fromat';
                        else
                            msg = err.Message;

                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                    }
                    break;
                case 401:
                    {
                        var msg = '';
                        if (err == '')
                            msg = 'You are unautorized to use this resource';
                        else
                            msg = err.Message;

                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                        //$state.go('app.error.error401');
                    }
                    break;
                case 403:
                    {
                        var msg = '';
                        if (err == '')
                            msg = 'This resource is not accessible for you';
                        else
                            msg = err.Message;

                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                    }
                    break;
                case 404:
                    {
                        var msg = '';
                        if (err == '')
                            msg = 'The resource you are looking is not found';
                        else
                            msg = err.Message;

                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                        $state.go('app.error.error404');
                    }
                    break;
                case 405:
                    {
                        var msg = '';
                        if (err == '')
                            msg = 'Bad action performed by user';
                        else
                            msg = err.Message;

                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                    }
                    break;
                case 409:
                    {
                        var msg = '';
                        if (err == '')
                            msg = 'Please sign up.';
                        else
                            msg = err.Message;

                        var title = 'Error';
                        $state.go('login');
                        //$rootScope.showNotify(msg, title, 'e');
                    }
                    break;
                case 500:
                    {
                        var msg = '';
                        if (err == '')
                            msg = 'Internal server error occured...';
                        else
                            msg = err.Message;

                        var title = 'Error';
                        $rootScope.showNotify(msg, title, 'e');
                        $state.go('app.error.error500');
                    }
                    break;

                default:
                    {
                    }
            }
        }
    };

    $rootScope.startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
        }, 1000);
    };

    $rootScope.$watch('files', function (files) {
        $rootScope.formUpload = false;
        if (files != null) {
            if (!angular.isArray(files)) {
                $timeout(function () {
                    $scope.files = files = [files];
                });
                return;
            }
        }
    });
});

"use strict";

angular.module('app').directive('toggleShortcut', function ($log, $timeout) {

    var initDomEvents = function ($element) {

        var shortcut_dropdown = $('#shortcut');

        $element.on('click', function () {

            if (shortcut_dropdown.is(":visible")) {
                shortcut_buttons_hide();
            } else {
                shortcut_buttons_show();
            }

        })

        shortcut_dropdown.find('a').click(function (e) {
            e.preventDefault();
            window.location = $(this).attr('href');
            setTimeout(shortcut_buttons_hide, 300);
        });



        // SHORTCUT buttons goes away if mouse is clicked outside of the area
        $(document).mouseup(function (e) {
            if (shortcut_dropdown && !shortcut_dropdown.is(e.target) && shortcut_dropdown.has(e.target).length === 0) {
                shortcut_buttons_hide();
            }
        });

        // SHORTCUT ANIMATE HIDE
        function shortcut_buttons_hide() {
            shortcut_dropdown.animate({
                height: "hide"
            }, 300, "easeOutCirc");
            $('body').removeClass('shortcut-on');

        }

        // SHORTCUT ANIMATE SHOW
        function shortcut_buttons_show() {
            shortcut_dropdown.animate({
                height: "show"
            }, 200, "easeOutCirc");
            $('body').addClass('shortcut-on');
        }
    }

    var link = function ($scope, $element) {
        $timeout(function () {
            initDomEvents($element);
        });
    }

    return {
        restrict: 'EA',
        link: link
    }
})



"use strict";

angular.module('app').controller("ActivitiesCtrl", function ActivitiesCtrl($scope, $log, activityService) {

    $scope.activeTab = 'default';
    $scope.currentActivityItems = [];

    // Getting different type of activites
    activityService.get(function (data) {

        $scope.activities = data.activities;

    });


    $scope.isActive = function (tab) {
        return $scope.activeTab === tab;
    };

    $scope.setTab = function (activityType) {
        $scope.activeTab = activityType;

        activityService.getbytype(activityType, function (data) {

            $scope.currentActivityItems = data.data;

        });

    };

});

"use strict";

angular.module('app').directive('activitiesDropdownToggle', function ($log) {

    var link = function ($scope, $element, attrs) {
        var ajax_dropdown = null;

        $element.on('click', function () {
            var badge = $(this).find('.badge');

            if (badge.hasClass('bg-color-red')) {

                badge.removeClass('bg-color-red').text(0);

            }

            ajax_dropdown = $(this).next('.ajax-dropdown');

            if (!ajax_dropdown.is(':visible')) {

                ajax_dropdown.fadeIn(150);

                $(this).addClass('active');

            }
            else {

                ajax_dropdown.fadeOut(150);

                $(this).removeClass('active');

            }

        })

        $(document).mouseup(function (e) {
            if (ajax_dropdown && !ajax_dropdown.is(e.target) && ajax_dropdown.has(e.target).length === 0) {
                ajax_dropdown.fadeOut(150);
                $element.removeClass('active');
            }
        });
    }

    return {
        restrict: 'EA',
        link: link
    }
});

"use strict";

angular.module('app').factory('activityService', function ($http, $log, APP_CONFIG) {

    function getActivities(callback) {

        $http.get(APP_CONFIG.apiRootUrl + '/activities/activity.json').success(function (data) {

            callback(data);

        }).error(function () {

            $log.log('Error');
            callback([]);

        });

    }

    function getActivitiesByType(type, callback) {

        $http.get(APP_CONFIG.apiRootUrl + '/activities/activity-' + type + '.json').success(function (data) {

            callback(data);

        }).error(function () {

            $log.log('Error');
            callback([]);

        });

    }

    return {
        get: function (callback) {
            getActivities(callback);
        },
        getbytype: function (type, callback) {
            getActivitiesByType(type, callback);
        }
    }
})

angular.module('app').directive("datepicker", function () {
    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, elem, attrs, ngModelCtrl) {
            var updateModel = function (dateText) {
                scope.$apply(function () {
                    ngModelCtrl.$setViewValue(dateText);
                });
            };
            var options = {
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                yearRange: "-60:+0",
                dateFormat: "mm/dd/yy",
                maskInput: false,
                onSelect: function (dateText) {
                    updateModel(dateText);
                }
            };
            elem.datepicker(options);
        }
    }
});

angular.module('app').directive('dateconvert', function (dateFilter) {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {

            //var dateFormat = 'MM/dd/yyyy';

            var dateFormat = attrs['date'] || 'MM/dd/yyyy';

            ctrl.$formatters.unshift(function (modelValue) {
                return dateFilter(modelValue, dateFormat);
            });
        }
    };
})

angular.module('app').directive('userLogout', function () {
    return {
        restrict: 'E',
        scope: {
            username: '='
        },
        link: function (scope, element, attrs) {
            
            element.on('click', '[data-action="userLogout"]', function (e) {
                var $this = $(this);
                smartActions.userLogout($this);
                e.preventDefault();
                $this = null;
            });

            var smartActions = {
                userLogout: function ($this) {
                    $.SmartMessageBox({
                        title: "<i class='fa fa-sign-out txt-color-orangeDark'></i> Logout <span class='txt-color-orangeDark'><strong>" + scope.username + "</strong></span> ?",
                        content: $this.data('logout-msg') || "You can improve your security further after logging out by closing this opened browser",
                        buttons: '[No][Yes]'

                    }, function (ButtonPressed) {
                        if (ButtonPressed == "Yes") {
                            logout();
                        }
                    });
                    function logout() {
                        window.location = $this.attr('href');
                    }
                },
            }
        }
    };
});


angular.module('app').directive("onClearSign", function () {
    return {
        restrict: "E",
        link: function (scope, elem, attrs) {
            ClearTablet();
        }
    }
});

angular.module('app').directive("onDoneSign", function () {
    return {
        restrict: "E",
        link: function (scope, elem, attrs) {
            if (NumberOfTabletPoints() == 0) {
                alert("Please sign before continuing");
            }
            else {
                SetTabletState(0, tmr);
                //RETURN TOPAZ-FORMAT SIGSTRING
                SetSigCompressionMode(1);
                document.FORM1.bioSigData.value = GetSigString();
                document.FORM1.sigStringData.value += GetSigString();
                //this returns the signature in Topaz's own format, with biometric information


                //RETURN BMP BYTE ARRAY CONVERTED TO BASE64 STRING
                SetImageXSize(500);
                SetImageYSize(100);
                SetImagePenWidth(5);
                GetSigImageB64(SigImageCallback);
            }

        }
    }
});

angular.module('app').directive("onSignSign", function () {
    return {
        restrict: "E",
        link: function (scope, elem, attrs) {
            var ctx = document.getElementById('cnv').getContext('2d');
            SetDisplayXSize(500);
            SetDisplayYSize(100);
            SetTabletState(0, tmr);
            SetJustifyMode(0);
            ClearTablet();
            if (tmr == null) {
                tmr = SetTabletState(1, ctx, 50);
            }
            else {
                SetTabletState(0, tmr);
                tmr = null;
                tmr = SetTabletState(1, ctx, 50);
            }


        }
    }
});