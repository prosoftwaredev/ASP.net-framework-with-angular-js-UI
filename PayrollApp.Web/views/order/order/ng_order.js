'use strict';

angular.module('app.order', []).config(function () { })

.factory('OrderService', function ($httpService) {
    return {
        getOrders: function (search) {
            var Obj = { postData: search, url: '/Order/GetOrders' };
            return $httpService.$postSearch(Obj);
        },

        getOrderByID: function (OrderId) {
            var Obj = { params: { OrderID: OrderId }, url: '/Order/GetOrderByID' };
            return $httpService.$get(Obj);
        },
        addOrder: function (Order) {
            var Obj = { postData: Order, url: '/Order/CreateOrder', successToast: 'Order added successfully', errorToast: 'Error while adding new Order' };
            return $httpService.$post(Obj);
        },
        editOrder: function (Order) {
            var Obj = { postData: Order, url: '/Order/UpdateOrder', successToast: 'Order edited successfully', errorToast: 'Error while editing existing Order' };
            return $httpService.$put(Obj);
        },
        removeOrder: function (ID) {
            var Obj = { url: '/Order/DeleteOrder/' + ID, successToast: 'Order removed successfully', errorToast: 'Error while removing existing Order' };
            return $httpService.$delete(Obj);
        },

        editOrderIsEnable: function (Employee) {
            var Obj = { postData: Employee, url: '/Order/UpdateOrderIsEnable', successToast: 'Order site edited successfully', errorToast: 'Error while editing existing Order site' };
            return $httpService.$put(Obj);
        },

        getAllCustomers: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/Customer/GetAllCustomers' };
            return $httpService.$get(Obj);
        },
        getAllCustomerSitesByCustomerID: function (isDisplayAll, CustomerID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, CustomerID: CustomerID }, url: '/Customer/GetAllCustomerSitesByCustomerID' };
            return $httpService.$get(Obj);
        },
        getCustomerSiteByID: function (CustomerSiteID) {
            var Obj = { params: { CustomerSiteID: CustomerSiteID }, url: '/Customer/GetCustomerSiteByID' };
            return $httpService.$get(Obj);
        },
        getAllLabourClassifications: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/LabourClassification/GetAllLabourClassifications' };
            return $httpService.$get(Obj);
        },
        getAllEquipments: function (isDisplayAll) {
            var Obj = { params: { isDisplayAll: isDisplayAll }, url: '/Equipment/GetAllEquipments' };
            return $httpService.$get(Obj);
        },
        getOrderEquipmentsByOrderID: function (isDisplayAll, OrderID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, OrderID: OrderID }, url: '/Order/GetAllOrderEquipmentsByOrderID' };
            return $httpService.$get(Obj);
        },
        getOrderTimeslipsByOrderID: function (isDisplayAll, OrderID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, OrderID: OrderID }, url: '/Order/GetAllOrderTimeslipsByOrderID' };
            return $httpService.$get(Obj);
        },
        getAllEmployeesByCustomerID: function (isDisplayAll, CustomerID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, CustomerID: CustomerID }, url: '/Employee/GetAllEmployeesByCustomerID' };
            return $httpService.$get(Obj);
        },
        getRatesByCustomerSiteIDAndLabourClassificationID: function (CustomerSiteID, LabourClassificationID) {
            var Obj = { params: { CustomerSiteID: CustomerSiteID, LabourClassificationID: LabourClassificationID }, url: '/Customer/GetRatesByCustomerSiteIDAndLabourClassificationID' };
            return $httpService.$get(Obj);
        },
        getOrderTimeslipByID: function (OrderTimeslipId) {
            var Obj = { params: { OrderTimeslipID: OrderTimeslipId }, url: '/Order/GetOrderTimeslipByID' };
            return $httpService.$get(Obj);
        },
        editOrderTimeslip: function (Order) {
            var Obj = { postData: Order, url: '/Order/UpdateOrderTimeslip', successToast: 'Timeslip edited successfully', errorToast: 'Error while editing existing timeslip' };
            return $httpService.$put(Obj);
        },

        getAllCustomerSiteJobLocationsByCustomerSiteID: function (isDisplayAll, CustomerSiteID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, CustomerSiteID: CustomerSiteID }, url: '/Customer/GetAllCustomerSiteJobLocationsByCustomerSiteID' };
            return $httpService.$get(Obj);
        },
        removeCustomerSiteJobLocation: function (ID) {
            var Obj = { url: '/Customer/DeleteCustSiteJobLocation/' + ID, successToast: 'Job location removed successfully', errorToast: 'Error while removing existing job location' };
            return $httpService.$delete(Obj);
        },
        editCustomerSiteJobLocation: function (CustomerSiteJobLocation) {
            var Obj = { postData: CustomerSiteJobLocation, url: '/Customer/UpdateCustomerSiteJobLocation', successToast: 'Job location Info edited successfully', errorToast: 'Error while editing existing job location' };
            return $httpService.$put(Obj);
        },

        getEmployeeLabourClassificationsByLabourClassificationIDAndEmployeeID: function (isDisplayAll, EmployeeID, LabourClassificationID) {
            var Obj = { params: { isDisplayAll: isDisplayAll, EmployeeID: EmployeeID, LabourClassificationID: LabourClassificationID }, url: '/Employee/GetEmployeeLabourClassificationsByLabourClassificationIDAndEmployeeID' };
            return $httpService.$get(Obj);
        },

        getOrderTimeslips: function (search) {
            var Obj = { postData: search, url: '/Order/GetOrderTimeslips' };
            return $httpService.$postSearch(Obj);
        },

        getTimeslipStatus: function (JobDuration, CustomerID, WorkStartRsv, StartTimeRsv, LabourClassificationID, dayOfWeek) {
            var Obj = { params: { JobDuration: JobDuration, CustomerID: CustomerID, WorkStartRsv: WorkStartRsv, StartTimeRsv: StartTimeRsv, LabourClassificationID: LabourClassificationID, dayOfWeek: dayOfWeek }, url: '/Order/GetTimeslipStatus' };
            return $httpService.$get(Obj);
        },

        getTimeslipStatusAfterClick: function (RequestData) {
            var Obj = { postData: RequestData, url: '/Order/GetTimeslipStatusAfterClick' };
            return $httpService.$postSearch(Obj);
        },

        getTimeslipStatusOnTimeslip: function (WorkStartRsv, StartTimeRsv) {
            var Obj = { params: { WorkStartRsv: WorkStartRsv, StartTimeRsv: StartTimeRsv }, url: '/Order/GetTimeslipStatus' };
            return $httpService.$get(Obj);
        },

        getRollOverDate: function (RollOver) {
            var Obj = { params: { RollOver: RollOver }, url: '/Order/GetRollOverDate' };
            return $httpService.$get(Obj);
        },

        recalculateConfirmedThrough: function () {
            var Obj = { url: '/Order/ReCalculateConfirmedThrough' };
            return $httpService.$get(Obj);
        },
    };
})

