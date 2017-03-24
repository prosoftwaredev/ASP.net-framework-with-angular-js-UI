'use strict';

angular.module('app.customer', []).config(function () { })

.factory('CustomerService', function ($httpService) {
    return {
        getCustomers: function (search) {
            var Obj = { postData: search, url: '/Customer/GetCustomers' };
            return $httpService.$postSearch(Obj);
        },

        getCustomerSites: function (search) {
            var Obj = { postData: search, url: '/Customer/GetCustomerSites' };
            return $httpService.$postSearch(Obj);
        },

        getCustomerByID: function (CustomerId) {
            var Obj = { params: { CustomerID: CustomerId }, url: '/Customer/GetCustomerByID' };
            return $httpService.$get(Obj);
        },
        addCustomer: function (Customer) {
            var Obj = { postData: Customer, url: '/Customer/CreateCustomer', successToast: null, errorToast: null };
            return $httpService.$post(Obj);
        },
        editCustomer: function (Customer) {
            var Obj = { postData: Customer, url: '/Customer/UpdateCustomer', successToast: 'Customer edited successfully', errorToast: 'Error while editing existing Customer' };
            return $httpService.$put(Obj);
        },
        removeCustomer: function (ID) {
            var Obj = { url: '/Customer/DeleteCustomer/' + ID, successToast: 'Customer removed successfully', errorToast: 'Error while removing existing Customer' };
            return $httpService.$delete(Obj);
        },

        editCustomerIsEnable: function (Employee) {
            var Obj = { postData: Employee, url: '/Customer/UpdateCustomerIsEnable', successToast: 'Customer site edited successfully', errorToast: 'Error while editing existing customer site' };
            return $httpService.$put(Obj);
        },

        editCustomerSiteIsEnable: function (Employee) {
            var Obj = { postData: Employee, url: '/Customer/UpdateCustomerSiteIsEnable', successToast: 'Customer site edited successfully', errorToast: 'Error while editing existing customer site' };
            return $httpService.$put(Obj);
        },
        editCustomerDelinquent: function (Employee) {
            var Obj = { postData: Employee, url: '/Customer/UpdateCustomerDelinquent', successToast: 'Customer edited successfully', errorToast: 'Error while editing existing customer' };
            return $httpService.$put(Obj);
        },

        getAllEquipments: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/Equipment/GetAllEquipments' };
            return $httpService.$get(Obj);
        },
        getAllCountries: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/Country/GetAllCountries' };
            return $httpService.$get(Obj);
        },
        getAllStatesByCountryID: function (isDisplayAll, CountryID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, CountryID: CountryID }, url: '/State/GetAllStatesByCountryID' };
            return $httpService.$get(Obj);
        },
        getAllCitiesByStateID: function (isDisplayAll, StateID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, StateID: StateID }, url: '/City/GetAllCitiesByStateID' };
            return $httpService.$get(Obj);
        },
        editContact: function (Customer) {
            var Obj = { postData: Customer, url: '/Customer/UpdateContact', successToast: 'Contact Info edited successfully', errorToast: 'Error while editing existing contact Info' };
            return $httpService.$put(Obj);
        },

        editOvertime: function (Customer) {
            var Obj = { postData: Customer, url: '/Customer/UpdateOvertime', successToast: 'Overtime Info edited successfully', errorToast: 'Error while editing existing overtime Info' };
            return $httpService.$put(Obj);
        },

        editReminder: function (Customer) {
            var Obj = { postData: Customer, url: '/Customer/UpdateReminder', successToast: 'Reminder Info edited successfully', errorToast: 'Error while editing existing reminder Info' };
            return $httpService.$put(Obj);
        },
        getAllPaymentTerms: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/PaymentTerm/GetAllPaymentTerms' };
            return $httpService.$get(Obj);
        },
        getAllSalesReps: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/SalesRep/GetAllSalesReps' };
            return $httpService.$get(Obj);
        },

        addCustomerSite: function (CustomerSite) {
            var Obj = { postData: CustomerSite, url: '/Customer/CreateCustomerSite', successToast: 'Customer & Site added successfully', errorToast: 'Error while adding new customer site' };
            return $httpService.$post(Obj);
        },
        getAllCustomerSitesByCustomerID: function (isDisplayAll, CustomerID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, CustomerID: CustomerID }, url: '/Customer/GetAllCustomerSitesByCustomerID' };
            return $httpService.$get(Obj);
        },
        getCustomerSiteByID: function (CustomerSiteID) {
            var Obj = { params: { CustomerSiteID: CustomerSiteID }, url: '/Customer/GetCustomerSiteByID' };
            return $httpService.$get(Obj);
        },

        addCustomerSiteNote: function (CustomerSiteNote) {
            var Obj = { postData: CustomerSiteNote, url: '/Customer/CreateCustomerSiteNote', successToast: 'Note added successfully', errorToast: 'Error while adding new site note' };
            return $httpService.$post(Obj);
        },
        getAllCustomerSiteNotesByCustomerSiteID: function (isDisplayAll, CustomerSiteID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, CustomerSiteID: CustomerSiteID }, url: '/Customer/GetAllCustomerSiteNotesByCustomerSiteID' };
            return $httpService.$get(Obj);
        },
        removeCustomerSiteNote: function (ID) {
            var Obj = { url: '/Customer/DeleteCustomerSiteNote/' + ID, successToast: 'Note removed successfully', errorToast: 'Error while removing existing note' };
            return $httpService.$delete(Obj);
        },

        addCustomerSiteLabourClassification: function (CustomerSiteLabourClassification) {
            var Obj = { postData: CustomerSiteLabourClassification, url: '/Customer/CreateCustomerSiteLabourClassification', successToast: null, errorToast: null };
            return $httpService.$post(Obj);
        },

        getAllCustomerSiteLabourClassificationsByCustomerSiteID: function (isDisplayAll, CustomerSiteID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, CustomerSiteID: CustomerSiteID }, url: '/Customer/GetAllCustomerSiteLabourClassificationsByCustomerSiteID' };
            return $httpService.$get(Obj);
        },
        removeCustomerSiteLabourClassification: function (ID) {
            var Obj = { url: '/Customer/DeleteCustSiteLabourClassification/' + ID, successToast: 'Labour classification removed successfully', errorToast: 'Error while removing existing labour classification' };
            return $httpService.$delete(Obj);
        },
        editCustomerSiteLabourClassification: function (CustomerSiteLabourClassification) {
            var Obj = { postData: CustomerSiteLabourClassification, url: '/Customer/UpdateCustomerSiteLabourClassification', successToast: 'Labour classification Info edited successfully', errorToast: 'Error while editing existing labour classification Info' };
            return $httpService.$put(Obj);
        },
        getPrimarySiteIDFromCustomerID: function (CustomerID) {
            var Obj = { params: { CustomerID: CustomerID }, url: '/Customer/GetPrimarySiteIDFromCustomerID' };
            return $httpService.$get(Obj);
        },
    };
})

