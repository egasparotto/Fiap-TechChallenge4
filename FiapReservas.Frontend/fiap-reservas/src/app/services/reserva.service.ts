import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReservaResponse } from '../entities/reserva/reserva-response';
import { Reservar } from '../entities/reserva/reservar';

@Injectable({
  providedIn: 'root'
})
export class ReservaService {

  url = environment.urlApi;

  constructor(private httpClient: HttpClient) { }

  reservar(reserva: Reservar) : Observable<ReservaResponse>{
    var path = '/Reserva/Reservar';
    return this.httpClient.post<ReservaResponse>(this.url + path, reserva);
  }

  obterReserva(id: string) : Observable<ReservaResponse>{    
    var path = '/Reserva/'+id;
    return this.httpClient.get<ReservaResponse>(this.url + path);
  }

  confirmarReserva(id: string) : Observable<ReservaResponse>{    
    var path = '/Reserva/'+id+'/Confirmar';
    return this.httpClient.post<ReservaResponse>(this.url + path, {});
  }
  
}
