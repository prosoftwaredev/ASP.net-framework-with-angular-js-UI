'use strict';

angular.module('app.account', []).config(function () { })

    .factory('AccountService', function ($httpService) {
        return {
            getUserByID: function (UserID) {
                var Obj = { params: { UserID: UserID }, url: '/User/GetUserByID' };
                return $httpService.$get(Obj);
            },
            editProfile: function (User, $file) {
                var Obj = { postData: User, postFile: $file, url: '/User/UpdateProfile', successToast: 'Profile edited successfully', errorToast: 'Error while editing profile' };
                return $httpService.$postMultiPart(Obj);
            },
            changePassword: function (PasswordData) {
                var Obj = { postData: PasswordData, url: '/User/ChangePassword', successToast: 'Password changed successfully', errorToast: 'Error while changing password' };
                return $httpService.$post(Obj);
            },
        };
    })

    .controller('ProfileCtrl', function ($scope, $state, $rootScope, AccountService, $filter) {

        var UserId = $rootScope.authData.id;
        getUserByID();

        function getUserByID() {
            AccountService.getUserByID(UserId).then(function (response) {
                $scope.User = response.data;
                $scope.Picture = $scope.User.Picture;
                $scope.User.DOB = $filter('date')($scope.User.DOB, "dd-MMM-yyyy");
            });
        };


        $scope.selectedFile = function ($files) {
            $scope.$files = $files;
        };

        $scope.editProfile = function (User) {
            if (!angular.isUndefined($scope.$files)) {
                for (var i = 0; i < $scope.$files.length; i++) {
                    var $file = $scope.$files[i];

                    (function (index) {
                        $rootScope.IsLoading = true;
                        AccountService.editProfile(User, $file).then(function (response) {
                            $rootScope.IsLoading = false;
                            getUserByID();
                        });
                    })(i);
                }
            }
            else {
                $rootScope.IsLoading = true;
                AccountService.editProfile(User, $file).then(function (response) {
                    $scope.editForm.$setPristine();
                    $scope.editForm.$setUntouched();
                    $scope.editForm.$submitted = false;
                    $rootScope.IsLoading = false;
                    getUserByID();
                });
            }
        };
    })

    .controller('ChangePasswordCtrl', function ($scope, $state, $rootScope, AccountService, $filter) {

        var UserId = $rootScope.authData.id;
        var Email = $rootScope.authData.userName;

        $scope.changePassword = function (PasswordData) {

            if (PasswordData.Password == null || PasswordData.Firstname == '')
                return $rootScope.showNotify('Current password field cannot be empty', 'Information', 'i');

            if (PasswordData.NewPassword == null || PasswordData.NewPassword == '')
                return $rootScope.showNotify('New password field cannot be empty', 'Information', 'i');

            if (PasswordData.ConfirmPassword == null || PasswordData.ConfirmPassword == '')
                return $rootScope.showNotify('Confirm password field cannot be empty', 'Information', 'i');

            PasswordData.UserID = UserId;

            $rootScope.IsLoading = true;
            AccountService.changePassword(PasswordData).then(function (response) {
                if (response.data == -1) {
                    $rootScope.showNotify('Password does not valid', 'Error', 'e');
                    $rootScope.IsLoading = false;
                }
                $rootScope.IsLoading = false;
            });
        };
    })

.directive('profileEditForm', function () {

    return {
        restrict: 'A',
        link: function (scope, form) {
            form.bootstrapValidator({
                //button: {
                //    // The submit buttons selector
                //    selector: '[type="submit"]',

                //    // The disabled class
                //    disabled: ''
                //},
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

.directive('changePasswordForm', function () {

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
                    currentpassword: {
                        validators: {
                            notEmpty: {
                                message: 'The current password is required'
                            }
                        }
                    },
                    newpassword: {
                        validators: {
                            notEmpty: {
                                message: 'The new password is required'
                            }
                        }
                    },
                    confirmpassword: {
                        validators: {
                            notEmpty: {
                                message: 'The confirm password is required'
                            },
                            identical: {
                                field: 'newpassword',
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
});
