import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PublicComponent } from './public.component';

import { myUrls } from '../core/constants/httpConst';
import { LoginComponent } from './login/login.component';
import { authGuard } from '../core/guards/auth.guard';
import { TaskToDoComponent } from './task-to-do/task-to-do.component';
import { HomeComponent } from './home/home.component';
import { TypeOfTaskComponent } from './type-of-task/type-of-task.component';

const routes: Routes = [
  {
    path: '',
    component: PublicComponent,
    children: [
      { path: '', redirectTo: myUrls.Home, pathMatch: 'full' },
      { path: myUrls.Home, component: HomeComponent },
      { path: myUrls.login, component: LoginComponent },
      { path: myUrls.TaskToDo, component: TaskToDoComponent ,  canActivate: [authGuard] },
      { path: myUrls.TypeOfTask, component: TypeOfTaskComponent ,  canActivate: [authGuard] },
    ],
  },
  { path: '', redirectTo: myUrls.Home, pathMatch: 'full' },
  { path: '**', redirectTo: myUrls.Home },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PublicRoutingModule {}
