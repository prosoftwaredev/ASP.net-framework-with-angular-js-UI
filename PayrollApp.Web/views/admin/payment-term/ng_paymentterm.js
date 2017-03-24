'use strict';

angular.module('app.paymentterm', []).config(function () { })

.factory('PaymentTermService', function ($httpService) {
    return {
        getPaymentTermByID: function (PaymentTermId) {
            var Obj = { params: { PaymentTermID: PaymentTermId }, url: '/PaymentTerm/GetPaymentTermByID' };
            return $httpService.$get(Obj);
        },
        addPaymentTerm: function (PaymentTerm) {
            var Obj = { postData: PaymentTerm, url: '/PaymentTerm/CreatePaymentTerm', successToast: 'PaymentTerm added successfully', errorToast: 'Error while adding new PaymentTerm' };
            return $httpService.$post(Obj);
        },
        editPaymentTerm: function (PaymentTerm) {
            var Obj = { postData: PaymentTerm, url: '/PaymentTerm/UpdatePaymentTerm', successToast: 'PaymentTerm edited successfully', errorToast: 'Error while editing existing PaymentTerm' };
            return $httpService.$put(Obj);
        },
        removePaymentTerm: function (ID) {
            var Obj = { url: '/PaymentTerm/DeletePaymentTerm/' + ID, successToast: 'PaymentTerm removed successfully', errorToast: 'Error while removing existing PaymentTerm' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('PaymentTermCtrl_Add', function ($uibModalInstance, $scope, $rootScope, PaymentTermService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addPaymentTerm = function (PaymentTerm) {

        if (PaymentTerm.PaymentTermName == null || PaymentTerm.PaymentTermName == '')
            return $rootScope.showNotify('PaymentTerm name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        PaymentTermService.addPaymentTerm(PaymentTerm).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('PaymentTermCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, PaymentTermService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getPaymentTermByID($scope.PaymentTermID);

    function getPaymentTermByID(PaymentTermID) {
        $rootScope.IsLoading = true;
        PaymentTermService.getPaymentTermByID(PaymentTermID).then(function (response) {
            $scope.PaymentTerm = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editPaymentTerm = function (PaymentTerm) {

        if (PaymentTerm.PaymentTermName == null || PaymentTerm.PaymentTermName == '')
            return $rootScope.showNotify('PaymentTerm name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        PaymentTermService.editPaymentTerm(PaymentTerm).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('PaymentTermCtrl', function ($scope, $state, $rootScope, $dataTableService, PaymentTermService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.PaymentTermID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/PaymentTerm/GetPaymentTerms',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.PaymentTermID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deletePaymentTerm(' + data.PaymentTermID + ')">' +
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
        { Column: 'PaymentTermID', Title: 'ID', containsHtml: false },
        { Column: 'PaymentTermName', Title: 'PaymentTerm Name', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.PaymentTerm = {};

    function clearAll() {
        $scope.PaymentTerm.PaymentTermName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (PaymentTermID) {
        $scope.PaymentTermID = PaymentTermID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editPaymentTermModal.html',
            controller: 'PaymentTermCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addPaymentTermModal.html',
            controller: 'PaymentTermCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deletePaymentTerm = function (PaymentTermID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            PaymentTermService.removePaymentTerm(PaymentTermID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('paymentTermForm', function () {

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
                    paymenttermname: {
                        validators: {
                            notEmpty: {
                                message: 'The Payment term name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The Payment term name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The Payment term name can only consist of alphabets, space.'
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