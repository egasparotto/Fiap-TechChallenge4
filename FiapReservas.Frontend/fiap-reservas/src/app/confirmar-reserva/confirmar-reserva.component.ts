import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReservaService } from '../services/reserva.service';

@Component({
  selector: 'app-confirmar-reserva',
  standalone: true,
  imports: [],
  templateUrl: './confirmar-reserva.component.html',
  styleUrl: './confirmar-reserva.component.css'
})
export class ConfirmarReservaComponent implements OnInit {
constructor(private route: ActivatedRoute,
            private reservaService: ReservaService,
            private router: Router
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params =>{
      this.reservaService.confirmarReserva(params['id']).subscribe(() =>{
        this.router.navigate(['/reserva',params['id']]);
      });
    });
  }
}



