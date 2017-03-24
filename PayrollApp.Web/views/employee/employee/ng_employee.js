'use strict';

angular.module('app.employee', []).config(function () { })

.constant('dualList', {
    settings: {
        title: 'Demo: Recent World Cup Winners',
        filterPlaceHolder: 'Start typing to filter the lists below.',
        labelAll: 'All Items',
        labelSelected: 'Selected Items',
        helpMessage: ' Click items to transfer them between fields.',
        forwardDirection: 1,
        backwordDirection: 0,
        orderProperty: 'name'
    }
})

.factory('EmployeeService', function ($httpService) {
    return {
        getEmployees: function (search) {
            var Obj = { postData: search, url: '/Employee/GetEmployees' };
            return $httpService.$postSearch(Obj);
        },

        getEmployeeByID: function (EmployeeId) {
            var Obj = { params: { EmployeeID: EmployeeId }, url: '/Employee/GetEmployeeByID' };
            return $httpService.$get(Obj);
        },
        addEmployee: function (Employee) {
            var Obj = { postData: Employee, url: '/Employee/CreateEmployee', successToast: 'Employee added successfully', errorToast: 'Error while adding new Employee' };
            return $httpService.$post(Obj);
        },
        editEmployeeAndPayroll: function (Employee) {
            var Obj = { postData: Employee, url: '/Employee/UpdateEmployeeAndPayroll', successToast: 'Employee & Payroll Info edited successfully', errorToast: 'Error while editing existing Employee & Payroll Info' };
            return $httpService.$put(Obj);
        },
        editContact: function (Employee) {
            var Obj = { postData: Employee, url: '/Employee/UpdateContact', successToast: 'Contact Info edited successfully', errorToast: 'Error while editing existing Contact Info' };
            return $httpService.$put(Obj);
        },
        editEmployee: function (Employee) {
            var Obj = { postData: Employee, url: '/Employee/UpdateEmployee', successToast: 'Employee edited successfully', errorToast: 'Error while editing existing Employee' };
            return $httpService.$put(Obj);
        },
        editEmployeeIsEnable: function (Employee) {
            var Obj = { postData: Employee, url: '/Employee/UpdateEmployeeIsEnable', successToast: 'Employee edited successfully', errorToast: 'Error while editing existing Employee' };
            return $httpService.$put(Obj);
        },
        editEmployeeIsNeverUse: function (Employee) {
            var Obj = { postData: Employee, url: '/Employee/UpdateEmployeeIsNeverUse', successToast: 'Employee edited successfully', errorToast: 'Error while editing existing Employee' };
            return $httpService.$put(Obj);
        },

        removeEmployee: function (ID) {
            var Obj = { url: '/Employee/DeleteEmployee/' + ID, successToast: 'Employee removed successfully', errorToast: 'Error while removing existing Employee' };
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
        getAllStatesByCountryID: function (isDisplayAll, CountryID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, CountryID: CountryID }, url: '/State/GetAllStatesByCountryID' };
            return $httpService.$get(Obj);
        },
        getAllCities: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/City/GetAllCities' };
            return $httpService.$get(Obj);
        },
        getAllCitiesByStateID: function (isDisplayAll, StateID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, StateID: StateID }, url: '/City/GetAllCitiesByStateID' };
            return $httpService.$get(Obj);
        },
        getAllEmployeeTypes: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/EmployeeType/GetAllEmployeeTypes' };
            return $httpService.$get(Obj);
        },
        getAllPayFrequencies: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/PayFrequency/GetAllPayFrequencies' };
            return $httpService.$get(Obj);
        },

        addEmployeeNote: function (EmployeeNote) {
            var Obj = { postData: EmployeeNote, url: '/Employee/CreateEmployeeNote', successToast: 'Note added successfully', errorToast: 'Error while adding new employee note' };
            return $httpService.$post(Obj);
        },
        getAllEmployeeNotesByEmployeeID: function (isDisplayAll, EmployeeID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, EmployeeID: EmployeeID }, url: '/Employee/GetAllEmployeeNotesByEmployeeID' };
            return $httpService.$get(Obj);
        },
        removeEmployeeNote: function (ID) {
            var Obj = { url: '/Employee/DeleteEmployeeNote/' + ID, successToast: 'Note removed successfully', errorToast: 'Error while removing existing note' };
            return $httpService.$delete(Obj);
        },

        getAllLabourClassifications: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/LabourClassification/GetAllLabourClassifications' };
            return $httpService.$get(Obj);
        },
        addEmployeeLabourClassification: function (EmployeeLabourClassification) {
            var Obj = { postData: EmployeeLabourClassification, url: '/Employee/CreateEmployeeLabourClassification', successToast: 'Labour classification added successfully', errorToast: 'Error while adding new employee labour classification' };
            return $httpService.$post(Obj);
        },
        getAllEmployeeLabourClassificationsByEmployeeID: function (isDisplayAll, EmployeeID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, EmployeeID: EmployeeID }, url: '/Employee/GetAllEmployeeLabourClassificationsByEmployeeID' };
            return $httpService.$get(Obj);
        },
        removeEmployeeLabourClassification: function (EmployeeLabourClassification) {
            var Obj = { postData: EmployeeLabourClassification, url: '/Employee/DeleteEmployeeLabourClassification', successToast: 'Labour classification removed successfully', errorToast: 'Error while removing existing labour classification' };
            return $httpService.$postDelete(Obj);
        },
        removeEmpLabourClassification: function (ID) {
            var Obj = { url: '/Employee/DeleteEmpLabourClassification/' + ID, successToast: 'Labour classification removed successfully', errorToast: 'Error while removing existing existing labour classification' };
            return $httpService.$delete(Obj);
        },
        editEmployeeLabourClassification: function (EmployeeLabourClassification) {
            var Obj = { postData: EmployeeLabourClassification, url: '/Employee/UpdateEmployeeLabourClassification', successToast: 'Labour classification edited successfully', errorToast: 'Error while editing existing labour classification' };
            return $httpService.$put(Obj);
        },

        getAllCustomers: function (isDisplayAll, EmployeeID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, EmployeeID: EmployeeID }, url: '/Customer/GetAllCustomers' };
            return $httpService.$get(Obj);
        },
        addEmployeeBlacklist: function (EmployeeBlacklist) {
            var Obj = { postData: EmployeeBlacklist, url: '/Employee/CreateEmployeeBlacklist', successToast: 'Blacklist added successfully', errorToast: 'Error while adding new employee blacklist' };
            return $httpService.$post(Obj);
        },
        getAllEmployeeBlacklistsByEmployeeID: function (isDisplayAll, EmployeeID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, EmployeeID: EmployeeID }, url: '/Employee/GetAllEmployeeBlacklistsByEmployeeID' };
            return $httpService.$get(Obj);
        },
        removeEmployeeBlacklist: function (EmployeeBlacklist) {
            var Obj = { postData: EmployeeBlacklist, url: '/Employee/DeleteEmployeeBlacklist', successToast: 'Blacklist removed successfully', errorToast: 'Error while removing existing blacklist' };
            return $httpService.$postDelete(Obj);
        },
        removeEmpBlacklist: function (ID) {
            var Obj = { url: '/Employee/DeleteEmpBlacklist/' + ID, successToast: 'Blacklist removed successfully', errorToast: 'Error while removing existing existing blacklist' };
            return $httpService.$delete(Obj);
        },

        getAllCertifications: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/Certification/GetAllCertifications' };
            return $httpService.$get(Obj);
        },
        addEmployeeCertification: function (EmployeeCertification) {
            var Obj = { postData: EmployeeCertification, url: '/Employee/CreateEmployeeCertification', successToast: 'Certification added successfully', errorToast: 'Error while adding new employee certification' };
            return $httpService.$post(Obj);
        },
        getAllEmployeeCertificationsByEmployeeID: function (isDisplayAll, EmployeeID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, EmployeeID: EmployeeID }, url: '/Employee/GetAllEmployeeCertificationsByEmployeeID' };
            return $httpService.$get(Obj);
        },
        removeEmployeeCertification: function (EmployeeCertification) {
            var Obj = { postData: EmployeeCertification, url: '/Employee/DeleteEmployeeCertification', successToast: 'Certification removed successfully', errorToast: 'Error while removing existing certification' };
            return $httpService.$postDelete(Obj);
        },
        removeEmpCertification: function (ID) {
            var Obj = { url: '/Employee/DeleteEmpCertification/' + ID, successToast: 'Certification removed successfully', errorToast: 'Error while removing existing existing certification' };
            return $httpService.$delete(Obj);
        },

        getAllSkills: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll}, url: '/Skill/GetAllSkills' };
            return $httpService.$get(Obj);
        },
        addEmployeeSkill: function (EmployeeSkill) {
            var Obj = { postData: EmployeeSkill, url: '/Employee/CreateEmployeeSkill', successToast: 'Skill added successfully', errorToast: 'Error while adding new employee skill' };
            return $httpService.$post(Obj);
        },
        getAllEmployeeSkillsByEmployeeID: function (isDisplayAll, EmployeeID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, EmployeeID: EmployeeID }, url: '/Employee/GetAllEmployeeSkillsByEmployeeID' };
            return $httpService.$get(Obj);
        },
        removeEmployeeSkill: function (EmployeeSkill) {
            var Obj = { postData: EmployeeSkill, url: '/Employee/DeleteEmployeeSkill', successToast: 'Skill removed successfully', errorToast: 'Error while removing existing skill' };
            return $httpService.$postDelete(Obj);
        },
        removeEmpSkill: function (ID) {
            var Obj = { url: '/Employee/DeleteEmpSkill/' + ID, successToast: 'Skill removed successfully', errorToast: 'Error while removing existing existing skill' };
            return $httpService.$delete(Obj);
        },

        getAllTitles: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/Title/GetAllTitles' };
            return $httpService.$get(Obj);
        },

        uploadCertificationImage: function (EmployeeCertification, $file) {
            var Obj = { postData: EmployeeCertification, postFile: $file, url: '/Employee/UpdateCertificationImage', successToast: 'Image uploaded successfully', errorToast: 'Error while uploading image' };
            return $httpService.$postMultiPart(Obj);
        },
    };
})