.controller('OrderCtrl_Add', function ($scope, $rootScope, OrderService, $filter, $state, $timeout, CFG) {

    var isDisplayAll = false;
    $scope.Order = {};
    $scope.EquipmentIDs = [];
    $scope.Order.CustomerSiteID = 0;
    $scope.Order.People = 1;
    $scope.Order.JobDuration = 1;
    $scope.Order.StartTimeRsv = '07:00';
    $scope.RequirePO = false;
    $scope.Order.CustomerSiteJobLocationID = -1;

    $scope.tomorrow = new Date();
    $scope.tomorrow.setDate($scope.tomorrow.getDate() + 1);

    var filteredTomorrow = $filter('date')($scope.tomorrow, "MM/dd/yyyy");

    $scope.Order.WorkStartRsv = filteredTomorrow;

    $scope.startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            var site = $filter('filter')($scope.CustomerSites, { IsPrimary: true });
            $scope.Order.CustomerSiteID = site[0].CustomerSiteID;
        }, 2000);
    };

    $scope.getAllCustomers = function () {
        $rootScope.IsLoading = true;
        OrderService.getAllCustomers(isDisplayAll).then(function (response) {
            $scope.Customers = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllCustomers();

    $scope.getAllLabourClassifications = function () {
        $rootScope.IsLoading = true;
        OrderService.getAllLabourClassifications(isDisplayAll).then(function (response) {
            $scope.LabourClassifications = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllLabourClassifications();

    $scope.getAllEquipments = function () {
        $rootScope.IsLoading = true;
        OrderService.getAllEquipments(isDisplayAll).then(function (response) {
            $scope.Equipments = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllEquipments();

    $scope.getAllCustomerSitesByCustomerID = function (CustomerID) {

        if (CustomerID != null) {

            var customer = $filter('filter')($scope.Customers, { CustomerID: CustomerID });
            $scope.Order.CompanyName = customer[0].CompanyName;
            $scope.RequirePO = customer[0].RequirePO;

            $rootScope.IsLoading = true;
            OrderService.getAllCustomerSitesByCustomerID(isDisplayAll, CustomerID).then(function (response) {
                $scope.CustomerSites = response.data;
                $rootScope.IsLoading = false;

                $scope.Order.AccountNo = null;
                $scope.Order.ContactName = null;
                $scope.Order.Phone = null;
                $scope.Order.SiteDescription = null;

                var site = $filter('filter')($scope.CustomerSites, { IsPrimary: true });

                if (site.length > 0) {
                    $scope.Order.CustomerSiteID = site[0].CustomerSiteID;

                    $scope.$watch('Order.CustomerSiteID', function (newvalue, oldvalue) {
                        $scope.getCustomerSiteByID(newvalue);
                        getJobLocationsByCustomerSiteID(newvalue);
                    });
                }
            });
        }
    };

    $scope.getCustomerSiteByID = function (CustomerSiteID) {
        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            OrderService.getCustomerSiteByID(CustomerSiteID).then(function (response) {
                $scope.Site = response.data;
                $scope.Order.AccountNo = $scope.Site.AccountNo;
                $scope.Order.ContactName = $scope.Site.PrContactName;
                $scope.Order.Phone = $scope.Site.PrPhone;
                $scope.Order.OTPerDay = $scope.Site.OTPerDay;
                $scope.Order.OTPerWeek = $scope.Site.OTPerWeek;
                $scope.Order.SiteDescription = $scope.Site.SiteDescription;
                $rootScope.IsLoading = false;
            });
        }
    };

    //----------------------------------Registration---------------------------

    $scope.addOrder = function (Order) {

        if (Order.CustomerID == null || Order.CustomerID == '')
            return $rootScope.showNotify('Customer name field cannot be empty', 'Information', 'i');

        //if ($scope.RequirePO) {
        //    if (Order.PONumber == null || Order.PONumber == '')
        //        return $rootScope.showNotify('PO Number field cannot be empty', 'Information', 'i');
        //}

        Order.EquipmentIDs = $scope.EquipmentIDs;

        $rootScope.IsLoading = true;
        OrderService.addOrder(Order).then(function (response) {
            $scope.OrderID = response.data;
            $rootScope.IsLoading = false;
            $state.go('app.order.order.timeslip', { id: $scope.OrderID });
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

    //----------------------------Job Location-----------------------------

    function getJobLocationsByCustomerSiteID(CustomerSiteID) {
        $scope.CustomerSiteJobLocations = [];
        $scope.isDisplayJobLocations = false;

        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            OrderService.getAllCustomerSiteJobLocationsByCustomerSiteID(isDisplayAll, CustomerSiteID).then(function (response) {
                $scope.CustomerSiteJobLocations = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayJobLocations = true;
                else
                    $scope.isDisplayJobLocations = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getCustomerSiteJobLocationByID = function (CustomerSiteJobLocationID) {

        $scope.CustomerSiteJobLocationID = CustomerSiteJobLocationID;
        $scope.isDisplayJobLocations = false;

        var jobLocation = $filter('filter')($scope.CustomerSiteJobLocations, { CustomerSiteJobLocationID: CustomerSiteJobLocationID });

        if (jobLocation.length > 0) {
            $scope.Order.JobLocation = jobLocation[0].JobLocation;
            $scope.Order.JobAddress = jobLocation[0].JobAddress;
            $scope.Order.JobNote = jobLocation[0].JobNote;
        }
    };

    $scope.addNewJob = function () {
        $scope.isDisplayJobLocations = false;
        $scope.CustomerSiteJobLocationID = 0;
        $scope.Order.CustomerSiteJobLocationID = 0;
        $scope.Order.JobLocation = null;
        $scope.Order.JobAddress = null;
        $scope.Order.JobNote = null;
    };

    $scope.showList = function () {
        $scope.isDisplayJobLocations = true;
    };

    $scope.getTimeslipStatus = function (JobDuration, CustomerID, WorkStartRsv, StartTimeRsv, LabourClassificationID) {
        $rootScope.IsLoading = true;
        OrderService.getTimeslipStatus(JobDuration, CustomerID, WorkStartRsv, StartTimeRsv, LabourClassificationID, CFG.settings.dayOfWeek).then(function (response) {
            $scope.TimeslipStatus = response.data;
            $scope.Order.ConfirmedThrough = response.data.ConfirmedThrough;
            $scope.Order.RollOverDate = response.data.RollOverDate;
            $scope.Order.IsToBeRolledOver = response.data.IsToBeRolledOver;
            $rootScope.IsLoading = false;
        });
    };


    $scope.getTimeslipStatusAfterClick = function (JobDuration, CustomerID, WorkStartRsv, StartTimeRsv, LabourClassificationID, Date) {


        $scope.TimeslipStatus.DayStatusList.forEach(function (i) {
            if (i.Date == Date)
                i.Click = true;
            else
                i.Click = false;
        });


        var RequestData = {
            JobDuration: JobDuration,
            CustomerID: CustomerID,
            WorkStartRsv: WorkStartRsv,
            StartTimeRsv: StartTimeRsv,
            LabourClassificationID: LabourClassificationID,
            dayOfWeek: CFG.settings.dayOfWeek,
            DayStatusList: $scope.TimeslipStatus.DayStatusList,
        };

        $rootScope.IsLoading = true;
        OrderService.getTimeslipStatusAfterClick(RequestData).then(function (response) {
            $scope.TimeslipStatus = response.data;
            $scope.Order.ConfirmedThrough = response.data.ConfirmedThrough;
            $scope.Order.RollOverDate = response.data.RollOverDate;
            $scope.Order.IsToBeRolledOver = response.data.IsToBeRolledOver;
            $rootScope.IsLoading = false;
        });
    };
})

.controller('OrderCtrl_Edit', function ($scope, $rootScope, $stateParams, OrderService, $filter) {

    var isDisplayAll = false;
    $scope.Order = {};
    $scope.EquipmentIDs = [];
    $scope.Order.CustomerSiteID = 0;
    $scope.Order.CustomerSiteJobLocationID = -1;

    $scope.OrderID = $stateParams.id;

    $scope.getAllCustomers = function () {
        $rootScope.IsLoading = true;
        OrderService.getAllCustomers(isDisplayAll).then(function (response) {
            $scope.Customers = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllCustomers();

    $scope.getAllLabourClassifications = function () {
        $rootScope.IsLoading = true;
        OrderService.getAllLabourClassifications(isDisplayAll).then(function (response) {
            $scope.LabourClassifications = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getAllLabourClassifications();

    $scope.getAllEquipments = function () {
        $rootScope.IsLoading = true;
        OrderService.getAllEquipments(isDisplayAll).then(function (response) {
            $scope.Equipments = response.data;
            $rootScope.IsLoading = false;
            $scope.getOrderEquipmentsByOrderID($scope.OrderID);
        });
    };

    $scope.getAllEquipments();

    $scope.getOrderEquipmentsByOrderID = function (OrderID) {
        $rootScope.IsLoading = true;
        OrderService.getOrderEquipmentsByOrderID(isDisplayAll, OrderID).then(function (response) {
            $scope.OrderEquipments = response.data;
            $rootScope.IsLoading = false;

            $scope.Equipments.forEach(function (i) {
                i.Flag = false;
                $scope.OrderEquipments.forEach(function (j) {
                    if (i.EquipmentID == j.EquipmentID) {
                        i.Flag = true;
                        $scope.EquipmentIDs.push(j.EquipmentID);
                    }
                });
            });
        });
    };

    $scope.getAllCustomerSitesByCustomerID = function (CustomerID) {

        if (CustomerID != null) {

            var customer = $filter('filter')($scope.Customers, { CustomerID: CustomerID });
            $scope.Order.CompanyName = customer[0].CompanyName;

            $rootScope.IsLoading = true;
            OrderService.getAllCustomerSitesByCustomerID(isDisplayAll, CustomerID).then(function (response) {
                $scope.CustomerSites = response.data;
                $rootScope.IsLoading = false;
                //$scope.startTimer();
            });
        }
    };

    $scope.getCustomerSiteByID = function (CustomerSiteID) {
        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            OrderService.getCustomerSiteByID(CustomerSiteID).then(function (response) {
                $scope.Site = response.data;
                $scope.Order.AccountNo = $scope.Site.AccountNo;
                $scope.Order.ContactName = $scope.Site.PrContactName;
                $scope.Order.Phone = $scope.Site.PrPhone;
                $rootScope.IsLoading = false;
            });
        }
    };

    getOrderByID($scope.OrderID);

    function getOrderByID(OrderID) {
        $rootScope.IsLoading = true;
        OrderService.getOrderByID(OrderID).then(function (response) {
            $scope.Order = response.data;
            $rootScope.IsLoading = false;
            $scope.getAllCustomerSitesByCustomerID($scope.Order.CustomerID);
            $scope.getCustomerSiteByID($scope.Order.CustomerSiteID);
            getJobLocationsByCustomerSiteID($scope.Order.CustomerSiteID);
        });
    };

    $scope.editOrder = function (Order) {

        if (Order.CustomerID == null || Order.CustomerID == '')
            return $rootScope.showNotify('Customer name field cannot be empty', 'Information', 'i');

        Order.EquipmentIDs = $scope.EquipmentIDs;

        Order.OrderID = $scope.OrderID;
        $rootScope.IsLoading = true;
        OrderService.editOrder(Order).then(function () {
            $rootScope.IsLoading = false;
        });
    };

    $scope.IsEquipmentChecked = function (flag, EquipmentID) {
        console.log(flag, EquipmentID);
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
        console.log($scope.EquipmentIDs);
    };

    //----------------------------Job Location-----------------------------

    function getJobLocationsByCustomerSiteID(CustomerSiteID) {
        $scope.CustomerSiteJobLocations = [];
        $scope.isDisplayJobLocations = false;

        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            OrderService.getAllCustomerSiteJobLocationsByCustomerSiteID(isDisplayAll, CustomerSiteID).then(function (response) {
                $scope.CustomerSiteJobLocations = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayJobLocations = true;
                else
                    $scope.isDisplayJobLocations = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getCustomerSiteJobLocationByID = function (CustomerSiteJobLocationID) {

        $scope.CustomerSiteJobLocationID = CustomerSiteJobLocationID;
        $scope.isDisplayJobLocations = false;

        var jobLocation = $filter('filter')($scope.CustomerSiteJobLocations, { CustomerSiteJobLocationID: CustomerSiteJobLocationID });

        if (jobLocation.length > 0) {
            $scope.Order.JobLocation = jobLocation[0].JobLocation;
            $scope.Order.JobAddress = jobLocation[0].JobAddress;
            $scope.Order.JobNote = jobLocation[0].JobNote;
        }
    };

    $scope.addNewJob = function () {
        $scope.isDisplayJobLocations = false;
        $scope.CustomerSiteJobLocationID = 0;
        $scope.Order.CustomerSiteJobLocationID = 0;
        $scope.Order.JobLocation = null;
        $scope.Order.JobAddress = null;
        $scope.Order.JobNote = null;
    };

    $scope.showList = function () {
        $scope.isDisplayJobLocations = true;
    };
})

.controller('OrderCtrl_Detail', function ($scope, $rootScope, $stateParams, OrderService) {

    var isDisplayAll = false;
    $scope.OrderID = $stateParams.id;

    getOrderByID($scope.OrderID);

    function getOrderByID(OrderID) {
        $rootScope.IsLoading = true;
        OrderService.getOrderByID(OrderID).then(function (response) {
            $scope.Order = response.data;
            $rootScope.IsLoading = false;
        });
    };

    getAllOrderSitesByOrderID($scope.OrderID);

    function getAllOrderSitesByOrderID(OrderID) {
        $rootScope.IsLoading = true;
        OrderService.getAllOrderSitesByOrderID(isDisplayAll, $scope.OrderID).then(function (response) {
            $scope.OrderSites = response.data;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getOrderSiteByID = function (OrderSiteID) {
        $rootScope.IsLoading = true;
        OrderService.getOrderSiteByID(OrderSiteID).then(function (response) {
            $scope.OrderSite = response.data;
            $scope.OrderSiteID = $scope.OrderSite.OrderSiteID;

            $rootScope.CustSiteID = 0;

            getLabourClassificationsByOrderSiteID($scope.OrderSiteID);
            getNotesByOrderSiteID($scope.OrderSiteID);
            $rootScope.IsLoading = false;
        });
    };

    $scope.getLabourClassificationsByOrderSiteID = function () {
        getLabourClassificationsByOrderSiteID($scope.OrderSiteID);
    };

    function getLabourClassificationsByOrderSiteID(OrderSiteID) {
        if (OrderSiteID != null) {
            $rootScope.IsLoading = true;
            OrderService.getAllOrderSiteLabourClassificationsByOrderSiteID(isDisplayAll, $scope.OrderSiteID).then(function (response) {
                $scope.OrderSiteLabourClassifications = response.data;
                $scope.FilteredLabourClassifications = $scope.OrderSiteLabourClassifications;

                if (response.data.length > 0)
                    $scope.isDisplayLabourClassifications = true;
                else
                    $scope.isDisplayLabourClassifications = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    $scope.getNotesByOrderSiteID = function () {
        getNotesByOrderSiteID($scope.OrderSiteID);
    };

    function getNotesByOrderSiteID(OrderSiteID) {
        if (OrderSiteID != null) {
            $rootScope.IsLoading = true;
            OrderService.getAllOrderSiteNotesByOrderSiteID(isDisplayAll, OrderSiteID).then(function (response) {
                $scope.OrderSiteNotes = response.data;

                if (response.data.length > 0)
                    $scope.isDisplayNotes = true;
                else
                    $scope.isDisplayNotes = false;

                $rootScope.IsLoading = false;
            });
        }
    };

    if ($rootScope.CustSiteID > 0)
        $scope.getOrderSiteByID($rootScope.CustSiteID);
})

.controller('OrderCtrl', function ($scope, $state, $rootScope, OrderService, $window, $compile, $filter, $uibModal) {

    var isDisplayAll = false;
    $rootScope.CustSiteID = 0;

    $scope.PageSizes = [];
    $scope.PageSizes.push({ PageSize: 10 }, { PageSize: 25 }, { PageSize: 50 }, { PageSize: 100 });

    $scope.Orders = [];
    $scope.totalOrders = 0;
    $scope.IsItemsFound = false;
    $scope.pageSize = 10;
    $scope.Search = {};

    $scope.loading = true;
    getOrders($rootScope.pageNumber);
    $scope.loading = false;

    $scope.refresh = function () {
        $route.reload();
    };

    $scope.pagination = {
        current: $rootScope.pageNumber
    };

    $scope.pageChanged = function (newPage) {
        getOrders(newPage);
    };

    $scope.getOrders = function () {
        getOrders(1);
    };

    function getOrders(pageNumber) {

        $rootScope.pageNumber = pageNumber;

        $scope.Search.pageNumber = $rootScope.pageNumber;
        $scope.Search.pageSize = $scope.pageSize;

        $rootScope.IsLoading = true;
        OrderService.getOrders($scope.Search)
            .then(function (result) {
                $scope.Orders = result.data.Items;
                $scope.totalOrders = result.data.Count;
                $rootScope.IsLoading = false;

                if (result.data.Items.length == 0)
                    $scope.IsItemsFound = true;
                else
                    $scope.IsItemsFound = false;
            });

        //$scope.totalOrders = 36;
        //$scope.Orders = [
        //    { PO: '646484', CustomerName: 'ABC1', CompanyName: 'PQR1', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ1', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },
        //    { PO: '646484', CustomerName: 'ABC2', CompanyName: 'PQR2', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ2', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },
        //    { PO: '646484', CustomerName: 'ABC3', CompanyName: 'PQR3', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ3', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },
        //    { PO: '646484', CustomerName: 'ABC4', CompanyName: 'PQR4', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ4', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },
        //    { PO: '646484', CustomerName: 'ABC5', CompanyName: 'PQR5', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ5', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },
        //    { PO: '646484', CustomerName: 'ABC6', CompanyName: 'PQR6', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ6', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },
        //    { PO: '646484', CustomerName: 'ABC7', CompanyName: 'PQR7', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ7', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },
        //    { PO: '646484', CustomerName: 'ABC8', CompanyName: 'PQR8', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ8', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },
        //    { PO: '646484', CustomerName: 'ABC9', CompanyName: 'PQR9', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ9', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },
        //    { PO: '646484', CustomerName: 'ABC10', CompanyName: 'PQR10', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ10', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },
        //    { PO: '646484', CustomerName: 'ABC11', CompanyName: 'PQR11', ContactName: '204-339-6704', PhoneNumber: '204-339-6704', SiteLocation: 'XYZ11', StartDate: '31-Jan-2017 12:15:53 PM', EndDate: '31-Jan-2017 12:15:53 PM', IsEnable: true },

        //];
        //$rootScope.IsLoading = false;
    };

    $scope.predicate = 'Name';
    $scope.reverse = false;
    $scope.order = function (predicate) {
        $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
        $scope.predicate = predicate;
    };


    $scope.deleteOrder = function (OrderID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            OrderService.removeOrder(OrderID).then(function () {
                $rootScope.IsLoading = false;
                getOrders($rootScope.pageNumber);
            });
        }
    };

    $scope.editOrderIsEnable = function (OrderID, IsEnable) {

        var Order = {};

        if (IsEnable == true) {
            Order = {
                OrderID: OrderID,
                IsEnable: false
            };
        }
        else {
            Order = {
                OrderID: OrderID,
                IsEnable: true
            };
        }

        $rootScope.IsLoading = true;
        OrderService.editOrderIsEnable(Order).then(function () {
            $rootScope.IsLoading = false;
            getOrders($rootScope.pageNumber);
        });
    };

    $scope.editOrderDelinquent = function (OrderID, Delinquent) {

        var Order = {};

        if (Delinquent == true) {
            Order = {
                OrderID: OrderID,
                Delinquent: false
            };
        }
        else {
            Order = {
                OrderID: OrderID,
                Delinquent: true
            };
        }

        $rootScope.IsLoading = true;
        OrderService.editOrderDelinquent(Order).then(function () {
            $rootScope.IsLoading = false;
            getOrders($rootScope.pageNumber);
        });
    };

    $scope.goToDetails = function (OrderID) {
        $state.go('app.Order.Order.detail', { id: OrderID });
    };
})

.controller('OrderCtrl_Timeslip', function ($scope, $state, $stateParams, $rootScope, OrderService, $window, $compile, $filter, CFG) {

    var isDisplayAll = false;
    $scope.OrderID = $stateParams.id;

    getAllLabourClassifications();

    function getAllLabourClassifications() {
        $rootScope.IsLoading = true;
        OrderService.getAllLabourClassifications(isDisplayAll).then(function (response) {
            $scope.LabourClassifications = response.data;
            $rootScope.IsLoading = false;
        });
    };


    getOrderTimeslipsByOrderID(isDisplayAll, $scope.OrderID);

    function getOrderTimeslipsByOrderID(isDisplayAll, OrderID) {
        $rootScope.IsLoading = true;
        OrderService.getOrderTimeslipsByOrderID(isDisplayAll, OrderID).then(function (response) {
            $scope.Timeslips = response.data;
            $rootScope.IsLoading = false;
            $scope.TimeslipID = $scope.Timeslips[0].OrderTimeslipID;
            $scope.getOrderTimeslipByID($scope.Timeslips[0].OrderTimeslipID);
        });
    };

    $scope.getOrderTimeslipByID = function (OrderTimeslipID) {
        if (OrderTimeslipID != null) {
            $scope.TimeslipID = OrderTimeslipID;

            $rootScope.IsLoading = true;
            OrderService.getOrderTimeslipByID(OrderTimeslipID).then(function (response) {
                $scope.Timeslip = response.data;
                $rootScope.IsLoading = false;

                $scope.Timeslip.Bus = 0;
                $scope.Timeslip.Hat = 0;
                $scope.Timeslip.Boot = 0;
                $scope.Timeslip.Gloves = 0;
                $scope.Timeslip.Glass = 0;
                $scope.Timeslip.Coat = 0;
                $scope.Timeslip.Vest = 0;

                $scope.TimeslipStatus = $scope.Timeslip.DayStatusData;
                $scope.Timeslip.WorkStartRsv = $scope.TimeslipStatus.ConfirmedThrough;
                //$scope.Timeslip.RollOverStart = $scope.TimeslipStatus.RollOverDate;
                $scope.Timeslip.RollOver = $scope.TimeslipStatus.IsToBeRolledOver;

                if ($scope.Timeslip.RollOver)
                    $scope.isDefaultRollOverDate = false;
                else
                    $scope.isDefaultRollOverDate = true;

                getAllEmployeesByCustomerID($scope.Timeslip.CustomerID);
                getJobLocationsByCustomerSiteID($scope.Timeslip.CustomerSiteID, $scope.Timeslip.CustomerSiteJobLocationID);
                getRatesByCustomerSiteIDAndLabourClassificationID($scope.Timeslip.CustomerSiteID, $scope.Timeslip.LabourClassificationID);
            });
        }
    };

    $scope.getTimeslipStatusAfterClick = function (Date) {


        $scope.TimeslipStatus.DayStatusList.forEach(function (i) {
            if (i.Date == Date)
                i.Click = true;
            else
                i.Click = false;
        });


        var RequestData = {
            DayStatusList: $scope.TimeslipStatus.DayStatusList,
        };

        $rootScope.IsLoading = true;
        OrderService.getTimeslipStatusAfterClick(RequestData).then(function (response) {
            $scope.TimeslipStatus = response.data;
            $scope.Timeslip.WorkStartRsv = response.data.ConfirmedThrough;
            $scope.Timeslip.RollOverStart = response.data.RollOverDate;
            $scope.Timeslip.RollOver = response.data.IsToBeRolledOver;
            $rootScope.IsLoading = false;

            $scope.recalculateConfirmedThrough();
        });
    };

    $scope.getTimeslipStatus = function (WorkStartRsv, StartTimeRsv) {
        $rootScope.IsLoading = true;

        OrderService.getTimeslipStatusOnTimeslip(WorkStartRsv, StartTimeRsv).then(function (response) {
            $scope.TimeslipStatus = response.data;
            $scope.Timeslip.WorkStartRsv = response.data.ConfirmedThrough;
            $scope.Timeslip.RollOverStart = response.data.RollOverDate;
            $scope.Timeslip.RollOver = response.data.IsToBeRolledOver;
            $scope.isDefaultRollOverDate = false;
            $rootScope.IsLoading = false;
        });
    };

    $scope.getRollOverDate = function (RollOver) {
        if (RollOver) {
            $rootScope.IsLoading = true;
            $scope.isDefaultRollOverDate = false;
            OrderService.getRollOverDate(RollOver).then(function (response) {
                $scope.Timeslip.RollOverStart = response.data;

                $rootScope.IsLoading = false;
            });
        }
        else {
            $scope.Timeslip.RollOverStart = CFG.settings.defaultRollOverDate;
            $rootScope.IsLoading = true;
            $scope.isDefaultRollOverDate = true;
            OrderService.getRollOverDate(RollOver).then(function (response) {
                $scope.Timeslip.RollOverStart = response.data;

                $rootScope.IsLoading = false;
            });
        }

    };


    $scope.recalculateConfirmedThrough = function () {
        $rootScope.IsLoading = true;
        OrderService.recalculateConfirmedThrough().then(function (response) {
            $scope.TimeslipStatus = response.data;
            $scope.Timeslip.WorkStartRsv = response.data.ConfirmedThrough;
            $scope.Timeslip.RollOverStart = response.data.RollOverDate;
            $scope.Timeslip.RollOver = response.data.IsToBeRolledOver;
            $rootScope.IsLoading = false;
        });
    };


    function getAllEmployeesByCustomerID(CustomerID) {

        if (CustomerID != null) {

            $rootScope.IsLoading = true;
            OrderService.getAllEmployeesByCustomerID(isDisplayAll, CustomerID).then(function (response) {
                $scope.Employees = response.data;
                $rootScope.IsLoading = false;

                if ($scope.Timeslip.EmployeeID != null)
                    $scope.getEmployeeByEmployeeID($scope.Timeslip.EmployeeID);
            });
        }
    };

    $scope.getRates = function (CustomerSiteID, LabourClassificationID) {
        getRatesByCustomerSiteIDAndLabourClassificationID(CustomerSiteID, LabourClassificationID);
    }

    function getRatesByCustomerSiteIDAndLabourClassificationID(CustomerSiteID, LabourClassificationID) {
        if (CustomerSiteID != null && LabourClassificationID != null) {
            $rootScope.IsLoading = true;
            OrderService.getRatesByCustomerSiteIDAndLabourClassificationID(CustomerSiteID, LabourClassificationID).then(function (response) {
                //$scope.Rates = response.data;
                $scope.Timeslip.PayRate = $scope.Timeslip.PayRate == 0 ? response.data.PayRate : $scope.Timeslip.PayRate;
                $scope.Timeslip.InvoiceRate = $scope.Timeslip.InvoiceRate == 0 ? response.data.InvoiceRate : $scope.Timeslip.InvoiceRate;
                $rootScope.IsLoading = false;
            });
        }
    }


    function getJobLocationsByCustomerSiteID(CustomerSiteID, CustomerSiteJobLocationID) {
        $scope.CustomerSiteJobLocations = [];

        if (CustomerSiteID != null) {
            $rootScope.IsLoading = true;
            OrderService.getAllCustomerSiteJobLocationsByCustomerSiteID(isDisplayAll, CustomerSiteID).then(function (response) {
                $scope.CustomerSiteJobLocations = response.data;
                $rootScope.IsLoading = false;
                $scope.getCustomerSiteJobLocationByID(CustomerSiteJobLocationID);
            });
        }
    };





    $scope.getEmployeeByID = function (EmployeeID) {
        var emp = $filter('filter')($scope.Employees, { EmployeeID: EmployeeID });

        $scope.Timeslip.AccountNo = emp[0].AccountNo;

        if (EmployeeID != null) {
            $rootScope.IsLoading = true;
            OrderService.getEmployeeLabourClassificationsByLabourClassificationIDAndEmployeeID(isDisplayAll, EmployeeID, $scope.Timeslip.LabourClassificationID).then(function (response) {

                if (response.data.Rate == 0) {
                    getRatesByCustomerSiteIDAndLabourClassificationID($scope.Timeslip.CustomerSiteID, $scope.Timeslip.LabourClassificationID);
                    return $rootScope.showNotify('No payrate was set for this employee... The default payrate has been considered', 'Information', 'i');
                }
                else
                    $scope.Rates.PayRate = response.data.Rate;

                $rootScope.IsLoading = false;
            });
        }
    };


    $scope.getEmployeeByEmployeeID = function (EmployeeID) {
        var emp = $filter('filter')($scope.Employees, { EmployeeID: EmployeeID });
        $scope.Timeslip.AccountNo = emp[0].AccountNo;
    };



    $scope.editOrderTimeslip = function (OrderTimeslip) {

        //if (OrderTimeslip.CustomerID == null || OrderTimeslip.CustomerID == '')
        //    return $rootScope.showNotify('Customer name field cannot be empty', 'Information', 'i');



        OrderTimeslip.OrderTimeslipID = $scope.Timeslip.OrderTimeslipID;
        OrderTimeslip.BillState = -2;
        $rootScope.IsLoading = true;
        OrderService.editOrderTimeslip(OrderTimeslip).then(function () {
            $rootScope.IsLoading = false;
        });
    };

    $scope.editOrderTimeslipBillState = function (OrderTimeslip) {

        //if (OrderTimeslip.CustomerID == null || OrderTimeslip.CustomerID == '')
        //    return $rootScope.showNotify('Customer name field cannot be empty', 'Information', 'i');



        OrderTimeslip.OrderTimeslipID = $scope.Timeslip.OrderTimeslipID;
        OrderTimeslip.BillState = -1;
        $rootScope.IsLoading = true;
        OrderService.editOrderTimeslip(OrderTimeslip).then(function () {
            $rootScope.IsLoading = false;
        });
    };

    $scope.getCustomerSiteJobLocationByID = function (CustomerSiteJobLocationID) {

        var jobLocation = $filter('filter')($scope.CustomerSiteJobLocations, { CustomerSiteJobLocationID: CustomerSiteJobLocationID });

        if (jobLocation.length > 0) {
            $scope.Timeslip.CustomerSiteJobLocationID = jobLocation[0].CustomerSiteJobLocationID;
            $scope.Timeslip.JobLocation = jobLocation[0].JobLocation;
            $scope.Timeslip.JobAddress = jobLocation[0].JobAddress;
            $scope.Timeslip.JobNote = jobLocation[0].JobNote;
        }
    };


    //function getAllCustomerSitesByCustomerID(CustomerID) {

    //    if (CustomerID != null) {

    //        $rootScope.IsLoading = true;
    //        OrderService.getAllCustomerSitesByCustomerID(isDisplayAll, CustomerID).then(function (response) {
    //            $scope.CustomerSites = response.data;
    //            $rootScope.IsLoading = false;

    //            var site = $filter('filter')($scope.CustomerSites, { CustomerID: CustomerID });

    //            if (site.length > 0) {
    //                $scope.Timeslip.CustomerSiteID = site[0].CustomerSiteID;

    //                $scope.$watch('Timeslip.CustomerSiteID', function (newvalue, oldvalue) {
    //                    $scope.getCustomerSiteByID(newvalue);
    //                });
    //            }
    //        });
    //    }
    //};

    //$scope.getCustomerSiteByID = function (CustomerSiteID) {
    //    if (CustomerSiteID != null) {
    //        $rootScope.IsLoading = true;
    //        OrderService.getCustomerSiteByID(CustomerSiteID).then(function (response) {
    //            $scope.Site = response.data;
    //            $rootScope.IsLoading = false;
    //        });
    //    }
    //};

})

.controller('OrderTimeslipCtrl', function ($scope, $state, $rootScope, OrderService, $window, $compile, $filter, $uibModal) {

    var isDisplayAll = false;
    $rootScope.CustSiteID = 0;

    $scope.PageSizes = [];
    $scope.PageSizes.push({ PageSize: 10 }, { PageSize: 25 }, { PageSize: 50 }, { PageSize: 100 });

    $scope.OrderTimeslips = [];
    $scope.totalOrderTimeslips = 0;
    $scope.IsItemsFound = false;
    $scope.pageSize = 10;
    $scope.Search = {};
    $scope.selectedOrder = 0;

    $scope.loading = true;
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

    $scope.openTimeslip = function (id) {
        if ($scope.selectedOrder == id)
            $scope.selectedOrder = 0;
        else
            $scope.selectedOrder = id;
    };

    function getOrderTimeslips(pageNumber) {

        $rootScope.pageNumber = pageNumber;

        $scope.Search.pageNumber = $rootScope.pageNumber;
        $scope.Search.pageSize = $scope.pageSize;

        $rootScope.IsLoading = true;
        OrderService.getOrderTimeslips($scope.Search)
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


    $scope.deleteOrder = function (OrderID) {
        if ($window.confirm('Are you sure to delete this record?')) {
            $rootScope.IsLoading = true;
            OrderService.removeOrder(OrderID).then(function () {
                $rootScope.IsLoading = false;
                getOrderTimeslips($rootScope.pageNumber);
            });
        }
    };

    $scope.editOrderIsEnable = function (OrderID, IsEnable) {

        var Order = {};

        if (IsEnable == true) {
            Order = {
                OrderID: OrderID,
                IsEnable: false
            };
        }
        else {
            Order = {
                OrderID: OrderID,
                IsEnable: true
            };
        }

        $rootScope.IsLoading = true;
        OrderService.editOrderIsEnable(Order).then(function () {
            $rootScope.IsLoading = false;
            getOrderTimeslips($rootScope.pageNumber);
        });
    };

    $scope.editOrderDelinquent = function (OrderID, Delinquent) {

        var Order = {};

        if (Delinquent == true) {
            Order = {
                OrderID: OrderID,
                Delinquent: false
            };
        }
        else {
            Order = {
                OrderID: OrderID,
                Delinquent: true
            };
        }

        $rootScope.IsLoading = true;
        OrderService.editOrderDelinquent(Order).then(function () {
            $rootScope.IsLoading = false;
            getOrderTimeslips($rootScope.pageNumber);
        });
    };

    $scope.goToDetails = function (OrderID) {
        $state.go('app.Order.Order.detail', { id: OrderID });
    };
})

.directive('orderForm', function () {

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
                            }
                        }
                    },
                    companyname: {
                        validators: {
                            notEmpty: {
                                message: 'The company name is required'
                            }
                        }
                    },
                    account: {
                        validators: {
                            notEmpty: {
                                message: 'The account number is required'
                            }
                        }
                    },
                    ponumer: {
                        validators: {
                            notEmpty: {
                                message: 'The PO number is required'
                            }
                        }
                    },
                    requested: {
                        validators: {
                            notEmpty: {
                                message: 'The requested by is required'
                            }
                        }
                    },
                    phone: {
                        validators: {
                            notEmpty: {
                                message: 'The phone number is required'
                            }
                        }
                    },
                    people: {
                        validators: {
                            notEmpty: {
                                message: 'The no of people is required'
                            }
                        }
                    },
                    workstart: {
                        validators: {
                            notEmpty: {
                                message: 'The work start date is required'
                            },
                            date: {
                                format: 'MM/DD/YYYY',
                                message: 'The date is not a valid'
                            }
                        }
                    },
                    starttime: {
                        validators: {
                            notEmpty: {
                                message: 'The work start time is required'
                            },
                            //regexp: {
                            //    regexp: /^(2[0-4]|1[0-9]|[1-9]):([1-5]?[0-9])$/,
                            //    message: 'The time is not valid.'
                            //}
                        }
                    },
                    labourclassification: {
                        validators: {
                            notEmpty: {
                                message: 'The labour classification is required'
                            }
                        }
                    },
                    customersite: {
                        validators: {
                            notEmpty: {
                                message: 'The site is required'
                            }
                        }
                    },
                    reporting: {
                        validators: {
                            notEmpty: {
                                message: 'The reporting person is required'
                            }
                        }
                    },
                    radio: {
                        validators: {
                            notEmpty: {
                                message: 'The job duration is required'
                            }
                        }
                    },
                    otperday: {
                        validators: {
                            notEmpty: {
                                message: 'The OT per day is required'
                            }
                        }
                    },
                    otperweek: {
                        validators: {
                            notEmpty: {
                                message: 'The OT per week duration is required'
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
            form.find('[name="phone"]').mask('+1 (999) 999 9999');
            form.find('[name="starttime"]').mask('99:99');
            form.find('[name="workstart"]').mask('99/99/9999');

            //form.bootstrapValidator('revalidateField', 'otperday');
        }
    }
})