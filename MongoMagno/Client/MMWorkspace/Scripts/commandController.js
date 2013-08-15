(function (app) {
    "use strict";

    var CommandController = function ($scope) {

        $scope.message = "Hello from Command";

    };

    CommandController.$inject = ["$scope"];

    app.controller("CommandController", CommandController);

}(angular.module("mongoMagno")));