import { Injectable } from '@angular/core';

@Injectable()
export class TokenManager {

    private _storageKey:string  = "reelStreamToken"

    saveToken(token) {
        localStorage.setItem(this._storageKey, token);
    }

    getToken() {
        return localStorage.getItem(this._storageKey);
    }

    removeToken() {
            localStorage.removeItem(this._storageKey);
    }
}