.controller('EmployeeCtrl_Add', function ($scope, $rootScope, $state, EmployeeService, $window, $filter, $compile, CFG, dualList, $uibModal) {


    var isDisplayAll = false;
    $scope.Employee = {};

    //----------------------------Personal Info---------------------------

    $scope.getAllTitles = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllTitles(isDisplayAll).then(function (response) {
            $scope.Titles = response.data;
            //$scope.SelectedTitles = $scope.Titles;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllTitles();

    $scope.getAllTitleByGender = function (gender) {
        $scope.SelectedTitles = $filter('filter')($scope.Titles, { Gender: gender }, true);
    };

    $scope.getAllEmployeeTypes = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllEmployeeTypes(isDisplayAll).then(function (response) {
            $scope.EmployeeTypes = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.addEmployee = function (Employee) {

        if (Employee.FirstName == null || Employee.FirstName == '')
            return $rootScope.showNotify('First name field cannot be left empty', 'Information', 'i');

        if (Employee.LastName == null || Employee.LastName == '')
            return $rootScope.showNotify('Last name field cannot be left empty', 'Information', 'i');

        if (Employee.SIN == null || Employee.SIN == '')
            return $rootScope.showNotify('SIN # field cannot be left empty', 'Information', 'i');

        if (Employee.Gender == null || Employee.Gender == '')
            return $rootScope.showNotify('Gender field cannot be left empty', 'Information', 'i');

        if (Employee.DOB == null || Employee.DOB == '')
            return $rootScope.showNotify('DOB field cannot be left empty', 'Information', 'i');

        if (Employee.Phone == null || Employee.Phone == '')
            return $rootScope.showNotify('Phone field cannot be left empty', 'Information', 'i');

        if (Employee.Address1 == null || Employee.Address1 == '')
            return $rootScope.showNotify('Address1 field cannot be left empty', 'Information', 'i');

        if (Employee.StateID == null || Employee.StateID == '')
            return $rootScope.showNotify('State field cannot be left empty', 'Information', 'i');

        if (Employee.CityID == null || Employee.CityID == '')
            return $rootScope.showNotify('City field cannot be left empty', 'Information', 'i');

        if (Employee.PostalCode == null || Employee.PostalCode == '')
            return $rootScope.showNotify('Postal code field cannot be left empty', 'Information', 'i');

        Employee.OTPerDay = 8;
        Employee.OTPerWeek = 40;

        $rootScope.IsLoading = true;
        EmployeeService.addEmployee(Employee).then(function (response) {
            $scope.EmployeeID = response.data;
            $rootScope.IsLoading = false;
            $state.go('app.employee.employee.edit', { id: $scope.EmployeeID });
        });
    };

    $scope.updatePrintName = function () {
        $scope.Employee.PrintName = $scope.Employee.FirstName + ' ' + $scope.Employee.MiddleName + ' ' + $scope.Employee.LastName;
    };

    $scope.getAllCountries = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;

            $scope.Employee.CountryID = CFG.settings.mainCountry;
            $scope.Employee.StateID = CFG.settings.mainState;
            $scope.Employee.CityID = CFG.settings.mainCity;

            $scope.getStatesByCountryID(CFG.settings.mainCountry);
            $scope.getCitiesByStateID(CFG.settings.mainState);
        });
    };

    $scope.getAllCountries();


    $scope.getStatesByCountryID = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.States = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getCitiesByStateID = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.Cities = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getAllPayFrequencies = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllPayFrequencies(isDisplayAll).then(function (response) {
            $scope.PayFrequencies = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllPayFrequencies();

    $scope.getZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.Employee.CountryID }, true);
        var state = $filter('filter')($scope.States, { StateID: $scope.Employee.StateID }, true);
        var city = $filter('filter')($scope.Cities, { CityID: $scope.Employee.CityID }, true);

        var address = $scope.Employee.Address1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.Employee.PostalCode = i.long_name;
                        }
                    })
                } else {
                    $rootScope.IsLoading = false;
                    window.alert('No results found');
                }
            }
            else
                $rootScope.IsLoading = false;
        });
    };
})

