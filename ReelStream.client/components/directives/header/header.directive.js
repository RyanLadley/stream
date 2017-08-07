app.directive('header', function () {
    return {
        restrict: 'E',
        controller: 'headerController',
        scope: {},
        templateUrl: '/components/directives/header/header.template.html',
    };
})