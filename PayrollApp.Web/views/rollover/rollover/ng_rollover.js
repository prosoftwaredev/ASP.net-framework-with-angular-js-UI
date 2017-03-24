'use strict';

angular.module('app.rollover', []).config(function () { })

.factory('RolloverService', function ($httpService) {
    return {
        getRollovers: function (search) {
            var Obj = { postData: search, url: '/Order/GetRollovers' };
            return $httpService.$postSearch(Obj);
        },
        changeStatusOnRollover: function (RequestData) {
            var Obj = { postData: RequestData, url: '/Order/ChangeStatusOnRollover' };
            return $httpService.$postSearch(Obj);
        },
        yelloButtonClick: function (RequestData) {
            var Obj = { postData: RequestData, url: '/Order/YelloButtonClick' };
            return $httpService.$postSearch(Obj);
        },
        blackButtonClick: function (RequestData) {
            var Obj = { postData: RequestData, url: '/Order/BlackButtonClick' };
            return $httpService.$postSearch(Obj);
        },
        makeRollover: function (RequestData) {
            var Obj = { postData: RequestData, url: '/Order/MakeRollover' };
            return $httpService.$postSearch(Obj);
        },
    };
})

.controller('RolloverCtrl', function ($scope, $state, $rootScope, RolloverService, $window, $compile, $filter) {

    var isDisplayAll = false;

    $scope.PageSizes = [];
    $scope.PageSizes.push({ PageSize: 10 }, { PageSize: 25 }, { PageSize: 50 }, { PageSize: 100 });

    $scope.Rollovers = [];
    $scope.totalRollovers = 0;
    $scope.IsItemsFound = false;
    $scope.pageSize = 10;
    $scope.Search = {}; 
    $scope.dispatchDate = new Date();

    $scope.$watch('dispatchDate', function (newValue) {
        $scope.dispatchDate = $filter('date')(newValue, 'MM/dd/yyyy');

        console.log(newValue);
        getRollovers($rootScope.pageNumber);
        //if (!$scope.dispatchDate.indexOf("T") >= 0) {
        //    getRollovers($rootScope.pageNumber);
        //}       
    });

    var Obj = {
        orders: [[1, 'asc']],
        pageSize: $scope.pageSize
    };

  

    $scope.refresh = function () {
        $route.reload();
    };

    $scope.pagination = {
        current: $rootScope.pageNumber
    };

    $scope.pageChanged = function (newPage) {
        getRollovers(newPage);
    };

    $scope.getRollovers = function () {
        getRollovers(1);
    };

    function getRollovers(pageNumber) {

        $rootScope.pageNumber = pageNumber;

        $scope.Search.pageNumber = $rootScope.pageNumber;
        $scope.Search.pageSize = $scope.pageSize;
        $scope.Search.dispatchDate = $scope.dispatchDate;

        $rootScope.IsLoading = true;
        RolloverService.getRollovers($scope.Search)
            .then(function (result) {
                $scope.Rollovers = result.data.Items;
                $scope.totalRollovers = result.data.Count;
                $rootScope.IsLoading = false;

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


    $scope.changeStatusOnRollover = function (rollover, Date) {

        rollover.DayStatusData.DayStatusList.forEach(function (i) {
            if (i.Date == Date)
                i.Click = true;
            else
                i.Click = false;
        });


        var RequestData = {
            OrderTimeslipID : rollover.OrderTimeslipID,
            DayStatusList: rollover.DayStatusData.DayStatusList,
        };

        console.log(RequestData.DayStatusList);

        $rootScope.IsLoading = true;
        RolloverService.changeStatusOnRollover(RequestData).then(function (response) {
            $scope.TimeslipStatus = response.data;
            getRollovers($rootScope.pageNumber);
            $rootScope.IsLoading = false;
        });

    };


    $scope.yelloButtonClick = function (rollover, Date) {

        var RequestData = {
            OrderTimeslipID: rollover.OrderTimeslipID,
            DayStatusList: rollover.DayStatusData.DayStatusList,
            DispatchDate: $scope.dispatchDate
        };

        $rootScope.IsLoading = true;
        RolloverService.yelloButtonClick(RequestData).then(function (response) {
            getRollovers($rootScope.pageNumber);
            $rootScope.IsLoading = false;
        });

    };

    $scope.blackButtonClick = function (rollover, Date) {

        var RequestData = {
            OrderTimeslipID: rollover.OrderTimeslipID,
            DayStatusList: rollover.DayStatusData.DayStatusList,
            DispatchDate: $scope.dispatchDate
        };

        $rootScope.IsLoading = true;
        RolloverService.blackButtonClick(RequestData).then(function (response) {
            getRollovers($rootScope.pageNumber);
            $rootScope.IsLoading = false;
        });

    };

    $scope.makeRollover = function () {

        var RequestData = {
            DispatchDate: $scope.dispatchDate
        };

        $rootScope.IsLoading = true;
        RolloverService.makeRollover(RequestData).then(function (response) {
            getRollovers($rootScope.pageNumber);
            $rootScope.IsLoading = false;
        });
    };
})