.controller('EmployeeCtrl_Edit', function ($scope, $rootScope, $stateParams, EmployeeService, $window, $filter, dualList, CFG, $uibModal) {

    var isDisplayAll = false;

    //if ($rootScope.IsThroughAdd) {
    //    angular.element(document.querySelector('#s2')).addClass('active').siblings('.active').removeClass('active');
    //    angular.element(document.querySelector('#s22')).addClass('active').siblings('.active').removeClass('active');
    //}
    //else {
    //    angular.element(document.querySelector('#s1')).addClass('active').siblings('.active').removeClass('active');
    //    angular.element(document.querySelector('#s11')).addClass('active').siblings('.active').removeClass('active');

    //}

    $scope.EmployeeID = $stateParams.id;

    getEmployeeByID($scope.EmployeeID);

    function getEmployeeByID(EmployeeID) {
        $rootScope.IsLoading = true;
        EmployeeService.getEmployeeByID(EmployeeID).then(function (response) {
            $scope.Employee = response.data;
            $scope.cntID = $scope.Employee.CountryID;
            $scope.stID = $scope.Employee.StateID;
            $scope.ctID = $scope.Employee.CityID;
            $scope.getAllCountries();
            $scope.getAllPayFrequencies();
            $scope.getAllLabourClassifications();
            $scope.getAllCertifications();
            $scope.getAllSkills();
            $scope.getAllCustomers();
            $scope.getNotesByEmployeeID()
            $rootScope.IsLoading = false;
        });
    };

    //----------------------------Personal Info---------------------------

    $scope.getAllTitles = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllTitles(isDisplayAll).then(function (response) {
            $scope.Titles = response.data;
            $scope.SelectedTitles = $scope.Titles;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllTitles();

    $scope.getAllTitleByGender = function (gender) {
        $scope.SelectedTitles = $filter('filter')($scope.Titles, { Gender: gender }, true);
    };

    getAllEmployeeTypes();

    function getAllEmployeeTypes() {
        $rootScope.IsLoading = true;
        EmployeeService.getAllEmployeeTypes(isDisplayAll).then(function (response) {
            $scope.EmployeeTypes = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editEmployee = function (Employee) {

        if (Employee.FirstName == null || Employee.FirstName == '')
            return $rootScope.showNotify('First name field cannot be left empty', 'Information', 'i');

        if (Employee.LastName == null || Employee.LastName == '')
            return $rootScope.showNotify('Last name field cannot be left empty', 'Information', 'i');

        if (Employee.SIN == null || Employee.SIN == '')
            return $rootScope.showNotify('SIN # field cannot be left empty', 'Information', 'i');

        if (Employee.Gender == null || Employee.Gender == '')
            return $rootScope.showNotify('Gender field cannot be left empty', 'Information', 'i');

        if (Employee.DOB == null || Employee.DOB == '')
            return $rootScope.showNotify('DOB field cannot be left empty', 'Information', 'i');

        if (Employee.Phone == null || Employee.Phone == '')
            return $rootScope.showNotify('Phone field cannot be left empty', 'Information', 'i');

        if (Employee.Address1 == null || Employee.Address1 == '')
            return $rootScope.showNotify('Address1 field cannot be left empty', 'Information', 'i');

        if (Employee.StateID == null || Employee.StateID == '')
            return $rootScope.showNotify('State field cannot be left empty', 'Information', 'i');

        if (Employee.CityID == null || Employee.CityID == '')
            return $rootScope.showNotify('City field cannot be left empty', 'Information', 'i');

        if (Employee.PostalCode == null || Employee.PostalCode == '')
            return $rootScope.showNotify('Postal code field cannot be left empty', 'Information', 'i');

        $rootScope.IsLoading = true;
        EmployeeService.editEmployee(Employee).then(function () {
            $rootScope.IsLoading = false;            
            $scope.getAllLabourClassifications();
        });
    };

    //----------------------------Employee & Payroll Info---------------------------

    $scope.getAllPayFrequencies = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllPayFrequencies(isDisplayAll).then(function (response) {
            $scope.PayFrequencies = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editEmployeeAndPayroll = function (Employee) {

        //if (Employee.SIN == null || Employee.SIN == '')
        //    return $rootScope.showNotify('SIN field cannot be empty', 'Information', 'i');


        Employee.EmployeeID = $scope.EmployeeID;
        $rootScope.IsLoading = true;
        EmployeeService.editEmployeeAndPayroll(Employee).then(function () {
            $rootScope.IsLoading = false;
            $scope.getAllLabourClassifications();
            //angular.element(document.querySelector('#s3')).addClass('active').siblings('.active').removeClass('active');
            //angular.element(document.querySelector('#s33')).addClass('active').siblings('.active').removeClass('active');
        });
    };

    //----------------------------Contact Info---------------------------

    $scope.getAllCountries = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;
            $scope.getStatesByCountryID($scope.cntID);
            $scope.getCitiesByStateID($scope.stID);
        });
    };

    $scope.getStatesByCountryID = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.States = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getCitiesByStateID = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.Cities = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.editContact = function (Employee) {

        if (Employee.EmailMain == null || Employee.EmailMain == '')
            return $rootScope.showNotify('Email field cannot be empty', 'Information', 'i');


        Employee.EmployeeID = $scope.EmployeeID;
        $rootScope.IsLoading = true;
        EmployeeService.editContact(Employee).then(function () {
            $rootScope.IsLoading = false;
        });
    };

    //----------------------------Notes Info---------------------------

    $scope.getNotesByEmployeeID = function () {
        getNotesByEmployeeID($scope.EmployeeID);
    };

    $scope.addEmployeeNote = function (EmployeeNote) {

        if (EmployeeNote.Note == null || EmployeeNote.Note == '')
            return $rootScope.showNotify('Note field cannot be empty', 'Information', 'i');

        EmployeeNote.EmployeeID = $scope.EmployeeID;
        $rootScope.IsLoading = true;
        EmployeeService.addEmployeeNote(EmployeeNote).then(function () {
            $scope.EmployeeNote.Note = null;
            $rootScope.IsLoading = false;
            getNotesByEmployeeID($scope.EmployeeID);
        });
    };

    function getNotesByEmployeeID(EmployeeID) {
        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllEmployeeNotesByEmployeeID(isDisplayAll, EmployeeID).then(function (response) {
                $scope.EmployeeNotes = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayNotes = true;
                else
                    $scope.isDisplayNotes = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.removeEmployeeNote = function (EmployeeNoteID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmployeeNote(EmployeeNoteID).then(function () {
                $rootScope.IsLoading = false;
                getNotesByEmployeeID($scope.EmployeeID);
            });
        }
    };

    //----------------------------Labour Classification Info---------------------------

    $scope.getAllLabourClassifications = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllLabourClassifications(isDisplayAll).then(function (response) {
            $scope.LabourClassifications = response.data;
            $scope.FilteredLabourClassifications = $scope.LabourClassifications;
            $rootScope.IsLoading = false;
            getLabourClassificationsByEmployeeID($scope.EmployeeID);
        });
    };

    function getLabourClassificationsByEmployeeID(EmployeeID) {
        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllEmployeeLabourClassificationsByEmployeeID(isDisplayAll, EmployeeID).then(function (response) {
                $scope.EmployeeLabourClassifications = response.data;
                $rootScope.IsLoading = false;

                if (response.data.length > 0)
                    $scope.isDisplayLabourClassifications = true;
                else
                    $scope.isDisplayLabourClassifications = false;

                $rootScope.IsLoading = false;

                $scope.LabourClassifications.forEach(function (i) {
                    i.IsChecked = false;
                    $scope.EmployeeLabourClassifications.forEach(function (j) {
                        if (i.LabourClassificationID == j.LabourClassificationID)
                            i.IsChecked = true;
                    });
                });
            });
        }
    };

    $scope.selectAllChecked = function (ParentCk) {
        $scope.FilteredLabourClassifications.forEach(function (j) {
            $scope.LabourClassificationChecked(ParentCk, j.LabourClassificationID);
        });
    };

    $scope.LabourClassificationChecked = function (IsChecked, LabourClassificationID) {
        if (IsChecked) {
            var EmployeeLabourClassification = {
                EmployeeID: $scope.EmployeeID,
                LabourClassificationID: LabourClassificationID,
                Rate: CFG.settings.defaultLabourRate
            };

            EmployeeService.addEmployeeLabourClassification(EmployeeLabourClassification).then(function (response) {
                getLabourClassificationsByEmployeeID($scope.EmployeeID);
            });
        }
        else {
            var EmployeeLabourClassification = {
                EmployeeID: $scope.EmployeeID,
                LabourClassificationID: LabourClassificationID
            };
            EmployeeService.removeEmployeeLabourClassification(EmployeeLabourClassification).then(function (response) {
                getLabourClassificationsByEmployeeID($scope.EmployeeID);
            });
        };
    };

    $scope.editEmployeeLabourClassification = function (EmployeeLabourClassificationID, Rate) {

        if (Rate == null || Rate == '')
            return $rootScope.showNotify('Rate field cannot be empty', 'Information', 'i');

        var EmployeeLabourClassification = {
            EmployeeLabourClassificationID: EmployeeLabourClassificationID,
            Rate: Rate
        };

        $rootScope.IsLoading = true;
        EmployeeService.editEmployeeLabourClassification(EmployeeLabourClassification).then(function () {
            $rootScope.IsLoading = false;
            $scope.tempEmployeeLabourClassificationID = 0;
        });
    };

    $scope.tempEmployeeLabourClassificationID = 0;

    $scope.editRow = function (EmployeeLabourClassificationID) {
        $scope.tempEmployeeLabourClassificationID = EmployeeLabourClassificationID;
    };

    $scope.removeEmployeeLabourClassification = function (EmployeeLabourClassificationID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmpLabourClassification(EmployeeLabourClassificationID).then(function () {
                $rootScope.IsLoading = false;
                getLabourClassificationsByEmployeeID($scope.EmployeeID);
            });
        }
    };

    $scope.getFilteredLabourClassifications = function (searchLabourClassification) {
        if (searchLabourClassification.length > 0) {
            $scope.FilteredLabourClassifications = $filter('filter')($scope.LabourClassifications, { LabourClassificationName: searchLabourClassification });
        }
        else {
            $scope.FilteredLabourClassifications = $scope.LabourClassifications;
        }
    };

    //---------------------------Blacklist Info---------------------------------

    $scope.blacklistListOptions = dualList.settings;

    $scope.getAllCustomers = function () {
            $rootScope.IsLoading = true;
            EmployeeService.getAllCustomers(isDisplayAll).then(function (response) {
                $scope.Customers = response.data;
                $scope.FilteredCustomers = $scope.Customers;
                $rootScope.IsLoading = false;
                getAllEmployeeBlacklistsByEmployeeID($scope.EmployeeID);
            });
    };

    function getAllEmployeeBlacklistsByEmployeeID(EmployeeID) {
        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllEmployeeBlacklistsByEmployeeID(isDisplayAll, EmployeeID).then(function (response) {
                $scope.EmployeeBlacklists = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayBlacklists = true;
                else
                    $scope.isDisplayBlacklists = false;

                $rootScope.IsLoading = false;

                $scope.Customers.forEach(function (i) {
                    i.IsChecked = false;
                    $scope.EmployeeBlacklists.forEach(function (j) {
                        if (i.CustomerID == j.CustomerID)
                            i.IsChecked = true;
                    });
                });
            });
        }
    };

    $scope.selectAllChecked = function (ParentCk) {
        $scope.FilteredCustomers.forEach(function (j) {
            $scope.CustomerChecked(ParentCk, j.CustomerID);
        });
    };

    $scope.CustomerChecked = function (IsChecked, CustomerID) {
        if (IsChecked) {
            var EmployeeBlacklist = {
                EmployeeID: $scope.EmployeeID,
                CustomerID: CustomerID
            };

            EmployeeService.addEmployeeBlacklist(EmployeeBlacklist).then(function (response) {
                getAllEmployeeBlacklistsByEmployeeID($scope.EmployeeID);
            });
        }
        else {
            var EmployeeBlacklist = {
                EmployeeID: $scope.EmployeeID,
                CustomerID: CustomerID
            };

            EmployeeService.removeEmployeeBlacklist(EmployeeBlacklist).then(function (response) {
                getAllEmployeeBlacklistsByEmployeeID($scope.EmployeeID);
            });
        };
    };

    $scope.removeEmployeeBlacklist = function (EmployeeBlacklistID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmpBlacklist(EmployeeBlacklistID).then(function () {
                $rootScope.IsLoading = false;
                getAllEmployeeBlacklistsByEmployeeID($scope.EmployeeID);
            });
        }
    };

    $scope.getFilteredCustomers = function (searchCustomer) {
        if (searchCustomer.length > 0) {
            $scope.FilteredCustomers = $filter('filter')($scope.Customers, { CustomerName: searchCustomer });
        }
        else {
            $scope.FilteredCustomers = $scope.Customers;
        }
    };

    //---------------------------Skill Info---------------------------------

    $scope.getAllSkills = function () {
            $rootScope.IsLoading = true;
            EmployeeService.getAllSkills(isDisplayAll).then(function (response) {
                $scope.Skills = response.data;
                $scope.FilteredSkills = $scope.Skills;
                $rootScope.IsLoading = false;
                getAllEmployeeSkillsByEmployeeID($scope.EmployeeID);
            });
    };

    function getAllEmployeeSkillsByEmployeeID(EmployeeID) {
        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllEmployeeSkillsByEmployeeID(isDisplayAll, EmployeeID).then(function (response) {
                $scope.EmployeeSkills = response.data;

                if (response.data.length > 0)
                    $scope.isDisplaySkills = true;
                else
                    $scope.isDisplaySkills = false;

                $rootScope.IsLoading = false;

                $scope.Skills.forEach(function (i) {
                    i.IsChecked = false;
                    $scope.EmployeeSkills.forEach(function (j) {
                        if (i.SkillID == j.SkillID)
                            i.IsChecked = true;
                    });
                });
            });
        }
    };

    $scope.selectAllChecked = function (ParentCk) {
        $scope.FilteredSkills.forEach(function (j) {
            $scope.SkillChecked(ParentCk, j.SkillID);
        });
    };

    $scope.SkillChecked = function (IsChecked, SkillID) {
        if (IsChecked) {
            var EmployeeSkill = {
                EmployeeID: $scope.EmployeeID,
                SkillID: SkillID
            };

            EmployeeService.addEmployeeSkill(EmployeeSkill).then(function (response) {
                getAllEmployeeSkillsByEmployeeID($scope.EmployeeID);
            });
        }
        else {
            var EmployeeSkill = {
                EmployeeID: $scope.EmployeeID,
                SkillID: SkillID
            };
            EmployeeService.removeEmployeeSkill(EmployeeSkill).then(function (response) {
                getAllEmployeeSkillsByEmployeeID($scope.EmployeeID);
            });
        };
    };

    $scope.removeEmployeeSkill = function (EmployeeSkillID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmpSkill(EmployeeSkillID).then(function () {
                $rootScope.IsLoading = false;
                getAllEmployeeSkillsByEmployeeID($scope.EmployeeID);
            });
        }
    };

    $scope.getFilteredSkills = function (searchSkill) {
        if (searchSkill.length > 0) {
            $scope.FilteredSkills = $filter('filter')($scope.Skills, { SkillName: searchSkill });
        }
        else {
            $scope.FilteredSkills = $scope.Skills;
        }
    };

    //---------------------------Certification Info---------------------------------

    $scope.getAllCertifications = function () {
        if ($scope.EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllCertifications(isDisplayAll).then(function (response) {
                $scope.Certifications = response.data;
                $scope.FilteredCertifications = $scope.Certifications;
                $rootScope.IsLoading = false;
                getAllEmployeeCertificationsByEmployeeID($scope.EmployeeID);
            });
        }
    };

    function getAllEmployeeCertificationsByEmployeeID(EmployeeID) {
        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllEmployeeCertificationsByEmployeeID(isDisplayAll, EmployeeID).then(function (response) {
                $scope.EmployeeCertifications = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayCertifications = true;
                else
                    $scope.isDisplayCertifications = false;

                $rootScope.IsLoading = false;

                $scope.Certifications.forEach(function (i) {
                    i.IsChecked = false;
                    $scope.EmployeeCertifications.forEach(function (j) {
                        if (i.CertificationID == j.CertificationID)
                            i.IsChecked = true;
                    });
                });
            });
        }
    };

    $scope.selectAllChecked = function (ParentCk) {
        $scope.FilteredCertifications.forEach(function (j) {
            $scope.CertificationChecked(ParentCk, j.CertificationID);
        });
    };

    $scope.CertificationChecked = function (IsChecked, CertificationID) {
        if (IsChecked) {
            var EmployeeCertification = {
                EmployeeID: $scope.EmployeeID,
                CertificationID: CertificationID
            };

            EmployeeService.addEmployeeCertification(EmployeeCertification).then(function (response) {
                getAllEmployeeCertificationsByEmployeeID($scope.EmployeeID);
            });
        }
        else {
            var EmployeeCertification = {
                EmployeeID: $scope.EmployeeID,
                CertificationID: CertificationID
            };
            EmployeeService.removeEmployeeCertification(EmployeeCertification).then(function (response) {
                getAllEmployeeCertificationsByEmployeeID($scope.EmployeeID);
            });
        };
    };

    $scope.removeEmployeeCertification = function (EmployeeCertificationID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmpCertification(EmployeeCertificationID).then(function () {
                $rootScope.IsLoading = false;
                getAllEmployeeCertificationsByEmployeeID($scope.EmployeeID);
            });
        }
    };

    $scope.getFilteredCertifications = function (searchCertification) {
        if (searchCertification.length > 0) {
            $scope.FilteredCertifications = $filter('filter')($scope.Certifications, { CertificationName: searchCertification });
        }
        else {
            $scope.FilteredCertifications = $scope.Certifications;
        }
    };

    $scope.OpenUploadModal = function (EmployeeCertificationID) {
        $scope.EmployeeCertificationID = EmployeeCertificationID;
        var modalInstance = $uibModal.open({
            templateUrl: 'uploadFileModal.html',
            controller: 'EmployeeCtrl_Upload',
            size: 'sm',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    //-------------------------------GetZipCode------------------

    $scope.getZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.Employee.CountryID }, true);
        var state = $filter('filter')($scope.States, { StateID: $scope.Employee.StateID }, true);
        var city = $filter('filter')($scope.Cities, { CityID: $scope.Employee.CityID }, true);

        var address = $scope.Employee.Address1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.Employee.PostalCode = i.long_name;
                        }
                    })
                } else {
                    $rootScope.IsLoading = false;
                    window.alert('No results found');
                }
            }
            else
                $rootScope.IsLoading = false;
        });
    };
})

