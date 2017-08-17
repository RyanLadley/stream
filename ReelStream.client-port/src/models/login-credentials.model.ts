export class LoginCredentials {
    username: string;
    passphrase: string
    

    constructor(user:string, pass:string) {
        this.username = user;
        this.passphrase = pass;
    }
}