(function () {
    'use strict';

    angular
        .module('app.postsalesunit')
        .controller('EditTower', EditTower);

    EditTower.$inject = ['$scope', '$routeParams', 'common', 'authService', 'projectFactory', 'towerFactory'];

    function EditTower($scope, $routeParams, common, authService, projectFactory, towerFactory) {
        var log = common.logger.info;        
        var $q = common.$q;                        
        var towerId = ($routeParams.towerId) ? parseInt($routeParams.towerId) : 0;

        $scope.towerId = towerId;
        $scope.title = 'Tower';
        
        $scope.Tower = {
            Id : towerId,
            BuildingNumber:'',
            BuildingName:'',
            NumberOfWings:'',
            ProjectId:''
        }
        
       
        $scope.Projects = [];
       
        $scope.BuildingNumbers = [];
        
        activate();
        getProjects();
        function activate() {


        };

        function getProjects() {
            projectFactory.getAllProjects().then(function (result) {
                $scope.Projects = result;

                if (towerId != 0) {
                    towerFactory.getTowerById(towerId).then(function (result) {

                        $scope.Tower.Id = result.Id;

                        $scope.Tower.BuildingNumber = result.BuildingNumber;
                        $scope.Tower.BuildingName = result.BuildingName;
                        $scope.Tower.NumberOfWings = result.NumberOfWings;
                        $scope.Tower.ProjectId = result.ProjectId;

                        $scope.getBuildingNumbers();

                    });
                }


            });
        };
        

        $scope.saveTower = function () {

            towerFactory.saveTower($scope.Tower)
               .then(function (result) {

                   $scope.Tower.Id = result.Id;
                   common.logger.success("Successfully saved the item");                   

               });

        };


        $scope.getBuildingNumbers = function getBuildingNumbers() {
            projectFactory.getBuildingNumbersFromProjectById($scope.Tower.ProjectId)
                          .then(function (result) {
                              $scope.BuildingNumbers = [];
                              var i = result;
                              for (i = result; i > 0; i--) {
                                  $scope.BuildingNumbers.push(i);
                              }

                          });
        };


    }
})();