import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { TaskToDoComponent } from './task-to-do/task-to-do.component';
import { HomeComponent } from './home/home.component';
import { CoreModule } from '../core/core.module';
import { PublicRoutingModule } from './public-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PublicComponent } from './public.component';
import { TypeOfTaskComponent } from './type-of-task/type-of-task.component';



@NgModule({
  declarations: [
    PublicComponent,
    LoginComponent,
    TaskToDoComponent,
    HomeComponent,
    TypeOfTaskComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    CoreModule,
    PublicRoutingModule,
    ReactiveFormsModule
  ]
})
export class PublicModule { }
