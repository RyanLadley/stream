import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import {
    AppRoot,
    HeaderComponent,
    BrowseTabComponent,
    BrowseComponent,
    LoginComponent,
    HomeComponent,
    AddMovieComponent,
    MovieQueueComponent,
    MovieRowComponent,
    MovieSelectComponent,
    VideoPlayerComponent
} from '../components/index'

import {
    AuthService,
    ServerRequest,
    TokenManager
} from '../services/index';

import {
    SecondsToTimePipe,
    CapitalizePipe
} from '../pipes/index'

import { appRoutes } from './routes';


import { AppSettings } from '../settings/settings'

@NgModule({
    declarations: [
      //Components
      AppRoot,
      HeaderComponent,
      BrowseTabComponent,
      BrowseComponent,
      LoginComponent,
      HomeComponent,
      AddMovieComponent,
      MovieQueueComponent,
      MovieRowComponent,
      MovieSelectComponent,
      VideoPlayerComponent,
      //Pipes
      SecondsToTimePipe,
      CapitalizePipe
  ],
  imports: [
      BrowserModule,
      HttpModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forRoot(appRoutes)
  ],
  providers: [
      AppSettings,
      ServerRequest,
      TokenManager,
      AuthService
  ],
  bootstrap: [AppRoot]
})
export class AppModule { }
