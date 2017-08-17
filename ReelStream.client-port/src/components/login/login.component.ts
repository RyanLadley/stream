import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { AuthService, ServerRequest } from '../../services/index';
import { LoginCredentials } from '../../models/index';

@Component({
  selector: 'login',
  templateUrl: './login.template.html'
})
export class LoginComponent implements OnInit{
    loginForm: FormGroup;
    errorMessage?: string;


    constructor(private _authService: AuthService, private _serverRequest: ServerRequest) {

    }

    ngOnInit() {
        let username = new FormControl(null, Validators.required);
        let passphrase = new FormControl(null, Validators.required);

        this.loginForm = new FormGroup({
            username: username,
            passphrase: passphrase
        })
    }

    login(credentialValues) {
        if (this.loginForm.valid) {
            let credentials = new LoginCredentials(credentialValues.username, credentialValues.passphrase)
            this._serverRequest.post('/api/user/login', credentials).subscribe(
                response => this._authService.loginUser(response),
                error => this.errorMessage = error.errorMessage
            );
        }
        else {
            this.errorMessage = "Please enter your username and password";
        }
    }
}
