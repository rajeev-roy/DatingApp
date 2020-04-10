import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/Auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  constructor(private authServie: AuthService) { }

  ngOnInit() {
  }

  register(){
    this.authServie.register(this.model).subscribe(() => {console.log('Registration successful'); },
    error => { console.log(error);}
    );
  }

  cancel(){
   this.cancelRegister.emit(false);
  }

}
