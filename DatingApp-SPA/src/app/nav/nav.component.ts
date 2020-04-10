import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/Auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  constructor(private authServive: AuthService) { }

  ngOnInit() {
  }

  login(){
    console.log(this.model);
    this.authServive.login(this.model).subscribe(
      next => {
        console.log('logging is successful');
        this.loggedIn();
      }, error => {
        console.log('Failed to login');
      }
    );
  }

  loggedIn(){
    const token = localStorage.getItem('token');
    return !!token;
  }

  loggedOut(){
    localStorage.removeItem('token');
    console.log('logged out');
  }

}
