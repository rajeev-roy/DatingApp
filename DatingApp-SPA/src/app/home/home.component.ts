import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
registerMode = false;
public values: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValue();
  }

  registerToggle(){
    this.registerMode = true;
  }

  getValue()  {
    this.http.get(environment.apiUrl + 'values').subscribe(response => {
        this.values = response;
    }, error => {
      console.log(error);
    }
    );
  }
  cancelRegisterMode(registerMode: boolean){
    this.registerMode = registerMode;

  }
}
