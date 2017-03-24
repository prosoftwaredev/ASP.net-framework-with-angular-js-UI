angular.module('ngHttp', ['ngFileUpload']).config(function () { }).factory('$httpService', function ($http, $q, CFG, $rootScope, Upload) {
    return {

        $get: function (obj) {
            var params = obj.params;
            var url = obj.url;

            var promised = $http.get(CFG.rest.baseURI + url, { params: params })
            .success(function (data, status, header, config) {
                $rootScope.handleErrors(status);
                return data;
            })
            .error(function (err, status, header, config) {
                //return $rootScope.handleErrors(status);
                return $rootScope.handleErrors(err, status);
            });
            return promised;
        },

        $post: function (obj) {
            var postData = obj.postData;
            var url = obj.url;
            var successToast = obj.successToast;
            var errorToast = obj.errorToast;

            var promise = $http.post(CFG.rest.baseURI + url, postData)
            .success(function (data, status, header, config) {
                if (data >= 1) {
                    if (successToast != null)
                        return $rootScope.showNotify(successToast, 'Success', 's');
                }
                else if (data == 0) {
                    $rootScope.handleErrors(status);

                    if (errorToast != null)
                        return $rootScope.showNotify(errorToast, 'Error', 'e');
                }
            })
            .error(function (err, status, header, config) {
                //return $rootScope.handleErrors(status);
                return $rootScope.handleErrors(err, status);
            });
            return promise;
        },

        $put: function (obj) {
            var postData = obj.postData;
            var url = obj.url;
            var successToast = obj.successToast;
            var errorToast = obj.errorToast;

            var promise = $http.put(CFG.rest.baseURI + url, postData)
            .success(function (data, status, header, config) {
                if (data >= 1) {
                    $rootScope.handleErrors(status);
                    return $rootScope.showNotify(successToast, 'Success', 's');
                }
                else if (data == 0) {
                    $rootScope.handleErrors(status);
                    return $rootScope.showNotify(errorToast, 'Error', 'e');
                }
            })
            .error(function (err, status, header, config) {
                //return $rootScope.handleErrors(status);
                return $rootScope.handleErrors(err, status);
            });
            return promise;
        },

        $delete: function (obj) {
            var url = obj.url;
            var successToast = obj.successToast;
            var errorToast = obj.errorToast;

            var promise = $http.delete(CFG.rest.baseURI + url, { headers: { 'Content-Type': 'application/json' } })
            .success(function (data, status, header, config) {
                if (data >= 1) {
                    $rootScope.handleErrors(status);
                    return $rootScope.showNotify(successToast, 'Success', 's');
                }
                else if (data == 0) {
                    $rootScope.handleErrors(status);
                    return $rootScope.showNotify(errorToast, 'Error', 'e');
                }
            })
            .error(function (err, status, header, config) {
                //return $rootScope.handleErrors(status);
                return $rootScope.handleErrors(err, status);
            });
            return promise;
        },

        $postMultiPart: function (obj) {
            var postData = obj.postData;
            var postFile = obj.postFile
            var url = obj.url;
            var params = obj.params;
            var successToast = obj.successToast;
            var errorToast = obj.errorToast;

            var promise = Upload.upload({
                url: CFG.rest.baseURI + url,
                method: "POST",
                data: postData,
                file: postFile,
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
                //params: params
            }).progress(function (evt) {
                $rootScope.Percent = 'Uploading... ' + parseInt(100.0 * evt.loaded / evt.total) + '%';
            }).success(function (data, status, header, config) {
                if (data >= 1) {
                    $rootScope.handleErrors(status);
                    return $rootScope.showNotify(successToast, 'Success', 's');
                }
                else if (data == 0) {
                    $rootScope.handleErrors(status);
                    return $rootScope.showNotify(errorToast, 'Error', 'e');
                }
            }).error(function (data, status, header, config) {
                //return $rootScope.handleErrors(status);
                return $rootScope.handleErrors(err, status);
            });
            return promise;
        },

        $postDelete: function (obj) {
            var postData = obj.postData;
            var url = obj.url;
            var successToast = obj.successToast;
            var errorToast = obj.errorToast;

            var promise = $http.post(CFG.rest.baseURI + url, postData)
            .success(function (data, status, header, config) {
                if (data >= 1) {
                    return $rootScope.showNotify(successToast, 'Success', 's');
                }
                else if (data == 0) {
                    $rootScope.handleErrors(status);
                    return $rootScope.showNotify(errorToast, 'Error', 'e');
                }
            })
            .error(function (err, status, header, config) {
                //return $rootScope.handleErrors(status);
                return $rootScope.handleErrors(err, status);
            });
            return promise;
        },

        $postSearch: function (obj) {
            var postData = obj.postData;
            var url = obj.url;
            var successToast = obj.successToast;
            var errorToast = obj.errorToast;

            //var promise = $http.post(CFG.rest.baseURI + url, postData, { transformRequest: angular.identity }, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            var promise = $http({
                method: 'POST',
                url: CFG.rest.baseURI + url,
                data: $.param(postData),
                transformRequest: angular.identity,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            })
            .success(function (data, status, header, config) {
                return data;
            })
            .error(function (err, status, header, config) {
                //return $rootScope.handleErrors(status);
                return $rootScope.handleErrors(err, status);
            });
            return promise;
        },
    };
});


