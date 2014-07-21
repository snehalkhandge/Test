(function () {
    'use strict';

    angular
        .module('app.presales')
        .run(['routehelper', function (routehelper) {
            routehelper.configureRoutes(getRoutes());
        }]);

    function getRoutes() {
        return [
            
            {
                url: '/presales/enquiry/edit/:enquiryId',
                config: {
                    templateUrl: '/app/presales/enquiries/editEnquiry.html',
                    title: 'Edit Enquiry',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Add Enquiry'
                    }
                }
            }
        ];
    }
})();