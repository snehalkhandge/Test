﻿(function () {
    'use strict';

    angular
        .module('app.presales')
        .run(['routehelper', function (routehelper) {
            routehelper.configureRoutes(getRoutes());
        }]);

    function getRoutes() {
        return [
            
            {
                url: '/presales/contactenquiry/edit/:contactEnquiryId',
                config: {
                    templateUrl: '/app/presales/contactenquiry/contactEnquiry.html',
                    title: 'Edit Contact Enquiry',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Add Contact Enquiry'
                    }
                }
            }
        ];
    }
})();