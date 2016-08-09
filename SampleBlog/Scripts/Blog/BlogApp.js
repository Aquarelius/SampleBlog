var app = angular.module('blogApp', ['BlogApp.Comments', 'ngMaterial']);
app.config(function ($mdIconProvider) {
    $mdIconProvider
      .iconSet("call", 'img/icons/sets/communication-icons.svg', 24)
      .iconSet("social", 'img/icons/sets/social-icons.svg', 24);
})
app.controller("navMenu", function () {
    var originatorEv;
    this.openMenu = function ($mdOpenMenu, ev) {
        console.log("click");
        originatorEv = ev;
        $mdOpenMenu(ev);
    };
});