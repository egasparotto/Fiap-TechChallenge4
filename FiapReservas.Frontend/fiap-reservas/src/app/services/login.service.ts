import { Injectable } from '@angular/core';
import { Login } from '../entities/login/login';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponse } from '../entities/login/login-response';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  url = environment.urlApi;

  constructor(private httpClient: HttpClient) { }

  login(login: Login) : Observable<LoginResponse>{
    var path = '/User/login';
    return this.httpClient.post<LoginResponse>(this.url + path, login);
  }
}
