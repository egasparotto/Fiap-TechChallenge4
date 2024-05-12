import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { RestauranteResponse } from '../entities/restaurante/restaurante-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RestauranteService {

  url = environment.urlApi;

  constructor(private httpClient: HttpClient) { }

  listarRestaurantes() : Observable<RestauranteResponse[]>{
    var path = '/Restaurante';
    return this.httpClient.get<RestauranteResponse[]>(this.url + path);
  }
  
}
