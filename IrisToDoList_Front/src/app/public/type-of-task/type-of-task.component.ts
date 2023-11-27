import { Component } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { GenericResponseModel } from 'src/app/core/models/GenericResponse.model';
import { TypeOfTaskModel } from 'src/app/core/models/TaskToDo.model';
import { AlertService } from 'src/app/core/services/alert.service';
import { TypeOfTaskService } from 'src/app/core/services/type-of-task.service';

@Component({
  selector: 'app-type-of-task',
  templateUrl: './type-of-task.component.html',
  styleUrls: ['./type-of-task.component.css'],
})
export class TypeOfTaskComponent {
  taskTypes: TypeOfTaskModel[] = [];
  newType: string = '';

  constructor(
    private totsvr: TypeOfTaskService,
    private toast: NgToastService,
    private alertService: AlertService
  ) {}

  ngOnInit(): void {
    this.GetTypeOfTask();
  }

  GetTypeOfTask() {
    this.totsvr.GetTypeOfTask().subscribe((data: GenericResponseModel) => {
      if (data.operationSuccess) {
        const response = data.objectResponse;
        this.taskTypes = response;
      } else {
        this.toast.error({
          detail: 'ERROR',
          summary: data.errorMessage,
          duration: 3000,
        });
      }
    });
  }

  addTask() {
    if (this.newType.trim() !== '') {
      const tas = {
        text: this.newType,
      };
      this.totsvr.Add(tas).subscribe((data: GenericResponseModel) => {
        if (data.operationSuccess) {
          this.GetTypeOfTask();
        } else {
          this.toast.error({
            detail: 'ERROR',
            summary: data.errorMessage,
            duration: 3000,
          });
        }
      });
    }
  }

  removeTask(id: number) {
    this.alertService.setOptions({
      title: '¿Estas seguro de eliminar este tipo de tarea?',
      text: 'Se eliminara el tipo',
      icon: 'question',
      showCancelButton: true,
    });

    this.alertService.confirmAlert().then((resp: boolean) => {
      if (resp) {
        this.alertService.loadingShow();
        this.totsvr.Delete(id).subscribe((data: GenericResponseModel) => {
          if (data.operationSuccess) {
            this.GetTypeOfTask();
            this.alertService.loadingClose();
            this.toast.success({
              detail: 'SUCCESS',
              summary: 'tipo eliminado correctamente',
              duration: 3000,
            });
          } else {
            this.toast.error({
              detail: 'ERROR',
              summary: data.errorMessage,
              duration: 3000,
            });
          }
        });
      }
    });
  }
}
