(function() {
    "use strict";
    var app = angular.module("mongoMagno", ["$strap"]);
    
}());

(function(app) {
    "use strict";

    var getSession = function (key) {
        return sessionStorage[key];

    };
    var setSession = function(key, value) {
        sessionStorage[key] = value;
    };

    var getLocal = function(key) {
        return localStorage[key];
    };

    var setLocal = function(key, value) {
        localStorage[key] = value;
    };

    var webStorage = function() {
        return {
            getSession: getSession,
            setSession: setSession,
            getLocal: getLocal,
            setLocal: setLocal
        };
    };
    webStorage.$inject = [];

    app.factory("webStorage", webStorage);

}(angular.module("mongoMagno")));

(function (app) {   
    "use strict";

    var server = function() {
        return {
            name: "localhost:27017",
            username: "",
            password: ""
        };
    };

    var selectServerDialog;
    var getSelectServerDialog = function($modal, $scope) {
        return $modal({ template: '/views/home/_serverSelect.html', persist: true, show: false, scope: $scope });
    };

    var showSelectServerDialog = function () {
        selectServerDialog.then(function (modal) { modal.modal("show"); });
    };

    var connect = function(server) {

    };

    var MainController = function ($scope, $modal) {
        selectServerDialog = getSelectServerDialog($modal, $scope);
        $scope.selectServer = showSelectServerDialog;
        $scope.server = server();

        $scope.selectServer();
    };
    MainController.$inject = ["$scope", "$modal"];

    app.controller("MainController", MainController);

}(angular.module("mongoMagno")));
