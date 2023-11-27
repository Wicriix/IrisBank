import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { StorageService } from '../services/storage.service';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  private unauthorizedResponse = false;

  constructor(
    private authService: AuthService,
    private storageService: StorageService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const token = this.authService.getToken();
    if (token != null) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${token}` },
      });
    }


    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.unauthorizedResponse = true;
          this.storageService.removeAll();
          this.router.navigate(['login']);
        } else {
          this.unauthorizedResponse = false;
        }
        return throwError(() => error);
      })
    );
  }

  isUnauthorizedResponse(): boolean {
    return this.unauthorizedResponse;
  }
}
