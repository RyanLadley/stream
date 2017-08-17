import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/index';
import { User } from '../../models/user.model';

@Component({
  selector: 'header',
  templateUrl: './header.template.html'
})
export class HeaderComponent implements OnInit {
    isLoggedIn: boolean;
    user: User;
    initials: string;
    constructor(private _authService: AuthService) {

    }

    ngOnInit() {
        //this.updateLoginStatus();
        this._authService.loginUpdated.subscribe(
            (updated: boolean) => {
                this.updateLoginStatus();
            }
        );
    }

    updateLoginStatus() {
        this.isLoggedIn = this._authService.isLoggedIn;
        if (this.isLoggedIn) {
            this.user = this._authService.getUser();
            this.initials = this.user.firstName[0] + this.user.lastName[0];
        }
    }
}
