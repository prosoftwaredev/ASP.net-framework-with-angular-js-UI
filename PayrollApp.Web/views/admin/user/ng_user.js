'use strict';

angular.module('app.user', []).config(function () { })

.factory('UserService', function ($httpService) {
        return {
            getUserByID: function (UserID) {
                var Obj = { params: { UserID: UserID }, url: '/User/GetUserByID' };
                return $httpService.$get(Obj);
            },
            addUser: function (User) {
                var Obj = { postData: User, url: '/User/CreateUser', successToast: 'User added successfully', errorToast: 'Error while adding new user' };
                return $httpService.$post(Obj);
            },
            editUser: function (User) {
                var Obj = { postData: User, url: '/User/UpdateUser', successToast: 'User edited successfully', errorToast: 'Error while editing existing user' };
                return $httpService.$put(Obj);
            },
            removeUser: function (ID) {
                var Obj = { url: '/User/DeleteUser/' + ID, successToast: 'User removed successfully', errorToast: 'Error while removing existing user' };
                return $httpService.$delete(Obj);
            },
            getAllRoles: function (isDisplayAll) {
                var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/Role/GetAllRoles' };
                return $httpService.$get(Obj);
            },

            addUserRole: function (UserRole) {
                var Obj = { postData: UserRole, url: '/UserRole/CreateUserRole', successToast: 'User role added successfully', errorToast: 'Error while adding new user role' };
                return $httpService.$post(Obj);
            },
            removeUserRole: function (UserRole) {
                var Obj = { postData: UserRole, url: '/UserRole/DeleteUserRole', successToast: 'User role removed successfully', errorToast: 'Error while removing existing user role' };
                return $httpService.$postDelete(Obj);
            },
            getAllRolesByUserID: function (UserID) {
                var Obj = { params: { UserID: UserID }, url: '/UserRole/GetAllRolesByUserID' };
                return $httpService.$get(Obj);
            },

            addUserRoleByRadio: function (UserRole) {
                var Obj = { postData: UserRole, url: '/UserRole/CreateUserRoleByRadio', successToast: 'User role added successfully', errorToast: 'Error while adding new user role' };
                return $httpService.$post(Obj);
            },
        };
    })

