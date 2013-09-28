(function (app) {

    var jsonResults = function () {

        return {
            restrict: 'EA',
            template: '<pre></pre>',
            replace: true,
            link: function(scope, element, attributes) {
                scope.$watch(attributes.value, function (newValue) {
                    element.html(JSON.stringify(newValue, null, "  "));
                });
            }
        };
    };


    app.directive("jsonResults", jsonResults);

}(angular.module("mongoMagno")));