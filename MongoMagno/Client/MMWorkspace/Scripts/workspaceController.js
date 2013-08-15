(function (app) {
    "use strict";

    var serverSelectDialogOptions = {
        templateUrl: "/Client/MMWorkspace/Views/serverSelect.html",
        controller: "SelectServerController"
    };

    var WorkspaceController = function ($scope, $dialog, mongoApiServer) {
        
        var setCollections = function(collections) {
            $scope.currentServer.collections = collections.data;
            $scope.currentServer.currentCollection = $scope.currentServer.collections[0];
        };

        var setDatabases = function (databases) {
            $scope.currentServer.databases = databases.data;
            $scope.currentServer.currentDatabase = $scope.currentServer.databases[0];
            selectCollection();
        };       

        var connectToServer = function (server) {
            if (server) {
                $scope.currentServer = server;
                $scope.workspace.name = $scope.currentServer.name;
                mongoApiServer
                    .getDatabases($scope.currentServer)
                    .then(setDatabases);
            }
        };

        var selectServer = function () {
            $dialog
                .dialog(serverSelectDialogOptions)
                .open()
                .then(connectToServer);
        };

        var selectCollection = function() {
            mongoApiServer
                .getCollections($scope.currentServer)
                .then(setCollections);
        };

        var selectDatabase = function() {
            selectCollection();
        };

        $scope.workspace = $scope.$parent.workspace;
        $scope.selectServer = selectServer;
        $scope.selectDatabase = selectDatabase;
        $scope.selectServer();
    };

    WorkspaceController.$inject = ["$scope", "$dialog", "mongoApiServer"];

    app.controller("WorkspaceController", WorkspaceController);

}(angular.module("mongoMagno")));