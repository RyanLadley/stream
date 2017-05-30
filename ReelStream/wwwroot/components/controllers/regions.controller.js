app.controller('regionsController', function ($scope, $routeParams, getRequest) {
    //Google maps api key  AIzaSyB5OcHLGGPAamonx9H9MTUUmSS4RcArIQ4 
    $scope.map = { center: { latitude: 0, longitude: 0 }}
    getRequest.request('/api/states/' + $routeParams.stateId + '/regions/' + $routeParams.regionId).then(function (response) {
        $scope.region = response.data;
        $scope.map = { center: { latitude: $scope.region.latitude, longitude: $scope.region.longitude }};
    });

    var currentModel = null;
    $scope.onClick = function (marker, eventName, model) {
        //Check that the selected model and the click model are not the same
        //Also check that the currentModel has been initilised
        if (currentModel != model && currentModel) currentModel.show = false;
        currentModel = model;
        model.show = !model.show;
    };
});