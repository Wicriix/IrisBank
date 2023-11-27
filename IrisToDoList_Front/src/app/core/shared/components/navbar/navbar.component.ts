import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { UserModel } from 'src/app/core/models/UserResponse.model';
import { AlertService } from 'src/app/core/services/alert.service';
import { StorageService } from 'src/app/core/services/storage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  userLogged!: UserModel;
  isLogged: boolean = false;

  constructor(
    private storageService: StorageService,
    private router: Router,
    private alertService: AlertService,
    private toast: NgToastService,
  ){}

  ngOnInit(): void {
    this.getInfoUser();
  }

  getInfoUser(){
    this.userLogged = this.storageService.GetObject("token");
    this.isLogged = this.userLogged == null ? false : true;
  }

  signOut(){
    this.alertService.setOptions({
      title: '¿Estas seguro de cerrar sesión?',
      text: 'Se cerrara sesión',
      icon: 'question',
      showCancelButton: true,
    });
    this.alertService.confirmAlert().then((resp: boolean) => {
      if(resp){
        this.storageService.removeAll();
        this.isLogged = false;
        this.toast.success({
          detail: 'SUCCESS',
          summary: 'Se cerro sesión correctamente',
          duration: 3000,
        });
        this.router.navigate(['Home']);
      }
    })
  }
}
