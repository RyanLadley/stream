import { Injectable, EventEmitter } from '@angular/core';
import { Http, Headers, RequestMethod, RequestOptions, Request, Response } from '@angular/http';
import { Subject, Observable } from 'rxjs/Rx';
import { TokenManager } from '../services/token-manager.service';
import { AppSettings } from '../settings/settings';

@Injectable()
export class ServerRequest {

    constructor(private _http: Http, private _tokenManager: TokenManager, private _appSettings: AppSettings) {

    }

    get(url: string, payload?: any) {
        return this._request(RequestMethod.Get, url, payload);
    }

    post(url: string, payload?: any) {
        return this._request(RequestMethod.Post, url, payload);
    }
    

    private _getHeaders() {
        let headers: Headers = new Headers({
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        });

        if (this._tokenManager.getToken()) {
            headers.append('Authorization', 'Bearer ' + this._tokenManager.getToken())
        }

        return headers
    }   

    private _request(httpMethod: RequestMethod, url: string, payload?: any): Observable<any>{
        

        let requestOptions = new RequestOptions(Object.assign({
            method: httpMethod,
            url: this._appSettings.serverUrl + url,
            headers: this._getHeaders(),
            body: payload
        }));

        return this._http.request(new Request(requestOptions))
            .map(response => { return response.json() })
            .catch((error) => { return this._handleError(error); })
    }

    private _handleError(error: Response) {
        return Observable.throw(error.json())
    }


}