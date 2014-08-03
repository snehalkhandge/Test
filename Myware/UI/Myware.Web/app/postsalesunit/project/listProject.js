(function () {
    'use strict';

    angular
        .module('app.postsalesunit')
        .controller('ListProject', ListProject);

    ListProject.$inject = ['$scope', 'common', 'authService', 'projectFactory'];

    function ListProject($scope, common, authService, projectFactory) {
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
        $scope.hidePagination = false;

        $scope.selectedCompanyName = '';
        $scope.selectedLocality = '';
        $scope.selectedCity = '';
        
        $scope.CompanyNames = '';
        $scope.Localities = '';
        $scope.Cities = '';

        $scope.SearchProjects = function () {
            $scope.page = 1;
            activate();
        };


        activate();

        function activate() {

            var searchData = {
                CompanyName : $scope.selectedCompanyName,
                Locality : $scope.selectedLocality,
                City : $scope.selectedCity,
                Page:$scope.page,
                PageSize: $scope.itemsPerPage
            };

            projectFactory.getProjects(searchData)
                          .then(function(result) {
                              $scope.totalItems = result.TotalItems;
                              $scope.results = result.Results;


                              if ($scope.totalItems <= $scope.itemsPerPage) {
                                  $scope.hidePagination = true;
                              } else {
                                  $scope.hidePagination = false;
                              }

                          });

            projectFactory.getProjectsRelatedCompanies()
                          .then(function (result) {
                              $scope.CompanyNames = result;
                        });
            projectFactory.getProjectsRelatedCities()
                          .then(function (result) {
                              $scope.Cities = result;
                        });
            projectFactory.getProjectsRelatedLocalities()
                          .then(function (result) {
                              $scope.Localities = result;
                            });
        };
    }
})();