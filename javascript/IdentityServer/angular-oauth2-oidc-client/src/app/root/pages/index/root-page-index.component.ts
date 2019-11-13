import {Component, OnInit} from '@angular/core';
import {OAuthService} from 'angular-oauth2-oidc';

@Component({
  selector: 'app-root',
  templateUrl: './root-page-index.component.html',
  styleUrls: ['./root-page-index.component.css']
})
export class AppRootPageIndexComponent implements OnInit {
  title = 'Начало';

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
    console.log('MAKC:AppRootPageIndexComponent:ngOnInit');
    if (!this.extOauthService.hasValidAccessToken()) {
      console.log('MAKC:AppRootPageIndexComponent:ngOnInit:initLoginFlow');
      this.extOauthService.initLoginFlow();
    }
  }
}
