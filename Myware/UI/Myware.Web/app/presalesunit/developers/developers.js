(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('Developers', Developers);

    Developers.$inject = ['$scope', '$timeout', '$interval', 'common','authService', 'developersFactory', 'companyFactory'];

    function Developers($scope, $timeout, $interval, common,authService, developersFactory, companyFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Developers';
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

        activate();

        function activate() {

            developersFactory.getDevelopers($scope.page, $scope.itemsPerPage, $scope.searchQuery)
                .then(function (result) {
                    
                    companyFactory.getAllCompany()
                                 .then(function (result) {

                                     $scope.companies = result.Results;

                                 }, function (reason) {
                                     common.logger.error(reason);

                                 });


                    $scope.results = result.Results;
                    $scope.totalItems = result.TotalItems;

                    if ($scope.totalItems <= $scope.itemsPerPage) {
                        $scope.hidePagination = true;
                    }

                }, function (reason) {
                        common.logger.error(reason);

                });            
        }


        $scope.addDeveloper = function () {

            $scope.inserted = {
                $id: $scope.results.length + 1,
                Id:'',
                Name: '',
                SelectedCompanies: [],

            };
            $scope.results.push($scope.inserted);
            
        };

        $scope.checkDeveloperName = function (developerName) {
            /*/if (data !== 'awesome') {
                return "Username 2 should be `awesome`";
            }*/

            if (developerName === '') {
                return "Developer name is a required field";
            }

            var deferred = $q.defer();
            deferred.resolve(

                developersFactory.uniqueDeveloper(developerName).then(function (result) {
                    if (result == 'true') {
                        return "All developer name has to be unique";
                    }
                    
                })
            );
            
            return deferred.promise;
        };

        $scope.deleteDeveloper = function (developer) {
            
            if (developer.Id != '') {
                common.logger.error("Sorry! you cannot delete a developer.");
            } else {
                $scope.results.splice(developer.$id - 1, 1);
            }
            

        };

        $scope.saveDeveloper = function (developer, Id) {

            angular.extend(developer, { Id: Id });
            angular.extend(developer, { UserId: authService.authentication.userId });

            developersFactory.saveDeveloper(developer)
                .then(function () {                    
                    $scope.page = 1;                    
                    common.logger.success("Successfully updated the item");
                    activate();
                });
                        
        };

        
        $scope.companies = [];

        $scope.showCompanies = function (developer) {
            var selected = [];

            if (developer.SelectedCompanies != null) {
                angular.forEach(developer.SelectedCompanies, function (x) {

                    angular.forEach($scope.companies, function (s) {
                        if (x == s.Id) {
                            selected.push(s.Name);
                        }

                    });

                });
            } else if (developer.DeveloperCompanies.length > 0) {
                    
                angular.forEach(developer.DeveloperCompanies, function (x) {

                    angular.forEach($scope.companies, function (s) {
                       if (x.CompanyId == s.Id) {
                                selected.push(s.Name);
                            }

                   });
                        
               });
            }

            return selected.length ? selected.join(', ') : 'Not set';
        };

    }
})();