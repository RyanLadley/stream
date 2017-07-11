app.directive('browseFilter', function () {
    return {
        restrict: 'E',
        controller: 'browseFilterController',
        scope: {},
        templateUrl: '/components/directives/browse-filter/browse-filter.template.html',
    };
})