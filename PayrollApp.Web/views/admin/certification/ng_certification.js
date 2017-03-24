'use strict';

angular.module('app.certification', []).config(function () { })

.factory('CertificationService', function ($httpService) {
    return {
        getCertificationByID: function (CertificationId) {
            var Obj = { params: { CertificationID: CertificationId }, url: '/Certification/GetCertificationByID' };
            return $httpService.$get(Obj);
        },
        addCertification: function (Certification) {
            var Obj = { postData: Certification, url: '/Certification/CreateCertification', successToast: 'Certification added successfully', errorToast: 'Error while adding new Certification' };
            return $httpService.$post(Obj);
        },
        editCertification: function (Certification) {
            var Obj = { postData: Certification, url: '/Certification/UpdateCertification', successToast: 'Certification edited successfully', errorToast: 'Error while editing existing Certification' };
            return $httpService.$put(Obj);
        },
        removeCertification: function (ID) {
            var Obj = { url: '/Certification/DeleteCertification/' + ID, successToast: 'Certification removed successfully', errorToast: 'Error while removing existing Certification' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('CertificationCtrl_Add', function ($uibModalInstance, $scope, $rootScope, CertificationService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addCertification = function (Certification) {

        if (Certification.CertificationName == null || Certification.CertificationName == '')
            return $rootScope.showNotify('Certification name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        CertificationService.addCertification(Certification).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('CertificationCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, CertificationService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getCertificationByID($scope.CertificationID);

    function getCertificationByID(CertificationID) {
        $rootScope.IsLoading = true;
        CertificationService.getCertificationByID(CertificationID).then(function (response) {
            $scope.Certification = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editCertification = function (Certification) {

        if (Certification.CertificationName == null || Certification.CertificationName == '')
            return $rootScope.showNotify('Certification name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        CertificationService.editCertification(Certification).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('CertificationCtrl', function ($scope, $state, $rootScope, $dataTableService, CertificationService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.CertificationID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/Certification/GetCertifications',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.CertificationID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteCertification(' + data.CertificationID + ')">' +
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
        { Column: 'CertificationID', Title: 'ID', containsHtml: false },
        { Column: 'CertificationName', Title: 'Certification Name', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.Certification = {};

    function clearAll() {
        $scope.Certification.CertificationName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (CertificationID) {
        $scope.CertificationID = CertificationID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editCertificationModal.html',
            controller: 'CertificationCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addCertificationModal.html',
            controller: 'CertificationCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteCertification = function (CertificationID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            CertificationService.removeCertification(CertificationID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('certificationForm', function () {

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
                    certificationname: {
                        validators: {
                            notEmpty: {
                                message: 'The certification name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The certification name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The certification name can only consist of alphabets, space.'
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