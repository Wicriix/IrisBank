import { Component } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { GenericResponseModel } from 'src/app/core/models/GenericResponse.model';
import { Task } from 'src/app/core/models/Task.model';
import {
  TaskToDoModel,
  TypeOfTaskModel,
} from 'src/app/core/models/TaskToDo.model';
import { AlertService } from 'src/app/core/services/alert.service';
import { TaskToDoService } from 'src/app/core/services/task-to-do.service';
import { TypeOfTaskService } from 'src/app/core/services/type-of-task.service';

@Component({
  selector: 'app-task-to-do',
  templateUrl: './task-to-do.component.html',
  styleUrls: ['./task-to-do.component.css'],
})
export class TaskToDoComponent {
  tasks: Task[] = [];
  newTask: string = '';
  newTaskType: string = 'ALL'; // Valor predeterminado para el tipo de tarea
  taskTypes: TypeOfTaskModel[] = []; // Tipos de tareas posibles

  constructor(
    private tstodosrv: TaskToDoService,
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
        this.GetTaskTodo();
      } else {
        this.toast.error({
          detail: 'ERROR',
          summary: data.errorMessage,
          duration: 3000,
        });
      }
    });
  }
  toggleTaskCompletion(task: Task) {
    const f = task.done;

    const tas = {
      id: task.idTaskToDo,
      text: task.description,
      done: task.done
    };

    this.tstodosrv.update(tas).subscribe((data: GenericResponseModel) => {
      if (data.operationSuccess) {
        this.GetTaskTodo();
      } else {
        this.toast.error({
          detail: 'ERROR',
          summary: data.errorMessage,
          duration: 3000,
        });
      }
    });
  }
  GetTaskTodo() {
    this.tstodosrv.Get().subscribe((data: GenericResponseModel) => {
      if (data.operationSuccess) {
        this.tasks = [];
        const response = data.objectResponse;
        response.forEach((element: TaskToDoModel) => {
          this.tasks.push({
            description: element.textData.trim(),
            type: element.typeOfTask.name,
            idTaskToDo: element.idTaskToDo,
            done: element.done
          });
          this.newTask = '';
          this.newTaskType = '';
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

  addTask() {
    if (this.newTask.trim() !== '' && this.newTask !== '') {
      const tas = {
        id: this.newTaskType,
        text: this.newTask,
      };

      this.tstodosrv.Add(tas).subscribe((data: GenericResponseModel) => {
        if (data.operationSuccess) {
          this.GetTaskTodo();
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

  Updateask(done:boolean) {
    if (this.newTask.trim() !== '' && this.newTask !== '') {
      const tas = {
        id: this.newTaskType,
        text: this.newTask,
      };

      this.tstodosrv.Add(tas).subscribe((data: GenericResponseModel) => {
        if (data.operationSuccess) {
          this.GetTaskTodo();
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
      title: '¿Estas seguro de eliminar esta tarea?',
      text: 'Se eliminara la tarea',
      icon: 'question',
      showCancelButton: true,
    });
    this.alertService.confirmAlert().then((resp: boolean) => {
      if (resp) {
        this.alertService.loadingShow();
        this.tstodosrv.Delete(id).subscribe((data: GenericResponseModel) => {
          if (data.operationSuccess) {
            this.GetTaskTodo();
            this.alertService.loadingClose();
            this.toast.success({
              detail: 'SUCCESS',
              summary: 'tarea eliminada correctamente',
              duration: 2000,
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
