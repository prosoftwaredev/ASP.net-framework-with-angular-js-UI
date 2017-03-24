'use strict';

angular.module('app.country', []).config(function () { })

.factory('CountryService', function ($httpService) {
    return {
        getCountryByID: function (CountryId) {
            var Obj = { params: { CountryID: CountryId }, url: '/Country/GetCountryByID' };
            return $httpService.$get(Obj);
        },
        addCountry: function (Country) {
            var Obj = { postData: Country, url: '/Country/CreateCountry', successToast: 'Country added successfully', errorToast: 'Error while adding new Country' };
            return $httpService.$post(Obj);
        },
        editCountry: function (Country) {
            var Obj = { postData: Country, url: '/Country/UpdateCountry', successToast: 'Country edited successfully', errorToast: 'Error while editing existing Country' };
            return $httpService.$put(Obj);
        },
        removeCountry: function (ID) {
            var Obj = { url: '/Country/DeleteCountry/' + ID, successToast: 'Country removed successfully', errorToast: 'Error while removing existing Country' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('CountryCtrl_Add', function ($uibModalInstance, $scope, $rootScope, CountryService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addCountry = function (Country) {

        if (Country.CountryName == null || Country.CountryName == '')
            return $rootScope.showNotify('Country name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        CountryService.addCountry(Country).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('CountryCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, CountryService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getCountryByID($scope.CountryID);

    function getCountryByID(CountryID) {
        $rootScope.IsLoading = true;
        CountryService.getCountryByID(CountryID).then(function (response) {
            $scope.Country = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editCountry = function (Country) {

        if (Country.CountryName == null || Country.CountryName == '')
            return $rootScope.showNotify('Country name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        CountryService.editCountry(Country).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('CountryCtrl', function ($scope, $state, $rootScope, $dataTableService, CountryService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.CountryID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/Country/GetCountrys',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.CountryID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteCountry(' + data.CountryID + ')">' +
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
        { Column: 'CountryID', Title: 'ID', containsHtml: false },
        { Column: 'CountryName', Title: 'Country Name', containsHtml: false },
        { Column: 'CountryCode', Title: 'Country Code', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.Country = {};

    function clearAll() {
        $scope.Country.CountryName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (CountryID) {
        $scope.CountryID = CountryID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editCountryModal.html',
            controller: 'CountryCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addCountryModal.html',
            controller: 'CountryCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteCountry = function (CountryID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            CountryService.removeCountry(CountryID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('countryForm', function () {

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
                    countryname: {
                        validators: {
                            notEmpty: {
                                message: 'The country name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The country name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The country name can only consist of alphabets, space.'
                            }
                        }
                    },
                    countrycode: {
                        validators: {
                            notEmpty: {
                                message: 'The country code is required'
                            },
                            stringLength: {
                                max: 2,
                                message: 'The country name must be less than 2 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z]*$/,
                                message: 'The country name can only consist of alphabets.'
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