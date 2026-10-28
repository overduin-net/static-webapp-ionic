import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable, of } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class AuthService  {
    constructor() { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        return this.validateLogin();
    }

    validateLogin(): Observable<boolean | UrlTree> {
        return of(true);
    }
}
