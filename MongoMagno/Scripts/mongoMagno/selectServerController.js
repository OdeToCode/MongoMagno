(function (app) {
    "use strict";
   
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
                $scope.servers = _.uniq($scope.servers).slice(0, 5);
                webStorage.setLocal("recentServers", $scope.servers);
            }
            dialog.close(result);
        };
    };

    SelectServerController.$inject = ["$scope", "webStorage", "dialog"]; 
    app.controller("SelectServerController", SelectServerController);
    
}(angular.module("mongoMagno")));