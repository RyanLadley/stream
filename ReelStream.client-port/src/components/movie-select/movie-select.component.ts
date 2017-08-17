import { Component, OnInit, Input, Output, EventEmitter, ElementRef } from '@angular/core'
import { AppSettings } from '../../settings/settings'

@Component({
    selector: 'movie-select',
    templateUrl: './movie-select.template.html'
})
export class MovieSelectComponent implements OnInit {
    @Input() movie: any;
    @Input() selectStyle: any;
    imageServer: string = this._appSettings.serverUrl;
    progressStyle = {}

    constructor(private _appSettings: AppSettings) {

    }

    ngOnInit() {
        if (this.showPlaybackTrack) {
            var progress = (this.movie.playbackTime / this.movie.duration) * 100;
            this.progressStyle = { height: progress + '%' };
        }
    }
    

    showPlaybackTrack = function () {
        return ((this.movie.playbackTime > 0)
            && (this.movie.duration != null || this.movie.duration != 0)
            && (this.movie.playbackTime < this.movie.duration));
    }
}