.controller('EmployeeCtrl_Upload', function ($uibModalInstance, $scope, $rootScope, $stateParams, EmployeeService) {

    var isDisplayAll = false;

    console.log($scope.EmployeeCertificationID);

    $scope.CloseModal = function () {
        $uibModalInstance.dismiss('cancel');
        $scope.getAllCertifications();
    };

    $scope.selectedFile = function ($files) {
        $scope.$files = $files;
    };

    $scope.uploadCertificationImage = function () {

        var EmployeeCertification = {
            EmployeeCertificationID: $scope.EmployeeCertificationID
        };

        for (var i = 0; i < $scope.$files.length; i++) {
            var $file = $scope.$files[i];

            (function (index) {
                $rootScope.IsLoading = true;
                EmployeeService.uploadCertificationImage(EmployeeCertification, $file).then(function (response) {
                    $rootScope.IsLoading = false;
                });
            })(i);
        }
    };
})

.controller('EmployeeCtrl_Detail', function ($scope, $rootScope, $stateParams, EmployeeService, $window, $filter, dualList, CFG, $uibModal) {

    var isDisplayAll = false;

    $scope.EmployeeID = $stateParams.id;

    getEmployeeByID($scope.EmployeeID);

    function getEmployeeByID(EmployeeID) {
        $rootScope.IsLoading = true;
        EmployeeService.getEmployeeByID(EmployeeID).then(function (response) {
            $scope.Employee = response.data;
            $scope.cntID = $scope.Employee.CountryID;
            $scope.stID = $scope.Employee.StateID;
            $scope.ctID = $scope.Employee.CityID;
            $scope.getAllCountries();
            $scope.getAllPayFrequencies();
            $scope.getAllLabourClassifications();
            $scope.getAllCertifications();
            $scope.getAllSkills();
            $scope.getAllCustomers();
            $scope.getNotesByEmployeeID()
            $rootScope.IsLoading = false;
        });
    };

    //----------------------------Personal Info---------------------------

    $scope.getAllTitles = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllTitles(isDisplayAll).then(function (response) {
            $scope.Titles = response.data;
            $scope.SelectedTitles = $scope.Titles;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllTitles();

    $scope.getAllTitleByGender = function (gender) {
        $scope.SelectedTitles = $filter('filter')($scope.Titles, { Gender: gender }, true);
    };

    getAllEmployeeTypes();

    function getAllEmployeeTypes() {
        $rootScope.IsLoading = true;
        EmployeeService.getAllEmployeeTypes(isDisplayAll).then(function (response) {
            $scope.EmployeeTypes = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editEmployee = function (Employee) {

        if (Employee.FirstName == null || Employee.FirstName == '')
            return $rootScope.showNotify('First name field cannot be left empty', 'Information', 'i');

        if (Employee.LastName == null || Employee.LastName == '')
            return $rootScope.showNotify('Last name field cannot be left empty', 'Information', 'i');

        if (Employee.SIN == null || Employee.SIN == '')
            return $rootScope.showNotify('SIN # field cannot be left empty', 'Information', 'i');

        if (Employee.Gender == null || Employee.Gender == '')
            return $rootScope.showNotify('Gender field cannot be left empty', 'Information', 'i');

        if (Employee.DOB == null || Employee.DOB == '')
            return $rootScope.showNotify('DOB field cannot be left empty', 'Information', 'i');

        if (Employee.Phone == null || Employee.Phone == '')
            return $rootScope.showNotify('Phone field cannot be left empty', 'Information', 'i');

        if (Employee.Address1 == null || Employee.Address1 == '')
            return $rootScope.showNotify('Address1 field cannot be left empty', 'Information', 'i');

        if (Employee.StateID == null || Employee.StateID == '')
            return $rootScope.showNotify('State field cannot be left empty', 'Information', 'i');

        if (Employee.CityID == null || Employee.CityID == '')
            return $rootScope.showNotify('City field cannot be left empty', 'Information', 'i');

        if (Employee.PostalCode == null || Employee.PostalCode == '')
            return $rootScope.showNotify('Postal code field cannot be left empty', 'Information', 'i');

        $rootScope.IsLoading = true;
        EmployeeService.editEmployee(Employee).then(function () {
            $rootScope.IsLoading = false;
            $scope.getAllLabourClassifications();
        });
    };

    //----------------------------Employee & Payroll Info---------------------------

    $scope.getAllPayFrequencies = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllPayFrequencies(isDisplayAll).then(function (response) {
            $scope.PayFrequencies = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editEmployeeAndPayroll = function (Employee) {

        //if (Employee.SIN == null || Employee.SIN == '')
        //    return $rootScope.showNotify('SIN field cannot be empty', 'Information', 'i');


        Employee.EmployeeID = $scope.EmployeeID;
        $rootScope.IsLoading = true;
        EmployeeService.editEmployeeAndPayroll(Employee).then(function () {
            $rootScope.IsLoading = false;
            $scope.getAllLabourClassifications();
            //angular.element(document.querySelector('#s3')).addClass('active').siblings('.active').removeClass('active');
            //angular.element(document.querySelector('#s33')).addClass('active').siblings('.active').removeClass('active');
        });
    };

    //----------------------------Contact Info---------------------------

    $scope.getAllCountries = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;
            $scope.getStatesByCountryID($scope.cntID);
            $scope.getCitiesByStateID($scope.stID);
        });
    };

    $scope.getStatesByCountryID = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.States = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getCitiesByStateID = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.Cities = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.editContact = function (Employee) {

        if (Employee.EmailMain == null || Employee.EmailMain == '')
            return $rootScope.showNotify('Email field cannot be empty', 'Information', 'i');


        Employee.EmployeeID = $scope.EmployeeID;
        $rootScope.IsLoading = true;
        EmployeeService.editContact(Employee).then(function () {
            $rootScope.IsLoading = false;
        });
    };

    //----------------------------Notes Info---------------------------

    $scope.getNotesByEmployeeID = function () {
        getNotesByEmployeeID($scope.EmployeeID);
    };

    $scope.addEmployeeNote = function (EmployeeNote) {

        if (EmployeeNote.Note == null || EmployeeNote.Note == '')
            return $rootScope.showNotify('Note field cannot be empty', 'Information', 'i');

        EmployeeNote.EmployeeID = $scope.EmployeeID;
        $rootScope.IsLoading = true;
        EmployeeService.addEmployeeNote(EmployeeNote).then(function () {
            $scope.EmployeeNote.Note = null;
            $rootScope.IsLoading = false;
            getNotesByEmployeeID($scope.EmployeeID);
        });
    };

    function getNotesByEmployeeID(EmployeeID) {
        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllEmployeeNotesByEmployeeID(isDisplayAll, EmployeeID).then(function (response) {
                $scope.EmployeeNotes = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayNotes = true;
                else
                    $scope.isDisplayNotes = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.removeEmployeeNote = function (EmployeeNoteID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmployeeNote(EmployeeNoteID).then(function () {
                $rootScope.IsLoading = false;
                getNotesByEmployeeID($scope.EmployeeID);
            });
        }
    };

    //----------------------------Labour Classification Info---------------------------

    $scope.getAllLabourClassifications = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllLabourClassifications(isDisplayAll).then(function (response) {
            $scope.LabourClassifications = response.data;
            $scope.FilteredLabourClassifications = $scope.LabourClassifications;
            $rootScope.IsLoading = false;
            getLabourClassificationsByEmployeeID($scope.EmployeeID);
        });
    };

    function getLabourClassificationsByEmployeeID(EmployeeID) {
        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllEmployeeLabourClassificationsByEmployeeID(isDisplayAll, EmployeeID).then(function (response) {
                $scope.EmployeeLabourClassifications = response.data;
                $rootScope.IsLoading = false;

                if (response.data.length > 0)
                    $scope.isDisplayLabourClassifications = true;
                else
                    $scope.isDisplayLabourClassifications = false;

                $rootScope.IsLoading = false;

                $scope.LabourClassifications.forEach(function (i) {
                    i.IsChecked = false;
                    $scope.EmployeeLabourClassifications.forEach(function (j) {
                        if (i.LabourClassificationID == j.LabourClassificationID)
                            i.IsChecked = true;
                    });
                });
            });
        }
    };

    $scope.selectAllChecked = function (ParentCk) {
        $scope.FilteredLabourClassifications.forEach(function (j) {
            $scope.LabourClassificationChecked(ParentCk, j.LabourClassificationID);
        });
    };

    $scope.LabourClassificationChecked = function (IsChecked, LabourClassificationID) {
        if (IsChecked) {
            var EmployeeLabourClassification = {
                EmployeeID: $scope.EmployeeID,
                LabourClassificationID: LabourClassificationID,
                Rate: CFG.settings.defaultLabourRate
            };

            EmployeeService.addEmployeeLabourClassification(EmployeeLabourClassification).then(function (response) {
                getLabourClassificationsByEmployeeID($scope.EmployeeID);
            });
        }
        else {
            var EmployeeLabourClassification = {
                EmployeeID: $scope.EmployeeID,
                LabourClassificationID: LabourClassificationID
            };
            EmployeeService.removeEmployeeLabourClassification(EmployeeLabourClassification).then(function (response) {
                getLabourClassificationsByEmployeeID($scope.EmployeeID);
            });
        };
    };

    $scope.editEmployeeLabourClassification = function (EmployeeLabourClassificationID, Rate) {

        if (Rate == null || Rate == '')
            return $rootScope.showNotify('Rate field cannot be empty', 'Information', 'i');

        var EmployeeLabourClassification = {
            EmployeeLabourClassificationID: EmployeeLabourClassificationID,
            Rate: Rate
        };

        $rootScope.IsLoading = true;
        EmployeeService.editEmployeeLabourClassification(EmployeeLabourClassification).then(function () {
            $rootScope.IsLoading = false;
            $scope.tempEmployeeLabourClassificationID = 0;
        });
    };

    $scope.tempEmployeeLabourClassificationID = 0;

    $scope.editRow = function (EmployeeLabourClassificationID) {
        $scope.tempEmployeeLabourClassificationID = EmployeeLabourClassificationID;
    };

    $scope.removeEmployeeLabourClassification = function (EmployeeLabourClassificationID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmpLabourClassification(EmployeeLabourClassificationID).then(function () {
                $rootScope.IsLoading = false;
                getLabourClassificationsByEmployeeID($scope.EmployeeID);
            });
        }
    };

    $scope.getFilteredLabourClassifications = function (searchLabourClassification) {
        if (searchLabourClassification.length > 0) {
            $scope.FilteredLabourClassifications = $filter('filter')($scope.LabourClassifications, { LabourClassificationName: searchLabourClassification });
        }
        else {
            $scope.FilteredLabourClassifications = $scope.LabourClassifications;
        }
    };

    //---------------------------Blacklist Info---------------------------------

    $scope.blacklistListOptions = dualList.settings;

    $scope.getAllCustomers = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllCustomers(isDisplayAll).then(function (response) {
            $scope.Customers = response.data;
            $scope.FilteredCustomers = $scope.Customers;
            $rootScope.IsLoading = false;
            getAllEmployeeBlacklistsByEmployeeID($scope.EmployeeID);
        });
    };

    function getAllEmployeeBlacklistsByEmployeeID(EmployeeID) {
        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllEmployeeBlacklistsByEmployeeID(isDisplayAll, EmployeeID).then(function (response) {
                $scope.EmployeeBlacklists = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayBlacklists = true;
                else
                    $scope.isDisplayBlacklists = false;

                $rootScope.IsLoading = false;

                $scope.Customers.forEach(function (i) {
                    i.IsChecked = false;
                    $scope.EmployeeBlacklists.forEach(function (j) {
                        if (i.CustomerID == j.CustomerID)
                            i.IsChecked = true;
                    });
                });
            });
        }
    };

    $scope.selectAllChecked = function (ParentCk) {
        $scope.FilteredCustomers.forEach(function (j) {
            $scope.CustomerChecked(ParentCk, j.CustomerID);
        });
    };

    $scope.CustomerChecked = function (IsChecked, CustomerID) {
        if (IsChecked) {
            var EmployeeBlacklist = {
                EmployeeID: $scope.EmployeeID,
                CustomerID: CustomerID
            };

            EmployeeService.addEmployeeBlacklist(EmployeeBlacklist).then(function (response) {
                getAllEmployeeBlacklistsByEmployeeID($scope.EmployeeID);
            });
        }
        else {
            var EmployeeBlacklist = {
                EmployeeID: $scope.EmployeeID,
                CustomerID: CustomerID
            };

            EmployeeService.removeEmployeeBlacklist(EmployeeBlacklist).then(function (response) {
                getAllEmployeeBlacklistsByEmployeeID($scope.EmployeeID);
            });
        };
    };

    $scope.removeEmployeeBlacklist = function (EmployeeBlacklistID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmpBlacklist(EmployeeBlacklistID).then(function () {
                $rootScope.IsLoading = false;
                getAllEmployeeBlacklistsByEmployeeID($scope.EmployeeID);
            });
        }
    };

    $scope.getFilteredCustomers = function (searchCustomer) {
        if (searchCustomer.length > 0) {
            $scope.FilteredCustomers = $filter('filter')($scope.Customers, { CustomerName: searchCustomer });
        }
        else {
            $scope.FilteredCustomers = $scope.Customers;
        }
    };

    //---------------------------Skill Info---------------------------------

    $scope.getAllSkills = function () {
        $rootScope.IsLoading = true;
        EmployeeService.getAllSkills(isDisplayAll).then(function (response) {
            $scope.Skills = response.data;
            $scope.FilteredSkills = $scope.Skills;
            $rootScope.IsLoading = false;
            getAllEmployeeSkillsByEmployeeID($scope.EmployeeID);
        });
    };

    function getAllEmployeeSkillsByEmployeeID(EmployeeID) {
        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllEmployeeSkillsByEmployeeID(isDisplayAll, EmployeeID).then(function (response) {
                $scope.EmployeeSkills = response.data;

                if (response.data.length > 0)
                    $scope.isDisplaySkills = true;
                else
                    $scope.isDisplaySkills = false;

                $rootScope.IsLoading = false;

                $scope.Skills.forEach(function (i) {
                    i.IsChecked = false;
                    $scope.EmployeeSkills.forEach(function (j) {
                        if (i.SkillID == j.SkillID)
                            i.IsChecked = true;
                    });
                });
            });
        }
    };

    $scope.selectAllChecked = function (ParentCk) {
        $scope.FilteredSkills.forEach(function (j) {
            $scope.SkillChecked(ParentCk, j.SkillID);
        });
    };

    $scope.SkillChecked = function (IsChecked, SkillID) {
        if (IsChecked) {
            var EmployeeSkill = {
                EmployeeID: $scope.EmployeeID,
                SkillID: SkillID
            };

            EmployeeService.addEmployeeSkill(EmployeeSkill).then(function (response) {
                getAllEmployeeSkillsByEmployeeID($scope.EmployeeID);
            });
        }
        else {
            var EmployeeSkill = {
                EmployeeID: $scope.EmployeeID,
                SkillID: SkillID
            };
            EmployeeService.removeEmployeeSkill(EmployeeSkill).then(function (response) {
                getAllEmployeeSkillsByEmployeeID($scope.EmployeeID);
            });
        };
    };

    $scope.removeEmployeeSkill = function (EmployeeSkillID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmpSkill(EmployeeSkillID).then(function () {
                $rootScope.IsLoading = false;
                getAllEmployeeSkillsByEmployeeID($scope.EmployeeID);
            });
        }
    };

    $scope.getFilteredSkills = function (searchSkill) {
        if (searchSkill.length > 0) {
            $scope.FilteredSkills = $filter('filter')($scope.Skills, { SkillName: searchSkill });
        }
        else {
            $scope.FilteredSkills = $scope.Skills;
        }
    };

    //---------------------------Certification Info---------------------------------

    $scope.getAllCertifications = function () {
        if ($scope.EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllCertifications(isDisplayAll).then(function (response) {
                $scope.Certifications = response.data;
                $scope.FilteredCertifications = $scope.Certifications;
                $rootScope.IsLoading = false;
                getAllEmployeeCertificationsByEmployeeID($scope.EmployeeID);
            });
        }
    };

    function getAllEmployeeCertificationsByEmployeeID(EmployeeID) {
        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            EmployeeService.getAllEmployeeCertificationsByEmployeeID(isDisplayAll, EmployeeID).then(function (response) {
                $scope.EmployeeCertifications = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayCertifications = true;
                else
                    $scope.isDisplayCertifications = false;

                $rootScope.IsLoading = false;

                $scope.Certifications.forEach(function (i) {
                    i.IsChecked = false;
                    $scope.EmployeeCertifications.forEach(function (j) {
                        if (i.CertificationID == j.CertificationID)
                            i.IsChecked = true;
                    });
                });
            });
        }
    };

    $scope.selectAllChecked = function (ParentCk) {
        $scope.FilteredCertifications.forEach(function (j) {
            $scope.CertificationChecked(ParentCk, j.CertificationID);
        });
    };

    $scope.CertificationChecked = function (IsChecked, CertificationID) {
        if (IsChecked) {
            var EmployeeCertification = {
                EmployeeID: $scope.EmployeeID,
                CertificationID: CertificationID
            };

            EmployeeService.addEmployeeCertification(EmployeeCertification).then(function (response) {
                getAllEmployeeCertificationsByEmployeeID($scope.EmployeeID);
            });
        }
        else {
            var EmployeeCertification = {
                EmployeeID: $scope.EmployeeID,
                CertificationID: CertificationID
            };
            EmployeeService.removeEmployeeCertification(EmployeeCertification).then(function (response) {
                getAllEmployeeCertificationsByEmployeeID($scope.EmployeeID);
            });
        };
    };

    $scope.removeEmployeeCertification = function (EmployeeCertificationID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmpCertification(EmployeeCertificationID).then(function () {
                $rootScope.IsLoading = false;
                getAllEmployeeCertificationsByEmployeeID($scope.EmployeeID);
            });
        }
    };

    $scope.getFilteredCertifications = function (searchCertification) {
        if (searchCertification.length > 0) {
            $scope.FilteredCertifications = $filter('filter')($scope.Certifications, { CertificationName: searchCertification });
        }
        else {
            $scope.FilteredCertifications = $scope.Certifications;
        }
    };

    $scope.OpenUploadModal = function (EmployeeCertificationID) {
        $scope.EmployeeCertificationID = EmployeeCertificationID;
        var modalInstance = $uibModal.open({
            templateUrl: 'uploadFileModal.html',
            controller: 'EmployeeCtrl_Upload',
            size: 'sm',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    //-------------------------------GetZipCode------------------

    $scope.getZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.Employee.CountryID }, true);
        var state = $filter('filter')($scope.States, { StateID: $scope.Employee.StateID }, true);
        var city = $filter('filter')($scope.Cities, { CityID: $scope.Employee.CityID }, true);

        var address = $scope.Employee.Address1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.Employee.PostalCode = i.long_name;
                        }
                    })
                } else {
                    $rootScope.IsLoading = false;
                    window.alert('No results found');
                }
            }
            else
                $rootScope.IsLoading = false;
        });
    };
})

