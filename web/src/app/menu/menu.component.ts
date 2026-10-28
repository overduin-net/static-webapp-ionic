import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface ClientPrincipal {
  identityProvider: string
  userId: string
  userDetails: string
  userRoles: string[]
}

@Component({
    selector: 'app-menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.scss'],
    standalone: false
})
export class MenuComponent {
  constructor(private http: HttpClient) {
    this.http.get('.auth/me').subscribe((resp: any)=> {
      this.user = resp.clientPrincipal;
    });
  }

  title = 'Template';
  public user: ClientPrincipal | undefined;


  isAuthenticated(){
    if(this.user && this.user.userRoles.some(x => x ==='authenticated'))
    {
      return true
    }
    return false;
  }

  isAdmin(){
    if(this.user && this.user.userRoles.some(x => x ==='administrator'))
    {
      return true
    }
    return false;
  }
  
  isReader(){
    return false;
  }


}
