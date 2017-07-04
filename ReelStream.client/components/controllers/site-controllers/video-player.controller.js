app.controller('videoPlayerController', function ($scope, $routeParams, $sce, appSettings) {

    $scope.server = appSettings.serverUrl;
    $scope.videoId = $routeParams.videoFileId;
    $scope.streamApi = $sce.trustAsResourceUrl($scope.server + "/api/video/" + $scope.videoId);
});
