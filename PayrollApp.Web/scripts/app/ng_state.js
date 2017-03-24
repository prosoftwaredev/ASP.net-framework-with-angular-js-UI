'use strict';
angular.module('ngAppState', ['ui.router'])
.config(function ($stateProvider) {
    $stateProvider

        //----------------------------------------------Auth------------------------------------------
        .state('login', {
            url: '/login',
            views: {
                root: {
                    templateUrl: 'views/auth/login.html',
                    controller: 'AuthCtrl'
                }
            },
            data: {
                title: 'Login',
                htmlId: 'extr-page',
                requireLogin: false,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])
                },
                signOut: function (authService) {
                    authService.signOut();
                }
            }
        })

        .state('register', {
            url: '/register',
            views: {
                root: {
                    templateUrl: 'views/auth/register.html',
                    controller: 'AuthCtrl'
                }
            },
            data: {
                title: 'Register',
                htmlId: 'extr-page',
                requireLogin: false,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            }
        })
        .state('forgotPasswordLink', {
            url: '/forgot-password-link',
            views: {
                root: {
                    templateUrl: 'views/auth/forgot-password-link.html',
                    controller: 'AuthCtrl'
                }
            },
            data: {
                title: 'Forgot Password',
                htmlId: 'extr-page',
                requireLogin: false,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            }
        })
        .state('forgotPassword', {
            url: '/forgot-password/:p1/:p2',
            views: {
                root: {
                    templateUrl: 'views/auth/forgot-password.html',
                    controller: 'AuthCtrl'
                }
            },
            data: {
                title: 'Forgot Password',
                htmlId: 'extr-page',
                requireLogin: false,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            }
        })
        .state('lock', {
            url: '/lock',
            views: {
                root: {
                    templateUrl: 'views/auth/lock.html',
                    controller: 'AuthCtrl'
                }
            },
            data: {
                title: 'Locked Screen',
                htmlId: 'lock-page',
                requireLogin: false,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            }
        })

        //----------------------------------------------App------------------------------------------
        .state('app', {
            abstract: true,
            views: {
                root: {
                    templateUrl: 'partials/layout/layout.tpl.html',
                }
            }
        })

        //----------------------------------------------Dashboard------------------------------------------
        .state('app.dashboard', {
            url: '/dashboard',
            views: {
                "content@app": {
                    controller: 'DashboardCtrl',
                    templateUrl: 'views/dashboard/dashboard.html'
                }
            },
            data: {
                title: 'Dashboard',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Home'
            }
        })

        //----------------------------------------------Account------------------------------------------
        .state('app.account', {
            abstract: true,
            data: {
                title: 'My Account'
            }
        })
        .state('app.account.profile', {
            url: '/profile',
            views: {
                "content@app": {
                    controller: 'ProfileCtrl',
                    templateUrl: 'views/account/profile.html'
                }
            },
            data: {
                title: 'Profile',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Profile',
                parent: 'app.dashboard'
            }
        })
        .state('app.account.changepassword', {
            url: '/change-password',
            views: {
                "content@app": {
                    controller: 'ChangePasswordCtrl',
                    templateUrl: 'views/account/change-password.html'
                }
            },
            data: {
                title: 'Change Password',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Change Password',
                parent: 'app.account.profile'
            }
        })

        //----------------------------------------------Admin------------------------------------------
        .state('app.admin', {
            abstract: true,
            data: {
                title: 'Admin'
            }
        })

         .state('app.admin.preference', {
             url: '/preferences',
             views: {
                 "content@app": {
                     controller: 'PreferenceCtrl',
                     templateUrl: 'views/admin/preference/preferences.html'
                 }
             },
             data: {
                 title: 'Preferences',
                 requireLogin: true,
                 requireFilledProfile: false,
                 activateMainMenuItem: '',
                 activateTemplate: '',
             },
             ncyBreadcrumb: {
                 label: 'Preferences',
                 parent: 'app.dashboard'
             }
         })

        .state('app.admin.user', {
            url: '/users',
            views: {
                "content@app": {
                    controller: 'UserCtrl',
                    templateUrl: 'views/admin/user/users.html'
                }
            },
            data: {
                title: 'Users',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Users',
                parent: 'app.dashboard'
            }
        })
        .state('app.admin.role', {
            url: '/roles',
            views: {
                "content@app": {
                    controller: 'RoleCtrl',
                    templateUrl: 'views/admin/role/roles.html'
                }
            },
            data: {
                title: 'Roles',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Roles',
                parent: 'app.dashboard'
            }
        })
         .state('app.admin.exception', {
             url: '/exceptions',
             views: {
                 "content@app": {
                     controller: 'ExcLoggerCtrl',
                     templateUrl: 'views/admin/exception/exceptions.html'
                 }
             },
             data: {
                 title: 'Exceptions',
                 requireLogin: true,
                 requireFilledProfile: false,
                 activateMainMenuItem: '',
                 activateTemplate: '',
             },
             ncyBreadcrumb: {
                 label: 'Exceptions',
                 parent: 'app.dashboard'
             }
         })
        .state('app.admin.country', {
            url: '/countries',
            views: {
                "content@app": {
                    controller: 'CountryCtrl',
                    templateUrl: 'views/admin/country/countries.html'
                }
            },
            data: {
                title: 'Countries',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Countries',
                parent: 'app.dashboard'
            }
        })
         .state('app.admin.state', {
             url: '/states',
             views: {
                 "content@app": {
                     controller: 'StateCtrl',
                     templateUrl: 'views/admin/state/states.html'
                 }
             },
             data: {
                 title: 'States',
                 requireLogin: true,
                 requireFilledProfile: false,
                 activateMainMenuItem: '',
                 activateTemplate: '',
             },
             ncyBreadcrumb: {
                 label: 'States',
                 parent: 'app.dashboard'
             }
         })
        .state('app.admin.city', {
            url: '/cities',
            views: {
                "content@app": {
                    controller: 'CityCtrl',
                    templateUrl: 'views/admin/city/cities.html'
                }
            },
            data: {
                title: 'Cities',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Cities',
                parent: 'app.dashboard'
            }
        })
        .state('app.admin.employeetype', {
            url: '/employee-types',
            views: {
                "content@app": {
                    controller: 'EmployeeTypeCtrl',
                    templateUrl: 'views/admin/employee-type/employee-types.html'
                }
            },
            data: {
                title: 'Employee Types',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Employee Types',
                parent: 'app.dashboard'
            }
        })
        .state('app.admin.payfrequency', {
            url: '/pay-frequencies',
            views: {
                "content@app": {
                    controller: 'PayFrequencyCtrl',
                    templateUrl: 'views/admin/pay-frequency/pay-frequencies.html'
                }
            },
            data: {
                title: 'Pay Frequencies',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Pay Frequencies',
                parent: 'app.dashboard'
            }
        })
        .state('app.admin.labourclassification', {
            url: '/labour-classifications',
            views: {
                "content@app": {
                    controller: 'LabourClassificationCtrl',
                    templateUrl: 'views/admin/labour-classification/labour-classifications.html'
                }
            },
            data: {
                title: 'Labour Classifications',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Labour Classifications',
                parent: 'app.dashboard'
            }
        })
         .state('app.admin.certification', {
             url: '/certifications',
             views: {
                 "content@app": {
                     controller: 'CertificationCtrl',
                     templateUrl: 'views/admin/certification/certifications.html'
                 }
             },
             data: {
                 title: 'Certifications',
                 requireLogin: true,
                 requireFilledProfile: false,
                 activateMainMenuItem: '',
                 activateTemplate: '',
             },
             ncyBreadcrumb: {
                 label: 'Certifications',
                 parent: 'app.dashboard'
             }
         })
         .state('app.admin.title', {
             url: '/titles',
             views: {
                 "content@app": {
                     controller: 'TitleCtrl',
                     templateUrl: 'views/admin/title/titles.html'
                 }
             },
             data: {
                 title: 'Titles',
                 requireLogin: true,
                 requireFilledProfile: false,
                 activateMainMenuItem: '',
                 activateTemplate: '',
             },
             ncyBreadcrumb: {
                 label: 'Titles',
                 parent: 'app.dashboard'
             }
         })
        .state('app.admin.skill', {
            url: '/skills',
            views: {
                "content@app": {
                    controller: 'SkillCtrl',
                    templateUrl: 'views/admin/skill/skills.html'
                }
            },
            data: {
                title: 'Skills',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Skills',
                parent: 'app.dashboard'
            }
        })
        .state('app.admin.paymentterm', {
            url: '/payment-terms',
            views: {
                "content@app": {
                    controller: 'PaymentTermCtrl',
                    templateUrl: 'views/admin/payment-term/payment-terms.html'
                }
            },
            data: {
                title: 'Payment Terms',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Payment Terms',
                parent: 'app.dashboard'
            }
        })
        .state('app.admin.salesrep', {
            url: '/sales-reps',
            views: {
                "content@app": {
                    controller: 'SalesRepCtrl',
                    templateUrl: 'views/admin/sales-rep/sales-reps.html'
                }
            },
            data: {
                title: 'Sales Reps',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Sales Reps',
                parent: 'app.dashboard'
            }
        })
        .state('app.admin.equipment', {
            url: '/equipments',
            views: {
                "content@app": {
                    controller: 'EquipmentCtrl',
                    templateUrl: 'views/admin/equipment/equipments.html'
                }
            },
            data: {
                title: 'Equipments',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Equipments',
                parent: 'app.dashboard'
            }
        })
        //----------------------------------------------Employee------------------------------------------
        .state('app.employee', {
            abstract: true,
            data: {
                title: 'Employee'
            }
        })
        .state('app.employee.employee', {
            url: '/employees',
            views: {
                "content@app": {
                    controller: 'EmployeeCtrl',
                    templateUrl: 'views/employee/employee/employees.html'
                }
            },
            data: {
                title: 'Employees',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Employees',
                parent: 'app.dashboard'
            }
        })
        .state('app.employee.employee.add', {
            url: '/employee-add',
            //parent: 'app.employee.employee',
            views: {
                "content@app": {
                    controller: 'EmployeeCtrl_Add',
                    templateUrl: 'views/employee/employee/employee-add-ui.html',
                }
            },
            data: {
                title: 'Add New Employee',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Add New Employee',
                parent: 'app.employee.employee'
            }
        })
        .state('app.employee.employee.edit', {
            url: '/employee-edit/:id',
            //parent: 'app.employee.employee',
            views: {
                "content@app": {
                    controller: 'EmployeeCtrl_Edit',
                    templateUrl: 'views/employee/employee/employee-edit-ui.html',
                }
            },
            data: {
                title: 'Edit Employee',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Edit Employee',
                parent: 'app.employee.employee'
            }
        })
        .state('app.employee.employee.detail', {
            url: '/employee-detail/:id',
            views: {
                "content@app": {
                    controller: 'EmployeeCtrl_Detail',
                    templateUrl: 'views/employee/employee/employee-detail.html'
                }
            },
            data: {
                title: 'Employee Detail',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Employee Detail',
                parent: 'app.employee.employee'
            }
        })
        //----------------------------------------------Images -------------------------------------------
         .state('app.image', {
             abstract: true,
             data: {
                 title: 'Image'
             }
         })
        .state('app.image.image', {
            url: '/images',
            views: {
                "content@app": {
                    controller: 'ImageCtrl',
                    templateUrl: 'views/image/image/images.html'
                }
            },
            data: {
                title: 'Images',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Images',
                parent: 'app.dashboard'
            }
        })

        //----------------------------------------------Customer------------------------------------------

        .state('app.customer', {
            abstract: true,
            data: {
                title: 'Customer'
            }
        })
        .state('app.customer.customer', {
            url: '/customers',
            views: {
                "content@app": {
                    controller: 'CustomerCtrl',
                    templateUrl: 'views/customer/customer/customers.html'
                }
            },
            data: {
                title: 'Customers',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Customers',
                parent: 'app.dashboard'
            }
        })
        .state('app.customer.customer.add', {
            url: '/customer-add',
            views: {
                "content@app": {
                    controller: 'CustomerCtrl_Add',
                    templateUrl: 'views/customer/customer/customer-add-ui.html'
                }
            },
            data: {
                title: 'Add New Customer',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Add New Customer',
                parent: 'app.customer.customer'
            }
        })
        .state('app.customer.customer.edit', {
            url: '/customer-edit/:id',
            views: {
                "content@app": {
                    controller: 'CustomerCtrl_Edit',
                    templateUrl: 'views/customer/customer/customer-edit.html'
                }
            },
            data: {
                title: 'Edit Customer',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Edit Customer',
                parent: 'app.customer.customer'
            }
        })
        .state('app.customer.customer.detail', {
            url: '/customer-detail/:id',
            views: {
                "content@app": {
                    controller: 'CustomerCtrl_Detail',
                    templateUrl: 'views/customer/customer/customer-detail.html'
                }
            },
            data: {
                title: 'Customer Detail',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Customer Detail',
                parent: 'app.customer.customer'
            }
        })
        .state('app.customer.customer.sites', {
            url: '/customer-site',
            views: {
                "content@app": {
                    controller: 'CustomerSiteCtrl',
                    templateUrl: 'views/customer/customer/customer-sites.html'
                }
            },
            data: {
                title: 'Customer Sites',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Customer Sites',
                parent: 'app.customer.customer'
            }
        })
        .state('app.customer.customer.sites.add', {
            url: '/customer-site-add/:id',
            views: {
                "content@app": {
                    controller: 'CustomerSiteCtrl_Add',
                    templateUrl: 'views/customer/customer/customer-site-add.html'
                }
            },
            data: {
                title: 'Add Customer Site',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Add Customer Site',
                parent: 'app.customer.customer.sites'
            }
        })
        .state('app.customer.customer.sites.edit', {
            url: '/customer-site-edit/:id',
            views: {
                "content@app": {
                    controller: 'CustomerSiteCtrl_Edit',
                    templateUrl: 'views/customer/customer/customer-site-edit.html',
                    resolve: {
                        siteID: function ($stateParams) {
                            var data = {
                                data: $stateParams.id
                            }
                            return data;
                        }
                    },
                }
            },
            data: {
                title: 'Edit Customer Site',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Edit Customer Site',
                parent: 'app.customer.customer.sites'
            }
        })
        .state('app.customer.customer.sites.detail', {
            url: '/customer-site-detail/:id',
            views: {
                "content@app": {
                    controller: 'CustomerSiteCtrl_Detail',
                    templateUrl: 'views/customer/customer/customer-site-detail.html',
                    resolve: {
                        siteID: function ($stateParams) {
                            var data = {
                                data: $stateParams.id
                            }
                            return data;
                        }
                    },
                }
            },
            data: {
                title: 'Customer Site Detail',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Customer Site Detail',
                parent: 'app.customer.customer.sites'
            }
        })

        //----------------------------------------------Order------------------------------------------

        .state('app.order', {
            abstract: true,
            data: {
                title: 'Order'
            }
        })
        .state('app.order.order', {
            url: '/orders',
            views: {
                "content@app": {
                    controller: 'OrderCtrl',
                    templateUrl: 'views/order/order/orders.html'
                }
            },
            data: {
                title: 'Orders',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Orders',
                parent: 'app.dashboard'
            }
        })
        .state('app.order.order.add', {
            url: '/order-add',
            views: {
                "content@app": {
                    controller: 'OrderCtrl_Add',
                    templateUrl: 'views/order/order/order-add-ui.html'
                }
            },
            data: {
                title: 'Add New Order',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Add New Order',
                parent: 'app.order.order'
            }
        })
        .state('app.order.order.edit', {
            url: '/order-edit/:id',
            views: {
                "content@app": {
                    controller: 'OrderCtrl_Edit',
                    templateUrl: 'views/order/order/order-edit.html'
                }
            },
            data: {
                title: 'Edit Order',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Edit Order',
                parent: 'app.order.order'
            }
        })
        .state('app.order.order.detail', {
            url: '/order-detail/:id',
            views: {
                "content@app": {
                    //controller: 'OrderCtrl_Detail',
                    templateUrl: 'views/order/order/order-detail.html'
                }
            },
            data: {
                title: 'Order Detail',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Order Detail',
                parent: 'app.order.order'
            }
        })
        .state('app.order.order.timeslip', {
            url: '/order-timeslip/:id',
            views: {
                "content@app": {
                    controller: 'OrderCtrl_Timeslip',
                    templateUrl: 'views/order/order/order-timeslip.html'
                }
            },
            data: {
                title: 'Order Timeslip',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Order Timeslip',
                parent: 'app.order.order'
            }
        })
        .state('app.order.timeslip', {
            url: '/order-timeslips',
            views: {
                "content@app": {
                    controller: 'OrderTimeslipCtrl',
                    templateUrl: 'views/order/order/timeslips.html'
                }
            },
            data: {
                title: 'Order Timeslips',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Order Timeslips',
                parent: 'app.dashboard'
            }
        })

    //----------------------------------------------Reports -------------------------------------------
         .state('app.report', {
             abstract: true,
             data: {
                 title: 'Report'
             }
         })
        .state('app.report.report', {
            url: '/report',
            views: {
                "content@app": {
                    controller: 'ReportCtrl',
                    templateUrl: 'views/report/report/reports.html'
                }
            },
            data: {
                title: 'Reports',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Reports',
                parent: 'app.dashboard'
            }
        })
        .state('app.report.report.view', {
            url: '/view/:id',
            views: {
                "content@app": {
                    controller: 'ReportViewCtrl',
                    templateUrl: 'views/report/report/report-view.html'
                }
            },
            data: {
                title: 'View',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'View',
                parent: 'app.report.report'
            }
        })

    //----------------------------------------------Rollover------------------------------------------
        .state('app.rollover', {
            abstract: true,
            data: {
                title: 'Rollover'
            }
        })
        .state('app.rollover.rollover', {
            url: '/rollovers',
            views: {
                "content@app": {
                    controller: 'RolloverCtrl',
                    templateUrl: 'views/rollover/rollover/rollovers.html'
                }
            },
            data: {
                title: 'Rollovers',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Rollovers',
                parent: 'app.dashboard'
            }
        })
    //----------------------------------------------Devices------------------------------------------
        .state('app.devices', {
            abstract: true,
            data: {
                title: 'Devices'
            }
        })
        .state('app.devices.topez', {
            url: '/topezs',
            views: {
                "content@app": {
                    controller: 'DeviceSignatureCtrl',
                    templateUrl: 'views/devices/topez/devices.html'
                }
            },
            data: {
                title: 'Devices',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Devices',
                parent: 'app.dashboard'
            }
        })

     //----------------------------------------------Timeslip------------------------------------------
        .state('app.timeslip', {
            abstract: true,
            data: {
                title: 'Timeslip'
            }
        })
         .state('app.timeslip.all', {
             url: '/timeslip-all',
             views: {
                 "content@app": {
                     controller: 'TimeslipCtrl',
                     templateUrl: 'views/timeslip/timeslip/timeslip.html'
                 }
             },
             data: {
                 title: 'Dispatch Timeslips',
                 requireLogin: true,
                 requireFilledProfile: false,
                 activateMainMenuItem: '',
                 activateTemplate: '',
             },
             ncyBreadcrumb: {
                 label: 'Dispatch Timeslips',
                 parent: 'app.dashboard'
             }
         })
        .state('app.timeslip.dispatch', {
            url: '/timeslip-dispatch',
            views: {
                "content@app": {
                    controller: 'TimeslipCtrl',
                    templateUrl: 'views/timeslip/timeslip/timeslip.html'
                }
            },
            data: {
                title: 'Dispatch Timeslips',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Dispatch Timeslips',
                parent: 'app.dashboard'
            }
        })
        .state('app.timeslip.active', {
            url: '/timeslip-active',
            views: {
                "content@app": {
                    controller: 'TimeslipCtrl',
                    templateUrl: 'views/timeslip/timeslip/timeslip.html'
                }
            },
            data: {
                title: 'Active Timeslips',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Active Timeslips',
                parent: 'app.dashboard'
            }
        })
        .state('app.timeslip.missing', {
            url: '/timeslip-missing',
            views: {
                "content@app": {
                    controller: 'TimeslipCtrl',
                    templateUrl: 'views/timeslip/timeslip/timeslip.html'
                }
            },
            data: {
                title: 'Missing Timeslips',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Missing Timeslips',
                parent: 'app.dashboard'
            }
        })
        .state('app.timeslip.completed', {
            url: '/timeslip-completed',
            views: {
                "content@app": {
                    controller: 'TimeslipCtrl',
                    templateUrl: 'views/timeslip/timeslip/timeslip.html'
                }
            },
            data: {
                title: 'Completed Timeslips',
                requireLogin: true,
                requireFilledProfile: false,
                activateMainMenuItem: '',
                activateTemplate: '',
            },
            ncyBreadcrumb: {
                label: 'Completed Timeslips',
                parent: 'app.dashboard'
            }
        })

});