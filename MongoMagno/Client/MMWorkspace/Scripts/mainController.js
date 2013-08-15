(function (app) {
    "use strict";
  
    var MainController = function ($scope) {
        $scope.workspaces =
        [
            { name: "Workspace 1", active:true }
        ];
       
        $scope.addWorkspace = function() {            
            angular.forEach($scope.workspaces, function (workspace) { workspace.active = false });
            $scope.workspaces.push({ name: "Workspace ", active: true });
        };
    };

    MainController.$inject = ["$scope"];

    app.controller("MainController", MainController);

}(angular.module("mongoMagno")));