.controller('XEmployeeCtrl', function ($scope, $state, $rootScope, $dataTableService, EmployeeService, $window, $compile, $filter, $uibModal) {

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('dblclick');
        $('td', nRow).bind('dblclick', function () {
            $scope.$apply(function () {
                $state.go('app.employee.employee.detail', { id: aData.EmployeeID });
            });
        });
        return nRow;
    }

    var Obj = {
        url: '/Employee/GetEmployees',
        type: 'POST',
        pageSize: 10,
        $compile: $compile,
        $scope: $scope,
        orders: [[0, 'asc']],
        rowCallback: rowCallback
    };
    $scope.dtOptions = $dataTableService.$dtOptions(Obj);

    var size = "sm";

    var renderAction = function (data, type, full, meta) {
        return '<a class="btn-cust" title="Edit Employee" ui-sref="app.employee.employee.edit({id: ' + data.EmployeeID + '})">' +
            '   <i class="fa fa-edit"></i> Edit' +
            '</a>&nbsp;'
            +
            '<a class="btn-cust" title="View Details of Employee" ui-sref="app.employee.employee.detail({id: ' + data.EmployeeID + '})">' +
            '   <i class="fa fa-eye"></i> View' +
            '</a>&nbsp;'
            +
            '<a class="btn-cust" title="Remove Employee" ng-click="deleteEmployee(' + data.EmployeeID + ')">' +
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

    var renderNotes = function (data, type, full, meta) {
        return $filter('limitTo')(data, 20, 0) + '...'
    };

    var Obj = [ //.withOption('width', '50%'),
         { Column: 'AccountNo', Title: 'Employee #', containsHtml: false },
        { Column: 'PrintName', Title: 'Name', containsHtml: false },
        { Column: 'EmailMain', Title: 'Email', containsHtml: false },
        { Column: 'Phone', Title: 'Phone', containsHtml: false },
        { Column: 'Created', Title: 'Date Created', containsHtml: true, renderWith: renderDate, Width: 14 },
         { Column: 'IsEnable', Title: 'IsEnable', containsHtml: true, renderWith: renderIsEnable, Width: 2 },
        { Column: null, Title: 'Actions', containsHtml: true, renderWith: renderAction, Width: 16 }
    ];
    $scope.dtColumns = $dataTableService.$dtColumns(Obj);

    $scope.dtInstance = {};

    $scope.Employee = {};

    function clearAll() {
        $scope.Employee.EmployeeName = null;
    };

    $scope.getClearedModal = function () {
        $scope.ID = null;
        clearAll();
    };

    $scope.OpenAddModal = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'addEmployeeModal.html',
            controller: 'EmployeeCtrl_Add',
            size: 'lg',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };


    $scope.OpenEditModal = function (EmployeeID) {
        $scope.EmployeeID = EmployeeID;
        var modalInstance = $uibModal.open({
            templateUrl: 'editEmployeeModal.html',
            controller: 'EmployeeCtrl_Edit',
            size: 'lg',
            scope: $scope,
            backdrop: false,
            keyboard: false
        });
    };

    $scope.deleteEmployee = function (EmployeeID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmployee(EmployeeID).then(function () {
                $rootScope.IsLoading = false;
                $scope.dtInstance.rerender();
            });
        }
    };
})

