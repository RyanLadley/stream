import { Routes } from '@angular/router'
import { LoginComponent, HomeComponent, VideoPlayerComponent, BrowseComponent } from '../components/index'

export const appRoutes = [
    { path: 'login', component: LoginComponent },
    { path: '', component: HomeComponent },
    { path: 'video-stream/:movieId', component: VideoPlayerComponent },
    { path: 'browse/genre/:genreId/:type', component: BrowseComponent }
];