import { HttpClient } from "@angular/common/http"
import { Injectable } from "@angular/core"
import { catchError, throwError } from "rxjs"
import { HttpMethod } from "../constants/httpConst"

@Injectable()
export class BaseHttpRepository{
  constructor(private http: HttpClient){}

  public Request(method: string, url: string, headers?: any, Body?: any):any{
    if(method === HttpMethod.get){
      return this.http.get<any>(url,{headers}).pipe(
        catchError (catchError(this.handlerError)
        )
      )
    }else if(method === HttpMethod.post){
      return this.http.post<any>(url,Body,{headers}).pipe(
        catchError (catchError(this.handlerError)
        )
      )
    }else if(method === HttpMethod.put){
      return this.http.put<any>(url,Body,{headers}).pipe(
         catchError(this.handlerError
        )
      )
    }else if(method === HttpMethod.delete){
      return this.http.delete<any>(url,{headers}).pipe(
         catchError(this.handlerError
        )
      )
    }
  }
  private handlerError(error: Response){
    const msg = 'Error status code ' + error.status + ' status ' + error.statusText + 'body ' + error.body
    const err = new Error(msg);
    return throwError(() => err);
  }
}
