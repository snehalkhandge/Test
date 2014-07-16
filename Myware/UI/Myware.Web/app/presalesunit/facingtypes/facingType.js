(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('FacingTypes', FacingTypes);

    FacingTypes.$inject = ['$scope', '$timeout', '$interval', 'common', 'authService', 'facingTypeFactory'];

    function FacingTypes($scope, $timeout, $interval, common, authService, facingTypeFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Facing Types';
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
            
           facingTypeFactory.getFacingTypes($scope.page, $scope.itemsPerPage, $scope.searchQuery)
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


        $scope.addFacingType = function () {

            $scope.inserted = {
                $id: $scope.results.length + 1,
                Id:'',
                Name: ''
            };
            $scope.results.push($scope.inserted);
            
        };

        $scope.checkFacingTypeName = function (facingTypeName) {
            /*/if (data !== 'awesome') {
                return "Username 2 should be `awesome`";
            }*/

            if (facingTypeName === '') {
                return "Facing type name is a required field";
            }

            var deferred = $q.defer();
            deferred.resolve(

                facingTypeFactory.uniqueFacingType(facingTypeName).then(function (result) {
                    if (result == 'true') {
                        return "All facing type name has to be unique";
                    }
                    
                })
            );
            
            return deferred.promise;
        };

        $scope.deleteFacingType = function (facingType) {
            
            if (facingType.Id != '') {
                common.logger.error("Sorry! you cannot delete a facing type.");
            } else {
                $scope.results.splice(facingType.$id - 1, 1);
            }
            

        };

        $scope.saveFacingType = function (facingType, Id) {

            angular.extend(facingType, { Id: Id});
            angular.extend(facingType, { UserId: authService.authentication.userId });

            facingTypeFactory.saveFacingType(facingType)
                .then(function () {                    
                    $scope.page = 1;                    
                    common.logger.success("Successfully updated the item");
                    activate();
                });
                        
        };

    }
})();