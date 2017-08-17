import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'add-movie',
    templateUrl: './add-movie.template.html'
})
export class AddMovieComponent implements OnInit {

    currentDisplay: any;
    selectedMovie: any;
    uploadingStyle: any = {};

    ngOnInit() {
        this.setCurrentDisplay()
        this.setSelectedMovie()
    }
    setCurrentDisplay() {
        this.currentDisplay = {
            current: 'file-select',
            previous: null
        };
    }

    setSelectedMovie() {
        this.selectedMovie = {
            flow: {
                isUploading: function () { return false },
                progress: function () { return 0; }
            }
        };
    }

    //This function holds the logic of controlling the "back button" 
    //TODO: Make this more elegant/scalable/readable
    back() {
        switch (this.currentDisplay.current) {
            case 'search':
                this.currentDisplay.current = 'file-select'
                this.currentDisplay.previous = null;
                break;
            case 'confirm':
                this.currentDisplay.current = 'search'
                this.currentDisplay.previous = 'file-select'
                break;
        }
    }

    /*
    var remaining = 0;
    $scope.$watch(function ($scope) { return $scope.selectedMovie.flow.progress() }, function () {
        remaining = ($scope.selectedMovie.flow.progress() * 100);
        $scope.uploadingStyle = {
            "clip-path": "inset(0px -10px " + remaining + "% 0px)"
        }
        $scope.$apply;
    });
    */
}
