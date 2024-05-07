import {Component} from '@angular/core';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import { FormGroup, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { LoginService } from '../services/login.service';
import { LoginResponse } from '../entities/login/login-response';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  erro: boolean = false;

  formLogin: FormGroup = this.formBuilder.group({
    email: [''],
    password: ['']
  });

  constructor(private formBuilder: FormBuilder, 
              private loginService: LoginService,
              private router: Router) { }

  onSubmit(){
    var login = this.formLogin.value;
    this.loginService.login(login).subscribe({ 
    next: (response: LoginResponse) =>{
      localStorage.setItem('token', response.token);
      this.router.navigate(['/home']);
    },
    error: () =>{
      this.erro = true;
    }});
  }

}
