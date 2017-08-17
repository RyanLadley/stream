import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from "rxjs";
import { AppSettings } from '../../settings/settings';
import { ServerRequest } from '../../services/server-request.service'

@Component({
    selector: 'video-player',
    templateUrl: './video-player.template.html',
    host: {
        '(window:resize)': 'onResize($event)'
    }
})
//TODO: This class needs to be refactored to seperate concerns better
export class VideoPlayerComponent implements OnInit {

    videoPlayer: any;
    fullScreen: any;
    playbackTrackWidth: number;

    server: string = this._appSettings.serverUrl;
    movieId: number;
    streamApi: string;
    movie: any;
    playbackUpdate: any;
    apiTimer: any;
    progressBarTimer: any;
    controlsTimeout: any;
    showControls: any;
    progressStyle: any;
    jumpLocation: number;
    jumpLocationPosition: any;

    constructor(private _appSettings: AppSettings, private route: ActivatedRoute, private _serverRequest: ServerRequest) {

    }

    ngOnInit() {
        this.videoPlayer = document.getElementById("main-video-player");
        this.fullScreen = document.getElementById("full-screen-container");
        this.setPlaybackTrackWidth();

        this.route.params.subscribe(params => this.movieId = parseInt(params['movieId']));
        this.streamApi = this.server + "/api/video/" + this.movieId;
        this.getMovie();

        this.apiTimer = Observable.timer(10000, 10000);
        this.apiTimer.subscribe(timer => this.updateApi());


        this.videoPlayer.muted = false;

        this.progressBarTimer = Observable.timer(1000, 1000);
        this.progressBarTimer.subscribe(() =>{
            if (this.videoPlayer.currentTime == this.videoPlayer.duration) {
                this.pauseVideo();
            }
            this.updateProgressBar()
        });

    }

    ngOnDestroy() {
        this.apiTimer.unsubscribe();
    }

    setPlaybackTrackWidth() {
        let playbackTrack:any = document.getElementById("playback-track");
        if (playbackTrack.style.pixelWidth) {
            this.playbackTrackWidth = parseInt(playbackTrack.style.pixelWidth);
        }
        else {
            this.playbackTrackWidth = playbackTrack.offsetWidth;
        }
    }

    onResize(event:Event){
        this.setPlaybackTrackWidth();
    }
    

    getMovie() {
        this._serverRequest.get('/api/movies/' + this.movieId).subscribe(
            response => {
                this.movie = response;

                if (this.movie.playbackTime != null) {
                    this.videoPlayer.currentTime = this.movie.playbackTime;
                }

                this.playbackUpdate = {
                    movieId: this.movie.movieId,
                    playbackTime: null
                }
            }
        );
    }


    //Update the API with the current playback time perisdically; 
    updateApi() {
        //Don't update if paused and movie hasn't been populated
        if (this.playbackUpdate !== undefined && this.playbackUpdate.playbackTime != this.videoPlayer.currentTime) {

            this.playbackUpdate.playbackTime = this.videoPlayer.currentTime;
            this._serverRequest.post('/api/movies/playback/' + this.movieId, this.playbackUpdate);
        }
    }
    

    
    //Toggle the display controls
    displayControls() {
        this.setShowControls(true);
        this.startControlsTimer();
    }

    //This function prevents the flicker that happens when setting the showControls variable
    private flickerLock:boolean = false;
    setShowControls(boolean) {
        if (!this.flickerLock) {
            this.showControls = boolean;
        }
    }

   
    startControlsTimer() {
        clearTimeout(this.controlsTimeout);
        this.controlsTimeout = setTimeout(
            () => {
            this.setShowControls(false);
            this.flickerLock = true;
            setTimeout(() => { this.flickerLock = false; }, 200);
        }, 2000);
    }



    //Play-Pause Logic
    showPlayButton = false;
    playOrPauseIcon = "fa-pause"
    togglePlayOrPause = function () {

        this.showPlayButton = !this.showPlayButton
        this.startControlsTimer();

        if (this.showPlayButton) {
            this.pauseVideo();
        }
        else {
            this.playVideo();
        }
    }

    pauseVideo() {
        this.playOrPauseIcon = "fa-play";
        this.videoPlayer.pause();
    }

    playVideo() {
        this.playOrPauseIcon = "fa-pause";
        this.videoPlayer.play();
    }



    //Full Screen Logic
    showExpandButton = true;
    fullScreenIcon = "fa-expand";
    toggleFullScreen() {
        var document: any = window.document;
        this.showExpandButton = !this.showExpandButton;
        this.startControlsTimer();

        if (!this.showExpandButton) {
            this.fullScreenIcon = "fa-compress";
            if (this.fullScreen.requestFullScreen) {
                this.fullScreen.requestFullScreen();
            }
            else if (this.fullScreen.webkitRequestFullScreen) {
                this.fullScreen.webkitRequestFullScreen()
            }
            else if (this.fullScreen.mozRequestFullScreen) {
                this.fullScreen.mozRequestFullScreen();
            }
        }
        else {
            this.fullScreenIcon = "fa-expand";
            if (document.exitFullscreen) {
                document.exitFullscreen();
            }
            else if (document.webkitExitFullscreen) {
                document.webkitExitFullscreen()
            }
            else if (document.msExitFullscreen) {
                document.msExitFullscreen();
            }
        }
    }


    //Volume Logic
    volumeIcon = "fa-volume-up"; //fa-volume-down fa-volume-off
    toggleMuted() {

        this.videoPlayer.muted = !this.videoPlayer.muted
        this.startControlsTimer();

        if (this.videoPlayer.muted) {
            this.volumeIcon = "fa-volume-off"
        }
        else {
            this.volumeIcon = "fa-volume-up"
        }
    }



    //Progress Bar Logic
    updateProgressBar() {
        var progress = (this.videoPlayer.currentTime / this.videoPlayer.duration) * 100;
        this.progressStyle = { 'width': progress + "%" };
    }


    //Track Width is defined in the directive;
    //This is called when the progress bar is clicked
    jumpProgress(event) {
        var jumpPercentage = event.offsetX / this.playbackTrackWidth;
        this.videoPlayer.currentTime = this.videoPlayer.duration * jumpPercentage;
        this.updateProgressBar();
    }

    //This is called when hovering over the progress bar
    getJumpLocation(event:any){
        var jumpPercentage = event.offsetX / this.playbackTrackWidth;
        this.jumpLocation = this.videoPlayer.duration * jumpPercentage;
        this.jumpLocationPosition = { 'left': (event.offsetX - 10) + "px" }
    }
}
