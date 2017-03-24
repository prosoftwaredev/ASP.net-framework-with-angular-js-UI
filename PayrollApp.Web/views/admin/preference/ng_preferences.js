'use strict';

angular.module('app.preference', []).config(function () { })

.factory('PreferenceService', function ($httpService) {
    return {
        getPreferenceByID: function (PreferenceId) {
            var Obj = { params: { PreferenceID: PreferenceId }, url: '/Preference/GetPreferenceByID' };
            return $httpService.$get(Obj);
        },
        addPreference: function (Preference) {
            var Obj = { postData: Preference, url: '/Preference/CreatePreference', successToast: 'Preference added successfully', errorToast: 'Error while adding new Preference' };
            return $httpService.$post(Obj);
        },
        editPreference: function (Preference) {
            var Obj = { postData: Preference, url: '/Preference/UpdatePreference', successToast: 'Preference edited successfully', errorToast: 'Error while editing existing Preference' };
            return $httpService.$put(Obj);
        },
        removePreference: function (ID) {
            var Obj = { url: '/Preference/DeletePreference/' + ID, successToast: 'Preference removed successfully', errorToast: 'Error while removing existing Preference' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('PreferenceCtrl_Add', function ($uibModalInstance, $scope, $rootScope, PreferenceService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addPreference = function (Preference) {

        if (Preference.PreferenceName == null || Preference.PreferenceName == '')
            return $rootScope.showNotify('Preference name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        PreferenceService.addPreference(Preference).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('PreferenceCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, PreferenceService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getPreferenceByID($scope.PreferenceID);

    function getPreferenceByID(PreferenceID) {
        $rootScope.IsLoading = true;
        PreferenceService.getPreferenceByID(PreferenceID).then(function (response) {
            $scope.Preference = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editPreference = function (Preference) {

        if (Preference.PreferenceName == null || Preference.PreferenceName == '')
            return $rootScope.showNotify('Preference name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        PreferenceService.editPreference(Preference).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('PreferenceCtrl', function ($scope, $state, $rootScope, $dataTableService, PreferenceService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.PreferenceID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/Preference/GetPreferences',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.PreferenceID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deletePreference(' + data.PreferenceID + ')">' +
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
        { Column: 'PreferenceID', Title: 'ID', containsHtml: false },
        { Column: 'PreferenceName', Title: 'Preference Name', containsHtml: false },
        { Column: 'PreferenceValue', Title: 'Preference Value', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.Preference = {};

    function clearAll() {
        $scope.Preference.PreferenceName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (PreferenceID) {
        $scope.PreferenceID = PreferenceID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editPreferenceModal.html',
            controller: 'PreferenceCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addPreferenceModal.html',
            controller: 'PreferenceCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deletePreference = function (PreferenceID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            PreferenceService.removePreference(PreferenceID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('preferenceForm', function () {

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
                    preferencename: {
                        validators: {
                            notEmpty: {
                                message: 'The preference name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The preference name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The preference name can only consist of alphabets, space.'
                            }
                        }
                    },
                    preferencevalue: {
                        validators: {
                            notEmpty: {
                                message: 'The preference value is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The preference value must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The preference value can only consist of alphabets, space.'
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