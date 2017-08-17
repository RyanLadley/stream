import { Component, Input } from '@angular/core';
import { ServerRequest } from '../../services/index';
import { User } from '../../models/user.model';

@Component({
  selector: 'movie-queue',
  templateUrl: './movie-queue.template.html'
})
export class MovieQueueComponent{
    @Input() movies: Array<any>;
    @Input() queueTitle: string;
    displayArrows: boolean;
    currentShown: number;
    currentPage: number = 0;
    shownMovies: Array<any> = [{}];

    //TODO: Keep movies from changing randomly when resizing screen
    updateMovieQueue(numberShown) {
        if (typeof this.movies !== 'undefined') {
            this.currentShown = numberShown;

            //Wraps the "currentPage" ie, if -1, the it will be changed to the slast page
            //Consider moving this loginc outisde of this function
            var numberOfPages;
            if (numberShown > this.movies.length) { //More shown than needed, so we know we have 1 page (will be zero if computed)
                numberShown = this.movies.length
                numberOfPages = 1;
            }
            else { //Compute number of pages
                numberOfPages = Math.ceil(this.movies.length / numberShown);
            }

            this.currentPage = this.modulo(this.currentPage, numberOfPages);
            var firstIndex = numberShown * this.currentPage;
            
            this.shownMovies = this.movies.slice(firstIndex, firstIndex + numberShown);
        }
    }

    changePage(change) {
        this.currentPage += change;
        this.updateMovieQueue(this.currentShown);
    }

    arrowDisplay(bool:boolean) {
        this.displayArrows = bool;
    }
    

    //Java Script modulos (%) are stupid with negative numbers. This fixes that. 
    modulo(n, m) {
        return ((n % m) + m) % m;
    }
}
