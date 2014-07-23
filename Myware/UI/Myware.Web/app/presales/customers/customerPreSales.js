(function () {
    'use strict';

    angular
        .module('app.presales')
        .controller('CustomerPreSales', CustomerPreSales);

    CustomerPreSales.$inject = ['$scope', 'common', 'customerTypeFactory', 'localityFactory', 'unitTypeFactory', 'customerPreSalesFactory'];

    function CustomerPreSales($scope, common, customerTypeFactory, localityFactory, unitTypeFactory, customerPreSalesFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Broker';
        $scope.results = {};
        $scope.totalItems = 0;
        $scope.itemsPerPage = 7;
        $scope.page = 1;
        $scope.searchQuery = "all";
        $scope.setPage = function (pageNo) {
            $scope.page = pageNo;
        };
        $scope.pageChanged = function () {
            activate();
        };
        $scope.hidePagination = false;


        $scope.ContactLeadFilters = {
            CustomerNames: [],
            Localities: [],
            CustomerTypes:[],
            UnitTypes: [],
            BudgetFromList: [],
            BudgetToList: [],
            ContactNumbers: [],
            page:'',
            pageSize:''
        };

        $scope.AllCustomers = [];

        activate();

        function activate() {


        };


        $scope.SearchCustomers = function () {

            $scope.ContactLeadFilters.page = $scope.page;
            $scope.ContactLeadFilters.pageSize = $scope.itemsPerPage;

            customerPreSalesFactory.getCustomers($scope.ContactLeadFilters)
                         .then(function (result) {
                             $scope.AllCustomers = result.Results
                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
        };

//--------------------------------------Filters----------------------------------------------------------------

        $scope.CustomerNames = function (queryParams) {

            customerPreSalesFactory.getAllCustomerNames(queryParams)
                         .then(function (result) {

                             return result.Results;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });

        };

        $scope.groupSelectCustomerNames = {
            minimumInputLength: 5,
            multiple: true,
            ajax: {
                data: function (term, page) {
                    return { query: term };
                },
                quietMillis: 500,
                transport: $scope.CustomerNames,
                results: function (data, page) {
                    {
                        results: data
                    };
                }
            }
        };

        $scope.CustomerTypes = function (queryParams) {

            customerTypeFactory.getAllCustomerTypes(queryParams)
                         .then(function (result) {

                            return result.Results;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });

        };

        $scope.groupSelectCustomerTypes = {
            minimumInputLength: 5,
            multiple: true,
            ajax: {
                data: function (term, page) {
                    return { query: term };
                },
                quietMillis: 500,
                transport: $scope.CustomerTypes,
                results: function (data, page) {
                    {
                        results: data
                    };
                }
            }
        };
        

        $scope.UnitTypes = function (queryParams) {
            
            unitTypeFactory.getAllUnitTypes(queryParams)
                .then(function (result) {
                   return result.Results;
                }, function (reason) {
                    common.logger.error(reason);
                    $scope.alerts.push({ type: 'danger', msg: reason });
                });
        };

        $scope.groupSelectUnitTypes = {
            minimumInputLength: 5,
            multiple: true,
            ajax: {
                data: function (term, page) {
                    return { query: term };
                },
                quietMillis: 500,
                transport: $scope.UnitTypes,
                results: function (data, page) {
                    {
                        results: data
                    };
                }
            }
        };

        
        $scope.Localities = function (queryParams) {
                        
            localityFactory.getAllLocality(queryParams)
                         .then(function (result) {

                             return result.Results

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
        };

        $scope.groupSelectLocalities = {
            minimumInputLength: 5,
            multiple: true,
            ajax: {
                data: function (term, page) {
                    return { query: term };
                },
                quietMillis: 500,
                transport: $scope.Localities,
                results: function (data, page) {
                    {
                        results: data
                    };
                }
            }
        };



        $scope.BudgetFromList = function (queryParams) {

            customerPreSalesFactory.getAllBudgetFrom(queryParams)
                         .then(function (result) {
                             return result.Results
                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
        };

        $scope.groupSelectBudgetFromList = {
            minimumInputLength: 5,
            multiple: true,
            ajax: {
                data: function (term, page) {
                    return { query: term };
                },
                quietMillis: 500,
                transport: $scope.BudgetFromList,
                results: function (data, page) {
                    {
                        results: data
                    };
                }
            }
        };


        $scope.BudgetToList = function (queryParams) {

            customerPreSalesFactory.getAllBudgetTo(queryParams)
                         .then(function (result) {
                             return result.Results
                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
        };

        $scope.groupSelectBudgetToList = {
            minimumInputLength: 5,
            multiple: true,
            ajax: {
                data: function (term, page) {
                    return { query: term };
                },
                quietMillis: 500,
                transport: $scope.BudgetToList,
                results: function (data, page) {
                    {
                        results: data
                    };
                }
            }
        };


        $scope.ContactNumbers = function (queryParams) {

            customerPreSalesFactory.getAllBudgetTo(queryParams)
                         .then(function (result) {
                             return result.Results
                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
        };

        $scope.groupSelectContactNumbers = {
            minimumInputLength: 5,
            multiple: true,
            ajax: {
                data: function (term, page) {
                    return { query: term };
                },
                quietMillis: 500,
                transport: $scope.ContactNumbers,
                results: function (data, page) {
                    {
                        results: data
                    };
                }
            }
        };
        
    }
})();