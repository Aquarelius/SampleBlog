var adminApp = angular.module('BlogApp.Admin', []);

adminApp.controller("usersController", function ($scope, $http, $mdDialog) {
	$scope.users = [{ "Name": "Hui" }];
	$scope.init = function () {
		$http({
			"method": "GET",
			"url": "/api/users/GetUsers"
		}).then(function (response) {
			$scope.users = [];
			for (var i in response.data) {
				var usr = response.data[i];
				$scope.users.push(usr);

			}
		});
	}
	$scope.saveRole = function (role, id) {
		
	    var value = false;
	    var checked = false;
		for (var i in $scope.users) {
			if ($scope.users[i].Id == id) {
				if (role == "Admin") {
					checked = $scope.users[i].Admin;
				}
				if (role == "Writer") {
				    checked = $scope.users[i].Writer;
				}
                break;
			}
		}
	    $http({
	        "method": "POST",
	        "url": "/api/users/ChangeRole",
	        "data": {
	            "UserId": id,
	            "Role": role,
	            "userInRole": checked
	        }
	    });
	}

	$scope.delete = function (id) {

	    var confirm = $mdDialog.confirm()
         .title('Delete User')
         .textContent('Do you realy want to delete this user?')
         .ariaLabel('Delete User')
         .ok('Delete')
         .cancel('Cancel');
	    $mdDialog.show(confirm).then(function () {
	        $http({
	            "method": "DELETE",
	            "url": "/api/users/" + id
	        }).then(function (response) {
	            var dId = response.data;
	            for (var i in $scope.users) {
	                if ($scope.users[i].Id == dId) $scope.users.splice(i, 1);break;
	            }
	        });
	    }, function () {
	        
	    });
        
    }
});