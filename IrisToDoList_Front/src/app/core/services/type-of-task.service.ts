import { Injectable } from '@angular/core';
import { HttpMethod, Service } from '../constants/httpConst';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { BaseHttpRepository } from '../shared/baseHttpRepository';
import { StorageService } from './storage.service';
import { UserModel } from '../models/UserResponse.model';

@Injectable({
  providedIn: 'root'
})
export class TypeOfTaskService {


  constructor(private baseHttpRepository: BaseHttpRepository, private storageService: StorageService) {}


  private getHeaders(): HttpHeaders {
    const headers = new HttpHeaders().set('Access-Control-Allow-Origin', '*');
    return headers;
  }

  GetTypeOfTask(): Observable<any> {
    const url = `${Service.GetTypeOfTask}`;
    return this.baseHttpRepository.Request(
      HttpMethod.get,
      url,
      this.getHeaders()
    );
  }

  Add(model: any): Observable<any> {
    const url = `${Service.AddTypeOfTask}?text=${model.text}`;
    return this.baseHttpRepository.Request(
      HttpMethod.post,
      url,
      this.getHeaders(),
      model
    );
  }

  Delete(id: number): Observable<any> {
    const url = `${Service.DeleteTypeOfTask}?id=${id}`;
    return this.baseHttpRepository.Request(
      HttpMethod.delete,
      url,
      this.getHeaders()
    );
  }

}
