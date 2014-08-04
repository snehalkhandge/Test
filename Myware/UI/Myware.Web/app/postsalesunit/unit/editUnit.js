(function () {
	'use strict';

	angular
		.module('app.postsalesunit')
		.controller('EditUnit', EditUnit);

	EditUnit.$inject = ['$scope', '$routeParams', 'common', 'authService', 'projectFactory', 'towerFactory', 'wingFactory','unitFactory', 'unitTypeFactory'];

	function EditUnit($scope, $routeParams, common, authService, projectFactory, towerFactory, wingFactory, unitFactory, unitTypeFactory) {
		var log = common.logger.info;        
		var $q = common.$q;                        
		var unitId = ($routeParams.unitId) ? parseInt($routeParams.unitId) : 0;

		$scope.unitId = unitId;

		$scope.title = 'Unit';
		$scope.Unit = {
			Id: unitId,
			BasicRate: '',
			BuildingType:'',
			CarpetArea: '',
			CarpetAreaUnit: '',
			DevelopmentCharge: '',
			FloorNumber: '',
			FloorRiseRate: '',
			OtherCharge: '',
			ProjectId: '',
			SaleableArea: '',
			SaleableAreaUnit: '',
			Status: '',
			TowerId: '',
			UnitName: '',
			UnitNumber: '',
			UnitType: '',
			WingId: '',
			Project: [],
			Tower: [],
			Wing: []
		};
				
	   
		$scope.Projects = [];
		$scope.Towers = [];
		$scope.Wings = [];

		$scope.UnitTypes = [];

		$scope.CarpetAreaUnits = [
			{ Name: 'Sq. Feet' },
			{ Name: 'Sq. Meter' },
			{ Name: 'Sq. Kilometer' },
			{ Name: 'Sq. Yard' },
			{ Name: 'Acre' },
			{ Name: 'Hectare' }
		];
		$scope.SaleableAreaUnits = [
			{ Name: 'Sq. Feet' },
			{ Name: 'Sq. Meter' },
			{ Name: 'Sq. Kilometer' },
			{ Name: 'Sq. Yard' },
			{ Name: 'Acre' },
			{ Name: 'Hectare' }
		];
		$scope.Statuses = [
			{ Name: 'Open' },
			{ Name: 'Close' },
			{ Name: 'Sold' }
		];
		$scope.BuildingTypes = [
			{ Name: 'Flat' },
			{ Name: 'Shop' },
			{ Name: 'Office' }
		];
		activate();
		getUnits();
		function activate() {

			unitTypeFactory.getAllUnitTypes()
				.then(function (result) {
					$scope.UnitTypes = result.Results;

				}, function (reason) {
					common.logger.error(reason);
					$scope.alerts.push({ type: 'danger', msg: reason });
				});

		};

		function getUnits() {

		    wingFactory.getAllWings()
					   .then(function (result) {
						     $scope.Wings = result;
					   });

		    towerFactory.getAllTowers()
						  .then(function (result) {
						      $scope.Towers = result;
						  });
		    
						
			projectFactory.getAllProjects().then(function (result) {
				$scope.Projects = result;

				if (unitId != 0) {
					unitFactory.getUnitById(unitId).then(function (unit) {
												
						$scope.Unit.Id = unit.Id;
						$scope.Unit.BasicRate = unit.BasicRate;
						$scope.Unit.BuildingType = unit.BuildingType;
						$scope.Unit.CarpetArea = unit.CarpetArea;
						$scope.Unit.CarpetAreaUnit = unit.CarpetAreaUnit;
						$scope.Unit.DevelopmentCharge = unit.DevelopmentCharge;
						$scope.Unit.FloorNumber = unit.FloorNumber;
						$scope.Unit.FloorRiseRate = unit.FloorRiseRate;
						$scope.Unit.OtherCharge = unit.OtherCharge;
						$scope.Unit.ProjectId = unit.ProjectId;
						$scope.Unit.SaleableArea = unit.SaleableArea;
						$scope.Unit.SaleableAreaUnit = unit.SaleableAreaUnit;
						$scope.Unit.Status = unit.Status;
						$scope.Unit.TowerId = unit.TowerId;
						$scope.Unit.UnitName = unit.UnitName;
						$scope.Unit.UnitNumber = unit.UnitNumber;
						$scope.Unit.UnitType = unit.UnitType;
						$scope.Unit.WingId = unit.WingId;
						$scope.Unit.Project = unit.Project;
						$scope.Unit.Tower = unit.Tower;
						$scope.Unit.Wing = unit.Wing;

						

						generateUnitName();

					});

				}


			});
		};

		function getName(arr, id) {
			for(var i=0; i<arr.length; i++) {

				if (arr[i].Id == id){
					return arr[i].Name;
				}
			}
		}


		function generateUnitName()
		{
			$scope.Unit.UnitName = getName($scope.Projects, $scope.Unit.ProjectId)+"-"+getName($scope.Towers, $scope.Unit.TowerId)+"-"+getName($scope.Wings, $scope.Unit.WingId)+"-"+$scope.Unit.UnitNumber;
		}
		

		$scope.saveUnit = function () {

			generateUnitName();

			unitFactory.saveUnit($scope.Unit)
			   .then(function (result) {

				   $scope.Unit.Id = result.Id;
				   common.logger.success("Successfully saved the item");                   

			   });

		};


		$scope.getAllTowers = function () {
			getAllTowers();
		};


		function getAllTowers() {
			towerFactory.getBuildingNamesByProjectId($scope.Unit.ProjectId)
						  .then(function (result) {
							  $scope.Towers = result;
						  });

			generateUnitName();
		};

		$scope.getAllWings = function () {

			getAllWings();
		};


	   function getAllWings() {
		   wingFactory.getAllWingsByBuildingId($scope.Unit.TowerId)
						 .then(function (result) {
							 $scope.Wings = result;
						 });

		   generateUnitName();

	   };


		

	}
})();