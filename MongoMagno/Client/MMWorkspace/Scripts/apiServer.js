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
            var url = "{0}/server/{1}".format(apiRootUrl, server.name);            
            var headers = getHeaders(server);
            return $http.get(url, { headers: headers });
        };

        var getCollections = function(server) {
            var url = "{0}/server/{1}/{2}".format(apiRootUrl, server.name, server.currentDatabase);
            var headers = getHeaders(server);
            return $http.get(url, { headers: headers });
        };

        var execute = function(server, command) {
            var url = "{0}/server/{1}/{2}".format(apiRootUrl, server.name, server.currentDatabase);
            var headers = getHeaders(server);
            return $http.post(url, { command: command }, { headers: headers });
        };

        return {
            getDatabases: getDatabases,
            getCollections: getCollections,
            execute: execute
        };

    };
    mongoApiServer.$inject = ["$http", "apiRootUrl"];

    app.factory("mongoApiServer", mongoApiServer);

}(angular.module("mongoMagno")));