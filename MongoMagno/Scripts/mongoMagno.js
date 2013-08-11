(function () {

    "use strict";
    var app = angular.module("mongoMagno", ["ui.bootstrap", "ui.bootstrap.transition"]);

    app.config(function ($dialogProvider) {
        $dialogProvider.options({ backdropClick: false, dialogFade: true });        
    });
    app.constant("apiRootUrl", "/api");
    app.config.$inject = ["$dialogProvider"];

}());

(function(app) {

    var mongoApiServer = function($http, apiRootUrl) {

        var getHeaders = function(server) {
            var headers = {};
            if (server && server.username) {
                headers["X-MMUsername"] = server.username;
            }
            if (server && server.password) {
                headers["X-MMPassword"] = server.password;
            }
            return headers;
        };

        var getDatabases = function(server) {
            var url = String.format("{0}/server/{1}", apiRootUrl, server.name);            
            var headers = getHeaders(server);
            return $http.get(url, { headers: headers });
        };

        var getCollections = function(server) {
            var url = String.format("{0}/server/{1}/{2}", apiRootUrl, server.name, server.currentDatabase);
            var headers = getHeaders(server);
            return $http.get(url, { headers: headers });
        };

        return {
            getDatabases: getDatabases,
            getCollections: getCollections
        };

    };
    mongoApiServer.$inject = ["$http", "apiRootUrl"];

    app.factory("mongoApiServer", mongoApiServer);

}(angular.module("mongoMagno")));

(function (app) {
    "use strict";

    var getSession = function (key) {
        return sessionStorage[key];

    };
    var setSession = function (key, value) {
        sessionStorage[key] = value;
    };

    var getLocal = function (key) {
        var value = localStorage[key];
        return value && JSON.parse(value);
    };

    var setLocal = function (key, value) {
        localStorage[key] = JSON.stringify(value);
    };

    var webStorage = function () {
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

    var serverSelectDialogOptions = {
        templateUrl: "/views/home/_serverSelect.html",
        controller: "SelectServerController"
    };

    var SelectServerController = function ($scope, webStorage, dialog) {
        
        $scope.servers = webStorage.getLocal("recentServers") || [];        
        $scope.server = {
            name: $scope.servers.length ? $scope.servers[0] : ""
        };
        $scope.close = function (ok) {
            var result = undefined;
            if (ok && $scope.server.name) {
                result = $scope.server;
                $scope.servers.unshift($scope.server.name);
                webStorage.setLocal("recentServers", $scope.servers);
            }
            dialog.close(result);
        };
    };

    var MainController = function ($scope, $dialog, mongoApiServer) {

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

        $scope.selectServer = selectServer;
        $scope.selectDatabase = selectDatabase;
        $scope.selectServer();
    };

    SelectServerController.$inject = ["$scope", "webStorage", "dialog"];
    MainController.$inject = ["$scope", "$dialog", "mongoApiServer"];

    app.controller("MainController", MainController)
       .controller("SelectServerController", SelectServerController);
    
}(angular.module("mongoMagno")));