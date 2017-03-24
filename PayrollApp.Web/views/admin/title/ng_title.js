'use strict';

angular.module('app.title', []).config(function () { })

.factory('TitleService', function ($httpService) {
    return {
        getTitleByID: function (TitleId) {
            var Obj = { params: { TitleID: TitleId }, url: '/Title/GetTitleByID' };
            return $httpService.$get(Obj);
        },
        addTitle: function (Title) {
            var Obj = { postData: Title, url: '/Title/CreateTitle', successToast: 'Title added successfully', errorToast: 'Error while adding new Title' };
            return $httpService.$post(Obj);
        },
        editTitle: function (Title) {
            var Obj = { postData: Title, url: '/Title/UpdateTitle', successToast: 'Title edited successfully', errorToast: 'Error while editing existing Title' };
            return $httpService.$put(Obj);
        },
        removeTitle: function (ID) {
            var Obj = { url: '/Title/DeleteTitle/' + ID, successToast: 'Title removed successfully', errorToast: 'Error while removing existing Title' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('TitleCtrl_Add', function ($uibModalInstance, $scope, $rootScope, TitleService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addTitle = function (Title) {

        if (Title.TitleName == null || Title.TitleName == '')
            return $rootScope.showNotify('Title name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        TitleService.addTitle(Title).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('TitleCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, TitleService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getTitleByID($scope.TitleID);

    function getTitleByID(TitleID) {
        $rootScope.IsLoading = true;
        TitleService.getTitleByID(TitleID).then(function (response) {
            $scope.Title = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editTitle = function (Title) {

        if (Title.TitleName == null || Title.TitleName == '')
            return $rootScope.showNotify('Title name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        TitleService.editTitle(Title).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('TitleCtrl', function ($scope, $state, $rootScope, $dataTableService, TitleService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.TitleID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/Title/GetTitles',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.TitleID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteTitle(' + data.TitleID + ')">' +
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
        { Column: 'TitleID', Title: 'ID', containsHtml: false },
        { Column: 'TitleName', Title: 'Title Name', containsHtml: false },
        { Column: 'Gender', Title: 'Gender', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.Title = {};

    function clearAll() {
        $scope.Title.TitleName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (TitleID) {
        $scope.TitleID = TitleID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editTitleModal.html',
            controller: 'TitleCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addTitleModal.html',
            controller: 'TitleCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteTitle = function (TitleID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            TitleService.removeTitle(TitleID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('titleForm', function () {

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
                    titlename: {
                        validators: {
                            notEmpty: {
                                message: 'The title name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The title name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s\.]*$/,
                                message: 'The title name can only consist of alphabets, space.'
                            }
                        }
                    },
                    gender: {
                        validators: {
                            notEmpty: {
                                message: 'The gender is required'
                            },
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