"use strict";

angular.module('app.error', ['ui.router'])

.config(function ($stateProvider) {
    $stateProvider
    .state('app.error', {
        abstract: true,
        data: {
            title: 'Error'
        }
    })
         .state('app.error.error401', {
             url: '/error401',
             views: {
                 "content@app": {
                     templateUrl: 'views/error/error401.html',
                 }
             },
             data: {
                 title: 'Error 401',
             },
             ncyBreadcrumb: {
                 label: 'Error 401',
                 parent: 'app.dashboard'
             }
         })


    .state('app.error.error404', {
        url: '/error404',
        views: {
            "content@app": {
                templateUrl: 'views/error/error404.html',
            }
        },
        data: {
            title: 'Error 404',
        },
        ncyBreadcrumb: {
            label: 'Error 404',
            parent: 'app.dashboard'
        }
    })

    .state('app.error.error500', {
        url: '/error500',
        views: {
            "content@app": {
                templateUrl: 'views/error/error500.html',
            }
        },
        data: {
            title: 'Error 500',
        },
        ncyBreadcrumb: {
            label: 'Error 500',
            parent: 'app.dashboard'
        }
    })
})