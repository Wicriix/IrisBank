import { Injectable } from '@angular/core';
import { HttpMethod, Service } from '../constants/httpConst';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { BaseHttpRepository } from '../shared/baseHttpRepository';
import { StorageService } from './storage.service';
import { UserModel } from '../models/UserResponse.model';
import { TaskToDoModel } from '../models/TaskToDo.model';

@Injectable({
  providedIn: 'root'
})
export class TaskToDoService {


  constructor(private baseHttpRepository: BaseHttpRepository, private storageService: StorageService) {}


  private getHeaders(): HttpHeaders {
    const headers = new HttpHeaders().set('Access-Control-Allow-Origin', '*');
    return headers;
  }

  Get(): Observable<any> {
    const url = `${Service.GetTaskToDo}`;
    return this.baseHttpRepository.Request(
      HttpMethod.get,
      url,
      this.getHeaders()
    );
  }

  Add(model: any): Observable<any> {
    const url = `${Service.AddTaskToDo}?idtype=${model.id}&text=${model.text}`;
    return this.baseHttpRepository.Request(
      HttpMethod.post,
      url,
      this.getHeaders(),
      model
    );
  }

  update(model: any): Observable<any> {
    const url = `${Service.UpdateTaskToDo}?done=${model.done}&id=${model.id}&text=${model.text}`;
    return this.baseHttpRepository.Request(
      HttpMethod.put,
      url,
      this.getHeaders(),
      model
    );
  }

  Delete(id: number): Observable<any> {
    const url = `${Service.DeleteTaskToDo}?id=${id}`;
    return this.baseHttpRepository.Request(
      HttpMethod.delete,
      url,
      this.getHeaders()
    );
  }


}
