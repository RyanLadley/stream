import { Injectable } from '@angular/core';
import { User } from '../models/user.model';

@Injectable()
export class AppSettings {

    serverUrl: string = "http://localhost:53734";
    externalMovieDBSettings: any = {
        imageUrl: "https://image.tmdb.org/t/p/w500"
    };

}