'use strict';

angular.module('app.image', []).config(function () { })


.factory('ImageService', function ($httpService) {
    return {
        getImageByID: function (ImageId) {
            var Obj = { params: { ImageID: ImageId }, url: '/Image/GetImageByID' };
            return $httpService.$get(Obj);
        },
    };
})

.controller('ImageCtrl', function ($scope, $state, $rootScope, $dataTableService, ImageService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.ImageID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/Image/GetImages',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.ImageID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteImage(' + data.ImageID + ')">' +
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
        { Column: 'ImageID', Title: 'ID', containsHtml: false },
        { Column: 'ImageName', Title: 'Image Name', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.Image = {};

    function clearAll() {
        $scope.Image.ImageName = null;
    };


})
