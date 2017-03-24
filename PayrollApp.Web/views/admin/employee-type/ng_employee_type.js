'use strict';

angular.module('app.employeetype', []).config(function () { })

.factory('EmployeeTypeService', function ($httpService) {
    return {
        getEmployeeTypeByID: function (EmployeeTypeId) {
            var Obj = { params: { EmployeeTypeID: EmployeeTypeId }, url: '/EmployeeType/GetEmployeeTypeByID' };
            return $httpService.$get(Obj);
        },
        addEmployeeType: function (EmployeeType) {
            var Obj = { postData: EmployeeType, url: '/EmployeeType/CreateEmployeeType', successToast: 'EmployeeType added successfully', errorToast: 'Error while adding new EmployeeType' };
            return $httpService.$post(Obj);
        },
        editEmployeeType: function (EmployeeType) {
            var Obj = { postData: EmployeeType, url: '/EmployeeType/UpdateEmployeeType', successToast: 'EmployeeType edited successfully', errorToast: 'Error while editing existing EmployeeType' };
            return $httpService.$put(Obj);
        },
        removeEmployeeType: function (ID) {
            var Obj = { url: '/EmployeeType/DeleteEmployeeType/' + ID, successToast: 'EmployeeType removed successfully', errorToast: 'Error while removing existing EmployeeType' };
            return $httpService.$delete(Obj);
        },
    };
})

    .controller('EmployeeTypeCtrl_Add', function ($uibModalInstance, $scope, $rootScope, EmployeeTypeService) {

        $scope.ID = null;

        $scope.CloseModal = function () {
            $uibModalInstance.dismiss('cancel');
        };

        $scope.addEmployeeType = function (EmployeeType) {

            if (EmployeeType.EmployeeTypeName == null || EmployeeType.EmployeeTypeName == '')
                return $rootScope.showNotify('EmployeeType name field cannot be empty', 'Information', 'i');


            $rootScope.IsLoading = true;
            EmployeeTypeService.addEmployeeType(EmployeeType).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        };
    })


.controller('EmployeeTypeCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, EmployeeTypeService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getEmployeeTypeByID($scope.EmployeeTypeID);

    function getEmployeeTypeByID(EmployeeTypeID) {
        $rootScope.IsLoading = true;
        EmployeeTypeService.getEmployeeTypeByID(EmployeeTypeID).then(function (response) {
            $scope.EmployeeType = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editEmployeeType = function (EmployeeType) {

        if (EmployeeType.EmployeeTypeName == null || EmployeeType.EmployeeTypeName == '')
            return $rootScope.showNotify('EmployeeType name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        EmployeeTypeService.editEmployeeType(EmployeeType).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('EmployeeTypeCtrl', function ($scope, $state, $rootScope, $dataTableService, EmployeeTypeService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.EmployeeTypeID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/EmployeeType/GetEmployeeTypes',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var size = "sm";

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.EmployeeTypeID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteEmployeeType(' + data.EmployeeTypeID + ')">' +
            '   <i class="fa fa-trash-o"></i> Delete' +
            '</a>';
    };

    var renderIsEnable = function (data, type, full, meta) {
        if (data == true)
            return '<span class="label label-success">' + 'Yes' + '</span>';
        else
            return '<span class="label label-danger">' + 'No' + '</span>';
    };

    var renderIsEmployee = function (data, type, full, meta) {
        if (data == true)
            return '<span class="label label-success">' + 'Yes' + '</span>';
        else
            return '<span class="label label-danger">' + 'No' + '</span>';
    };

    var renderDate = function (data, type, full, meta) {
        return $filter('date')(data, "dd-MMM-yyyy hh:mm:ss a");
    };

    var renderNotes = function (data, type, full, meta) {
        return $filter('limitTo')(data, 20, 0) + '...'
    };

    var Obj = [
        { Column: 'EmployeeTypeID', Title: 'ID', containsHtml: false },
        { Column: 'EmployeeTypeName', Title: 'Employee Type', containsHtml: false },
        { Column: 'IsEmployee', Title: 'Is Employee', containsHtml: true, renderWith: renderIsEmployee },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.EmployeeType = {};

    function clearAll() {
        $scope.EmployeeType.EmployeeTypeName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addEmployeeTypeModal.html',
            controller: 'EmployeeTypeCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };


    $scope.OpenEditModal = function (EmployeeTypeID) {
        $scope.EmployeeTypeID = EmployeeTypeID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editEmployeeTypeModal.html',
            controller: 'EmployeeTypeCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteEmployeeType = function (EmployeeTypeID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeTypeService.removeEmployeeType(EmployeeTypeID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('employeeTypeForm', function () {

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
                    employeetypename: {
                        validators: {
                            notEmpty: {
                                message: 'The type name is required'
                            },
                            stringLength: {
                                max: 50,
                                message: 'The type name must be less than 50 characters long'
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