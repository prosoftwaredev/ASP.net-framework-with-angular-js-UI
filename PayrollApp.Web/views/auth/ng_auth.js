
"use strict";

angular.module('app.auth', []).config(function () { })
    .constant('authKeys', {
        googleClientId: '',
        facebookAppId: ''
    })

    .controller('AuthCtrl', function ($scope, $state, $stateParams, $rootScope, authService, $window, CFG, $location) {

        $scope.signUp = function (SignUp) {
            $rootScope.IsLoading = true;
            authService.signUp(SignUp).then(function (response) {
                $scope.SignUp.Firstname = null;
                $scope.SignUp.Lastname = null;
                $scope.SignUp.Email = null;
                $scope.SignUp.Password = null;
                $scope.SignUp.Gender = null;
                $rootScope.IsLoading = false;
                $state.go('login');
            });
        };

        $scope.signIn = function (SignIn) {
            $rootScope.IsLoading = true;
            authService.signIn(SignIn).then(function (response) {
                $rootScope.showNotify($rootScope.authData.firstName + ' Logged in successfully', 'Success', 's');
                $scope.SignIn.Email = null;
                $scope.SignIn.Password = null;

                $rootScope.IsLoading = false;
                $state.go('app.dashboard');
            }, function (error) {
                $rootScope.IsLoading = false;
            });
        };

        $scope.sendForgotPasswordLink = function (Email) {

            if (Email == null || Email == '')
                return $rootScope.showNotify('Email field cannot be empty', 'Information', 'i');
            

            $rootScope.IsLoading = true;
            authService.sendForgotPasswordLink(Email).then(function (response) {
                if (response.data)
                    $rootScope.showNotify('The link has been send to your registered email address. Please login your email.', 'Success', 's');
                else
                    $rootScope.showNotify('Failed to send link to your registered email address. Please try again later.', 'Error', 'e');

                $scope.Email = null;
                $rootScope.IsLoading = false;
            });
        };

        $scope.resetPassword = function (User) {
            $rootScope.IsLoading = true;
            User.Email = $stateParams.p1;
            User.Hash = $stateParams.p2;
            authService.resetPassword(User).then(function (response) {
                $scope.User.Password = null;
                $scope.User.CPassword = null;
                $rootScope.IsLoading = false;
                $state.go('login');
            });
        };
    })

.directive('loginForm', function () {

    return {
        restrict: 'A',
        link: function (scope, form) {
            form.bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    email: {
                        validators: {
                            notEmpty: {
                                message: 'The email address is required'
                            },
                            emailAddress: {
                                message: 'The email address is not valid'
                            }
                        }
                    },
                    password: {
                        validators: {
                            notEmpty: {
                                message: 'The password is required'
                            }
                        }
                    }
                }
            })
             .on('error.field.bv', function (e, data) {
                 var $invalidFields = data.bv.getInvalidFields();
                 if ($invalidFields.length > 0)
                     data.bv.disableSubmitButtons(true);
                 else
                     data.bv.disableSubmitButtons(false);
             })
                .on('success.field.bv', function (e, data) {
                    var $invalidFields = data.bv.getInvalidFields();
                    if ($invalidFields.length > 0)
                        data.bv.disableSubmitButtons(true);
                    else
                        data.bv.disableSubmitButtons(false);
                });
        }
    }
})

.directive('registerForm', function () {

    return {
        restrict: 'A',
        link: function (scope, form) {
            form.bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    firstname: {
                        validators: {
                            notEmpty: {
                                message: 'The first name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The first name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The first name can only consist of alphabets, space.'
                            }
                        }
                    },
                    lastname: {
                        validators: {
                            notEmpty: {
                                message: 'The last name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The last name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The last name can only consist of alphabets, space.'
                            }
                        }
                    },

                    email: {
                        validators: {
                            notEmpty: {
                                message: 'The email address is required'
                            },
                            emailAddress: {
                                message: 'The email address is not valid'
                            }
                        }
                    },
                    password: {
                        validators: {
                            notEmpty: {
                                message: 'The password is required'
                            }
                        }
                    },

                    gender: {
                        validators: {
                            notEmpty: {
                                message: 'The gender is required'
                            }
                        }
                    }
                }
            })
             .on('error.field.bv', function (e, data) {
                 var $invalidFields = data.bv.getInvalidFields();
                 if ($invalidFields.length > 0)
                     data.bv.disableSubmitButtons(true);
                 else
                     data.bv.disableSubmitButtons(false);
             })
                .on('success.field.bv', function (e, data) {
                    var $invalidFields = data.bv.getInvalidFields();
                    if ($invalidFields.length > 0)
                        data.bv.disableSubmitButtons(true);
                    else
                        data.bv.disableSubmitButtons(false);
                });
        }
    }
})

.directive('forgotLinkForm', function () {

    return {
        restrict: 'A',
        link: function (scope, form) {
            form.bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    email: {
                        validators: {
                            notEmpty: {
                                message: 'The email address is required'
                            },
                            emailAddress: {
                                message: 'The email address is not valid'
                            }
                        }
                    }
                }
            })
             .on('error.field.bv', function (e, data) {
                 var $invalidFields = data.bv.getInvalidFields();
                 if ($invalidFields.length > 0)
                     data.bv.disableSubmitButtons(true);
                 else
                     data.bv.disableSubmitButtons(false);
             })
                .on('success.field.bv', function (e, data) {
                    var $invalidFields = data.bv.getInvalidFields();
                    if ($invalidFields.length > 0)
                        data.bv.disableSubmitButtons(true);
                    else
                        data.bv.disableSubmitButtons(false);
                });
        }
    }
})

.directive('forgotForm', function () {

    return {
        restrict: 'A',
        link: function (scope, form) {
            form.bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    password: {
                        validators: {
                            notEmpty: {
                                message: 'The password is required'
                            }
                        }
                    },
                    confirmpassword: {
                        validators: {
                            notEmpty: {
                                message: 'The password is required'
                            },
                            identical: {
                                field: 'password',
                                message: 'The password and its confirm are not the same'
                            }
                        }
                    }
                }
            })
             .on('error.field.bv', function (e, data) {
                 var $invalidFields = data.bv.getInvalidFields();
                 if ($invalidFields.length > 0)
                     data.bv.disableSubmitButtons(true);
                 else
                     data.bv.disableSubmitButtons(false);
             })
                .on('success.field.bv', function (e, data) {
                    var $invalidFields = data.bv.getInvalidFields();
                    if ($invalidFields.length > 0)
                        data.bv.disableSubmitButtons(true);
                    else
                        data.bv.disableSubmitButtons(false);
                });
        }
    }
})






