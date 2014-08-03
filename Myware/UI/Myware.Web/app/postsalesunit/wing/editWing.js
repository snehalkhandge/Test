(function () {
    'use strict';

    angular
        .module('app.postsalesunit')
        .controller('EditWing', EditWing);

    EditWing.$inject = ['$scope', '$routeParams', 'common', 'authService', 'projectFactory', 'towerFactory', 'wingFactory'];

    function EditWing($scope, $routeParams, common, authService, projectFactory, towerFactory, wingFactory) {
        var log = common.logger.info;        
        var $q = common.$q;                        
        var wingId = ($routeParams.wingId) ? parseInt($routeParams.wingId) : 0;

        $scope.wingId = wingId;

        $scope.title = 'Wing';
        $scope.Wing = {
            Id: wingId,
            NumberOfFloors: '',
            TowerId: '',
            WingName: '',
            WingNumber: '',
            ProjectId:''
        }
        
       
        $scope.Projects = [];
        $scope.Towers = [];
        $scope.WingNumbers = [];
        
        activate();
        getProjects();
        function activate() {


        };

        function getProjects() {
            projectFactory.getAllProjects().then(function (result) {
                $scope.Projects = result;

                if (wingId != 0) {
                    wingFactory.getWingById(wingId).then(function (result) {
                                                
                        $scope.Wing.Id = result.Id;
                        $scope.Wing.NumberOfFloors = result.NumberOfFloors;
                        $scope.Wing.TowerId = result.TowerId;
                        $scope.Wing.WingName = result.WingName;
                        $scope.Wing.WingNumber = result.WingNumber;
                        $scope.Wing.ProjectId = result.ProjectId;

                        $scope.getWingNumbers();

                    });

                    towerFactory.getAllTowers()
                          .then(function (result) {                              
                              $scope.Towers = result;
                          });
                    

                    
                }


            });
        };
        

        $scope.saveWing = function () {

            wingFactory.saveWing($scope.Wing)
               .then(function (result) {

                   $scope.Wing.Id = result.Id;
                   common.logger.success("Successfully saved the item");                   

               });

        };


        $scope.getWingNumbers = function getWingNumbers() {
            towerFactory.getWingNumbersFromBuildingById($scope.Wing.TowerId)
                          .then(function (result) {
                              $scope.WingNumbers = [];
                              var i = result;
                              for (i = result; i > 0; i--) {
                                  $scope.WingNumbers.push(i);
                              }

                          });
        };

        $scope.getBuildingNames = function getBuildingNames() {
            towerFactory.getBuildingNamesByProjectId($scope.Wing.ProjectId)
                          .then(function (result) {
                              $scope.Towers = result;                             

                          });
        };

        

    }
})();