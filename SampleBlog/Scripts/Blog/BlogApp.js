var app = angular.module('blogApp', ['BlogApp.Comments', 'BlogApp.Admin', 'ngMaterial']);

app.config(function ($mdThemingProvider, $locationProvider) {
    $mdThemingProvider
        .theme('default')
        .primaryPalette('teal')
        .accentPalette('blue-grey')
        .warnPalette('red');


});
app.controller("navMenu", function () {
    var originatorEv;
    this.openMenu = function ($mdOpenMenu, ev) {
        console.log("click");
        originatorEv = ev;
        $mdOpenMenu(ev);
    };

    this.logOff = function () {
        document.getElementById('logoutForm').submit();
    }
});

app.controller("rightPane", function () { });
app.controller("footer", function () { });
app.controller("login", function () { });
app.controller("editor", function ($scope, $http) {
    $scope.tags = [];
   
    $scope.init = function (tags) {
        tinymce.init({
            selector: '#tbEdit'
        });
        $scope.tags = tags;
    }
    $scope.querySearch = function (pattern) {
      return  $http.get('/api/Tags/' + pattern).then(function(response) {return   response.data;});
    }
});
app.controller("myPostsList", function ($scope, $mdDialog, $window) {
    $scope.delete = function (url) {
        var confirm = $mdDialog.confirm()
      .title('Delete Post')
      .textContent('Do you realy want to delete this post?')
      .ariaLabel('Delete Post')
      .ok('Delete')
      .cancel('Cancel');
        $mdDialog.show(confirm).then(function () {
            $window.location = url;
        });
    }
});