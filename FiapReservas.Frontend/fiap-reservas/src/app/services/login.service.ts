import { Injectable } from '@angular/core';
import { Login } from '../entities/login/login';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponse } from '../entities/login/login-response';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  url = 'http://localhost:9999';

  constructor(private httpClient: HttpClient) { }

  login(login: Login) : Observable<LoginResponse>{
    var path = '/User/login';
    return this.httpClient.post<LoginResponse>(this.url + path, login);
  }
}
