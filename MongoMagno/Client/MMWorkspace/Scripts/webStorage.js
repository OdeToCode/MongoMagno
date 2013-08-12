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

