'use strict';

angular.module('app.timeslip', []).config(function () { })

.factory('TimeslipService', function ($httpService) {
    return {

        getOrderTimeslips: function (search) {
            var Obj = { postData: search, url: '/Timeslip/GetOrderTimeslips' };
            return $httpService.$postSearch(Obj);
        },
        getAllCustomers: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/Customer/GetAllCustomers' };
            return $httpService.$get(Obj);
        },
        getAllEmployeesByCustomerID: function (isDisplayAll, CustomerID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, CustomerID: CustomerID }, url: '/Employee/GetAllEmployeesByCustomerID' };
            return $httpService.$get(Obj);
        },
    };
})

.controller('TimeslipCtrl', function ($scope, $state, $rootScope, TimeslipService, $window, $compile, $filter, $uibModal) {

    var isDisplayAll = false;
    $rootScope.CustSiteID = 0;

    var BillState = null;

    getBillState();   

    function getBillState() {

        switch ($state.current.name)
        {
            case 'app.timeslip.all':
                BillState = -100;
                break;

            case 'app.timeslip.active':
                BillState = -2;
                break;

            case 'app.timeslip.dispatch':
                BillState = -1;
                break;

            case 'app.timeslip.missing':
                BillState = -100;   //value set after overdue known
                break;

            case 'app.timeslip.completed':
                BillState = 0;
                break;
        }
    };


    $scope.PageSizes = [];
    $scope.PageSizes.push({ PageSize: 10 }, { PageSize: 25 }, { PageSize: 50 }, { PageSize: 100 });

    $scope.OrderTimeslips = [];
    $scope.totalOrderTimeslips = 0;
    $scope.IsItemsFound = false;
    $scope.pageSize = 10;
    $scope.Search = {};

    function getAllCustomers() {
        $rootScope.IsLoading = true;
        TimeslipService.getAllCustomers(isDisplayAll).then(function (response) {
            $scope.Customers = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllEmployeesByCustomerID =  function (CustomerID) {
        if (CustomerID != null) {
            $rootScope.IsLoading = true;
            TimeslipService.getAllEmployeesByCustomerID(isDisplayAll, CustomerID).then(function (response) {
                $scope.Employees = response.data;
                $rootScope.IsLoading = false;
                getOrderTimeslips(1);
            });
        }
    };

    $scope.loading = true;
    getAllCustomers();
    getOrderTimeslips($rootScope.pageNumber);
    $scope.loading = false;


    $scope.refresh = function () {
        $route.reload();
    };

    $scope.pagination = {
        current: $rootScope.pageNumber
    };

    $scope.pageChanged = function (newPage) {
        getOrderTimeslips(newPage);
    };

    $scope.getOrderTimeslips = function () {
        getOrderTimeslips(1);
    };

    function getOrderTimeslips(pageNumber) {

        $rootScope.pageNumber = pageNumber;

        $scope.Search.pageNumber = $rootScope.pageNumber;
        $scope.Search.pageSize = $scope.pageSize;
        $scope.Search.BillState = BillState;

        $rootScope.IsLoading = true;
        TimeslipService.getOrderTimeslips($scope.Search)
            .then(function (result) {
                $scope.OrderTimeslips = result.data.Items;
                $scope.totalOrderTimeslips = result.data.Count;
                $rootScope.IsLoading = false;

                console.log($scope.OrderTimeslips);

                if (result.data.Items.length == 0)
                    $scope.IsItemsFound = true;
                else
                    $scope.IsItemsFound = false;
            });
    };

    $scope.predicate = 'Name';
    $scope.reverse = false;
    $scope.order = function (predicate) {
        $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
        $scope.predicate = predicate;
    };
})