import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AppModAuthPageRedirectComponent} from './mods/auth/pages/redirect/mod-auth-page-redirect.component';
import {AppRootPageIndexComponent} from './root/pages/index/root-page-index.component';

const routes: Routes = [
  {
    component: AppRootPageIndexComponent,
    path: '',
    pathMatch: 'full'
  },
  {
    component: AppModAuthPageRedirectComponent,
    path: 'mods/auth/redirect'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
