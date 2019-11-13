import {Component, OnInit} from '@angular/core';
import {OAuthService} from 'angular-oauth2-oidc';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './mod-auth-page-redirect.component.html',
  styleUrls: ['./mod-auth-page-redirect.component.css']
})
export class AppModAuthPageRedirectComponent implements OnInit {
  private url = 'http://localhost:5003/api/auth-test/current-user/';

  title = 'Перенаправление';

  /**
   * Конструктор.
   * @param {HttpClient} http
   * @param {OAuthService} extOauthService Авторизация OAuth 2.0.
   */
  constructor(
    private http: HttpClient,
    private extOauthService: OAuthService
  ) {
  }

  /** @inheritDoc */
  ngOnInit() {
    this.http.get(this.url).subscribe(resp => {
      console.log('MAKC:AppModAuthPageRedirectComponent:ngOnInit:isAuthenticated', resp);
    });
  }

  callApi() {
    this.http.get(this.url).subscribe(resp => {
      console.log('MAKC:AppModAuthPageRedirectComponent:callApi:isAuthenticated', resp);
    });
  }
}
