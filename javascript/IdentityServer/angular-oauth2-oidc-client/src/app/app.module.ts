import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {AppModAuthPageRedirectComponent} from './mods/auth/pages/redirect/mod-auth-page-redirect.component';
import {AppRootPageIndexComponent} from './root/pages/index/root-page-index.component';
import {OAuthModule} from 'angular-oauth2-oidc';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    AppModAuthPageRedirectComponent,
    AppRootPageIndexComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: ['http://localhost:5003/api/'],
        sendAccessToken: true
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
