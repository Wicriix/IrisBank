import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
conect = `"ConnectionStrings": {
  "IrisDbConnection": "Data Source=.,1434;Initial Catalog=IrisDB;User ID=sa;Password=Calltech#2050;
  Connect Timeout=30;Encrypt=false;TrustServerCertificate=true;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
}`

}