angular.module('ngDataTable', ['datatables', 'datatables.bootstrap']).config(function () { }).factory('$dataTableService', function (DTOptionsBuilder, DTColumnBuilder, CFG, localStorageService) {
    return {
        $dtOptions: function (obj) {
            var url = obj.url;
            var type = obj.type;
            var pageSize = obj.pageSize;
            var $compile = obj.$compile;
            var $scope = obj.$scope;
            //var fnPromise = obj.fnPromise;
            var orders = obj.orders;
            var rowCallback = obj.rowCallback;

            var lang = {
                "processing": '<span class="animation-spin icon32 icon-loading"></span>',
                "search": '<span class="input-group-addon input-sm">' + '<i class="glyphicon glyphicon-search"></i>' + '</span>'
            }

            var dtOptions = DTOptionsBuilder
                .newOptions()
                .withOption('ajax', {
                    url: CFG.rest.baseURI + url,
                    type: type,
                    beforeSend: function (xhr) {
                        var authData = localStorageService.get('authorizationData');
                        if (authData) {
                            xhr.setRequestHeader("Authorization",
                            "Bearer " + authData.token);
                        }                        
                    },
                })
                .withOption('serverSide', true)
                .withDataProp('data')                
                .withOption('processing', true)
                .withOption('stateSave', true)
                .withOption('filter', true)
                .withOption('paging', true)
                .withOption('rowCallback', rowCallback)
                .withOption('language', lang)
                .withDisplayLength(pageSize)
                .withOption('order', orders)
                .withPaginationType('full_numbers')
                .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>")
                .withBootstrap()
              .withOption('fnDrawCallback', function (settings) {
                  $compile(angular.element('#' + settings.sTableId).contents())($scope);
              });

            return dtOptions;
        },


        $dtColumns: function (obj) {
            var DTColumnBuilderArr = [];
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].containsHtml == true) {
                    var currentColumn = DTColumnBuilder.newColumn(obj[i].Column).withTitle(obj[i].Title).withOption('name', obj[i].Column).notSortable().renderWith(obj[i].renderWith).withOption('width', obj[i].Width + '%');
                    DTColumnBuilderArr.push(currentColumn);
                }
                else {
                    var currentColumn = DTColumnBuilder.newColumn(obj[i].Column).withTitle(obj[i].Title).withOption('name', obj[i].Column);
                    DTColumnBuilderArr.push(currentColumn);
                }
            }
            return DTColumnBuilderArr;
        },

        $dtCustomOptions: function (obj) {

            var orders = obj.orders;
            var pageSize = obj.pageSize;

            var dtOptions = DTOptionsBuilder
                .newOptions()
                .withOption('sDom', '')  //lfrtip
                .withOption('order', orders)
                //.withOption('iDisplayLength', pageSize)
                //.withDisplayLength(pageSize)

            return dtOptions;
        }
    };
})