app.directive('fileUpload', function () {
    return {
        restrict: 'A',
        scope: {
            file: '='
        },

        link: function (scope, element, attrs) {
            element.bind("change", function (event) {
                scope.$apply(function () {
                    scope.file = event.target.files[0];
                })
            })
        }
    };
});