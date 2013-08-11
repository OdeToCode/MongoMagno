(function () {

    "use strict";
    var app = angular.module("mongoMagno", ["ui.bootstrap", "ui.bootstrap.transition"]);

    app.config(function ($dialogProvider) {
        $dialogProvider.options({ backdropClick: false, dialogFade: true });        
    });
    app.constant("apiRootUrl", "/api");
    app.config.$inject = ["$dialogProvider"];

}());

