'use strict';

angular.module('app.city', []).config(function () { })

.factory('CityService', function ($httpService) {
    return {
        getCityByID: function (CityId) {
            var Obj = { params: { CityID: CityId }, url: '/City/GetCityByID' };
            return $httpService.$get(Obj);
        },
        addCity: function (City) {
            var Obj = { postData: City, url: '/City/CreateCity', successToast: 'City added successfully', errorToast: 'Error while adding new City' };
            return $httpService.$post(Obj);
        },
        editCity: function (City) {
            var Obj = { postData: City, url: '/City/UpdateCity', successToast: 'City edited successfully', errorToast: 'Error while editing existing City' };
            return $httpService.$put(Obj);
        },
        removeCity: function (ID) {
            var Obj = { url: '/City/DeleteCity/' + ID, successToast: 'City removed successfully', errorToast: 'Error while removing existing City' };
            return $httpService.$delete(Obj);
        },
        getAllCountries: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/Country/GetAllCountries' };
            return $httpService.$get(Obj);
        },
        getAllStates: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/State/GetAllStates' };
            return $httpService.$get(Obj);
        },

    };
})

.controller('CityCtrl_Add', function ($uibModalInstance, $scope, $rootScope, CityService, $filter) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    var isDisplayAll = true;
    getAllCountries(isDisplayAll);
    getAllStates(isDisplayAll);

    function getAllCountries(isDisplayAll) {
        $rootScope.IsLoading = true;
        CityService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;
        });
    };

    function getAllStates(isDisplayAll) {
        $rootScope.IsLoading = true;
        CityService.getAllStates(isDisplayAll).then(function (response) {
            $scope.States = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.addCity = function (City) {

        if (City.CityName == null || City.CityName == '')
            return $rootScope.showNotify('City name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        CityService.addCity(City).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };

    $scope.getStatesByCountryID = function (CountryID) {
        $scope.StatesForSelectList = $filter('filter')($scope.States, { CountryID: CountryID});
    };
})

.controller('CityCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, CityService, $filter) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    var isDisplayAll = true;
    getAllCountries(isDisplayAll);
    getAllStates(isDisplayAll);

    function getAllCountries(isDisplayAll) {
        $rootScope.IsLoading = true;
        CityService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;
        });
    };

    function getAllStates(isDisplayAll) {
        $rootScope.IsLoading = true;
        CityService.getAllStates(isDisplayAll).then(function (response) {
            $scope.States = response.data;
            $scope.StatesForSelectList = $scope.States;
            $rootScope.IsLoading = false;
        });
    };


    getCityByID($scope.CityID);

    function getCityByID(CityID) {
        $rootScope.IsLoading = true;
        CityService.getCityByID(CityID).then(function (response) {
            $scope.City = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editCity = function (City) {

        if (City.CityName == null || City.CityName == '')
            return $rootScope.showNotify('City name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        CityService.editCity(City).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };

    $scope.getStatesByCountryID = function (CountryID) {
        $scope.StatesForSelectList = $filter('filter')($scope.States, { CountryID: CountryID });
    };
})

.controller('CityCtrl', function ($scope, $state, $rootScope, $dataTableService, CityService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.CityID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/City/GetCitys',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.CityID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteCity(' + data.CityID + ')">' +
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
        { Column: 'CityID', Title: 'ID', containsHtml: false },
        { Column: 'CityName', Title: 'City Name', containsHtml: false },
        { Column: 'StateName', Title: 'State Name', containsHtml: false },
        { Column: 'CountryName', Title: 'Country Name', containsHtml: false },
         { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.City = {};

    function clearAll() {
        $scope.City.CityName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (CityID) {
        $scope.CityID = CityID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editCityModal.html',
            controller: 'CityCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addCityModal.html',
            controller: 'CityCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteCity = function (CityID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            CityService.removeCity(CityID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('cityForm', function () {

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
                    cityname: {
                        validators: {
                            notEmpty: {
                                message: 'The city name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The city name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The city name can only consist of alphabets, space.'
                            }
                        }
                    },
                    country: {
                        validators: {
                            notEmpty: {
                                message: 'The country name is required'
                            },
                        }
                    },
                    state: {
                        validators: {
                            notEmpty: {
                                message: 'The state name is required'
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