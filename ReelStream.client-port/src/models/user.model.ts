export class User {
    firstName: string;
    lastName: string

    get name(): string { return this.firstName + ' ' + this.lastName };

    constructor(init?: Partial<User>) {
        Object.assign(this, init);
    }
}