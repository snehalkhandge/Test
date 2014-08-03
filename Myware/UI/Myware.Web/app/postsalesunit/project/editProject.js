(function () {
    'use strict';

    angular
        .module('app.postsalesunit')
        .controller('EditProject', EditProject);

    EditProject.$inject = ['$scope', '$routeParams', 'common', 'authService', 'projectFactory', 'companyFactory', 'developersFactory', '$upload'];

    function EditProject($scope, $routeParams, common, authService, projectFactory, companyFactory, developersFactory, $upload) {
        var log = common.logger.info;        
        var $q = common.$q;                        
        var projectId = ($routeParams.projectId) ? parseInt($routeParams.projectId) : 0;

        $scope.projectId = projectId;

        $scope.projectView = {
            OtherInformation: {},
            PropertyCharges: {},
            BankDetails:{}
        };

        $scope.projectView.ProjectInformation = {
            disabled: false,
            active: true,
            Id: '',
            ProjectId: '',
            ProjectName: '',
            ProjectTypeId:''
        };

        $scope.projectView.OtherInformation = {
            disabled: false,
            active: false,
            CompanyId: '',
            Developers: [],
            Id:'',
            PlotNumber:'',
            SurveyOrSectorNumber:'',
            Locality:'',
            City:'',
            PlotArea:'',
            PlotAreaUnit:'',
            Address:'',
            FSI:'',
            NumberOfBuilding:'',
            NumberOfShops:'',
            NumberOfFlats:'',
            NumberOfOffices:'',
            Amneties:'',
            FloorPlan:'',
            ProjectId:''
                   
        };

        $scope.Developer = {
            DeveloperId :'',
            DeveloperName :'',
            ProjectOtherInformationId : ''
        }


        $scope.projectView.PropertyCharges = {
            disabled: false,
            active: false,
            Id:'',
            DevelopmentCharge:'',
            OtherCharge:'',
            LumpSum:'',
            BasicQuateRate:'',
            FloorRiseRate:'',
            FloorNumberOnWord:'',
            PenaltyDefaulter:'',
            GracePeriod:'',
            ProjectId: '',
            Parkings:[]
        };

        $scope.projectView.BankDetails = {
            disabled: false,
            active: false,
            Accounts:[]
        };


        $scope.projectTypes = {};
        $scope.Companies = [];
        $scope.PlotAreaUnits = [
            { Name: 'Sq. Feet' },
            { Name: 'Sq. Meter' },            
            { Name: 'Sq. Kilometer' },
            { Name: 'Sq. Yard' },
            { Name: 'Acre' },
            { Name: 'Hectare' }
        ];

        $scope.ParkingTypes = [
            { Name: 'Open' },
            { Name: 'Close' },
            { Name: 'Stilt' },
            { Name: 'Basement' },
            { Name: 'Shade' },
            { Name: 'Podium' }
        ];

        $scope.addParkingType = function () {

            $scope.inserted = {
                $id: $scope.projectView.PropertyCharges.Parkings.length + 1,
                Type: '',
                Count: ''

            };
            $scope.projectView.BankDetails.Accounts.push($scope.inserted);
        };

        $scope.removeParkingType = function ($index) {
            $scope.projectView.BankDetails.Accounts.splice($index, 1);
        };

        $scope.addAccount = function () {

            $scope.inserted = {
                $id: $scope.projectView.BankDetails.Accounts.length + 1,
                AccountNumber: '',
                BankName: '',
                BranchName: '',
                Id:''

            };
            $scope.projectView.BankDetails.Accounts.push($scope.inserted);
        };

        $scope.removeAccount = function ($index) {
            $scope.projectView.BankDetails.Accounts.splice($index, 1);
        };


        $scope.getDevelopers = function (companyId) {

            developersFactory.getDeveloperByCompanyId(companyId)
                .then(function (result) {

                    $scope.projectView.OtherInformation.Developers = [];

                    angular.forEach(result, function (value, key) {
                        this.push({
                            DeveloperId: value.Id,
                            DeveloperName: value.Name                            
                        });
                    }, $scope.projectView.OtherInformation.Developers);

            });

        };

        $scope.saveBaseProjectInformation = function (projectInformation) {

            angular.extend(projectInformation, { UserId: authService.authentication.userId });

            projectFactory.saveProjectBase(projectInformation)
                .then(function (result) {

                    $scope.projectView.ProjectInformation.Id = result.Id;
                    common.logger.success("Successfully saved the item");
                    $scope.projectView.ProjectInformation.disabled = false;
                    $scope.projectView.ProjectInformation.active = false;
                    $scope.projectView.OtherInformation.disabled = false;
                    $scope.projectView.OtherInformation.active = true;

                    
                });
        };

        $scope.saveOtherProjectInformation = function (otherInformation) {

            angular.extend(otherInformation, { UserId: authService.authentication.userId });
            angular.extend(otherInformation, { ProjectId: $scope.projectView.ProjectInformation.Id });
            angular.extend(otherInformation, { Company: { Name: 'name' } });

            projectFactory.saveProjectOtherInformation(otherInformation)
                .then(function (result) {

                    $scope.projectView.OtherInformation.Id = result.Id;
                    common.logger.success("Successfully saved the item");                    
                    $scope.projectView.OtherInformation.disabled = false;
                    $scope.projectView.OtherInformation.active = false;

                    $scope.projectView.PropertyCharges.disabled = false;
                    $scope.projectView.PropertyCharges.active = true;

                });

        };

        $scope.savePropertyCharges = function (propertyCharges) {

            angular.extend(propertyCharges, { UserId: authService.authentication.userId });
            angular.extend(propertyCharges, { ProjectId: $scope.projectView.ProjectInformation.Id });
            
            projectFactory.saveProjectPropertyCharge(propertyCharges)
                .then(function (result) {

                    $scope.projectView.OtherInformation.Id = result.Id;
                    common.logger.success("Successfully saved the item");
                    $scope.projectView.OtherInformation.disabled = false;
                    $scope.projectView.OtherInformation.active = false;

                    $scope.projectView.PropertyCharges.disabled = false;
                    $scope.projectView.PropertyCharges.active = true;

                });

        };


        $scope.saveProjectBankDetail = function (bankDetails) {

            angular.forEach(bankDetails, function (value, key) {

                if (value.AccountNumber != '')
                {
                    angular.extend(value, { ProjectId: $scope.projectView.ProjectInformation.Id });

                    projectFactory.saveProjectBankDetail(value)
                        .then(function (result) {


                        });
                }                

            });

        };

        activate();
        getCompanies();
        function activate() {

            if (projectId != 0)
            {
                $scope.projectView.ProjectInformation.Id = projectId;
                
                projectFactory.getProjectById(projectId)
                              .then(function (result) {                                  
                                  $scope.projectView.ProjectInformation.Id = result.ProjectBase.Id;
                                  $scope.projectView.ProjectInformation.ProjectId = result.ProjectBase.ProjectId;
                                  $scope.projectView.ProjectInformation.ProjectName = result.ProjectBase.ProjectName;
                                  $scope.projectView.ProjectInformation.ProjectTypeId = result.ProjectBase.ProjectTypeId;
                                  $scope.projectView.BankDetails.Accounts = result.BankDetails;
                                  $scope.projectView.OtherInformation = result.ProjectInformation;
                                  $scope.projectView.PropertyCharges = result.PropertyCharges;
                                  

                              });
                    
                
            
            }

            projectFactory.getProjectTypes()
                          .then(function (result) {
                              $scope.projectTypes = result;
                          });

            $scope.addParkingType();
            $scope.addAccount();
            
        };
     

        function getCompanies() {
            companyFactory.getAllCompany().then(function(result) {
                $scope.Companies = result.Results;
            });
        };

        $scope.onFileSelect = function ($files) {

            //$files: an array of files selected, each file has name, size, and type.
            for (var i = 0; i < $files.length; i++) {
                var file = $files[i];
                $scope.upload = $upload.upload({
                    url: common.apiUrl + '/saveFloorPlans', //upload.php script, node.js route, or servlet url
                    method: 'POST',
                    // headers: {'header-key': 'header-value'},
                    // withCredentials: true,
                    data: { projectObject: { id: '' } },
                    file: file,
                }).progress(function (evt) {
                    $scope.progress = parseInt(100.0 * evt.loaded / evt.total);

                }).success(function (data, status, headers, config) {
                    // file is uploaded successfully
                    $scope.projectView.OtherInformation.FloorPlan = data;
                    common.logger.success("Successfully saved the image.");
                    
                })
                .error(function () {                    
                    common.logger.error("Error, while saving the image.");                    
                });
            }
        };


    }
})();