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
        $scope.itemsPerPage = 2;
        $scope.page = 1;
        $scope.searchQuery = "all";
        $scope.setPage = function (pageNo) {
            $scope.page = pageNo;
        };
        $scope.pageChanged = function () {
            SearchCustomers();
        };
        $scope.hidePagination = false;

        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        $scope.ContactLeadFilters = {
            CustomerNames: '',
            Localities: '',
            CustomerTypes:'',
            UnitTypes: '',
            BudgetFromList: '',
            BudgetToList: '',
            ContactNumbers: '',
            Page:'',
            PageSize:''
        };

        $scope.AllCustomers = [];

        activate();

        function activate() {

        };

        $scope.SearchCustomers = function () {
            $scope.ContactLeadFilters.Page = $scope.page = 1;
            
            SearchCustomers();
        }; 

        function SearchCustomers() {

            $scope.ContactLeadFilters.Page = $scope.page;
            $scope.ContactLeadFilters.PageSize = $scope.itemsPerPage;

            customerPreSalesFactory.getCustomers($scope.ContactLeadFilters)
                         .then(function (result) {
                             $scope.AllCustomers = result.Customers;
                             $scope.totalItems = result.Total;

                             if ($scope.totalItems <= $scope.itemsPerPage) {
                                 $scope.hidePagination = true;
                             } else {
                                 $scope.hidePagination = false;
                             }

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
        };

//--------------------------------------Filters----------------------------------------------------------------

        
        $scope.CustomerNames = function () {

            var query = {
                Query: $scope.ContactLeadFilters.CustomerNames
            };
            
            var items = [];

            return customerPreSalesFactory.getAllCustomerNames(query)
                         .then(function (result) {

                             
                             angular.forEach(result, function (item) {
                                 items.push({
                                     Id: item.Id,
                                     Name:item.Name
                                 });
                             });                           
                             
                             return items;
                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
            

        };

        
        $scope.CustomerTypes = function (queryParams) {

           return customerTypeFactory.getAllCustomerTypes(queryParams)
                         .then(function (result) {
                             log = [];
                             angular.forEach(result.Results, function (value, key) {
                                 this.push({
                                     Id : value.Id,
                                     Name: value.Name
                                 });
                             }, log);

                             return log;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });

        };

        $scope.UnitTypes = function (queryParams) {
            
          return  unitTypeFactory.getAllUnitTypes(queryParams)
                .then(function (result) {
                   return result.Results;
                }, function (reason) {
                    common.logger.error(reason);
                    $scope.alerts.push({ type: 'danger', msg: reason });
                });
        };
        
        $scope.Localities = function (queryParams) {
                        
           return localityFactory.getAllLocality(queryParams)
                         .then(function (result) {

                             return result.Results

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
        };

        $scope.BudgetFromList = function (queryParams) {

            var query = {
                Query: $scope.ContactLeadFilters.BudgetFromList
            };

            return customerPreSalesFactory.getAllBudgetFrom(query)
                         .then(function (result) {

                             var items = [];
                             angular.forEach(result, function (item) {
                                 items.push({
                                     PersonalInformationId: item.PersonalInformationId,
                                     Budget: item.Budget
                                 });
                             });

                             return items;
                             
                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
        };

        $scope.BudgetToList = function (queryParams) {

            var query = {
                Query: $scope.ContactLeadFilters.BudgetToList
            };
            return customerPreSalesFactory.getAllBudgetTo(query)
                         .then(function (result) {

                             var items = [];
                             angular.forEach(result, function (item) {
                                 items.push({
                                     PersonalInformationId: item.PersonalInformationId,
                                     Budget: item.Budget
                                 });
                             });

                             return items;
                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
        };

        $scope.ContactNumbers = function (queryParams) {

            var query = {
                Query: $scope.ContactLeadFilters.ContactNumbers
            };

            return customerPreSalesFactory.getAllContactNumbers(query)
                         .then(function (result) {
                             var items = [];
                             angular.forEach(result, function (item) {
                                 items.push({
                                     PersonalInformationId: item.PersonalInformationId,
                                     Number: item.Number
                                 });
                             });

                             return items;
                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
        };
                        
    }
})();