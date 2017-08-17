import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
declare let Flow: any;

@Component({
    selector: 'add-movie-file-select',
    templateUrl: './add-movie.template.html'
})
export class AddMovieFileSelectComponent implements OnInit {
    @Input() display: any;
    @Output() selectEmmitter = new EventEmitter(); 

    //When a file is selected, we assign it to the selected movie and automaticly move on to the searchs creen
    //We are using flowjs for upload -> https://github.com/flowjs/flow.js
    selectedMovie: any;

    ngOnInit() {
        this.initiateFlow();

        this.selectedMovie.on('fileAdded', function (file, event) {
            let newDisplay: any = {
                previous: this.display.current,
                current: 'search'
            }
            this.selectEmmitter.emmit({
                'display': newDisplay,
                'selectedMovie': this.selectedMovie
            })
        })
    }

    initiateFlow() {
        this.selectedMovie = new Flow({
            target: '/upload',
            singleFile: true
        });

        this.selectedMovie.assignBrowse(document.getElementById('flow-file-select'));
    }
    
}
