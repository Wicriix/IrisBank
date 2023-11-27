import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseHttpRepository } from './baseHttpRepository';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NgToastModule } from 'ng-angular-popup';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { TokenInterceptor } from '../interceptors/token.interceptor';



@NgModule({
  declarations: [
    NavbarComponent
  ],
  imports: [
    HttpClientModule,
    RouterModule,
    CommonModule,
    NgToastModule
  ],
  exports:[
    NavbarComponent,
    NgToastModule,
    HttpClientModule,
    RouterModule
  ],

  providers:[
    BaseHttpRepository,
    {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}
  ]
})
export class SharedModule { }
