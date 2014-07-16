(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('Locations', Locations);

    Locations.$inject = ['$scope', '$timeout', '$interval', 'common', 'authService', 'locationFactory'];

    function Locations($scope, $timeout, $interval, common, authService, locationFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Locations';
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
            
           locationFactory.getLocations($scope.page, $scope.itemsPerPage, $scope.searchQuery)
                .then(function (result) {

                    $scope.results = result.Results;
                    $scope.totalItems = result.TotalItems;

                    if ($scope.totalItems <= $scope.itemsPerPage) {
                        $scope.hidePagination = true;
                    }

                }, function (reason) {
                        common.logger.error(reason);

                });            
        }


        $scope.addLocation = function () {

            $scope.inserted = {
                $id: $scope.results.length + 1,
                Id:'',
                City: '',
                State: '',
                Country: ''
            };
            $scope.results.push($scope.inserted);
            
        };

        $scope.checkCityName = function (cityName) {
            /*/if (data !== 'awesome') {
                return "Username 2 should be `awesome`";
            }*/

            if (cityName === '') {
                return "City name is a required field";
            }

            var deferred = $q.defer();
            deferred.resolve(

                locationFactory.uniqueLocation(cityName).then(function (result) {
                    if (result == 'true') {
                        return "All city name has to be unique";
                    }
                    
                })
            );
            
            return deferred.promise;
        };

        $scope.checkStateName = function (stateName) {
            
            if (stateName === '') {
                return "State name is a required field";
            }

        };


        $scope.checkCountryName = function (countryName) {

            if (countryName === '') {
                return "Country name is a required field";
            }

        };

        $scope.deleteLocation = function (location) {
            
            if (location.Id != '') {
                common.logger.error("Sorry! you cannot delete a location type.");
            } else {
                $scope.results.splice(location.$id - 1, 1);
            }
            

        };

        $scope.saveLocation = function (location, Id) {

            angular.extend(location, { Id: Id});
            angular.extend(location, { UserId: authService.authentication.userId });

            locationFactory.saveLocation(location)
                .then(function () {                    
                    $scope.page = 1;                    
                    common.logger.success("Successfully updated the item");
                    activate();
                });
                        
        };

    }
})();