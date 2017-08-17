import { Component, OnInit } from '@angular/core';
import { AppSettings } from '../../settings/settings'
import { ServerRequest } from '../../services/server-request.service'


@Component({
  selector: 'browse-tab',
  templateUrl: './browse-tab.template.html'
})
export class BrowseTabComponent implements OnInit {

    server: string = this._appSettings.serverUrl;
    columnHeight:number = 5;
    columnPositions: number[];
    genres: any;
    displayTimeOut: any;
    displayTab: boolean;

    constructor(private _serverRequest: ServerRequest, private _appSettings: AppSettings) {

    }

    ngOnInit() {
        this.displayTab = false;
        this.columnPositions = this.initializePositions();
        this.getGenres()
    }

    //Simply retunr an array counting up to column height we can ngFor over;
    initializePositions() {
        let positions: number[] = new Array(this.columnHeight);
        for (var i = 0; i < positions.length; i++) {
            positions[i] = i
        }
        return positions;
    }

    getGenres() {
        this._serverRequest.get('/api/genres').subscribe(
            response => this.genres = response 
        )
    }

    tabDisplay(status: boolean) {
        let delayTime = (status) ? 500 : 300;
        clearTimeout(this.displayTimeOut);

        this.displayTimeOut = setTimeout(
            () => this.displayTab = status, delayTime);
    }
}
