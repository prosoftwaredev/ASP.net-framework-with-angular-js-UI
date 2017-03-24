'use strict';

angular.module('app.labourclassification', []).config(function () { })

.factory('LabourClassificationService', function ($httpService) {
    return {
        getLabourClassificationByID: function (LabourClassificationId) {
            var Obj = { params: { LabourClassificationID: LabourClassificationId }, url: '/LabourClassification/GetLabourClassificationByID' };
            return $httpService.$get(Obj);
        },
        addLabourClassification: function (LabourClassification) {
            var Obj = { postData: LabourClassification, url: '/LabourClassification/CreateLabourClassification', successToast: 'LabourClassification added successfully', errorToast: 'Error while adding new LabourClassification' };
            return $httpService.$post(Obj);
        },
        editLabourClassification: function (LabourClassification) {
            var Obj = { postData: LabourClassification, url: '/LabourClassification/UpdateLabourClassification', successToast: 'LabourClassification edited successfully', errorToast: 'Error while editing existing LabourClassification' };
            return $httpService.$put(Obj);
        },
        removeLabourClassification: function (ID) {
            var Obj = { url: '/LabourClassification/DeleteLabourClassification/' + ID, successToast: 'LabourClassification removed successfully', errorToast: 'Error while removing existing LabourClassification' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('LabourClassificationCtrl_Add', function ($uibModalInstance, $scope, $rootScope, LabourClassificationService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addLabourClassification = function (LabourClassification) {

        if (LabourClassification.LabourClassificationName == null || LabourClassification.LabourClassificationName == '')
            return $rootScope.showNotify('LabourClassification name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        LabourClassificationService.addLabourClassification(LabourClassification).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('LabourClassificationCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, LabourClassificationService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getLabourClassificationByID($scope.LabourClassificationID);

    function getLabourClassificationByID(LabourClassificationID) {
        $rootScope.IsLoading = true;
        LabourClassificationService.getLabourClassificationByID(LabourClassificationID).then(function (response) {
            $scope.LabourClassification = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editLabourClassification = function (LabourClassification) {

        if (LabourClassification.LabourClassificationName == null || LabourClassification.LabourClassificationName == '')
            return $rootScope.showNotify('LabourClassification name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        LabourClassificationService.editLabourClassification(LabourClassification).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('LabourClassificationCtrl', function ($scope, $state, $rootScope, $dataTableService, LabourClassificationService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.LabourClassificationID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/LabourClassification/GetLabourClassifications',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.LabourClassificationID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteLabourClassification(' + data.LabourClassificationID + ')">' +
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
        { Column: 'LabourClassificationID', Title: 'ID', containsHtml: false },
        { Column: 'LabourClassificationName', Title: 'Labour Classification Name', containsHtml: false },
       { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.LabourClassification = {};

    function clearAll() {
        $scope.LabourClassification.LabourClassificationName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (LabourClassificationID) {
        $scope.LabourClassificationID = LabourClassificationID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editLabourClassificationModal.html',
            controller: 'LabourClassificationCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addLabourClassificationModal.html',
            controller: 'LabourClassificationCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteLabourClassification = function (LabourClassificationID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            LabourClassificationService.removeLabourClassification(LabourClassificationID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('labourClassificationForm', function () {

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
                    labourclassificationname: {
                        validators: {
                            notEmpty: {
                                message: 'The labour classification name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The labour classification name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The labour classification name can only consist of alphabets, space.'
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