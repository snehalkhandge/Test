(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('Locality', Locality);

    Locality.$inject = ['$scope', 'common','authService', 'localityFactory', 'locationFactory'];

    function Locality($scope, common,authService, localityFactory, locationFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Localities';
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

            localityFactory.getLocality($scope.page, $scope.itemsPerPage, $scope.searchQuery)
                .then(function (result) {


                    locationFactory.getAllLocations()
                                 .then(function (result) {

                                     $scope.locations = result.Results;

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


        $scope.addLocality = function () {

            $scope.inserted = {
                $id: $scope.results.length + 1,
                Id:'',
                Name: '',
                SelectedLocation: '',

            };
            $scope.results.push($scope.inserted);
            
        };

        $scope.checkLocalityName = function (localityName) {
            /*/if (data !== 'awesome') {
                return "Username 2 should be `awesome`";
            }*/

            if (localityName === '') {
                return "Locality name is a required field";
            }

        };

        $scope.deleteLocality = function (locality) {
            /*common.logger.error("Permission Id : " + permission.Id);
            common.logger.error("Index : " + permission.$id); */
            if (locality.Id != '') {
                common.logger.error("Sorry! you cannot delete a locality.");
            } else {
                $scope.results.splice(locality.$id - 1, 1);
            }
            

        };

        $scope.saveLocality = function (locality,Id) {

            angular.extend(locality, { Id: Id, UserId: authService.authentication.userId });

            localityFactory.saveLocality(locality)
                .then(function () {                    
                    $scope.page = 1;                    
                    common.logger.success("Successfully updated the item");
                    activate();
                });
                        
        };

        
        $scope.locations = [];

        $scope.showLocations = function (locationId) {
            var selected = [];

             angular.forEach($scope.locations, function (s) {
                 if (locationId == s.Id) {
                     var place = s.City + "," + s.State;
                     selected.push(place);
                        }

                    });

            return selected.length ? selected.join(', ') : 'Not set';
        };

    }
})();