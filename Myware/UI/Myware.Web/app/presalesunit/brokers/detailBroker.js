(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('DetailBroker', DetailBroker);

    DetailBroker.$inject = ['$scope', '$timeout', '$routeParams', 'common', 'brokerFactory', 'localityFactory'];

    function DetailBroker($scope, $timeout, $routeParams, common, brokerFactory, localityFactory) {
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

        $scope.title = 'Broker Details';
        


        $scope.isClean = function () {
            return angular.equals(original, $scope.broker);
        }

        activate();

        function activate() {

            if (brokerId != 0)
            {

                brokerFactory.getBrokerById(brokerId)
                    .then(function (result) {                        
                        $scope.broker = result;

                    }, function (reason) {
                        common.logger.error(reason);
                        $scope.alerts.push({ type: 'danger', msg: reason });

                    });

            }                       
        }

        


    }
})();