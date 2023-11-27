import { Component } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { GenericResponseModel } from 'src/app/core/models/GenericResponse.model';
import { AlertService } from 'src/app/core/services/alert.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  titleComponent = 'Login | Iris';
  loginForm!: FormGroup;
  hide: boolean = true;

  constructor(
    private title: Title,
    private authService: AuthService,
    private router: Router,
    private toast: NgToastService,
    private fb: FormBuilder,
    private alertService: AlertService
  ) {}

  ngOnInit(): void {
    this.title.setTitle(this.titleComponent);
    this.builForm();
  }

  builForm() {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  login() {
    let username = this.loginForm.controls['username'].value;
    let password = this.loginForm.controls['password'].value;
    this.loginForm.markAllAsTouched();


    if(this.loginForm.valid){
      this.alertService.loadingShow();
      this.authService.login(username, password).subscribe({
        next: (data: GenericResponseModel) => {
          this.alertService.loadingClose();
          if (data.operationSuccess) {
            this.authService.storeToken(data.objectResponse);
            this.toast.success({
              detail: 'SUCCESS',
              summary: data.successMessage,
              duration: 3000,
            });
            this.router.navigate(['Home']);
          } else {
            this.toast.error({
              detail: 'ERROR',
              summary: data.errorMessage,
              duration: 3000,
            });
          }
        },
        error: (error: any) => {
          this.alertService.loadingClose();
          if (error.status === 404) {
            this.toast.error({
              detail: 'ERROR',
              summary: 'Usuario No encontrado',
              duration: 3000,
            });
          } else {
            this.toast.error({
              detail: 'ERROR',
              summary: error,
              duration: 3000,
            });
          }
        }
      });
    }else{
      this.toast.error({
        detail: 'ERROR',
        summary: 'Diligenciar los campos obligatorios',
        duration: 3000,
      });
    }


    }
    getClass(input: string){
      let error = false;
      if(this.loginForm.controls[input].invalid && this.loginForm.controls[input].touched){
        error = true
      }
      return error;
    }
}
