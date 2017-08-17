import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppSettings } from '../../settings/settings'
import { ServerRequest } from '../../services/server-request.service'


@Component({
  selector: 'browse',
  templateUrl: './browse.template.html'
})
export class BrowseComponent implements OnInit {

    genreId: number;
    type: string;
    genre: any;
    movies: any;

    movieCollectionRows: any = [[]];
    numberOfRows: number;
    moviesPerRow: number;

    constructor(private _serverRequest: ServerRequest, private _appSettings: AppSettings, private _route: ActivatedRoute) {

    }

    ngOnInit() {
        this.numberOfRows = 0;
        this.setRouteValues();
        this.getGenres();
        this.getMovies();
    }

    //Sets values found in the route of this page to class variables
    setRouteValues() {
        this._route.params.subscribe(
            params => {
                this.genreId = parseInt(params['genreId'])
                this.type = params['type']
            });
    }

    //Api call to get genre details
    getGenres() {
        this._serverRequest.get('/api/genres/' + this.genreId).subscribe(
            response => this.genre = response 
        )
    }

    //APi call get movies for the genres specified in route
    getMovies() {
        let apiString:string = '/api/' + this.type + '/genre/' + this.genreId;
        this._serverRequest.get(apiString).subscribe(
            response => {
                console.log("hello")
                this.movies = response;
                this.updateRows();
            }
        );
    }

    //Called when the rowLength changes on movie-row in the dom
    rowAdjustment(event) {
        //without this conditional, a never ending loop occurs as changing the array causes movie-row to reinialize which calls this function
        if (this.rowsHaveChanged(event) || !this.moviesPerRow) {
            this.moviesPerRow = event;
            this.updateRows();
        }
    }

    rowsHaveChanged(newRowLength:number) {
        return newRowLength !== this.moviesPerRow;
    }

    
    //Creates the the movieCollections for the individial rows. 
    updateRows() {
        if (typeof this.movies !== 'undefined') {
            this.numberOfRows = Math.ceil(this.movies.length / this.moviesPerRow)
            var firstIndex = 0;

            for (var i = 0; i < this.numberOfRows; i++) {
                firstIndex = i * this.moviesPerRow
                this.movieCollectionRows[i] = this.movies.slice(firstIndex, firstIndex + this.moviesPerRow);
            }
        }
}
}
