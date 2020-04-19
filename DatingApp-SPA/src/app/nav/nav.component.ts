import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/Auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  constructor(public authServive: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login(){
     this.authServive.login(this.model).subscribe(
      next => {
        this.alertify.success('logging is successful');
        this.loggedIn();
      }, error => {
        this.alertify.error(error);
      }
    );
  }

  loggedIn(){
    return this.authServive.loggedIn();
  }

  loggedOut(){
    localStorage.removeItem('token');
    this.alertify.message('logged out');
  }

}
