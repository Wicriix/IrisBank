# Bienvenido al Proyecto IRIS

## Descripción
El proyecto **IRIS Task List** está diseñado para ayudar a los usuarios a gestionar sus tareas de manera eficiente. Los usuarios pueden añadir y eliminar tareas. La interfaz frontend está construida con **Angular 16**, ofreciendo una experiencia de usuario dinámica y responsive. El backend de este proyecto está construido con **.NET 7.0** y desplegado en Azure en un app service con su base de datos.

## Clonar el Repositorio
Para clonar el repositorio, utiliza el siguiente comando git:
git clone https://github.com/yourusername/iris-project.git


## Ejecución del Backend
El backend está desplegado en la nube y es accesible en https://iristestback.azurewebsites.net. Está configurado en la colección de Postman incluida en el proyecto. Si deseas ejecutarlo localmente, sigue estos pasos:

- **Restaurar IrisDB.bak**:
  - [Ver Video Tutorial]([video-link](https://www.youtube.com/watch?v=5kcDdZS2hBE)
- **Cambiar la Cadena de Conexión** en el appsettings.json:
  ```json
  "ConnectionStrings": {
    "IrisDbConnection": "Data Source=.,1434;Initial Catalog=IrisDB;User ID=sa;Password=Calltech#2050;Connect Timeout=30;Encrypt=false;TrustServerCertificate=true;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  }
## Ejecución del Frontend

### Instalación de Node.js y npm
- Visita [Node.js](https://nodejs.org/en) y descarga la versión **16.17.0**. Instálala y verifica la instalación en la terminal con `node -v` y `npm -v`.

### Instalar Angular CLI
- En la terminal, ejecuta `npm install -g @angular/cli@16.2.10` y verifica con `ng version`.

### Configuración del Proyecto Angular
- Abre la carpeta **IrisToDoList_Front** con Visual Studio Code o navega a esta ubicación con un cmd.
- Instala las dependencias ejecutando `npm install` en la ubicación del proyecto.

### Ejecución de la Aplicación Angular
- Ejecuta `ng serve` en la carpeta del proyecto.
- Abre un navegador y visita `http://localhost:4200`.

## video explicacion: https://youtu.be/UskcI6jMLYw

### usuario y clave
```json
{
  "email": "williamx1962@gmail.com",
  "password": "prueba"
}
