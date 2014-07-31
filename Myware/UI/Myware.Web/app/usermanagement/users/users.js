(function () {
    'use strict';

    angular
        .module('app.usermanagement')
        .controller('Users', Users);

    Users.$inject = ['$scope', 'common', 'userFactory'];

    function Users($scope, common, userFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        
        $scope.title = "Manage Users";
        $scope.results = {};
        $scope.totalItems = 0;
        $scope.itemsPerPage = 2;
        $scope.page = 1;
        $scope.searchQuery = "all";
        $scope.setPage = function (pageNo) {
            $scope.page = pageNo;
        };
        $scope.pageChanged = function () {
            activate();
        };
        $scope.hidePagination = false;

        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };


        activate();

        function activate() {
            
            userFactory.getUsers($scope.page, $scope.itemsPerPage, $scope.searchQuery)
                       .then(function (result) {
                           $scope.results = result.Users;
                           $scope.totalItems = result.Total;
                           if ($scope.totalItems <= $scope.itemsPerPage) {
                               $scope.hidePagination = true;
                           }

                       }, function (reason) {
                           common.logger.error(reason);

                       }); 

            
        }

    }
})();