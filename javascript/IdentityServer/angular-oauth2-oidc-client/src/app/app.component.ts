import {Component, OnInit} from '@angular/core';
import {AuthConfig, JwksValidationHandler, OAuthService} from 'angular-oauth2-oidc';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'angular-oauth2-oidc-client';

  /**
   * Конструктор.
   * @param {OAuthService} extOauthService Авторизация OAuth 2.0.
   */
  constructor(
    private extOauthService: OAuthService
  ) {
  }

  /** @inheritDoc */
  ngOnInit() {
    const authConfig = {

      // Url of the Identity Provider
      issuer: 'http://localhost:6003',

      // URL of the SPA to redirect the user to after login
      redirectUri: 'http://localhost:4203/mods/auth/redirect',

      // The SPA's id. The SPA is registered with this id at the auth-server
      // clientId: 'server.code',
      clientId: 'SrtdbWebClient',

      // Just needed if your auth server demands a secret. In general, this
      // is a sign that the auth server is not configured with SPAs in mind
      // and it might not enforce further best practices vital for security
      // such applications.
      // dummyClientSecret: 'secret',

      responseType: 'code',

      // set the scope for the permissions the client should request
      // The first four are defined by OIDC.
      // Important: Request offline_access to get a refresh token
      // The api scope is a use case specific one
      scope: 'offline_access openid profile SrtdbWebApi',

      showDebugInformation: true,

      // Not recommended:
      // disablePKCI: true,
    } as AuthConfig;

    this.extOauthService.configure(authConfig);
    this.extOauthService.tokenValidationHandler = new JwksValidationHandler();
    console.log('MAKC:loadDiscoveryDocumentAndTryLogin');
    this.extOauthService.loadDiscoveryDocumentAndTryLogin()
      .then(resp => {
        console.log('MAKC:loadDiscoveryDocumentAndTryLogin:resp', resp);
        this.extOauthService.loadUserProfile().then(obj => {
          console.log('MAKC:loadUserProfile:obj', obj);
        });
      })
      .catch(error => {
      console.log('MAKC:loadDiscoveryDocumentAndTryLogin:error', error);
    });
  }
}
