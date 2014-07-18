(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .run(['routehelper', function (routehelper) {
            routehelper.configureRoutes(getRoutes());
        }]);

    function getRoutes() {
        return [
            
            {
                url: '/presalesunit/brokers',
                config: {
                    templateUrl: '/app/presalesunit/brokers/brokers.html',
                    title: 'Manage Brokers',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Brokers'
                    }
                }
            },
            {
                url: '/presalesunit/broker/edit/:brokerId',
                config: {
                    templateUrl: '/app/presalesunit/brokers/editBroker.html',
                    title: 'Edit Broker',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Edit Broker'
                    }
                }
            },
            {
                url: '/presalesunit/broker/:brokerId',
                config: {
                    templateUrl: '/app/presalesunit/brokers/detailBroker.html',
                    title: 'Detail Company',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Detail Broker'
                    }
                }
            }

        ];
    }
})();