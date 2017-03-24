'use strict';

angular.module('app.role', []).config(function () { })

.factory('RoleService', function ($httpService) {
    return {
        getRoleByID: function (RoleId) {
            var Obj = { params: { RoleID: RoleId }, url: '/Role/GetRoleByID' };
            return $httpService.$get(Obj);
        },
        addRole: function (Role) {
            var Obj = { postData: Role, url: '/Role/CreateRole', successToast: 'Role added successfully', errorToast: 'Error while adding new role' };
            return $httpService.$post(Obj);
        },
        editRole: function (Role) {
            var Obj = { postData: Role, url: '/Role/UpdateRole', successToast: 'Role edited successfully', errorToast: 'Error while editing existing role' };
            return $httpService.$put(Obj);
        },
        removeRole: function (ID) {
            var Obj = { url: '/Role/DeleteRole/' + ID, successToast: 'Role removed successfully', errorToast: 'Error while removing existing role' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('RoleCtrl_Add', function ($uibModalInstance, $scope, $rootScope, RoleService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addRole = function (Role) {

        if (Role.RoleName == null || Role.RoleName == '')
            return $rootScope.showNotify('Role name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        RoleService.addRole(Role).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('RoleCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, RoleService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getRoleByID($scope.RoleID);

    function getRoleByID(RoleID) {
        $rootScope.IsLoading = true;
        RoleService.getRoleByID(RoleID).then(function (response) {
            $scope.Role = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editRole = function (Role) {

        if (Role.RoleName == null || Role.RoleName == '')
            return $rootScope.showNotify('Role name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        RoleService.editRole(Role).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('RoleCtrl', function ($scope, $state, $rootScope, $dataTableService, RoleService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.RoleID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/Role/GetRoles',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.RoleID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteRole(' + data.RoleID + ')">' +
            '   <i class="fa fa-trash-o"></i> Delete' +
            '</a>';
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
        { Column: 'RoleID', Title: 'ID', containsHtml: false },
        { Column: 'RoleName', Title: 'Role Name', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.Role = {};

    function clearAll() {
        $scope.Role.RoleName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (RoleID) {
        $scope.RoleID = RoleID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editRoleModal.html',
            controller: 'RoleCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addRoleModal.html',
            controller: 'RoleCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteRole = function (RoleID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            RoleService.removeRole(RoleID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('roleForm', function () {

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
                    rolename: {
                        validators: {
                            notEmpty: {
                                message: 'The role name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The role name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The role name can only consist of alphabets, space.'
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