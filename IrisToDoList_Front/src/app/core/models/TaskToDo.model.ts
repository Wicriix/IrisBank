export interface TaskToDoModel {
  idTaskToDo: number
  userId: number
  typeOfTaskId: number
  textData: string
  typeOfTask: TypeOfTaskModel
  user: any
  done: boolean
}

export interface TypeOfTaskModel {
  userId: any
  name: string
  idTypeOfTask: number
  user: any
}
