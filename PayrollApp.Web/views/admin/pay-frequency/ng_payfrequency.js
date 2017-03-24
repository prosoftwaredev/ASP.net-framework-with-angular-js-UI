'use strict';

angular.module('app.payfrequency', []).config(function () { })

.factory('PayFrequencyService', function ($httpService) {
    return {
        getPayFrequencyByID: function (PayFrequencyId) {
            var Obj = { params: { PayFrequencyID: PayFrequencyId }, url: '/PayFrequency/GetPayFrequencyByID' };
            return $httpService.$get(Obj);
        },
        addPayFrequency: function (PayFrequency) {
            var Obj = { postData: PayFrequency, url: '/PayFrequency/CreatePayFrequency', successToast: 'PayFrequency added successfully', errorToast: 'Error while adding new PayFrequency' };
            return $httpService.$post(Obj);
        },
        editPayFrequency: function (PayFrequency) {
            var Obj = { postData: PayFrequency, url: '/PayFrequency/UpdatePayFrequency', successToast: 'PayFrequency edited successfully', errorToast: 'Error while editing existing PayFrequency' };
            return $httpService.$put(Obj);
        },
        removePayFrequency: function (ID) {
            var Obj = { url: '/PayFrequency/DeletePayFrequency/' + ID, successToast: 'PayFrequency removed successfully', errorToast: 'Error while removing existing PayFrequency' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('PayFrequencyCtrl_Add', function ($uibModalInstance, $scope, $rootScope, PayFrequencyService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addPayFrequency = function (PayFrequency) {

        if (PayFrequency.PayFrequencyName == null || PayFrequency.PayFrequencyName == '')
            return $rootScope.showNotify('PayFrequency name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        PayFrequencyService.addPayFrequency(PayFrequency).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('PayFrequencyCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, PayFrequencyService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getPayFrequencyByID($scope.PayFrequencyID);

    function getPayFrequencyByID(PayFrequencyID) {
        $rootScope.IsLoading = true;
        PayFrequencyService.getPayFrequencyByID(PayFrequencyID).then(function (response) {
            $scope.PayFrequency = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editPayFrequency = function (PayFrequency) {

        if (PayFrequency.PayFrequencyName == null || PayFrequency.PayFrequencyName == '')
            return $rootScope.showNotify('PayFrequency name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        PayFrequencyService.editPayFrequency(PayFrequency).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('PayFrequencyCtrl', function ($scope, $state, $rootScope, $dataTableService, PayFrequencyService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.PayFrequencyID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/PayFrequency/GetPayFrequencys',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.PayFrequencyID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deletePayFrequency(' + data.PayFrequencyID + ')">' +
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
        { Column: 'PayFrequencyID', Title: 'ID', containsHtml: false },
        { Column: 'PayFrequencyName', Title: 'Pay Frequency Name', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.PayFrequency = {};

    function clearAll() {
        $scope.PayFrequency.PayFrequencyName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (PayFrequencyID) {
        $scope.PayFrequencyID = PayFrequencyID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editPayFrequencyModal.html',
            controller: 'PayFrequencyCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addPayFrequencyModal.html',
            controller: 'PayFrequencyCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deletePayFrequency = function (PayFrequencyID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            PayFrequencyService.removePayFrequency(PayFrequencyID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('payFrequencyForm', function () {

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
                    payfrequencyname: {
                        validators: {
                            notEmpty: {
                                message: 'The pay frequency name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The pay frequency name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9\s]*$/,
                                message: 'The pay frequency name can only consist of alphabets, space.'
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