(function (app) {
    "use strict";

    var CommandController = function ($scope, mongoApiServer) {

        $scope.command = "";

        $scope.execute = function() {
            mongoApiServer.execute($scope.currentServer, $scope.command)
                          .then(setResults, $scope.setError);
        };

        var setResults = function(results) {
            $scope.executionResults = results;
        };

    };

    CommandController.$inject = ["$scope", "mongoApiServer"];

    app.controller("CommandController", CommandController);

}(angular.module("mongoMagno")));