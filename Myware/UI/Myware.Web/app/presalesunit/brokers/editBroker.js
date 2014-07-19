(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('EditBroker', EditBroker);

    EditBroker.$inject = ['$scope', '$timeout', '$routeParams', '$upload', 'common', 'authService', 'brokerFactory', 'localityFactory'];

    function EditBroker($scope, $timeout, $routeParams,$upload, common,authService, brokerFactory, localityFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;                 
       
        var brokerId = ($routeParams.brokerId) ? parseInt($routeParams.brokerId) : 0;
        var defaultForm = {
            Id: brokerId,
            Name: '',
            CompanyName: '',
            Address: '',
            Locality: '',
            Email: '',
            PanCard: '',
            ReferenceName: '',
            ImageUrl: '',
            LocalityId: '',
            ContactNumbers: []
        };

        $scope.broker = defaultForm;

        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        $scope.title = (brokerId > 0) ? 'Edit Broker' : 'Add Broker';
        $scope.buttonText = (brokerId > 0) ? 'Update Broker' : 'Add New Broker';              
        $scope.Localities = {};
        $scope.SelectedLocalities = {};


        $scope.isClean = function () {
            return angular.equals(original, $scope.broker);
        }

        
        $scope.saveBroker = function (broker) {
            
            angular.extend(broker, { UserId: authService.authentication.userId });
            delete broker.Location;
            delete broker.Locality;
            
            brokerFactory.saveBroker(broker)
                .then(function () {
                    $scope.buttonText = "Update Broker"
                    common.logger.success("Successfully saved the item");
                    $scope.alerts.push({ type: 'success', msg: "Successfully saved the item" });
                });
        };

        $scope.resetForm = function () {
            $scope.myForm.$setPristine();
            $scope.broker = defaultForm;
        }

        var newDate = new Date();
        $scope.addContactNumber = function () {

            $scope.inserted = {
                $id: $scope.broker.ContactNumbers.length + 1,
                PhoneNumber: '',
                Type: ''
                
            };
            $scope.broker.ContactNumbers.push($scope.inserted);
        };

        $scope.removeContactNumber = function($index)
        {
            $scope.broker.ContactNumbers.splice($index,1);
        }

        $scope.onFileSelect = function ($files) {

                //$files: an array of files selected, each file has name, size, and type.
                for (var i = 0; i < $files.length; i++) {
                    var file = $files[i];
                    $scope.upload = $upload.upload({
                        url: common.apiUrl + '/saveBrokerImage', //upload.php script, node.js route, or servlet url
                        method: 'POST',
                        // headers: {'header-key': 'header-value'},
                        // withCredentials: true,
                        data: { brokerObject: { id: brokerId } },
                        file: file,
                    }).progress(function (evt) {
                        $scope.progress = parseInt(100.0 * evt.loaded / evt.total);

                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        $scope.broker.ImageUrl = data;
                        common.logger.success("Successfully saved the image.");
                        $scope.alerts.push({ type: 'success', msg: "Successfully saved the image." });
                    })
                    .error(function () {
                        $scope.broker.ImageUrl = "http://placehold.it/150x150.png&text=Error upload image";
                        common.logger.error("Error, while saving the image.");
                        $scope.alerts.push({ type: 'danger', msg: "Error, while saving the image." });
                    });                    
                }
            };


        activate();

        function activate() {

            if (brokerId != 0)
            {

                brokerFactory.getBrokerById(brokerId)
                    .then(function (result) {                        
                        $scope.broker = result;

                        if(result.ContactNumbers.length == 0)
                        {
                            $scope.addContactNumber();
                        }

                    }, function (reason) {
                        common.logger.error(reason);
                        $scope.alerts.push({ type: 'danger', msg: reason });

                    });

            }
            else {
                $scope.addContactNumber();
            }

            localityFactory.getAllLocality()
                         .then(function (result) {

                             $scope.Localities = result.Results;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });
            
        }

        


    }
})();