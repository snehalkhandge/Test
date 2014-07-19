(function() {
	'use strict';

	var serviceId = 'taskmanagerFactory';

	angular.module('app.taskmanager')
		.factory(serviceId, dataservice);


	dataservice.$inject = ['$http','$timeout', 'DSCacheFactory', 'common'];

	function dataservice($http, $timeout, DSCacheFactory, common) {

		var $q = common.$q;

		

		var taskmanagerCache = DSCacheFactory('taskmanagerCache', {
			maxAge: 3600000,
			capacity: 100,
			deleteOnExpire: 'aggressive',
			storageMode: 'localStorage',
			onExpire: function(key, value) {

			}
		});

		var dataCache = DSCacheFactory.get('taskmanagerCache');

		var getAllTaskmanager = function () {
		   var deferred = $q.defer(),
			   start = new Date().getTime(),
			   cacheId = "taskmanager-All";



		   if (dataCache.get(cacheId)) {
			   deferred.resolve(dataCache.get(cacheId));
		   } else {
			   $http.get(common.apiUrl + '/taskmanagers/all')
				   .success(function (data) {
					   data = data || {};
					   if (data.Messages == null) {
						   dataCache.put(cacheId, data);
					   }

					   deferred.resolve(data);
				   })
				   .error(function (data, status, headers, config) {
					   deferred.reject(new Error(angular.toJson(data)));
				   });

		   }


		   return deferred.promise;


	   };

		var getTaskmanagerAssignedByMe = function(userId, page, pageSize, searchQuery) {
			var deferred = $q.defer(),
				start = new Date().getTime(),
				cacheId = "taskmanagersAssignedByMe" + userId + page + pageSize + searchQuery;

			

				if (dataCache.get(cacheId)) {
					deferred.resolve(dataCache.get(cacheId));
				} else {
					$http.get(common.apiUrl + '/taskmanagersAssignedByMe/user/'+userId+'/page/' + page + '/size/' + pageSize + '/search/' + searchQuery)
						.success(function (data) {
							data = data || {};
							if (data.Messages == null) {
								dataCache.put(cacheId, data);
							}

							deferred.resolve(data);
						})
						.error(function (data, status, headers, config) {                            
							deferred.reject(new Error(angular.toJson(data)));
						});

				}


				return deferred.promise;

		   
	   };



	   var getTaskmanagerAssignedToMe = function (userId,page, pageSize, searchQuery) {
			var deferred = $q.defer(),
				start = new Date().getTime(),
				cacheId = "taskmanagersAssignedToMe" + userId + page + pageSize + searchQuery;



			if (dataCache.get(cacheId)) {
				deferred.resolve(dataCache.get(cacheId));
			} else {
				$http.get(common.apiUrl + '/taskmanagersAssignedToMe/user/' + userId + '/page/' + page + '/size/' + pageSize + '/search/' + searchQuery)
					.success(function (data) {
						data = data || {};
						if (data.Messages == null) {
							dataCache.put(cacheId, data);
						}

						deferred.resolve(data);
					})
					.error(function (data, status, headers, config) {
						deferred.reject(new Error(angular.toJson(data)));
					});

			}


			return deferred.promise;


		};

		var getTaskmanagerById = function (id) {
			var deferred = $q.defer(),
				start = new Date().getTime();
				
			$http.get(common.apiUrl + '/taskmanagerById/' +id)
					.success(function (data) {
						data = data || {};                        
						deferred.resolve(data);
					})
					.error(function (data, status, headers, config) {
						deferred.reject(new Error(angular.toJson(data)));
					});

			return deferred.promise;

		};


		var getTaskChildrenByParentId = function (id) {
			var deferred = $q.defer(),
				start = new Date().getTime();

			$http.get(common.apiUrl + '/task/children/' + id)
					.success(function (data) {
						data = data || {};
						deferred.resolve(data.Results);
					})
					.error(function (data, status, headers, config) {
						deferred.reject(new Error(angular.toJson(data)));
					});

			return deferred.promise;

		};


		var saveTaskmanager = function (taskmanager) {

		   var deferred = $q.defer();
		   if (taskmanager.Id == '') {
			   taskmanager.Id = 0;
		   }

		   
		    $http.post(common.apiUrl + '/saveTask/' + taskmanager.Id, taskmanager)
					.success(function (data) {

						if (dataCache.info()) {
							dataCache.removeAll();
						}
						
						deferred.resolve(data);
					})
					.error(function (data, status, headers, config) {
						common.logger.error(data);
						deferred.reject({});
					});
		   return deferred.promise;

	   };

		var getAllTaskStatus = function (id) {
			return [
					{
					  status: "Completed"	        
					},
					{
					  status: "Waiting"
					},
					{
					  status: "Replied"
					},
					{
					  status: "Pending"
					},
					{
					  status: "Accepted"
					},
			];

		};

		var getAllTaskType = function (id) {
		    return [
					{
					    type: "Notification"
					},
					{
					    type: "Assignment"
					}
		    ];

		};

		return {
			getTaskmanagerAssignedByMe: getTaskmanagerAssignedByMe,
			getTaskmanagerAssignedToMe:getTaskmanagerAssignedToMe,
			saveTaskmanager: saveTaskmanager,			
			getAllTaskmanager: getAllTaskmanager,
			getTaskmanagerById: getTaskmanagerById,
			getTaskChildrenByParentId: getTaskChildrenByParentId,
			getAllTaskStatus: getAllTaskStatus,
			getAllTaskType: getAllTaskType
			
		};

	}
})();