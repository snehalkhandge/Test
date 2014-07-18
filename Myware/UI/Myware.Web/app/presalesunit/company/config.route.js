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
                url: '/presalesunit/company',
                config: {
                    templateUrl: '/app/presalesunit/company/company.html',
                    title: 'Manage Company',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Company'
                    }
                }
            },
            {
                url: '/presalesunit/company/edit/:companyId',
                config: {
                    templateUrl: '/app/presalesunit/company/editCompany.html',
                    title: 'Edit Company',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Edit Company'
                    }
                }
            },
            {
                url: '/presalesunit/company/:companyId',
                config: {
                    templateUrl: '/app/presalesunit/company/detailCompany.html',
                    title: 'Detail Company',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Detail Company'
                    }
                }
            }

        ];
    }
})();