.controller('EmployeeCtrl', function ($scope, $state, $rootScope, $dataTableService, EmployeeService, $window, $compile, $filter, $uibModal) {

    var isDisplayAll = false;

    getAllSkills();
    getAllCertifications();

    function getAllSkills() {
        $rootScope.IsLoading = true;
        EmployeeService.getAllSkills(isDisplayAll).then(function (response) {
            $scope.Skills = response.data;
            $rootScope.IsLoading = false;
        });
    };

    function getAllCertifications() {
        $rootScope.IsLoading = true;
        EmployeeService.getAllCertifications(isDisplayAll).then(function (response) {
            $scope.Certifications = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.PageSizes = [];
    $scope.PageSizes.push({ PageSize: 10 }, { PageSize: 25 }, { PageSize: 50 }, { PageSize: 100 });

    $scope.Employees = [];
    $scope.totalEmployees = 0;
    $scope.IsItemsFound = false;
    $scope.pageSize = 10;
    $scope.Search = {};

    var Obj = {
        orders: [[1, 'asc']],
        pageSize: $scope.pageSize
    };
    $scope.dtOptions = $dataTableService.$dtCustomOptions(Obj);

    $scope.loading = true;
    getEmployees($rootScope.pageNumber);
    $scope.loading = false;

    $scope.refresh = function () {
        $route.reload();
    };

    $scope.pagination = {
        current: $rootScope.pageNumber
    };

    $scope.pageChanged = function (newPage) {
        getEmployees(newPage);
    };

    $scope.getEmployees = function () {
        getEmployees(1);
    };

    function getEmployees(pageNumber) {

        $rootScope.pageNumber = pageNumber;

        $scope.Search.pageNumber = $rootScope.pageNumber;
        $scope.Search.pageSize = $scope.pageSize;

        $rootScope.IsLoading = true;
        EmployeeService.getEmployees($scope.Search)
            .then(function (result) {
                $scope.Employees = result.data.Items;
                $scope.totalEmployees = result.data.Count;
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


    $scope.deleteEmployee = function (EmployeeID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            EmployeeService.removeEmployee(EmployeeID).then(function () {
                $rootScope.IsLoading = false;
                getEmployees($rootScope.pageNumber);
            });
        }
    };

    $scope.editEmployeeIsEnable = function (EmployeeID, IsEnable) {

        var Employee = {};

        if (IsEnable == true) {
            Employee = {
                EmployeeID: EmployeeID,
                IsEnable: false
            };
        }
        else {
            Employee = {
                EmployeeID: EmployeeID,
                IsEnable: true
            };
        }

        $rootScope.IsLoading = true;
        EmployeeService.editEmployeeIsEnable(Employee).then(function () {
            $rootScope.IsLoading = false;
            getEmployees($rootScope.pageNumber);
        });
    };

    $scope.editEmployeeIsNeverUse = function (EmployeeID, IsNeverUse) {

        var Employee = {};

        if (IsNeverUse == true) {
            Employee = {
                EmployeeID: EmployeeID,
                IsNeverUse: false
            };
        }
        else {
            Employee = {
                EmployeeID: EmployeeID,
                IsNeverUse: true
            };
        }

        $rootScope.IsLoading = true;
        EmployeeService.editEmployeeIsNeverUse(Employee).then(function () {
            $rootScope.IsLoading = false;
            getEmployees($rootScope.pageNumber);
        });
    };

    $scope.goToDetails = function (EmployeeID) {
        $state.go('app.employee.employee.detail', { id: EmployeeID });
    };
})

.directive('personalForm', function () {

    return {
        restrict: 'A',
        link: function (scope, form) {
            form.bootstrapValidator({
                framework: 'bootstrap',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    initial: {
                        validators: {
                            //notEmpty: {
                            //    message: 'The initial is required'
                            //},
                        }
                    },
                    firstname: {
                        validators: {
                            notEmpty: {
                                message: 'The first name is required'
                            },
                            stringLength: {
                                max: 25,
                                message: 'The first name must be less than 25 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The first name can only consist of alphabets, space.'
                            }
                        }
                    },
                    middlename: {
                        validators: {
                            //notEmpty: {
                            //    message: 'The middle name is required'
                            //},
                            stringLength: {
                                max: 2,
                                message: 'The middle name must be less than 2 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The middle name can only consist of alphabets, space.'
                            }
                        }
                    },
                    lastname: {
                        validators: {
                            notEmpty: {
                                message: 'The last name is required'
                            },
                            stringLength: {
                                max: 25,
                                message: 'The last name must be less than 25 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z\s]*$/,
                                message: 'The last name can only consist of alphabets, space.'
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
                    dob: {
                        validators: {
                            notEmpty: {
                                message: 'The dob is required'
                            },
                            date: {
                                format: 'MM/DD/YYYY',
                                message: 'The date is not a valid'
                            }
                        }
                    },
                    phone: {
                        validators: {
                            notEmpty: {
                                message: 'The phone is required'
                            },
                            //callback: {
                            //    message: 'The phone number is not valid',
                            //    callback: function (value, validator, $field) {
                            //        return value === '' || $field.intlTelInput('isValidNumber');
                            //    }
                            //}
                        }
                    },
                    email: {
                        validators: {
                            //notEmpty: {
                            //    message: 'The email address is required'
                            //},
                            emailAddress: {
                                message: 'The email address is not valid'
                            }
                        }
                    },
                    address1: {
                        validators: {
                            notEmpty: {
                                message: 'The address is required'
                            },
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
                    },
                    city: {
                        validators: {
                            notEmpty: {
                                message: 'The city name is required'
                            },
                        }
                    },
                    postalcode: {
                        validators: {
                            notEmpty: {
                                message: 'The postal code is required'
                            },
                        }
                    },
                    website: {
                        validators: {
                            regexp: {
                                regexp: /^((?:http|ftp)s?:\/\/)(?:(?:[A-Z0-9](?:[A-Z0-9-]{0,61}[A-Z0-9])?\.)+(?:[A-Z]{2,6}\.?|[A-Z0-9-]{2,}\.?)|localhost|\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})(?::\d+)?(?:\/?|[\/?]\S+)$/i,
                                message: 'The website is not valid',
                            }
                        }
                    },
                    sin: {
                        validators: {
                            notEmpty: {
                                message: 'The sin # is required'
                            },
                            callback: {
                                message: 'Wrong answer',
                                callback: function (value, validator, $field) {

                                    var valid = true;
                                    var msg = "";
                                    var digits = 0;

                                    //value = trim(value);

                                    digits = value.length;
                                    if (digits > 9) {
                                        valid = false;
                                        msg = "Invalid: " + value + " has more than maximum 9 digits.";
                                    }
                                    else if (digits < 9) {
                                        valid = false;
                                        if (digits == 0) {
                                            msg = "Please enter a 9 digit Social Insurance Number.";
                                        } else {
                                            msg = "Invalid: " + value + " has less than the required 9 digits.";
                                        }
                                    }
                                    else if (!value.match(/^\d+$/)) {
                                        valid = false;
                                        msg = "Invalid: " + value + " contains invalid non-numeric characters";
                                    }
                                    else if (value == "000000000") {
                                        msg = "000000000 may be used only when SIN is unknown - please revalidate when SIN is available";
                                    }
                                    else {

                                        var checkdigit = value.substr(8, 1);

                                        var double2 = parseInt(value.substr(1, 1)) * 2;
                                        var double4 = parseInt(value.substr(3, 1)) * 2;
                                        var double6 = parseInt(value.substr(5, 1)) * 2;
                                        var double8 = parseInt(value.substr(7, 1)) * 2;

                                        var num1 = double2.toString() + double4.toString() + double6.toString() + double8.toString();

                                        var digit1 = value.substr(0, 1);
                                        var digit3 = value.substr(2, 1);
                                        var digit5 = value.substr(4, 1);
                                        var digit7 = value.substr(6, 1);

                                        var num2 = digit1 + digit3 + digit5 + digit7;

                                        var crossadd1 = 0;
                                        var position = 0;
                                        for (position = 0; position < num1.length; position++) {
                                            crossadd1 = crossadd1 + parseInt(num1.substring(position, position + 1));
                                        }

                                        var crossadd2 = 0;
                                        for (position = 0; position < num2.length; position++) {
                                            crossadd2 = crossadd2 + parseInt(num2.substring(position, position + 1));
                                        }

                                        var checksum1 = crossadd1 + crossadd2;
                                        var checksum2;
                                        var checkdigitX;

                                        if (checksum1.toString().substring(checksum1.toString().length - 1) == "0") {
                                            checksum2 = checksum1;
                                            checkdigitX = '0';
                                        } else {
                                            checksum2 = (Math.ceil(checksum1 / 10.0) * 10.0);
                                            checkdigitX = parseFloat(checksum2 - checksum1).toString();
                                        }

                                        if (checkdigitX == checkdigit) {
                                            valid = true;
                                        } else {
                                            valid = false;
                                            msg = "Invalid: " + value + " - check digit does not pass validation test.";
                                        }
                                    }

                                    return {
                                        valid: valid,
                                        message: msg
                                    }
                                }
                            }
                        }
                    },
                }
            })
                .on('error.field.bv', function (e, data) {
                    var $invalidFields = data.bv.getInvalidFields();
                    if($invalidFields.length > 0)
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
                })
                .on('blur', '#sin', function () {
                    console.log('click');
                    form.bootstrapValidator('revalidateField', 'sin');
                })

            form.find('[name="phone"], [name="mobile"]').mask('+1 (999) 999 9999');
            form.find('[name="dob"], [name="paystub"]').mask('99/99/9999');

        }
    }
})

.directive('payrollForm', function () {

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
                    balancedue: {
                        validators: {
                            regexp: {
                                regexp: /^[0-9]+(\.[0-9]{1,2})?$/,
                                message: 'The value is not valid',
                            }
                        }
                    },
                    withhold: {
                        validators: {
                            regexp: {
                                regexp: /^[0-9]+(\.[0-9]{1,2})?$/,
                                message: 'The value is not valid',
                            }
                        }
                    },
                    dormant: {
                        validators: {
                            regexp: {
                                regexp: /^[0-9]+(\.[0-9]{1,2})?$/,
                                message: 'The value is not valid',
                            }
                        }
                    }
                }
            })
             .on('success.field.bv', function (e, data) {
                 var $invalidFields = data.bv.getInvalidFields();
                 if ($invalidFields.length == 0)
                     angular.element(document.querySelector('#next')).removeClass('disabled');
             })
                .on('error.field.bv', function (e, data) {
                    var $invalidFields = data.bv.getInvalidFields();
                    if ($invalidFields.length > 0)
                        angular.element(document.querySelector('#next')).addClass('disabled');
                })
        }
    }
})

.directive('noteForm', function () {

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
                    note: {
                        validators: {
                            notEmpty: {
                                message: 'The note is required'
                            },
                        }
                    }
                }
            })
             .on('success.field.bv', function (e, data) {
                 var $invalidFields = data.bv.getInvalidFields();
                 if ($invalidFields.length == 0)
                     angular.element(document.querySelector('#next')).removeClass('disabled');
             })
                .on('error.field.bv', function (e, data) {
                    var $invalidFields = data.bv.getInvalidFields();
                    if ($invalidFields.length > 0)
                        angular.element(document.querySelector('#next')).addClass('disabled');
                })
        }
    }
})