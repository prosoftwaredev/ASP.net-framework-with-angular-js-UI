'use strict';

angular.module('app.equipment', []).config(function () { })

.factory('EquipmentService', function ($httpService) {
    return {
        getEquipmentByID: function (EquipmentId) {
            var Obj = { params: { EquipmentID: EquipmentId }, url: '/Equipment/GetEquipmentByID' };
            return $httpService.$get(Obj);
        },
        addEquipment: function (Equipment) {
            var Obj = { postData: Equipment, url: '/Equipment/CreateEquipment', successToast: 'Equipment added successfully', errorToast: 'Error while adding new Equipment' };
            return $httpService.$post(Obj);
        },
        editEquipment: function (Equipment) {
            var Obj = { postData: Equipment, url: '/Equipment/UpdateEquipment', successToast: 'Equipment edited successfully', errorToast: 'Error while editing existing Equipment' };
            return $httpService.$put(Obj);
        },
        removeEquipment: function (ID) {
            var Obj = { url: '/Equipment/DeleteEquipment/' + ID, successToast: 'Equipment removed successfully', errorToast: 'Error while removing existing Equipment' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('EquipmentCtrl_Add', function ($uibModalInstance, $scope, $rootScope, EquipmentService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addEquipment = function (Equipment) {

        if (Equipment.EquipmentName == null || Equipment.EquipmentName == '')
            return $rootScope.showNotify('Equipment name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        EquipmentService.addEquipment(Equipment).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('EquipmentCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, EquipmentService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getEquipmentByID($scope.EquipmentID);

    function getEquipmentByID(EquipmentID) {
        $rootScope.IsLoading = true;
        EquipmentService.getEquipmentByID(EquipmentID).then(function (response) {
            $scope.Equipment = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editEquipment = function (Equipment) {

        if (Equipment.EquipmentName == null || Equipment.EquipmentName == '')
            return $rootScope.showNotify('Equipment name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        EquipmentService.editEquipment(Equipment).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('EquipmentCtrl', function ($scope, $state, $rootScope, $dataTableService, EquipmentService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.EquipmentID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/Equipment/GetEquipments',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.EquipmentID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteEquipment(' + data.EquipmentID + ')">' +
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
        { Column: 'EquipmentID', Title: 'ID', containsHtml: false },
        { Column: 'EquipmentName', Title: 'Equipment Name', containsHtml: false },
        { Column: 'Rate', Title: 'Rate', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.Equipment = {};

    function clearAll() {
        $scope.Equipment.EquipmentName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (EquipmentID) {
        $scope.EquipmentID = EquipmentID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editEquipmentModal.html',
            controller: 'EquipmentCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addEquipmentModal.html',
            controller: 'EquipmentCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteEquipment = function (EquipmentID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EquipmentService.removeEquipment(EquipmentID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('equipmentForm', function () {

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
                    equipmentname: {
                        validators: {
                            notEmpty: {
                                message: 'The equipment name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The equipment name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The equipment name can only consist of alphabets, space.'
                            }
                        }
                    },
                    rate: {
                        validators: {
                            notEmpty: {
                                message: 'The equipment rate is required'
                            },
                            regexp: {
                                regexp: /^[0-9]+(\.[0-9]{1,2})?$/,
                                message: 'The value is not valid',
                            }
                        }
                    },
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