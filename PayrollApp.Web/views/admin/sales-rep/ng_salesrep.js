'use strict';

angular.module('app.salesrep', []).config(function () { })

.factory('SalesRepService', function ($httpService) {
    return {
        getSalesRepByID: function (SalesRepId) {
            var Obj = { params: { SalesRepID: SalesRepId }, url: '/SalesRep/GetSalesRepByID' };
            return $httpService.$get(Obj);
        },
        addSalesRep: function (SalesRep) {
            var Obj = { postData: SalesRep, url: '/SalesRep/CreateSalesRep', successToast: 'SalesRep added successfully', errorToast: 'Error while adding new SalesRep' };
            return $httpService.$post(Obj);
        },
        editSalesRep: function (SalesRep) {
            var Obj = { postData: SalesRep, url: '/SalesRep/UpdateSalesRep', successToast: 'SalesRep edited successfully', errorToast: 'Error while editing existing SalesRep' };
            return $httpService.$put(Obj);
        },
        removeSalesRep: function (ID) {
            var Obj = { url: '/SalesRep/DeleteSalesRep/' + ID, successToast: 'SalesRep removed successfully', errorToast: 'Error while removing existing SalesRep' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('SalesRepCtrl_Add', function ($uibModalInstance, $scope, $rootScope, SalesRepService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addSalesRep = function (SalesRep) {

        if (SalesRep.SalesRepName == null || SalesRep.SalesRepName == '')
            return $rootScope.showNotify('SalesRep name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        SalesRepService.addSalesRep(SalesRep).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('SalesRepCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, SalesRepService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getSalesRepByID($scope.SalesRepID);

    function getSalesRepByID(SalesRepID) {
        $rootScope.IsLoading = true;
        SalesRepService.getSalesRepByID(SalesRepID).then(function (response) {
            $scope.SalesRep = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editSalesRep = function (SalesRep) {

        if (SalesRep.SalesRepName == null || SalesRep.SalesRepName == '')
            return $rootScope.showNotify('SalesRep name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        SalesRepService.editSalesRep(SalesRep).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('SalesRepCtrl', function ($scope, $state, $rootScope, $dataTableService, SalesRepService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.SalesRepID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/SalesRep/GetSalesReps',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.SalesRepID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteSalesRep(' + data.SalesRepID + ')">' +
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
        { Column: 'SalesRepID', Title: 'ID', containsHtml: false },
        { Column: 'SalesRepName', Title: 'SalesRep Name', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.SalesRep = {};

    function clearAll() {
        $scope.SalesRep.SalesRepName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (SalesRepID) {
        $scope.SalesRepID = SalesRepID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editSalesRepModal.html',
            controller: 'SalesRepCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addSalesRepModal.html',
            controller: 'SalesRepCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteSalesRep = function (SalesRepID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            SalesRepService.removeSalesRep(SalesRepID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('salesRepForm', function () {

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
                    salesrepname: {
                        validators: {
                            notEmpty: {
                                message: 'The sales rep name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The sales rep name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The sales rep name can only consist of alphabets, space.'
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