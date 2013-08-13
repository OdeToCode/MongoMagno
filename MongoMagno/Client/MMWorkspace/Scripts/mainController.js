(function (app) {
    "use strict";
  
    var MainController = function ($scope) {
        $scope.workspaces =
        [
            { id: "workspace1", name: "Workspace 1" }
        ];
    };

    MainController.$inject = ["$scope"];

    app.controller("MainController", MainController);

}(angular.module("mongoMagno")));