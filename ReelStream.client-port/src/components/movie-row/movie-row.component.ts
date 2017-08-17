import { Component, OnInit, Input, Output, EventEmitter, ElementRef, HostListener } from '@angular/core'

@Component({
    selector: 'movie-row',
    templateUrl: './movie-row.template.html'
})
export class MovieRowComponent implements OnInit {
    @Input() movieCollection: any;
    @Output() numberToDisplay = new EventEmitter();

    movieSelectSize: number = 269;
    rowLength: number = 1;
    queueWidth: number;
    movieStyle = []

    initiated: boolean = false

    constructor(private element: ElementRef) {
    }

    ngOnInit() {
        this.onResize();
    }

    @HostListener('window:resize')
    onResize() {
        //Timer prevents ExpressionChangedAfterItHasBeenCheckedError
        setTimeout(() => {
            this.setRowWidth();
        });
    }

    setRowWidth() {
        this.queueWidth = this.element.nativeElement.offsetWidth;

        //Number of movies that can fit onto the screen at once 
        //QueueWidth calculated in the directive itself
        this.rowLength = Math.floor(this.queueWidth / this.movieSelectSize);
        if (this.rowLength == 0) {
            this.rowLength = 1;
        }
        this.numberToDisplay.emit(this.rowLength)

    }

    enlargeElement(index) {
        var smallWidth = this.movieSelectSize - ((this.movieSelectSize) / (this.rowLength - 1)) - 2;
        for (var i = 0; i < this.movieCollection.length; i++) {
            this.movieStyle[i] = {
                'width': smallWidth + 'px'
            };
        }
        this.movieStyle[index] = '';
    }

    returnElements() {
        for (var i = 0; i < this.movieCollection.length; i++) {
            this.movieStyle[i] = '';
        }
    }

}