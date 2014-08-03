﻿(function () {
    'use strict';

    angular
        .module('app.postsalesunit')
        .controller('ListWing', ListWing);

    ListWing.$inject = ['$scope', 'common', 'authService', 'projectFactory', 'towerFactory', 'wingFactory'];

    function ListWing($scope, common, authService, projectFactory, towerFactory, wingFactory) {
        var log = common.logger.info;        
        var $q = common.$q;
        $scope.title = 'Projects';
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

        $scope.selectedProjectedId = 0;
        $scope.selectedTowerId = 0;
                
        $scope.Projects = [];
        $scope.Towers = [];

        $scope.SearchWings = function () {
            $scope.page = 1;
            activate();
        };


        activate();

        function activate() {

            var searchData = {
                ProjectId: $scope.selectedProjectedId,                
                Page:$scope.page,
                PageSize: $scope.itemsPerPage
            };

            wingFactory.getWings(searchData)
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

            towerFactory.getAllTowers()
                          .then(function (result) {
                              $scope.Towers = result;
                        });
           
        };
    }
})();