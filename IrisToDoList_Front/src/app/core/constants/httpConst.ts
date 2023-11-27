import { environment } from "src/environments/environment";

export const myUrls ={
    Home: "home",
    login:"login",
    TaskToDo:"TaskToDo",
    TypeOfTask:"TypeOfTask"
}

export const HttpMethod ={
    get: 'get',
    post: 'post',
    put: 'put',
    delete: 'delete',
  };

  export const UrlService ={
    BaseUrl: environment.apiUrl,
    UserAuth: environment.apiUrl + "/api/v1/Auth/aunthenticate",
    TaskToDo: environment.apiUrl + "/api/v1/TaskToDo",
    TypeOfTask: environment.apiUrl + "/api/v1/TypeOfTask"
  };

  export const Service = {
    GetTaskToDo: UrlService.TaskToDo + "/Get",
    AddTaskToDo: UrlService.TaskToDo + "/Add",
    UpdateTaskToDo: UrlService.TaskToDo + "/Update",
    DeleteTaskToDo: UrlService.TaskToDo + "/Delete",
    GetTypeOfTask: UrlService.TypeOfTask + "/Get",
    AddTypeOfTask: UrlService.TypeOfTask + "/Add",
    DeleteTypeOfTask: UrlService.TypeOfTask + "/Delete",
  }