.controller('CustomerCtrl_Add', function ($scope, $rootScope, CustomerService, $filter, $state, CFG, $window) {

    var isDisplayAll = false;
    $scope.CustomerSite = {};
    $scope.EquipmentIDs = [];

    $scope.getAllPaymentTerms = function () {
        $rootScope.IsLoading = true;
        CustomerService.getAllPaymentTerms(isDisplayAll).then(function (response) {
            $scope.PaymentTerms = response.data;
            $scope.CustomerSite.PaymentTermID = CFG.settings.defaultPaytermForCustomer;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllPaymentTerms();

    $scope.getAllSalesReps = function () {
        $rootScope.IsLoading = true;
        CustomerService.getAllSalesReps(isDisplayAll).then(function (response) {
            $scope.SalesReps = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllSalesReps();

    getAllEquipments();

    function getAllEquipments() {
        $rootScope.IsLoading = true;
        CustomerService.getAllEquipments(isDisplayAll).then(function (response) {
            $scope.Equipments = response.data;
            $rootScope.IsLoading = false;
        });
    };


    $scope.IsEquipmentChecked = function (flag, EquipmentID) {
        if (flag) {
            $scope.EquipmentIDs.push(EquipmentID);
        }
        else {
            for (var i = $scope.EquipmentIDs.length - 1; i >= 0; i--) {
                if ($scope.EquipmentIDs[i] == EquipmentID) {
                    $scope.EquipmentIDs.splice(i, 1);
                }
            }
        }
    };


    //----------------------------------Registration---------------------------

    $scope.addCustomer = function (CustomerSite) {

        var Customer = CustomerSite;   //Note:- remaining model value collected to this variable...
        Customer.EquipmentIDs = $scope.EquipmentIDs;

        if (Customer.CustomerName == null || Customer.CustomerName == '')
            return $rootScope.showNotify('Customer name field cannot be empty', 'Information', 'i');

        if (Customer.PrContactName == null || Customer.PrContactName == '')
            return $rootScope.showNotify('Contact name field cannot be empty', 'Information', 'i');

        $rootScope.IsLoading = true;
        CustomerService.addCustomer(Customer).then(function (response) {
            $scope.CustomerID = response.data;
            $rootScope.IsLoading = false;

            CustomerSite.CustomerID = $scope.CustomerID;
            CustomerSite.SiteName = Customer.CustomerName + ' - Default Site';
            if (CustomerSite.OTPerDay == null) {
                CustomerSite.OTPerDay = 8;
            }
            if (CustomerSite.OTPerWeek == null) {
                CustomerSite.OTPerWeek = 40;
            }

            $rootScope.IsLoading = true;
            CustomerService.addCustomerSite(CustomerSite).then(function (response) {
                $scope.CustomerSiteID = response.data;
                $rootScope.IsLoading = false;
                addCustomerSiteLabourClassification();
            });

        });
    };

    function addCustomerSiteLabourClassification() {

        var CustomerSiteLabourClassification = {
            CustomerSiteID: $scope.CustomerSiteID,
            PayRate: CFG.settings.defaultLabourRate,
            InvoiceRate: CFG.settings.defaultInvoiceRate,
        };

        $rootScope.IsLoading = true;
        CustomerService.addCustomerSiteLabourClassification(CustomerSiteLabourClassification).then(function (response) {
            $rootScope.IsLoading = false;
            var CustomerSiteNote = $scope.CustomerSiteNote;
            CustomerSiteNote.CustomerSiteID = $scope.CustomerSiteID;
            CustomerSiteNote.CustomerID = $scope.CustomerID;

            $rootScope.IsLoading = true;
            CustomerService.addCustomerSiteNote(CustomerSiteNote).then(function () {
                $rootScope.IsLoading = false;
                $state.go('app.customer.customer.edit', { id: $scope.CustomerID });
            });
        });
    };


    //----------------------------------Primary Contact---------------------------

    $scope.InStates = [];
    $scope.InCities = [];
    $scope.CustomerSite = {
        IsEnable : true
    };
    $scope.CustomerSiteNote = {};
    getAllCountries();

    function getAllCountries() {
        $rootScope.IsLoading = true;
        CustomerService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;

            $scope.CustomerSite.PrCountryID = CFG.settings.mainCountry;
            $scope.CustomerSite.PrStateID = CFG.settings.mainState;
            $scope.CustomerSite.PrCityID = CFG.settings.mainCity;

            $scope.CustomerSite.InCountryID = CFG.settings.mainCountry;
            $scope.CustomerSite.InStateID = CFG.settings.mainState;
            $scope.CustomerSite.InCityID = CFG.settings.mainCity;

            $scope.getStatesByCountryIDForPrimary(CFG.settings.mainCountry);
            $scope.getCitiesByStateIDForPrimary(CFG.settings.mainState);
            $scope.getStatesByCountryIDForInvoice(CFG.settings.mainCountry);
            $scope.getCitiesByStateIDForInvoice(CFG.settings.mainState);
        });
    };

    $scope.getStatesByCountryIDForPrimary = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.PrStates = response.data;
                $rootScope.IsLoading = false;

                $scope.InStates = $filter('filter')($scope.PrStates, { CountryID: CountryID });
            });
        }
    };

    $scope.getCitiesByStateIDForPrimary = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.PrCities = response.data;
                $rootScope.IsLoading = false;

                $scope.InCities = $filter('filter')($scope.PrCities, { StateID: StateID });
            });
        }
    };

    $scope.getPrZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.CustomerSite.PrCountryID }, true);
        var state = $filter('filter')($scope.PrStates, { StateID: $scope.CustomerSite.PrStateID }, true);
        var city = $filter('filter')($scope.PrCities, { CityID: $scope.CustomerSite.PrCityID }, true);

        var address = $scope.CustomerSite.PrAddress1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.CustomerSite.PrPostalCode = i.long_name;
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
   

    //----------------------------------Invoice Contact---------------------------

    $scope.getStatesByCountryIDForInvoice = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.InStates = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getCitiesByStateIDForInvoice = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.InCities = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getInZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.CustomerSite.InCountryID }, true);
        var state = $filter('filter')($scope.InStates, { StateID: $scope.CustomerSite.InStateID }, true);
        var city = $filter('filter')($scope.InCities, { CityID: $scope.CustomerSite.InCityID }, true);

        var address = $scope.CustomerSite.InAddress1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.CustomerSite.InPostalCode = i.long_name;
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

    $scope.sameAsPrimary = function (IsChecked) {
        if (IsChecked) {
            $scope.CustomerSite.InContactName = $scope.CustomerSite.PrContactName;
            $scope.CustomerSite.InAddress1 = $scope.CustomerSite.PrAddress1;
            $scope.CustomerSite.InAddress2 = $scope.CustomerSite.PrAddress2;
            $scope.CustomerSite.InCountryID = $scope.CustomerSite.PrCountryID;
            $scope.CustomerSite.InStateID = $scope.CustomerSite.PrStateID;
            $scope.CustomerSite.InCityID = $scope.CustomerSite.PrCityID;
            $scope.CustomerSite.InPostalCode = $scope.CustomerSite.PrPostalCode;
            $scope.CustomerSite.InPhone = $scope.CustomerSite.PrPhone;
            $scope.CustomerSite.InFax = $scope.CustomerSite.PrFax;
            $scope.CustomerSite.InEmail = $scope.CustomerSite.PrEmail;
            $scope.CustomerSite.InMobile = $scope.CustomerSite.PrMobile;
        }
        else {
            $scope.CustomerSite.InContactName = null;
            $scope.CustomerSite.InAddress1 = null;
            $scope.CustomerSite.InAddress2 = null;
            $scope.CustomerSite.InCountryID = null;
            $scope.CustomerSite.InStateID = null;
            $scope.CustomerSite.InCityID = null;
            $scope.CustomerSite.InPostalCode = null;
            $scope.CustomerSite.InPhone = null;
            $scope.CustomerSite.InFax = null;
            $scope.CustomerSite.InEmail = null;
            $scope.CustomerSite.InMobile = null;
        }
    };
})

