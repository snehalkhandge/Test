(function () {
    'use strict';

    angular
        .module('app.postsalesunit')
        .controller('ListUnit', ListUnit);

    ListUnit.$inject = ['$scope', 'common', 'authService', 'projectFactory', 'towerFactory', 'wingFactory', 'unitFactory'];

    function ListUnit($scope, common, authService, projectFactory, towerFactory, wingFactory, unitFactory) {
        var log = common.logger.info;        
        var $q = common.$q;
        $scope.title = 'Units';
        $scope.results = {};
        $scope.totalItems = 0;
        $scope.itemsPerPage = 2;
        $scope.page = 1;
        
        $scope.setPage = function (pageNo) {
            $scope.page = pageNo;
        };
        $scope.pageChanged = function () {
            activate();
        };
        $scope.hidePagination = true;

        $scope.selectedProjectId = 0;
        $scope.selectedTowerId = 0;
        $scope.selectedWingId = 0;
                
        $scope.Projects = [];
        $scope.Towers = [];
        $scope.Wings = [];

        $scope.SearchUnits = function () {
            $scope.page = 1;
            activate();
        };

        $scope.getAllTowers = function getAllTowers() {
            towerFactory.getBuildingNamesByProjectId($scope.selectedProjectId)
                          .then(function (result) {
                              $scope.Towers = result;
                          });
        };

        $scope.getAllWings = function getAllWings() {
            wingFactory.getAllWingsByBuildingId($scope.selectedTowerId)
                          .then(function (result) {
                              $scope.Wings = result;
                          });
        };



        activate();

        function activate() {

            var searchData = {
                ProjectId: $scope.selectedProjectId,
                TowerId: $scope.selectedTowerId,     
                WingId: $scope.selectedWingId,     
                Page:$scope.page,
                PageSize: $scope.itemsPerPage
            };

            unitFactory.getUnits(searchData)
                          .then(function(result) {
                              $scope.totalItems = result.TotalItems;
                              $scope.results = result.Results;


                              if ($scope.totalItems <= $scope.itemsPerPage) {
                                  $scope.hidePagination = true;
                              } else {
                                  $scope.hidePagination = false;
                              }

                          });

            

            projectFactory.getAllProjects()
                          .then(function (result) {
                              $scope.Projects = result;
                          });
                                  
        };
    }
})();