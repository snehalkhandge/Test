(function () {
    'use strict';

    angular
        .module('app.presalesunit')
        .controller('Source', Source);

    Source.$inject = ['$scope', 'common','authService', 'sourceFactory'];

    function Source($scope, common,authService, sourceFactory) {
        var log = common.logger.info;

        /*jshint validthis: true */
        var $q = common.$q;        
        $scope.title = 'Sources';
        $scope.results = {};
        $scope.totalItems = 0;
        $scope.itemsPerPage = 7;
        $scope.page = 1;
        $scope.searchQuery = "all";
        $scope.setPage = function (pageNo) {
            $scope.page = pageNo;
        };
        $scope.pageChanged = function () {
            activate();
        };
        $scope.hidePagination = false;

        activate();

        function activate() {

            sourceFactory.getSource($scope.page, $scope.itemsPerPage, $scope.searchQuery)
                .then(function (result) {


                    sourceFactory.getAllParentSource()
                                 .then(function (result) {

                                     $scope.parentCampaigns = result.Results;

                                 }, function (reason) {
                                     common.logger.error(reason);

                                 });


                    $scope.results = result.Results;
                    $scope.totalItems = result.TotalItems;

                    if ($scope.totalItems <= $scope.itemsPerPage) {
                        $scope.hidePagination = true;
                    }

                }, function (reason) {
                        common.logger.error(reason);

                });            
        }


        $scope.addSource = function () {

            $scope.inserted = {
                $id: $scope.results.length + 1,
                Id:'',
                Name: '',
                SelectedParentSource: '',

            };
            $scope.results.push($scope.inserted);
            
        };

        $scope.checkSourceName = function (sourceName) {
            /*/if (data !== 'awesome') {
                return "Username 2 should be `awesome`";
            }*/

            if (sourceName === '') {
                return "Source name is a required field";
            }

        };

        $scope.deleteSource = function (source) {
            /*common.logger.error("Permission Id : " + permission.Id);
            common.logger.error("Index : " + permission.$id); */
            if (source.Id != '') {
                common.logger.error("Sorry! you cannot delete a source.");
            } else {
                $scope.results.splice(source.$id - 1, 1);
            }
            

        };

        $scope.saveSource = function (source,Id) {

            angular.extend(source, { Id: Id, UserId: authService.authentication.userId });

            if (source.ParentCampaignId == null)
            {
                source.ParentCampaignId = 0;
            }

            sourceFactory.saveSource(source)
                .then(function () {                    
                    $scope.page = 1;                    
                    common.logger.success("Successfully updated the item");
                    activate();
                });
                        
        };

        
        $scope.parentCampaigns = [];

        $scope.showParent = function (campaignId) {
            var selected = [];

            angular.forEach($scope.parentCampaigns, function (s) {
                 if (campaignId == s.Id) {
                     selected.push(s.Name);
                 }

        });

            return selected.length ? selected.join(', ') : 'Not set';
        };

    }

    

})();