.controller('CustomerCtrl_Edit', function ($scope, $rootScope, $stateParams, $filter, CustomerService, $window, $state, CFG) {

    var isDisplayAll = false;

    $scope.CustomerID = $stateParams.id;
    $scope.EquipmentIDs = [];


    $scope.getAllPaymentTerms = function () {
        $rootScope.IsLoading = true;
        CustomerService.getAllPaymentTerms(isDisplayAll).then(function (response) {
            $scope.PaymentTerms = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllPaymentTerms();

    $scope.getAllSalesReps = function () {
        $rootScope.IsLoading = true;
        CustomerService.getAllSalesReps(isDisplayAll).then(function (response) {
            $scope.SalesReps = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllSalesReps();


    getAllEquipments();

    function getAllEquipments() {
        $rootScope.IsLoading = true;
        CustomerService.getAllEquipments(isDisplayAll).then(function (response) {
            $scope.Equipments = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.IsEquipmentChecked = function (flag, EquipmentID) {
        if (flag) {
            $scope.EquipmentIDs.push(EquipmentID);
        }
        else {
            for (var i = $scope.EquipmentIDs.length - 1; i >= 0; i--) {
                if ($scope.EquipmentIDs[i] == EquipmentID) {
                    $scope.EquipmentIDs.splice(i, 1);
                }
            }
        }
    };

    getCustomerByID($scope.CustomerID);

    function getCustomerByID(CustomerID) {
        $rootScope.IsLoading = true;
        CustomerService.getCustomerByID(CustomerID).then(function (response) {
            $scope.Customer = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.editCustomer = function (Customer) {

        if (Customer.CustomerName == null || Customer.CustomerName == '')
            return $rootScope.showNotify('Customer name field cannot be empty', 'Information', 'i');

        Customer.CustomerID = $scope.CustomerID;
        Customer.EquipmentIDs = $scope.EquipmentIDs;
        $rootScope.IsLoading = true;
        CustomerService.editCustomer(Customer).then(function () {
            updateContract($scope.CustomerSite);
            $rootScope.IsLoading = false;
        });
    };

    function updateContract(CustomerSite) {

        if (CustomerSite.PrContactName == null || CustomerSite.PrContactName == '')
            return $rootScope.showNotify('Contact name field cannot be empty', 'Information', 'i');

        CustomerSite.CustomerSiteID = $scope.CustomerSiteID;

        $rootScope.IsLoading = true;
        CustomerService.editContact(CustomerSite).then(function () {
            $rootScope.IsLoading = false;
            updateOvertime(CustomerSite);
        });
    };

    $rootScope.IsLoading = true;
    CustomerService.getPrimarySiteIDFromCustomerID($scope.CustomerID).then(function (response) {
        $scope.CustomerSiteID = response.data;
        if ($scope.CustomerSiteID > 0)
            getCustomerSiteByID($scope.CustomerSiteID);
    });    

    function getCustomerSiteByID(CustomerSiteID) {
        $rootScope.IsLoading = true;
        CustomerService.getCustomerSiteByID(CustomerSiteID).then(function (response) {
            $scope.CustomerSite = response.data;

            $scope.prCntID = $scope.CustomerSite.PrCountryID == 0 ? CFG.settings.mainCountry : $scope.CustomerSite.PrCountryID;
            $scope.prStID = $scope.CustomerSite.PrStateID == 0 ? CFG.settings.mainState : $scope.CustomerSite.PrStateID;
            $scope.prCtID = $scope.CustomerSite.PrCityID == 0 ? CFG.settings.mainCity : $scope.CustomerSite.PrCityID;

            $scope.CustomerSite.PrCountryID = $scope.prCntID;
            $scope.CustomerSite.PrStateID = $scope.prStID;
            $scope.CustomerSite.PrCityID = $scope.prCtID;

            $scope.inCntID = $scope.CustomerSite.InCountryID == 0 ? CFG.settings.mainCountry : $scope.CustomerSite.InCountryID;
            $scope.inStID = $scope.CustomerSite.InStateID == 0 ? CFG.settings.mainState : $scope.CustomerSite.InStateID;
            $scope.inCtID = $scope.CustomerSite.InCityID == 0 ? CFG.settings.mainCity : $scope.CustomerSite.InCityID;

            $scope.CustomerSite.InCountryID = $scope.inCntID;
            $scope.CustomerSite.InStateID = $scope.inStID;
            $scope.CustomerSite.InCityID = $scope.inCtID;

            //$rootScope.CustSiteID = 0;

            //getCustomerByID($scope.CustomerSite.CustomerID);
            getAllCountries($scope.prCntID, $scope.prStID, $scope.inCntID, $scope.inStID);
            getLabourClassificationsByCustomerSiteID($scope.CustomerSiteID);
            getNotesByCustomerSiteID($scope.CustomerSiteID);
            $rootScope.IsLoading = false;
        });
    };

    $scope.InStates = [];
    $scope.InCities = [];

    function getAllCountries(prCntID, prStID, inCntID, inStID) {
        $rootScope.IsLoading = true;
        CustomerService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;

            $scope.getStatesByCountryIDForPrimary(prCntID);
            $scope.getCitiesByStateIDForPrimary(prStID);
            $scope.getStatesByCountryIDForInvoice(inCntID);
            $scope.getCitiesByStateIDForInvoice(inStID);
        });
    };

    $scope.getStatesByCountryIDForPrimary = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.PrStates = response.data;
                $rootScope.IsLoading = false;

                $scope.InStates = $filter('filter')($scope.PrStates, { CountryID: CountryID });
            });
        }
    };

    $scope.getCitiesByStateIDForPrimary = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.PrCities = response.data;
                $rootScope.IsLoading = false;

                $scope.InCities = $filter('filter')($scope.PrCities, { StateID: StateID });
            });
        }
    };

    $scope.getPrZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.CustomerSite.PrCountryID }, true);
        var state = $filter('filter')($scope.PrStates, { StateID: $scope.CustomerSite.PrStateID }, true);
        var city = $filter('filter')($scope.PrCities, { CityID: $scope.CustomerSite.PrCityID }, true);

        var address = $scope.CustomerSite.PrAddress1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.CustomerSite.PrPostalCode = i.long_name;
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

    //----------------------------------Invoice Contact---------------------------

    $scope.getStatesByCountryIDForInvoice = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.InStates = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getCitiesByStateIDForInvoice = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.InCities = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.sameAsPrimary = function (IsChecked) {
        if (IsChecked) {
            $scope.CustomerSite.InContactName = $scope.CustomerSite.PrContactName;
            $scope.CustomerSite.InAddress1 = $scope.CustomerSite.PrAddress1;
            $scope.CustomerSite.InAddress2 = $scope.CustomerSite.PrAddress2;
            $scope.CustomerSite.InCountryID = $scope.CustomerSite.PrCountryID;
            $scope.CustomerSite.InStateID = $scope.CustomerSite.PrStateID;
            $scope.CustomerSite.InCityID = $scope.CustomerSite.PrCityID;
            $scope.CustomerSite.InPostalCode = $scope.CustomerSite.PrPostalCode;
            $scope.CustomerSite.InPhone = $scope.CustomerSite.PrPhone;
            $scope.CustomerSite.InFax = $scope.CustomerSite.PrFax;
            $scope.CustomerSite.InEmail = $scope.CustomerSite.PrEmail;
            $scope.CustomerSite.InMobile = $scope.CustomerSite.PrMobile;
        }
        else {
            $scope.CustomerSite.InContactName = null;
            $scope.CustomerSite.InAddress1 = null;
            $scope.CustomerSite.InAddress2 = null;
            $scope.CustomerSite.InCountryID = null;
            $scope.CustomerSite.InStateID = null;
            $scope.CustomerSite.InCityID = null;
            $scope.CustomerSite.InPostalCode = null;
            $scope.CustomerSite.InPhone = null;
            $scope.CustomerSite.InFax = null;
            $scope.CustomerSite.InEmail = null;
            $scope.CustomerSite.InMobile = null;
        }
    };

    $scope.getInZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.CustomerSite.InCountryID }, true);
        var state = $filter('filter')($scope.InStates, { StateID: $scope.CustomerSite.InStateID }, true);
        var city = $filter('filter')($scope.InCities, { CityID: $scope.CustomerSite.InCityID }, true);

        var address = $scope.CustomerSite.InAddress1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.CustomerSite.InPostalCode = i.long_name;
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

    //-------------------------------------------Labour Classifications-------------------------------------

    $scope.tempCustomerSiteLabourClassificationID = 0;

    $scope.getLabourClassificationsByCustomerSiteID = function () {
        getLabourClassificationsByCustomerSiteID($scope.CustomerSiteID);
    };

    $scope.editRow = function (CustomerSiteLabourClassificationID) {
        $scope.tempCustomerSiteLabourClassificationID = CustomerSiteLabourClassificationID;
    };

    function getLabourClassificationsByCustomerSiteID(CustomerSiteID) {
        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCustomerSiteLabourClassificationsByCustomerSiteID(isDisplayAll, $scope.CustomerSiteID).then(function (response) {
                $scope.CustomerSiteLabourClassifications = response.data;
                $scope.FilteredLabourClassifications = $scope.CustomerSiteLabourClassifications;

                if (response.data.length > 0)
                    $scope.isDisplayLabourClassifications = true;
                else
                    $scope.isDisplayLabourClassifications = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.removeCustomerSiteLabourClassification = function (CustomerSiteLabourClassificationID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            CustomerService.removeCustomerSiteLabourClassification(CustomerSiteLabourClassificationID).then(function () {
                $rootScope.IsLoading = false;
                getLabourClassificationsByCustomerSiteID($scope.CustomerSiteID);
            });
        }

    };

    $scope.editCustomerSiteLabourClassification = function (CustomerSiteLabourClassificationID, PayRate, InvoiceRate) {

        if (PayRate == null || PayRate == '')
            return $rootScope.showNotify('Pay rate field cannot be empty', 'Information', 'i');

        if (InvoiceRate == null || InvoiceRate == '')
            return $rootScope.showNotify('Invoice rate field cannot be empty', 'Information', 'i');

        var CustomerSiteLabourClassification = {
            CustomerSiteLabourClassificationID: CustomerSiteLabourClassificationID,
            PayRate: PayRate,
            InvoiceRate: InvoiceRate
        };

        // $rootScope.IsLoading = true;
        CustomerService.editCustomerSiteLabourClassification(CustomerSiteLabourClassification).then(function () {
            //  $rootScope.IsLoading = false;
            $scope.tempCustomerSiteLabourClassificationID = 0;
        });
    };

    $scope.getFilteredLabourClassifications = function (searchLabourClassification) {
        if (searchLabourClassification.length > 0) {
            $scope.FilteredLabourClassifications = $filter('filter')($scope.CustomerSiteLabourClassifications, { LabourClassificationName: searchLabourClassification });
        }
        else {
            $scope.FilteredLabourClassifications = $scope.CustomerSiteLabourClassifications;
        }
    };

    //-------------------------------------------Overtime-------------------------------------

    function updateOvertime(CustomerSite) {

        if (CustomerSite.CertificateNo == null || CustomerSite.CertificateNo == '')
            return $rootScope.showNotify('Certificate # field cannot be empty', 'Information', 'i');

        CustomerSite.CustomerSiteID = $scope.CustomerSiteID;

        $rootScope.IsLoading = true;
        CustomerService.editOvertime(CustomerSite).then(function () {
            $rootScope.IsLoading = false;
        });

    };

    //-------------------------------------------Reminder-------------------------------------

    $scope.editReminder = function (CustomerSite) {

        if (CustomerSite.Reminder == null || CustomerSite.Reminder == '')
            return $rootScope.showNotify('Reminder field cannot be empty', 'Information', 'i');

        CustomerSite.CustomerSiteID = $scope.CustomerSiteID;

        $rootScope.IsLoading = true;
        CustomerService.editReminder(CustomerSite).then(function () {
            $rootScope.IsLoading = false;
        });

    };

    //-------------------------------------------Notes Info---------------------------

    $scope.getNotesByCustomerSiteID = function () {
        getNotesByCustomerSiteID($scope.CustomerSiteID);
    };

    $scope.addCustomerSiteNote = function (CustomerSiteNote) {

        if (CustomerSiteNote.Note == null || CustomerSiteNote.Note == '')
            return $rootScope.showNotify('Note field cannot be empty', 'Information', 'i');

        CustomerSiteNote.CustomerSiteID = $scope.CustomerSiteID;
        CustomerSiteNote.CustomerID = $scope.CustomerID;
        $rootScope.IsLoading = true;
        CustomerService.addCustomerSiteNote(CustomerSiteNote).then(function () {
            $scope.CustomerSiteNote.Note = null;
            $rootScope.IsLoading = false;
            getNotesByCustomerSiteID($scope.CustomerSiteID);
        });
    };

    function getNotesByCustomerSiteID(CustomerSiteID) {
        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCustomerSiteNotesByCustomerSiteID(isDisplayAll, CustomerSiteID).then(function (response) {
                $scope.CustomerSiteNotes = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayNotes = true;
                else
                    $scope.isDisplayNotes = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.removeCustomerSiteNote = function (CustomerSiteNoteID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            CustomerService.removeCustomerSiteNote(CustomerSiteNoteID).then(function () {
                $rootScope.IsLoading = false;
                getNotesByCustomerSiteID($scope.CustomerSiteID);
            });
        }
    };
})

.controller('CustomerSiteCtrl_Add', function ($scope, $rootScope, $state, $stateParams, CustomerService, $filter, CFG, $window) {

    var isDisplayAll = false;

    $scope.CustomerID = $stateParams.id;;

    getCustomerByID($scope.CustomerID);

    function getCustomerByID(CustomerID) {
        $rootScope.IsLoading = true;
        CustomerService.getCustomerByID(CustomerID).then(function (response) {
            $scope.Customer = response.data;
            $rootScope.IsLoading = false;
        });
    };

    //----------------------------------Primary Contact---------------------------

    $scope.InStates = [];
    $scope.InCities = [];
    $scope.CustomerSite = {};
    getAllCountries();

    function getAllCountries() {
        $rootScope.IsLoading = true;
        CustomerService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;

            $scope.CustomerSite.PrCountryID = CFG.settings.mainCountry;
            $scope.CustomerSite.PrStateID = CFG.settings.mainState;
            $scope.CustomerSite.PrCityID = CFG.settings.mainCity;

            $scope.CustomerSite.InCountryID = CFG.settings.mainCountry;
            $scope.CustomerSite.InStateID = CFG.settings.mainState;
            $scope.CustomerSite.InCityID = CFG.settings.mainCity;

            $scope.getStatesByCountryIDForPrimary(CFG.settings.mainCountry);
            $scope.getCitiesByStateIDForPrimary(CFG.settings.mainState);
            $scope.getStatesByCountryIDForInvoice(CFG.settings.mainCountry);
            $scope.getCitiesByStateIDForInvoice(CFG.settings.mainState);
        });
    };

    $scope.getStatesByCountryIDForPrimary = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.PrStates = response.data;
                $rootScope.IsLoading = false;

                $scope.InStates = $filter('filter')($scope.PrStates, { CountryID: CountryID });
            });
        }
    };

    $scope.getCitiesByStateIDForPrimary = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.PrCities = response.data;
                $rootScope.IsLoading = false;

                $scope.InCities = $filter('filter')($scope.PrCities, { StateID: StateID });
            });
        }
    };

    $scope.getPrZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.CustomerSite.PrCountryID }, true);
        var state = $filter('filter')($scope.PrStates, { StateID: $scope.CustomerSite.PrStateID }, true);
        var city = $filter('filter')($scope.PrCities, { CityID: $scope.CustomerSite.PrCityID }, true);

        var address = $scope.CustomerSite.PrAddress1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.CustomerSite.PrPostalCode = i.long_name;
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
   

    //----------------------------------Invoice Contact---------------------------

    $scope.getStatesByCountryIDForInvoice = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.InStates = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getCitiesByStateIDForInvoice = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.InCities = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getInZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.CustomerSite.InCountryID }, true);
        var state = $filter('filter')($scope.InStates, { StateID: $scope.CustomerSite.InStateID }, true);
        var city = $filter('filter')($scope.InCities, { CityID: $scope.CustomerSite.InCityID }, true);

        var address = $scope.CustomerSite.InAddress1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.CustomerSite.InPostalCode = i.long_name;
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

    $scope.sameAsPrimary = function (IsChecked) {
        if (IsChecked) {
            $scope.CustomerSite.InContactName = $scope.CustomerSite.PrContactName;
            $scope.CustomerSite.InAddress1 = $scope.CustomerSite.PrAddress1;
            $scope.CustomerSite.InAddress2 = $scope.CustomerSite.PrAddress2;
            $scope.CustomerSite.InCountryID = $scope.CustomerSite.PrCountryID;
            $scope.CustomerSite.InStateID = $scope.CustomerSite.PrStateID;
            $scope.CustomerSite.InCityID = $scope.CustomerSite.PrCityID;
            $scope.CustomerSite.InPostalCode = $scope.CustomerSite.PrPostalCode;
            $scope.CustomerSite.InPhone = $scope.CustomerSite.PrPhone;
            $scope.CustomerSite.InFax = $scope.CustomerSite.PrFax;
            $scope.CustomerSite.InEmail = $scope.CustomerSite.PrEmail;
            $scope.CustomerSite.InMobile = $scope.CustomerSite.PrMobile;
        }
        else {
            $scope.CustomerSite.InContactName = null;
            $scope.CustomerSite.InAddress1 = null;
            $scope.CustomerSite.InAddress2 = null;
            $scope.CustomerSite.InCountryID = null;
            $scope.CustomerSite.InStateID = null;
            $scope.CustomerSite.InCityID = null;
            $scope.CustomerSite.InPostalCode = null;
            $scope.CustomerSite.InPhone = null;
            $scope.CustomerSite.InFax = null;
            $scope.CustomerSite.InEmail = null;
            $scope.CustomerSite.InMobile = null;
        }
    };

    $scope.addCustomerSite = function (CustomerSite) {

        if (CustomerSite.PrContactName == null || CustomerSite.PrContactName == '')
            return $rootScope.showNotify('Contact name field cannot be empty', 'Information', 'i');

        CustomerSite.CustomerID = $scope.CustomerID;
        CustomerSite.OTPerDay = 8;
        CustomerSite.OTPerWeek = 40;

        $rootScope.IsLoading = true;
        CustomerService.addCustomerSite(CustomerSite).then(function (response) {
            $scope.CustomerSiteID = response.data;
            $rootScope.IsLoading = false;
            addCustomerSiteLabourClassification();
        });
    };

    function addCustomerSiteLabourClassification() {

        var CustomerSiteLabourClassification = {
            CustomerSiteID: $scope.CustomerSiteID,
            PayRate: CFG.settings.defaultLabourRate,
            InvoiceRate: CFG.settings.defaultInvoiceRate,
        };

        $rootScope.IsLoading = true;
        CustomerService.addCustomerSiteLabourClassification(CustomerSiteLabourClassification).then(function (response) {
            $rootScope.IsLoading = false;
            $state.go('app.customer.customer.sites.edit', { id: $scope.CustomerSiteID });
        });
    };
})

