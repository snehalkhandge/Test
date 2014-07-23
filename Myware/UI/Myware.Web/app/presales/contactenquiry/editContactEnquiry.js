(function () {
    'use strict';

    angular
        .module('app.presales')
        .controller('EditContactEnquiry', EditContactEnquiry);

    EditContactEnquiry.$inject = ['$scope', '$routeParams', '$upload', 'common', 'authService',
                            'contactEnquiryFactory', 'localityFactory', 'facingTypeFactory',
                            'unitTypeFactory', 'contactStatusTypeFactory' ];

    function EditContactEnquiry($scope, $routeParams, $upload, common, authService,
                          contactEnquiryFactory, localityFactory, facingTypeFactory,
                          unitTypeFactory, contactStatusTypeFactory ) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;                 
       
        var personalId = ($routeParams.personalId) ? parseInt($routeParams.personalId) : 0;
        var contactId = ($routeParams.contactId) ? parseInt($routeParams.contactId) : 0;
        

        var defaultForm = {
            Id: '',
            PersonalInformationId: personalId,
            Remarks: '',
            AssignedDate: '',
            LeadStatus: '',
            TransactionType: '',
            LookingForType: '',
            BudgetFrom: '',
            BudgetTo: '',
            SaleAreaFrom: '',
            SaleAreaTo: '',
            CarpetAreaFrom: '',
            CarpetAreaTo: '',
            PropertyAge: '',
            IsFurnished: '',
            OfferedRate: '',
            EnquiryDate: '',
            FacingType: '',
            ContactStatus: '',
            PreferredUnitTypes: [],
            PreferredLocations: [],            
            
        };


        $scope.enquiry = defaultForm;


        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        $scope.title = (contactId > 0) ? 'Edit Enquiry Information' : 'Add Enquiry Information';
        $scope.buttonText = (contactId > 0) ? 'Update Enquiry Information' : 'Add New Enquiry Information';

        $scope.Localities = {};
        $scope.FacingTypes = {};
        $scope.UnitTypes = {};
        $scope.LookingForTypes = [
                                    { Name: 'LFType 1' },
                                    { Name: 'LFType 2' },
                                    { Name: 'LFType 3' },
                                 ];
        $scope.TransactionTypes = [
                                    { Name: 'TransType 1' },
                                    { Name: 'TransType 2' },
                                    { Name: 'TransType 3' },
                                  ];
        $scope.PropertyAges = [
                                    { Name: '0 - 1 year' },
                                    { Name: '1 - 3 year' },
                                    { Name: '4 - 7 year' },
                              ];
        $scope.LeadStatuses = [
                                    { Name: 'Lead Status 1' },
                                    { Name: 'Lead Status 2' },
                                    { Name: 'Lead Status 3' },
                              ];
        $scope.ContactStatuses = [
                                    { Name: 'Contact Status 1' },
                                    { Name: 'Contact Status 2' },
                                    { Name: 'Contact Status 3' },
                                 ];

        $scope.isClean = function () {
            return angular.equals(original, $scope.enquiry);
        }

        
        $scope.saveContactEnquiry = function (enquiry) {
            
            angular.extend(enquiry, { UserId: authService.authentication.userId });
                        
            var prefLoc = [];
            
            angular.forEach(enquiry.PreferredLocations, function (value, key) {
                this.push({ Locality: value });
            }, prefLoc);



            enquiry.PreferredLocations = prefLoc;

            var prefUnit = [];

            angular.forEach(enquiry.PreferredUnitTypes, function(value, key) {
                this.push({ UnitType: value });
            }, prefUnit);

            enquiry.PreferredUnitTypes = prefUnit;

            contactEnquiryFactory.savecontactEnquiry(enquiry)
                .then(function (result) {

                    $scope.enquiry.Id = result.Id;
                    $scope.buttonText = "Update Enquiry Information"
                    common.logger.success("Successfully saved the item");
                    $scope.alerts.push({ type: 'success', msg: "Successfully saved the item" });
                });
        };

        $scope.resetForm = function () {
            $scope.myForm.$setPristine();
            $scope.enquiry = defaultForm;
        }

        activate();

        function activate() {

            if(contactId != 0)
            {

                contactEnquiryFactory.getBusinessById(contactId)
                    .then(function (result) {                        
                        $scope.enquiry = result;
                        
                    }, function (reason) {
                        common.logger.error(reason);
                        $scope.alerts.push({ type: 'danger', msg: reason });

                    });

            }
            

            localityFactory.getAllLocality()
                         .then(function (result) {

                             $scope.Localities = result.Results;

                         }, function (reason) {
                             common.logger.error(reason);
                             $scope.alerts.push({ type: 'danger', msg: reason });
                         });

            facingTypeFactory.getAllFacingTypes()
                .then(function (result) {
                    $scope.FacingTypes = result.Results;

                    }, function (reason) {
                        common.logger.error(reason);
                        $scope.alerts.push({ type: 'danger', msg: reason });
                    });

            unitTypeFactory.getAllUnitTypes()
                .then(function (result) {
                    $scope.UnitTypes = result.Results;

                }, function (reason) {
                    common.logger.error(reason);
                    $scope.alerts.push({ type: 'danger', msg: reason });
                });
            
            contactStatusTypeFactory.getAllContactStatusTypes()
                .then(function (result) {
                    $scope.ContactStatuses  = result.Results;

                }, function (reason) {
                    common.logger.error(reason);
                    $scope.alerts.push({ type: 'danger', msg: reason });
                });
        }




        $scope.initDate = new Date('2016-15-20');
        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];

        $scope.openAssignedDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.openedAssignedDate = true;
        };

        $scope.openEnquiryDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.openedEnquiryDate = true;
        };

        $scope.today = function () {
            $scope.dt = new Date();
        };
        $scope.today();

        $scope.minDate = new Date('1930-15-20');


    }
})();