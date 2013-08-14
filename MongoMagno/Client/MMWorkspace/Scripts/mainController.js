(function (app) {
    "use strict";
  
    var MainController = function ($scope) {
        $scope.workspaces =
        [
            { id: "workspace1", name: "Workspace 1", selected:true }
        ];

        $scope.addActiveIfSelected = function(workspace, existingClass) {
            var result = existingClass || "";
            if (workspace.selected) {
                result += " active";
            }
            return result;
        };

        $scope.addWorkspace = function() {
            var id = $scope.workspaces.length + 1;
            angular.forEach($scope.workspaces, function (workspace) { workspace.selected = false });
            $scope.workspaces.push({ id: "workspace" + id, name: "Workspace " + id, selected: true });
        };
    };

    MainController.$inject = ["$scope"];

    app.controller("MainController", MainController);

}(angular.module("mongoMagno")));