.controller('UserCtrl_Add', function ($uibModalInstance, $scope, $rootScope, UserService) {

        $scope.ID = null;

        $scope.CloseModal = function () {
            $uibModalInstance.dismiss('cancel');
            $uibModalInstance.close();
        };

        $scope.getClearedModal = function () {
            $scope.ID = null;
            $scope.User.Firstname = null;
            $scope.User.Lastname = null;
            $scope.User.Email = null;
        };

        $scope.addUser = function (User) {

            if (User.Firstname == null || User.Firstname == '')
                return $rootScope.showNotify('First name field cannot be empty', 'Information', 'i');

            if (User.Lastname == null || User.Lastname == '')
                return $rootScope.showNotify('Last name field cannot be empty', 'Information', 'i');

            if (User.Email == null || User.Email == '')
                return $rootScope.showNotify('Email field cannot be empty', 'Information', 'i');


            $rootScope.IsLoading = true;
            UserService.addUser(User).then(function (response) {
                $scope.ID = response.data;
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        };

        $scope.getAllRolesByUserID = function () {
            $scope.getAllRoles();
            if ($scope.ID != null) {
                $rootScope.IsLoading = true;
                UserService.getAllRolesByUserID($scope.ID).then(function (response) {
                    $scope.SelectedRoles = response.data;
                    $rootScope.IsLoading = false;

                    $scope.Roles.forEach(function (i) {
                        i.Flag = false;
                        $scope.SelectedRoles.forEach(function (j) {
                            if (i.RoleID == j.RoleID)
                            {
                                if ($rootScope.oneRolePerUser)
                                    i.Flag = i.RoleID;
                                else
                                    i.Flag = true;
                            }
                        });
                    });
                });
            }
        };

        $scope.addUserRole = function (flag, RoleID) {

            var UserRole = {
                UserID: $scope.ID,
                RoleID: RoleID
            };
            if ($scope.ID != null) {
                if (flag) {
                    $rootScope.IsLoading = true;
                    UserService.addUserRole(UserRole).then(function (response) {
                        $rootScope.IsLoading = false;
                    });
                }
                else {
                    $rootScope.IsLoading = true;
                    UserService.removeUserRole(UserRole).then(function (response) {
                        $rootScope.IsLoading = false;
                    });
                }
            }
            else
                $rootScope.showNotify('Please add user before assigning role.', 'Error', 'e');
        };


        $scope.addUserRoleByRadio = function (RoleID) {
            var UserRole = {
                UserID: $scope.ID,
                RoleID: RoleID
            };
            if ($scope.ID != null) {
                $rootScope.IsLoading = true;
                UserService.addUserRoleByRadio(UserRole).then(function (response) {
                    $rootScope.IsLoading = false;
                });
            }
            else
                $rootScope.showNotify('Please add user before assigning role.', 'Error', 'e');
        };
    })

.controller('UserCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, UserService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
        $uibModalInstance.close();
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        $scope.User.Firstname = null;
        $scope.User.Lastname = null;
        $scope.User.Email = null;
    };

    getUserByID($scope.UserID);

    function getUserByID(UserID) {
        $rootScope.IsLoading = true;
        UserService.getUserByID(UserID).then(function (response) {
            $scope.ID = response.data.UserID;
            $scope.User = response.data;
            $rootScope.IsLoading = false;
            //$scope.getAllRolesByUserID();
        });
    };

    $scope.editUser = function (User) {

        if (User.Firstname == null || User.Firstname == '')
            return $rootScope.showNotify('First name field cannot be empty', 'Information', 'i');

        if (User.Lastname == null || User.Lastname == '')
            return $rootScope.showNotify('Last name field cannot be empty', 'Information', 'i');

        if (User.Email == null || User.Email == '')
            return $rootScope.showNotify('Email field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        UserService.editUser(User).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };

    $scope.getAllRolesByUserID = function () {
        $scope.getAllRoles();
        if ($scope.ID != null) {
            $rootScope.IsLoading = true;
            UserService.getAllRolesByUserID($scope.ID).then(function (response) {
                $scope.SelectedRoles = response.data;
                $rootScope.IsLoading = false;

                $scope.Roles.forEach(function (i) {
                    i.Flag = false;
                    $scope.SelectedRoles.forEach(function (j) {
                        if (i.RoleID == j.RoleID)
                        {
                            if($rootScope.oneRolePerUser)
                                i.Flag = i.RoleID;
                            else
                                i.Flag = true;
                        }
                    });
                });
            });
        }
    };

    $scope.addUserRole = function (flag, RoleID) {

        var UserRole = {
            UserID: $scope.ID,
            RoleID: RoleID
        };
        if ($scope.ID != null) {
            if (flag) {
                $rootScope.IsLoading = true;
                UserService.addUserRole(UserRole).then(function (response) {
                    $rootScope.IsLoading = false;
                });
            }
            else {
                $rootScope.IsLoading = true;
                UserService.removeUserRole(UserRole).then(function (response) {
                    $rootScope.IsLoading = false;
                });
            }
        }
        else
            $rootScope.showNotify('Please add user before assigning role.', 'Error', 'e');
    };

    $scope.addUserRoleByRadio = function (RoleID) {
        var UserRole = {
            UserID: $scope.ID,
            RoleID: RoleID
        };
        if ($scope.ID != null) {
            $rootScope.IsLoading = true;
            UserService.addUserRoleByRadio(UserRole).then(function (response) {
                $rootScope.IsLoading = false;
            });
        }
        else
            $rootScope.showNotify('Please add user before assigning role.', 'Error', 'e');
    };
})

.controller('UserCtrl', function ($scope, $state, $rootScope, $dataTableService, UserService, $window, $compile, $filter, $uibModal, $log, CFG) {

    $rootScope.oneRolePerUser = CFG.settings.oneRolePerUser;

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal('md', aData.UserID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/User/GetUsers',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };

    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {

        return '<a class="btn-cust" ng-click="OpenEditModal(md, ' + data.UserID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;'
            +
            '<a class="btn-cust" ng-click="deleteUser(' + data.UserID + ')">' +
            '   <i class="fa fa-trash-o"></i> Delete' +
            '</a>&nbsp;';
    };

    var renderIsEnable = function (data, type, full, meta) {
        if (data == true)
            return '<span class="label label-success">' + 'Yes' + '</span>';
        else
            return '<span class="label label-danger">' + 'No' + '</span>';
    };

    var renderDate = function (data, type, full, meta) {
        return $filter('date')(data, "dd-MMM-yyyy hh:mm:ss a");
    };

    var Obj = [
        { Column: 'UserID', Title: 'ID', containsHtml: false },
        { Column: 'Firstname', Title: 'First Name', containsHtml: false },
        { Column: 'Lastname', Title: 'Last Name', containsHtml: false },
        { Column: 'Email', Title: 'Email', containsHtml: false },
        { Column: 'Phone', Title: 'Phone', containsHtml: false },
        { Column: 'LastLogin', Title: 'Last Login & IP', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};   

    $scope.User = {};

    var isDisplayAll = false;

    $scope.getAllRoles = function () {
        $rootScope.IsLoading = true;
        UserService.getAllRoles(isDisplayAll).then(function (response) {
            $scope.Roles = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.OpenEditModal = function (size, UserID) {
        $scope.UserID = UserID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editUserModal.html',
            controller: 'UserCtrl_Edit',
            size: size,
            scope: $scope,
            backdrop: false,
            keyboard: false
        });

        modalInstance.result.then(function (selectedItem) {
            $ctrl.selected = selectedItem;
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.OpenAddModal = function (size) {
        var modalInstance = $uibModal.open({
            templateUrl: 'addUserModal.html',
            controller: 'UserCtrl_Add',
            size: size,
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteUser = function (UserID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            UserService.removeUser(UserID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };

    //$scope.dtInstance = function (dtInst) {
    //    $scope.dtInstanc = dtInst;

    //    var table = $scope.dtInstanc.DataTable;

    //    table.columns().eq(0).each(function (colIdx) {
    //        $('input', table.column(colIdx).footer()).on('keyup change', function () {
    //            console.log("colIdx", colIdx);
    //            table
    //                .column(colIdx)
    //                .search(this.value)
    //                .draw();

    //            console.log(table
    //                .column(colIdx)
    //                .search(this.value));
    //        });
    //    });
    //};
})

.directive('userForm', function () {

    return {
        restrict: 'A',
        link: function (scope, form) {
            form.bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                submitButtons: 'button[type="submit"]',
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
                    phone: {
                        validators: {
                            callback: {
                                message: 'The phone number is not valid',
                                callback: function (value, validator, $field) {
                                    return value === '' || $field.intlTelInput('isValidNumber');
                                }
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
                })
                .on('click', '.country-list', function () {
                    form.bootstrapValidator('revalidateField', 'phone');
                });

            form.find('[name="phone"]').mask('+1 (999) 999 9999');
            form.find('[name="dob"]').mask('99/99/9999');
        }
    }
})