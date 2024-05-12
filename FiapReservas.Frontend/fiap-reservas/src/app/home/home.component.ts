import { Component, OnInit } from '@angular/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { RestauranteService } from '../services/restaurante.service';
import { RestauranteResponse } from '../entities/restaurante/restaurante-response';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})

export class HomeComponent implements OnInit {

  restaurantes : RestauranteResponse[] = [];

  constructor(private restauranteService: RestauranteService){
    
  }

  ngOnInit(): void {
    this.restauranteService.listarRestaurantes().subscribe((restaurantes) => {
      this.restaurantes = restaurantes;
    });
  }

}
