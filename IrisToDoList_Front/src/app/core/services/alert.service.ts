import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})

export class AlertService {

  constructor(){}

  options={
    icon: 'question',
    message: '',
    timer: '',
    from: 'top',
    type: 'success',
    title: '',
    text: '',
    buttonStyling : false,
    confirmButton: 'btn',
    showCancelButton: false,
    cancelButtonText: 'Cancelar',
    confirmButtonText: 'Aceptar',
    confirmButtonColor: '#4F90C8',
    cancelButtonColor: '#d33',
  };
  icon : any = 'question';

  setOptions(options: any): void{
    if(options.title){
      this.options.title = options.title;
    }
    if(options.text){
      this.options.text = options.text;
    }
    if(options.confirmButton){
      this.options.confirmButton = options.confirmButton;
    }
    if(options.message){
      this.options.message = options.message;
    }
    if(options.icon){
      this.icon = options.icon;
    }
    if(options.showCancelButton){
      this.options.showCancelButton = options.showCancelButton;
    }
    if(options.cancelButtonText){
      this.options.cancelButtonText = options.cancelButtonText;
    }
    if(options.confirmButtonText){
      this.options.confirmButtonText = options.confirmButtonText;
    }
    if(options.confirmButtonColor){
      this.options.confirmButtonColor = options.confirmButtonColor;
    }
    if(options.cancelButtonColor){
      this.options.cancelButtonColor = options.cancelButtonColor;
    }
  }
  alert(): void{
    Swal.fire({
      title: this.options.title,
      text: this.options.text,
      icon: this.icon,
      confirmButtonColor: this.options.confirmButtonColor,
      customClass:{
        confirmButton: this.options.confirmButton,
      }
    });
  }
loadingShow(): void{
  Swal.fire({
    icon: 'info',
    title: 'Cargando...',
    didOpen: () => {
      Swal.showLoading();
    }
  })
}

loadingClose(): void{
  Swal.fire({
    didOpen: () => {
      Swal.hideLoading();
    }
  })
}

confirmAlert(yesOptions:any = null, noOptions:any = null): Promise<boolean>{
  return new Promise((resolve) => {
    Swal.fire({
      title: this.options.title,
      text: this.options.text,
      icon: this.icon,
      showCancelButton: true,
      confirmButtonText: this.options.confirmButtonText,
      cancelButtonText: this.options.cancelButtonText,
      confirmButtonColor: this.options.confirmButtonColor,
      cancelButtonColor: this.options.cancelButtonColor,
    }).then((result) => {
      if(result.isConfirmed){
        if(yesOptions){
          Swal.fire(
            yesOptions.title,
            yesOptions.text,
            yesOptions.icon,
          );
        }
        resolve(true);
      }else{
        if(noOptions){
          Swal.fire(
            noOptions.title,
            noOptions.text,
            noOptions.icon,
          );
        }
      }
    });
  });
}
}
