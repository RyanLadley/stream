import { Component, OnInit } from '@angular/core';
import { ServerRequest } from '../../services/index';
import { User } from '../../models/user.model';

@Component({
  selector: 'home',
  templateUrl: './home.template.html'
})
export class HomeComponent implements OnInit {
    queues: any;

    constructor(private _serverRequest: ServerRequest) {

    }

    ngOnInit() {
        this._serverRequest.get('/api/movies/queues').subscribe(
            response => this.queues = response 
        );
    }
}
