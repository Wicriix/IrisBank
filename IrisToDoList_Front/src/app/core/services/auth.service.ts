import { Injectable } from '@angular/core';
import { BaseHttpRepository } from '../shared/baseHttpRepository';
import { Observable } from 'rxjs';
import { HttpMethod, UrlService } from '../constants/httpConst';
import { HttpHeaders } from '@angular/common/http';
import { StorageService } from './storage.service';
import { UserModel } from '../models/UserResponse.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private baseHttpRepository: BaseHttpRepository,
    private storageService: StorageService) {}


  private getHeaders(): HttpHeaders {
    const headers = new HttpHeaders().set('Access-Control-Allow-Origin', '*');
    return headers;
  }

  login(email:string, password:string ): Observable<any> {
    const url = `${UrlService.UserAuth}`;
    const body = {
      email,
      password
    }
    return this.baseHttpRepository.Request(
      HttpMethod.post,
      url,
      this.getHeaders(),
      body
    );
  }

  storeToken(tokenValue: string){
    this.storageService.store('token', tokenValue);
  }

  getToken(){
    const token: UserModel = this.storageService.GetObject('token')
    return token == null ? token : token.token;
  }

  isLoggedIn():boolean{
    return !!this.storageService.retrieve('token');
  }
}
