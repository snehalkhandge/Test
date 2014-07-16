(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('UnitTypes', UnitTypes);

    UnitTypes.$inject = ['$scope', '$timeout', '$interval', 'common', 'authService', 'unitTypeFactory'];

    function UnitTypes($scope, $timeout, $interval, common, authService, unitTypeFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Unit Types';
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
            
           unitTypeFactory.getUnitTypes($scope.page, $scope.itemsPerPage, $scope.searchQuery)
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


        $scope.addUnitType = function () {

            $scope.inserted = {
                $id: $scope.results.length + 1,
                Id:'',
                Name: ''
            };
            $scope.results.push($scope.inserted);
            
        };

        $scope.checkUnitTypeName = function (unitTypeName) {
            /*/if (data !== 'awesome') {
                return "Username 2 should be `awesome`";
            }*/

            if (unitTypeName === '') {
                return "Unit type name is a required field";
            }

            var deferred = $q.defer();
            deferred.resolve(

                unitTypeFactory.uniqueUnitType(unitTypeName).then(function (result) {
                    if (result == 'true') {
                        return "All unit type name has to be unique";
                    }
                    
                })
            );
            
            return deferred.promise;
        };

        $scope.deleteUnitType = function (unitType) {
            
            if (unitType.Id != '') {
                common.logger.error("Sorry! you cannot delete a unit type.");
            } else {
                $scope.results.splice(unitType.$id - 1, 1);
            }
            

        };

        $scope.saveUnitType = function (unitType, Id) {

            angular.extend(unitType, { Id: Id});
            angular.extend(unitType, { UserId: authService.authentication.userId });

            unitTypeFactory.saveUnitType(unitType)
                .then(function () {                    
                    $scope.page = 1;                    
                    common.logger.success("Successfully updated the item");
                    activate();
                });
                        
        };

    }
})();