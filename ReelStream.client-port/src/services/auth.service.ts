import { Injectable, EventEmitter} from '@angular/core';
import { Router } from '@angular/router'
import { User } from '../models/user.model';
import { TokenManager } from '../services/token-manager.service';
import { ServerRequest } from '../services/server-request.service';

@Injectable()
export class AuthService{
    loginUpdated: EventEmitter<boolean> = new EventEmitter();

    user: User
    isLoggedIn: boolean;

    constructor(private _tokenManager: TokenManager, private router: Router, private _serverRequest: ServerRequest) {
        this._serverRequest.get('/api/user/token').subscribe(
            response => this.loginUser(response),
            error => this.router.navigate(["/login"])
        );
    }
    


    loginUser(token: any) {
        this.isLoggedIn = true;

        this._tokenManager.saveToken(token.accessToken);

        this.user = new User({
            firstName: token.user.firstName as string,
            lastName: token.user.lastName as string
        });

        if (this.router.url === "/login" || this.router.url  === "/register") {
            this.router.navigate(["/"]);
        }

        this.loginUpdated.emit(true);
    }

    getUser() {

        return this.user;
    }
}