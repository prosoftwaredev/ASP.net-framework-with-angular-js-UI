'use strict';

angular.module('app.skill', []).config(function () { })

.factory('SkillService', function ($httpService) { 
    return {
        getSkillByID: function (SkillId) {
            var Obj = { params: { SkillID: SkillId }, url: '/Skill/GetSkillByID' };
            return $httpService.$get(Obj);
        },
        addSkill: function (Skill) {
            var Obj = { postData: Skill, url: '/Skill/CreateSkill', successToast: 'Skill added successfully', errorToast: 'Error while adding new Skill' };
            return $httpService.$post(Obj);
        },
        editSkill: function (Skill) {
            var Obj = { postData: Skill, url: '/Skill/UpdateSkill', successToast: 'Skill edited successfully', errorToast: 'Error while editing existing Skill' };
            return $httpService.$put(Obj);
        },
        removeSkill: function (ID) {
            var Obj = { url: '/Skill/DeleteSkill/' + ID, successToast: 'Skill removed successfully', errorToast: 'Error while removing existing Skill' };
            return $httpService.$delete(Obj);
        },
    };
})

.controller('SkillCtrl_Add', function ($uibModalInstance, $scope, $rootScope, SkillService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addSkill = function (Skill) {

        if (Skill.SkillName == null || Skill.SkillName == '')
            return $rootScope.showNotify('Skill name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        SkillService.addSkill(Skill).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('SkillCtrl_Edit', function ($uibModalInstance, $scope, $rootScope, SkillService) {

    $scope.ID = null;

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
    };

    getSkillByID($scope.SkillID);

    function getSkillByID(SkillID) {
        $rootScope.IsLoading = true;
        SkillService.getSkillByID(SkillID).then(function (response) {
            $scope.Skill = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editSkill = function (Skill) {

        if (Skill.SkillName == null || Skill.SkillName == '')
            return $rootScope.showNotify('Skill name field cannot be empty', 'Information', 'i');


        $rootScope.IsLoading = true;
        SkillService.editSkill(Skill).then(function () {
            $rootScope.IsLoading = false;
            $scope.dtInstance.rerender();
        });
    };
})

.controller('SkillCtrl', function ($scope, $state, $rootScope, $dataTableService, SkillService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $scope.OpenEditModal(aData.SkillID);
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/Skill/GetSkills',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" ng-click="OpenEditModal(' + data.SkillID + ')">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;' +
            '<a class="btn-cust" ng-click="deleteSkill(' + data.SkillID + ')">' +
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
        { Column: 'SkillID', Title: 'ID', containsHtml: false },
        { Column: 'SkillName', Title: 'Skill Name', containsHtml: false },
        { Column: 'Created', Title: 'Date', containsHtml: true, renderWith: renderDate, Width: 14 },
        { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 12 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.Skill = {};

    function clearAll() {
        $scope.Skill.SkillName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenEditModal = function (SkillID) {
        $scope.SkillID = SkillID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editSkillModal.html',
            controller: 'SkillCtrl_Edit',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addSkillModal.html',
            controller: 'SkillCtrl_Add',
            size: 'md',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteSkill = function (SkillID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            SkillService.removeSkill(SkillID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.directive('skillForm', function () {

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
                    skillname: {
                        validators: {
                            notEmpty: {
                                message: 'The skill name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The Skill name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The Skill name can only consist of alphabets, space.'
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