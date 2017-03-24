'use strict';

angular.module('app.exception', []).config(function () { })

.factory('ExcLoggerService', function ($httpService) {
    return {
        getExcLoggerByID: function (ExcLoggerId) {
            var Obj = { params: { ExcLoggerID: ExcLoggerId }, url: '/ExcLogger/GetExcLoggerByID' };
            return $httpService.$get(Obj);
        },
        addExcLogger: function (ExcLogger) {
            var Obj = { postData: ExcLogger, url: '/ExcLogger/CreateExcLogger', successToast: 'ExcLogger added successfully', errorToast: 'Error while adding new ExcLogger' };
            return $httpService.$post(Obj);
        },
        editExcLogger: function (ExcLogger) {
            var Obj = { postData: ExcLogger, url: '/ExcLogger/UpdateExcLogger', successToast: 'ExcLogger edited successfully', errorToast: 'Error while editing existing ExcLogger' };
            return $httpService.$put(Obj);
        },
        removeExcLogger: function (ID) {
            var Obj = { url: '/ExcLogger/DeleteExcLogger/' + ID, successToast: 'ExcLogger removed successfully', errorToast: 'Error while removing existing ExcLogger' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('ExcLoggerCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, ExcLoggerService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getExcLoggerByID($scope.ExcLoggerID);

    function getExcLoggerByID(ExcLoggerID) {
        $rootScope.IsLoading = true;
        ExcLoggerService.getExcLoggerByID(ExcLoggerID).then(function (response) {
            $scope.ExcLogger = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editExcLogger = function (ExcLogger) {

        if (ExcLogger.ExcLoggerName == null || ExcLogger.ExcLoggerName == '')
            return $rootScope.showNotify('ExcLogger name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        ExcLoggerService.editExcLogger(ExcLogger).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('ExcLoggerCtrl', function ($scope, $state, $rootScope, $dataTableService, ExcLoggerService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.ExcLoggerID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/ExcLogger/GetExcLoggers',
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
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.ExcLoggerID + ')">' +
            '   <i class="fa fa-eye"></i> View' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteExcLogger(' + data.ExcLoggerID + ')">' +
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

    var renderMessage = function (data, type, full, meta) {
        return $filter('limitTo')(data, 50, 0) + '...'
    };

    var Obj = [
        { Column: 'ExcLoggerID', Title: 'ID', containsHtml: false },
        { Column: 'Message', Title: 'Error', containsHtml: true, renderWith: renderMessage },
        { Column: 'Controller', Title: 'Controller', containsHtml: false },
        { Column: 'Action', Title: 'Action', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.ExcLogger = {};

    function clearAll() {
        $scope.ExcLogger.ExcLoggerName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (ExcLoggerID) {
        console.log(size);
        $scope.ExcLoggerID = ExcLoggerID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editExcLoggerModal.html',
            controller: 'ExcLoggerCtrl_Edit',
            size: 'lg',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteExcLogger = function (ExcLoggerID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            ExcLoggerService.removeExcLogger(ExcLoggerID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})