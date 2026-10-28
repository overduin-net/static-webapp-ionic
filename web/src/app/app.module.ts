import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import { AppRoutingModule } from './app-routing.module';
import { IonicModule } from '@ionic/angular';

import { AppComponent } from './app.component';

import { MenuComponent } from './menu/menu.component';
import { HomeComponent } from './home/home.component';
import { PersonComponent } from './person/person.component';
import { environment } from 'src/environments/environment';

import { AuthService } from 'src/services/auth.service';
import { ApiService, API_URL } from 'src/services/generated/api.service';
import { AccountService } from 'src/services/account.service';

@NgModule({ declarations: [
        AppComponent,
        PersonComponent,
        HomeComponent,
        MenuComponent
    ],
    bootstrap: [AppComponent], imports: [BrowserModule,
        AppRoutingModule,
        IonicModule.forRoot(),
        FormsModule,
    ], providers: [
        { provide: API_URL, useValue: getApiUrl(environment.apiUrl) },
        AccountService,
        AuthService,
        ApiService,
        provideHttpClient(withInterceptorsFromDi())
    ] })
export class AppModule { }

function getApiUrl(configuredApiUrl: string): string {
    if (configuredApiUrl) {
        return configuredApiUrl;
    }

    const isAngularDevServer = window.location.hostname === 'localhost' && window.location.port === '4200';
    if (isAngularDevServer) {
        return 'http://localhost:7071';
    }

    return '';
}