.controller('CustomerSiteCtrl_Edit', function ($scope, $rootScope, $stateParams, CustomerService, $filter, CFG, $window, siteID) {

    var isDisplayAll = false;

    $scope.CustomerSiteID = siteID.data;

    if ($scope.CustomerSiteID > 0)
        getCustomerSiteByID($scope.CustomerSiteID);

    function getCustomerSiteByID (CustomerSiteID) {
        $rootScope.IsLoading = true;
        CustomerService.getCustomerSiteByID(CustomerSiteID).then(function (response) {
            $scope.CustomerSite = response.data;

            $scope.prCntID = $scope.CustomerSite.PrCountryID == 0 ? CFG.settings.mainCountry : $scope.CustomerSite.PrCountryID;
            $scope.prStID = $scope.CustomerSite.PrStateID == 0 ? CFG.settings.mainState : $scope.CustomerSite.PrStateID;
            $scope.prCtID = $scope.CustomerSite.PrCityID == 0 ? CFG.settings.mainCity : $scope.CustomerSite.PrCityID;

            $scope.CustomerSite.PrCountryID = $scope.prCntID;
            $scope.CustomerSite.PrStateID = $scope.prStID;
            $scope.CustomerSite.PrCityID = $scope.prCtID;

            $scope.inCntID = $scope.CustomerSite.InCountryID == 0 ? CFG.settings.mainCountry : $scope.CustomerSite.InCountryID;
            $scope.inStID = $scope.CustomerSite.InStateID == 0 ? CFG.settings.mainState : $scope.CustomerSite.InStateID;
            $scope.inCtID = $scope.CustomerSite.InCityID == 0 ? CFG.settings.mainCity : $scope.CustomerSite.InCityID;

            $scope.CustomerSite.InCountryID = $scope.inCntID;
            $scope.CustomerSite.InStateID = $scope.inStID;
            $scope.CustomerSite.InCityID = $scope.inCtID;

            $rootScope.CustSiteID = 0;

            getCustomerByID($scope.CustomerSite.CustomerID);
            getAllCountries($scope.prCntID, $scope.prStID, $scope.inCntID, $scope.inStID);
            getLabourClassificationsByCustomerSiteID($scope.CustomerSiteID);
            getNotesByCustomerSiteID($scope.CustomerSiteID);
            $rootScope.IsLoading = false;
        });
    };

    if ($rootScope.CustSiteID > 0)
        $scope.getCustomerSiteByID($rootScope.CustSiteID);
    

    function getCustomerByID(CustomerID) {
        $rootScope.IsLoading = true;
        CustomerService.getCustomerByID(CustomerID).then(function (response) {
            $scope.Customer = response.data;
            $rootScope.IsLoading = false;
        });
    };


    //----------------------------------Primary Contact---------------------------

    $scope.InStates = [];
    $scope.InCities = [];

    function getAllCountries(prCntID, prStID, inCntID, inStID) {
        $rootScope.IsLoading = true;
        CustomerService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;

            $scope.getStatesByCountryIDForPrimary(prCntID);
            $scope.getCitiesByStateIDForPrimary(prStID);
            $scope.getStatesByCountryIDForInvoice(inCntID);
            $scope.getCitiesByStateIDForInvoice(inStID);
        });
    };

    $scope.getStatesByCountryIDForPrimary = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.PrStates = response.data;
                $rootScope.IsLoading = false;

                $scope.InStates = $filter('filter')($scope.PrStates, { CountryID: CountryID });
            });
        }
    };

    $scope.getCitiesByStateIDForPrimary = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.PrCities = response.data;
                $rootScope.IsLoading = false;

                $scope.InCities = $filter('filter')($scope.PrCities, { StateID: StateID });
            });
        }
    };

    $scope.getPrZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.CustomerSite.PrCountryID }, true);
        var state = $filter('filter')($scope.PrStates, { StateID: $scope.CustomerSite.PrStateID }, true);
        var city = $filter('filter')($scope.PrCities, { CityID: $scope.CustomerSite.PrCityID }, true);

        var address = $scope.CustomerSite.PrAddress1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.CustomerSite.PrPostalCode = i.long_name;
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

    $scope.editContact = function (CustomerSite) {

        if (CustomerSite.PrContactName == null || CustomerSite.PrContactName == '')
            return $rootScope.showNotify('Contact name field cannot be empty', 'Information', 'i');

        CustomerSite.CustomerSiteID = $scope.CustomerSiteID;

        $rootScope.IsLoading = true;
        CustomerService.editContact(CustomerSite).then(function () {
            $rootScope.IsLoading = false;
        });
    };

    //----------------------------------Invoice Contact---------------------------

    $scope.getStatesByCountryIDForInvoice = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.InStates = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getCitiesByStateIDForInvoice = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.InCities = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.sameAsPrimary = function (IsChecked) {
        if (IsChecked) {
            $scope.CustomerSite.InContactName = $scope.CustomerSite.PrContactName;
            $scope.CustomerSite.InAddress1 = $scope.CustomerSite.PrAddress1;
            $scope.CustomerSite.InAddress2 = $scope.CustomerSite.PrAddress2;
            $scope.CustomerSite.InCountryID = $scope.CustomerSite.PrCountryID;
            $scope.CustomerSite.InStateID = $scope.CustomerSite.PrStateID;
            $scope.CustomerSite.InCityID = $scope.CustomerSite.PrCityID;
            $scope.CustomerSite.InPostalCode = $scope.CustomerSite.PrPostalCode;
            $scope.CustomerSite.InPhone = $scope.CustomerSite.PrPhone;
            $scope.CustomerSite.InFax = $scope.CustomerSite.PrFax;
            $scope.CustomerSite.InEmail = $scope.CustomerSite.PrEmail;
            $scope.CustomerSite.InMobile = $scope.CustomerSite.PrMobile;
        }
        else {
            $scope.CustomerSite.InContactName = null;
            $scope.CustomerSite.InAddress1 = null;
            $scope.CustomerSite.InAddress2 = null;
            $scope.CustomerSite.InCountryID = null;
            $scope.CustomerSite.InStateID = null;
            $scope.CustomerSite.InCityID = null;
            $scope.CustomerSite.InPostalCode = null;
            $scope.CustomerSite.InPhone = null;
            $scope.CustomerSite.InFax = null;
            $scope.CustomerSite.InEmail = null;
            $scope.CustomerSite.InMobile = null;
        }
    };

    $scope.getInZipCode = function () {

        var geocoder = new google.maps.Geocoder;
        $rootScope.IsLoading = true;

        var country = $filter('filter')($scope.Countries, { CountryID: $scope.CustomerSite.InCountryID }, true);
        var state = $filter('filter')($scope.InStates, { StateID: $scope.CustomerSite.InStateID }, true);
        var city = $filter('filter')($scope.InCities, { CityID: $scope.CustomerSite.InCityID }, true);

        var address = $scope.CustomerSite.InAddress1 + ', ' + city[0].CityName + ', ' + state[0].StateName + ', ' + country[0].CountryName;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    $scope.address_components = results[0].address_components;
                    $scope.address_components.forEach(function (i) {
                        if (i.types[0] == "postal_code") {
                            $rootScope.IsLoading = false;
                            $scope.CustomerSite.InPostalCode = i.long_name;
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

    //-------------------------------------------Labour Classifications-------------------------------------

    $scope.tempCustomerSiteLabourClassificationID = 0;

    $scope.getLabourClassificationsByCustomerSiteID = function () {
        getLabourClassificationsByCustomerSiteID($scope.CustomerSiteID);
    };

    $scope.editRow = function (CustomerSiteLabourClassificationID) {
        $scope.tempCustomerSiteLabourClassificationID = CustomerSiteLabourClassificationID;
    };

    function getLabourClassificationsByCustomerSiteID(CustomerSiteID) {
        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCustomerSiteLabourClassificationsByCustomerSiteID(isDisplayAll, $scope.CustomerSiteID).then(function (response) {
                $scope.CustomerSiteLabourClassifications = response.data;
                $scope.FilteredLabourClassifications = $scope.CustomerSiteLabourClassifications;

                if (response.data.length > 0)
                    $scope.isDisplayLabourClassifications = true;
                else
                    $scope.isDisplayLabourClassifications = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.removeCustomerSiteLabourClassification = function (CustomerSiteLabourClassificationID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            CustomerService.removeCustomerSiteLabourClassification(CustomerSiteLabourClassificationID).then(function () {
                $rootScope.IsLoading = false;
                getLabourClassificationsByCustomerSiteID($scope.CustomerSiteID);
            });
        }

    };

    $scope.editCustomerSiteLabourClassification = function (CustomerSiteLabourClassificationID, PayRate, InvoiceRate) {

        if (PayRate == null || PayRate == '')
            return $rootScope.showNotify('Pay rate field cannot be empty', 'Information', 'i');

        if (InvoiceRate == null || InvoiceRate == '')
            return $rootScope.showNotify('Invoice rate field cannot be empty', 'Information', 'i');

        var CustomerSiteLabourClassification = {
            CustomerSiteLabourClassificationID: CustomerSiteLabourClassificationID,
            PayRate: PayRate,
            InvoiceRate: InvoiceRate
        };

       // $rootScope.IsLoading = true;
        CustomerService.editCustomerSiteLabourClassification(CustomerSiteLabourClassification).then(function () {
          //  $rootScope.IsLoading = false;
            $scope.tempCustomerSiteLabourClassificationID = 0;
        });
    };

    $scope.getFilteredLabourClassifications = function (searchLabourClassification) {
        if (searchLabourClassification.length > 0) {
            $scope.FilteredLabourClassifications = $filter('filter')($scope.CustomerSiteLabourClassifications, { LabourClassificationName: searchLabourClassification });
        }
        else {
            $scope.FilteredLabourClassifications = $scope.CustomerSiteLabourClassifications;
        }
    };

    //-------------------------------------------Overtime-------------------------------------

    $scope.editOvertime = function (CustomerSite) {

        if (CustomerSite.CertificateNo == null || CustomerSite.CertificateNo == '')
            return $rootScope.showNotify('Certificate # field cannot be empty', 'Information', 'i');

        CustomerSite.CustomerSiteID = $scope.CustomerSiteID;

        $rootScope.IsLoading = true;
        CustomerService.editOvertime(CustomerSite).then(function () {
            $rootScope.IsLoading = false;
        });

    };

    //-------------------------------------------Reminder-------------------------------------

    $scope.editReminder = function (CustomerSite) {

        if (CustomerSite.Reminder == null || CustomerSite.Reminder == '')
            return $rootScope.showNotify('Reminder field cannot be empty', 'Information', 'i');

        CustomerSite.CustomerSiteID = $scope.CustomerSiteID;

        $rootScope.IsLoading = true;
        CustomerService.editReminder(CustomerSite).then(function () {
            $rootScope.IsLoading = false;
        });

    };

    //-------------------------------------------Notes Info---------------------------

    $scope.getNotesByCustomerSiteID = function () {
        getNotesByCustomerSiteID($scope.CustomerSiteID);
    };

    $scope.addCustomerSiteNote = function (CustomerSiteNote) {

        if (CustomerSiteNote.Note == null || CustomerSiteNote.Note == '')
            return $rootScope.showNotify('Note field cannot be empty', 'Information', 'i');

        CustomerSiteNote.CustomerSiteID = $scope.CustomerSiteID;
        $rootScope.IsLoading = true;
        CustomerService.addCustomerSiteNote(CustomerSiteNote).then(function () {
            $scope.CustomerSiteNote.Note = null;
            $rootScope.IsLoading = false;
            getNotesByCustomerSiteID($scope.CustomerSiteID);
        });
    };

    function getNotesByCustomerSiteID(CustomerSiteID) {
        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCustomerSiteNotesByCustomerSiteID(isDisplayAll, CustomerSiteID).then(function (response) {
                $scope.CustomerSiteNotes = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayNotes = true;
                else
                    $scope.isDisplayNotes = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.removeCustomerSiteNote = function (CustomerSiteNoteID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            CustomerService.removeCustomerSiteNote(CustomerSiteNoteID).then(function () {
                $rootScope.IsLoading = false;
                getNotesByCustomerSiteID($scope.CustomerSiteID);
            });
        }
    };
})

.controller('CustomerSiteCtrl_Detail', function ($scope, $rootScope, $stateParams, CustomerService, siteID) {

    var isDisplayAll = false;
    $scope.CustomerSiteID = siteID.data;

    if ($scope.CustomerSiteID > 0)
        getCustomerSiteByID($scope.CustomerSiteID);


    function getCustomerSiteByID(CustomerSiteID) {
        $rootScope.IsLoading = true;
        CustomerService.getCustomerSiteByID(CustomerSiteID).then(function (response) {
            $scope.CustomerSite = response.data;
            $scope.CustomerSiteID = $scope.CustomerSite.CustomerSiteID;

            $rootScope.CustSiteID = 0;


            getCustomerByID($scope.CustomerSite.CustomerID);
            getLabourClassificationsByCustomerSiteID($scope.CustomerSiteID);
            getNotesByCustomerSiteID($scope.CustomerSiteID);
            $rootScope.IsLoading = false;
        });
    };

    function getCustomerByID(CustomerID) {
        $rootScope.IsLoading = true;
        CustomerService.getCustomerByID(CustomerID).then(function (response) {
            $scope.Customer = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getLabourClassificationsByCustomerSiteID = function () {
        getLabourClassificationsByCustomerSiteID($scope.CustomerSiteID);
    };

    function getLabourClassificationsByCustomerSiteID(CustomerSiteID) {
        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCustomerSiteLabourClassificationsByCustomerSiteID(isDisplayAll, $scope.CustomerSiteID).then(function (response) {
                $scope.CustomerSiteLabourClassifications = response.data;
                $scope.FilteredLabourClassifications = $scope.CustomerSiteLabourClassifications;

                if (response.data.length > 0)
                    $scope.isDisplayLabourClassifications = true;
                else
                    $scope.isDisplayLabourClassifications = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getNotesByCustomerSiteID = function () {
        getNotesByCustomerSiteID($scope.CustomerSiteID);
    };

    function getNotesByCustomerSiteID(CustomerSiteID) {
        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCustomerSiteNotesByCustomerSiteID(isDisplayAll, CustomerSiteID).then(function (response) {
                $scope.CustomerSiteNotes = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayNotes = true;
                else
                    $scope.isDisplayNotes = false;

                $rootScope.IsLoading = false;
            });
        }
    };
})

.controller('CustomerCtrl_Detail', function ($scope, $rootScope, $stateParams, $filter, CustomerService, $window, $state, CFG) {

    var isDisplayAll = false;

    $scope.CustomerID = $stateParams.id;

    $scope.getAllPaymentTerms = function () {
        $rootScope.IsLoading = true;
        CustomerService.getAllPaymentTerms(isDisplayAll).then(function (response) {
            $scope.PaymentTerms = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllPaymentTerms();

    $scope.getAllSalesReps = function () {
        $rootScope.IsLoading = true;
        CustomerService.getAllSalesReps(isDisplayAll).then(function (response) {
            $scope.SalesReps = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllSalesReps();

    getCustomerByID($scope.CustomerID);

    function getCustomerByID(CustomerID) {
        $rootScope.IsLoading = true;
        CustomerService.getCustomerByID(CustomerID).then(function (response) {
            $scope.Customer = response.data;
            $rootScope.IsLoading = false;
        });
    };

    

    

    $rootScope.IsLoading = true;
    CustomerService.getPrimarySiteIDFromCustomerID($scope.CustomerID).then(function (response) {
        $scope.CustomerSiteID = response.data;
        if ($scope.CustomerSiteID > 0)
            getCustomerSiteByID($scope.CustomerSiteID);
    });

    function getCustomerSiteByID(CustomerSiteID) {
        $rootScope.IsLoading = true;
        CustomerService.getCustomerSiteByID(CustomerSiteID).then(function (response) {
            $scope.CustomerSite = response.data;

            $scope.prCntID = $scope.CustomerSite.PrCountryID == 0 ? CFG.settings.mainCountry : $scope.CustomerSite.PrCountryID;
            $scope.prStID = $scope.CustomerSite.PrStateID == 0 ? CFG.settings.mainState : $scope.CustomerSite.PrStateID;
            $scope.prCtID = $scope.CustomerSite.PrCityID == 0 ? CFG.settings.mainCity : $scope.CustomerSite.PrCityID;

            $scope.CustomerSite.PrCountryID = $scope.prCntID;
            $scope.CustomerSite.PrStateID = $scope.prStID;
            $scope.CustomerSite.PrCityID = $scope.prCtID;

            $scope.inCntID = $scope.CustomerSite.InCountryID == 0 ? CFG.settings.mainCountry : $scope.CustomerSite.InCountryID;
            $scope.inStID = $scope.CustomerSite.InStateID == 0 ? CFG.settings.mainState : $scope.CustomerSite.InStateID;
            $scope.inCtID = $scope.CustomerSite.InCityID == 0 ? CFG.settings.mainCity : $scope.CustomerSite.InCityID;

            $scope.CustomerSite.InCountryID = $scope.inCntID;
            $scope.CustomerSite.InStateID = $scope.inStID;
            $scope.CustomerSite.InCityID = $scope.inCtID;

            //$rootScope.CustSiteID = 0;

            //getCustomerByID($scope.CustomerSite.CustomerID);
            getAllCountries($scope.prCntID, $scope.prStID, $scope.inCntID, $scope.inStID);
            getLabourClassificationsByCustomerSiteID($scope.CustomerSiteID);
            getNotesByCustomerSiteID($scope.CustomerSiteID);
            $rootScope.IsLoading = false;
        });
    };

    $scope.InStates = [];
    $scope.InCities = [];

    function getAllCountries(prCntID, prStID, inCntID, inStID) {
        $rootScope.IsLoading = true;
        CustomerService.getAllCountries(isDisplayAll).then(function (response) {
            $scope.Countries = response.data;
            $rootScope.IsLoading = false;

            $scope.getStatesByCountryIDForPrimary(prCntID);
            $scope.getCitiesByStateIDForPrimary(prStID);
            $scope.getStatesByCountryIDForInvoice(inCntID);
            $scope.getCitiesByStateIDForInvoice(inStID);
        });
    };

    $scope.getStatesByCountryIDForPrimary = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.PrStates = response.data;
                $rootScope.IsLoading = false;

                $scope.InStates = $filter('filter')($scope.PrStates, { CountryID: CountryID });
            });
        }
    };

    $scope.getCitiesByStateIDForPrimary = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.PrCities = response.data;
                $rootScope.IsLoading = false;

                $scope.InCities = $filter('filter')($scope.PrCities, { StateID: StateID });
            });
        }
    };    

    //----------------------------------Invoice Contact---------------------------

    $scope.getStatesByCountryIDForInvoice = function (CountryID) {
        if (CountryID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllStatesByCountryID(isDisplayAll, CountryID).then(function (response) {
                $scope.InStates = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getCitiesByStateIDForInvoice = function (StateID) {
        if (StateID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCitiesByStateID(isDisplayAll, StateID).then(function (response) {
                $scope.InCities = response.data;
                $rootScope.IsLoading = false;
            });
        }
    };

    //-------------------------------------------Labour Classifications-------------------------------------   

    $scope.getLabourClassificationsByCustomerSiteID = function () {
        getLabourClassificationsByCustomerSiteID($scope.CustomerSiteID);
    };

    function getLabourClassificationsByCustomerSiteID(CustomerSiteID) {
        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCustomerSiteLabourClassificationsByCustomerSiteID(isDisplayAll, $scope.CustomerSiteID).then(function (response) {
                $scope.CustomerSiteLabourClassifications = response.data;
                $scope.FilteredLabourClassifications = $scope.CustomerSiteLabourClassifications;

                if (response.data.length > 0)
                    $scope.isDisplayLabourClassifications = true;
                else
                    $scope.isDisplayLabourClassifications = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getFilteredLabourClassifications = function (searchLabourClassification) {
        if (searchLabourClassification.length > 0) {
            $scope.FilteredLabourClassifications = $filter('filter')($scope.CustomerSiteLabourClassifications, { LabourClassificationName: searchLabourClassification });
        }
        else {
            $scope.FilteredLabourClassifications = $scope.CustomerSiteLabourClassifications;
        }
    };

    //-------------------------------------------Overtime-------------------------------------

    function updateOvertime(CustomerSite) {

        if (CustomerSite.CertificateNo == null || CustomerSite.CertificateNo == '')
            return $rootScope.showNotify('Certificate # field cannot be empty', 'Information', 'i');

        CustomerSite.CustomerSiteID = $scope.CustomerSiteID;
        CustomerSite.CustomerID = $scope.CustomerID;

        $rootScope.IsLoading = true;
        CustomerService.editOvertime(CustomerSite).then(function () {
            $rootScope.IsLoading = false;
        });

    };

    //-------------------------------------------Reminder-------------------------------------

    $scope.editReminder = function (CustomerSite) {

        if (CustomerSite.Reminder == null || CustomerSite.Reminder == '')
            return $rootScope.showNotify('Reminder field cannot be empty', 'Information', 'i');

        CustomerSite.CustomerSiteID = $scope.CustomerSiteID;

        $rootScope.IsLoading = true;
        CustomerService.editReminder(CustomerSite).then(function () {
            $rootScope.IsLoading = false;
        });

    };

    //-------------------------------------------Notes Info---------------------------

    $scope.getNotesByCustomerSiteID = function () {
        getNotesByCustomerSiteID($scope.CustomerSiteID);
    };

    function getNotesByCustomerSiteID(CustomerSiteID) {
        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            CustomerService.getAllCustomerSiteNotesByCustomerSiteID(isDisplayAll, CustomerSiteID).then(function (response) {
                $scope.CustomerSiteNotes = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayNotes = true;
                else
                    $scope.isDisplayNotes = false;

                $rootScope.IsLoading = false;
            });
        }
    };
})

.controller('CustomerCtrl', function ($scope, $state, $rootScope, CustomerService, $window, $compile, $filter, $uibModal) {

    var isDisplayAll = false;
    $rootScope.CustSiteID = 0;

    $scope.PageSizes = [];
    $scope.PageSizes.push({ PageSize: 10 }, { PageSize: 25 }, { PageSize: 50 }, { PageSize: 100 });

    $scope.Customers = [];
    $scope.totalCustomers = 0;
    $scope.IsItemsFound = false;
    $scope.pageSize = 10;
    $scope.Search = {};

    $scope.loading = true;
    getCustomers($rootScope.pageNumber);
    $scope.loading = false;

    $scope.refresh = function () {
        $route.reload();
    };

    $scope.pagination = {
        current: $rootScope.pageNumber
    };

    $scope.pageChanged = function (newPage) {
        getCustomers(newPage);
    };

    $scope.getCustomers = function () {
        getCustomers(1);
    };

    function getCustomers(pageNumber) {

        $rootScope.pageNumber = pageNumber;

        $scope.Search.pageNumber = $rootScope.pageNumber;
        $scope.Search.pageSize = $scope.pageSize;

        $rootScope.IsLoading = true;
        CustomerService.getCustomers($scope.Search)
            .then(function (result) {
                $scope.Customers = result.data.Items;
                $scope.totalCustomers = result.data.Count;
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


    $scope.deleteCustomer = function (CustomerID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            CustomerService.removeCustomer(CustomerID).then(function () {
                $rootScope.IsLoading = false;
                getCustomers($rootScope.pageNumber);
            });
        }
    };

    $scope.editCustomerIsEnable = function (CustomerID, IsEnable) {

        var Customer = {};

        if (IsEnable == true) {
            Customer = {
                CustomerID: CustomerID,
                IsEnable: false
            };
        }
        else {
            Customer = {
                CustomerID: CustomerID,
                IsEnable: true
            };
        }

        $rootScope.IsLoading = true;
        CustomerService.editCustomerIsEnable(Customer).then(function () {
            $rootScope.IsLoading = false;
            getCustomers($rootScope.pageNumber);
        });
    };

    $scope.editCustomerDelinquent = function (CustomerID, Delinquent) {

        var Customer = {};

        if (Delinquent == true) {
            Customer = {
                CustomerID: CustomerID,
                Delinquent: false
            };
        }
        else {
            Customer = {
                CustomerID: CustomerID,
                Delinquent: true
            };
        }

        $rootScope.IsLoading = true;
        CustomerService.editCustomerDelinquent(Customer).then(function () {
            $rootScope.IsLoading = false;
            getCustomers($rootScope.pageNumber);
        });
    };

    $scope.goToDetails = function (CustomerID) {
        $state.go('app.customer.customer.detail', { id: CustomerID });
    };
})

.controller('CustomerSiteCtrl', function ($scope, $state, $rootScope, CustomerService, $window, $compile, $filter, $uibModal) {

    var isDisplayAll = false;
    $rootScope.CustSiteID = 0;

    $scope.PageSizes = [];
    $scope.PageSizes.push({ PageSize: 10 }, { PageSize: 25 }, { PageSize: 50 }, { PageSize: 100 });

    $scope.CustomerSites = [];
    $scope.totalCustomerSites = 0;
    $scope.IsItemsFound = false;
    $scope.pageSize = 10;
    $scope.Search = {};

    $scope.loading = true;
    getCustomerSites($rootScope.pageNumber);
    $scope.loading = false;

    $scope.refresh = function () {
        $route.reload();
    };

    $scope.pagination = {
        current: $rootScope.pageNumber
    };

    $scope.pageChanged = function (newPage) {
        getCustomerSites(newPage);
    };

    $scope.getCustomerSites = function () {
        getCustomerSites(1);
    };

    function getCustomerSites(pageNumber) {

        $rootScope.pageNumber = pageNumber;

        $scope.Search.pageNumber = $rootScope.pageNumber;
        $scope.Search.pageSize = $scope.pageSize;

        $rootScope.IsLoading = true;
        CustomerService.getCustomerSites($scope.Search)
            .then(function (result) {
                $scope.CustomerSites = result.data.Items;
                $scope.totalCustomerSites = result.data.Count;
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


    $scope.deleteCustomer = function (CustomerID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            CustomerService.removeCustomer(CustomerID).then(function () {
                $rootScope.IsLoading = false;
                getCustomerSites($rootScope.pageNumber);
            });
        }
    };

    $scope.editCustomerSiteIsEnable = function (CustomerSiteID, IsEnable) {

        var CustomerSite = {};

        if (IsEnable == true) {
            CustomerSite = {
                CustomerSiteID: CustomerSiteID,
                IsEnable: false
            };
        }
        else {
            CustomerSite = {
                CustomerSiteID: CustomerSiteID,
                IsEnable: true
            };
        }

        $rootScope.IsLoading = true;
        CustomerService.editCustomerSiteIsEnable(CustomerSite).then(function () {
            $rootScope.IsLoading = false;
            getCustomerSites($rootScope.pageNumber);
        });
    };


    $scope.goToDetails = function (CustomerID) {
        $state.go('app.customer.customer.sites.detail', { id: CustomerID });
    };
})

.directive('customerForm', function () {

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
                    customername: {
                        validators: {
                            notEmpty: {
                                message: 'The customer name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The customer name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9\s]*$/,
                                message: 'The Customer name can only consist of alphabets, numbers, space.'
                            }
                        }
                    },
                    companyname: {
                        validators: {
                            notEmpty: {
                                message: 'The company name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The company name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9\s]*$/,
                                message: 'The company name can only consist of alphabets, numbers, space.'
                            }
                        }
                    },
                    accountno: {
                        validators: {
                            notEmpty: {
                                message: 'The account no is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The account no must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9]*$/,
                                message: 'The account no can only consist of alphabets, numbers.'
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

.directive('contactForm', function () {

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
                    sitename: {
                        validators: {
                            notEmpty: {
                                message: 'The site name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The site name must be less than 250 characters long'
                            },
                        }
                    },
                    prcontactname: {
                        validators: {
                            notEmpty: {
                                message: 'The contact name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The contact name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9\s]*$/,
                                message: 'The contact name can only consist of alphabets, numbers, space.'
                            }
                        }
                    },
                    praddress1: {
                        validators: {
                            notEmpty: {
                                message: 'The address is required'
                            },
                        }
                    },
                    prcountry: {
                        validators: {
                            notEmpty: {
                                message: 'The country is required'
                            },
                        }
                    },
                    prstate: {
                        validators: {
                            notEmpty: {
                                message: 'The state is required'
                            },
                        }
                    },
                    prcity: {
                        validators: {
                            notEmpty: {
                                message: 'The city is required'
                            },
                        }
                    },
                    prpostalcode: {
                        validators: {
                            notEmpty: {
                                message: 'The postal code is required'
                            },
                        }
                    },
                    prphone: {
                        validators: {
                            notEmpty: {
                                message: 'The phone is required'
                            },
                        }
                    },
                    premail: {
                        validators: {
                            notEmpty: {
                                message: 'The email is required'
                            },
                            emailAddress: {
                                message: 'The email address is not valid'
                            }
                        }
                    },

                    incontactname: {
                        validators: {
                            notEmpty: {
                                message: 'The contact name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The contact name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9\s]*$/,
                                message: 'The contact name can only consist of alphabets, numbers, space.'
                            }
                        }
                    },
                    inaddress1: {
                        validators: {
                            notEmpty: {
                                message: 'The address is required'
                            },
                        }
                    },
                    incountry: {
                        validators: {
                            notEmpty: {
                                message: 'The country is required'
                            },
                        }
                    },
                    instate: {
                        validators: {
                            notEmpty: {
                                message: 'The state is required'
                            },
                        }
                    },
                    incity: {
                        validators: {
                            notEmpty: {
                                message: 'The city is required'
                            },
                        }
                    },
                    inpostalcode: {
                        validators: {
                            notEmpty: {
                                message: 'The postal code is required'
                            },
                        }
                    },
                    inphone: {
                        validators: {
                            notEmpty: {
                                message: 'The phone is required'
                            },
                        }
                    },
                    inemail: {
                        validators: {
                            notEmpty: {
                                message: 'The email is required'
                            },
                            emailAddress: {
                                message: 'The email address is not valid'
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

            form.find('[name="prphone"], [name="prmobile"],  [name="prfax"]').mask('+1 (999) 999 9999');
            form.find('[name="inphone"], [name="inmobile"],  [name="infax"]').mask('+1 (999) 999 9999');
        }
    }
})

.directive('customerContactForm', function () {   //Note:- Mix of above both customer and contact form

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
                    customername: {
                        validators: {
                            notEmpty: {
                                message: 'The customer name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The customer name must be less than 250 characters long'
                            },
                        }
                    },
                    companyname: {
                        validators: {
                            notEmpty: {
                                message: 'The company name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The company name must be less than 250 characters long'
                            },
                        }
                    },
                    paymentterm: {
                        validators: {
                            notEmpty: {
                                message: 'The payment term is required'
                            },
                        }
                    },

                    prcontactname: {
                        validators: {
                            notEmpty: {
                                message: 'The contact name is required'
                            },
                            stringLength: {
                                max: 250,
                                message: 'The contact name must be less than 250 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9\s]*$/,
                                message: 'The contact name can only consist of alphabets, numbers, space.'
                            }
                        }
                    },
                   
                    praddress1: {
                        validators: {
                            notEmpty: {
                                message: 'The address is required'
                            },
                        }
                    },
                    prcountry: {
                        validators: {
                            notEmpty: {
                                message: 'The country is required'
                            },
                        }
                    },
                    prstate: {
                        validators: {
                            notEmpty: {
                                message: 'The state is required'
                            },
                        }
                    },
                    prcity: {
                        validators: {
                            notEmpty: {
                                message: 'The city is required'
                            },
                        }
                    },
                    prpostalcode: {
                        validators: {
                            notEmpty: {
                                message: 'The postal code is required'
                            },
                        }
                    },
                    prphone: {
                        validators: {
                            notEmpty: {
                                message: 'The phone is required'
                            },
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

            form.find('[name="prphone"], [name="prmobile"],  [name="prfax"]').mask('+1 (999) 999 9999');
            form.find('[name="inphone"], [name="inmobile"],  [name="infax"]').mask('+1 (999) 999 9999');
        }
    }
})