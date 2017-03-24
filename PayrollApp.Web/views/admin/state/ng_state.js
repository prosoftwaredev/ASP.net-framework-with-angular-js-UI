'use strict';

angular.module('app.state', []).config(function () { })

.factory('StateService', function ($httpService) {
    return {
        getStateByID: function (StateId) {
            var Obj = { params: { StateID: StateId }, url: '/State/GetStateByID' };
            return $httpService.$get(Obj);
        },
        addState: function (State) {
            var Obj = { postData: State, url: '/State/CreateState', successToast: 'State added successfully', errorToast: 'Error while adding new State' };
            return $httpService.$post(Obj);
        },
        editState: function (State) {
            var Obj = { postData: State, url: '/State/UpdateState', successToast: 'State edited successfully', errorToast: 'Error while editing existing State' };
            return $httpService.$put(Obj);
        },
        removeState: function (ID) {
            var Obj = { url: '/State/DeleteState/' + ID, successToast: 'State removed successfully', errorToast: 'Error while removing existing State' };
            return $httpService.$delete(Obj);
        },
        getAllCountries: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/Country/GetAllCountries' };
            return $httpService.$get(Obj);
        },
    };
})

.controller('StateCtrl_Add', function ($uibModalInstance, $scope, $rootScope, StateService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    var isDisplayAll = true;
    getAllCountries(isDisplayAll);

    function getAllCountries(isDisplayAll) {
        $rootScope.IsLoading = true;
        StateService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.addState = function (State) {

        if (State.StateName == null || State.StateName == '')
            return $rootScope.showNotify('State name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        StateService.addState(State).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('StateCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, StateService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    var isDisplayAll = true;
    getAllCountries(isDisplayAll);

    function getAllCountries(isDisplayAll) {
        $rootScope.IsLoading = true;
        StateService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;
        });
    };


    getStateByID($scope.StateID);

    function getStateByID(StateID) {
        $rootScope.IsLoading = true;
        StateService.getStateByID(StateID).then(function (response) {
            $scope.State = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editState = function (State) {

        if (State.StateName == null || State.StateName == '')
            return $rootScope.showNotify('State name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        StateService.editState(State).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('StateCtrl', function ($scope, $state, $rootScope, $dataTableService, StateService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.StateID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/State/GetStates',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.StateID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteState(' + data.StateID + ')">' +
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
        { Column: 'StateID', Title: 'ID', containsHtml: false },
        { Column: 'StateName', Title: 'State Name', containsHtml: false },
         { Column: 'StateCode', Title: 'State Code', containsHtml: false },
        { Column: 'CountryName', Title: 'Country Name', containsHtml: false },
       { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.State = {};

    function clearAll() {
        $scope.State.StateName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (StateID) {
        $scope.StateID = StateID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editStateModal.html',
            controller: 'StateCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addStateModal.html',
            controller: 'StateCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteState = function (StateID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            StateService.removeState(StateID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('stateForm', function () {

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
                    statename: {
                        validators: {
                            notEmpty: {
                                message: 'The state name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The state name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The state name can only consist of alphabets, space.'
                            }
                        }
                    },
                    statecode: {
                        validators: {
                            notEmpty: {
                                message: 'The state code is required'
                            },
                            stringLength: {
                                max: 2,
                                message: 'The short name must be less than 2 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z]*$/,
                                message: 'The short name can only consist of alphabets.'
                            }
                        }
                    },
                    country: {
                        validators: {
                            notEmpty: {
                                message: 'The country name is required'
                            },
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