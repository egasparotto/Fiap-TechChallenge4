import { Component, Inject, OnInit } from '@angular/core';
import { RestauranteService } from '../services/restaurante.service';
import { RestauranteResponse } from '../entities/restaurante/restaurante-response';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogTitle,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { NgxMaskDirective } from 'ngx-mask';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {NgxMaterialTimepickerModule} from 'ngx-material-timepicker';
import { Router } from '@angular/router';
import { ReservaService } from '../services/reserva.service';



@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})

export class HomeComponent implements OnInit {

  restaurantes : RestauranteResponse[] = [];

  constructor(private restauranteService: RestauranteService, public reservarDialog : MatDialog){
    
  }

  ngOnInit(): void {
    this.restauranteService.listarRestaurantes().subscribe((restaurantes) => {
      this.restaurantes = restaurantes;
    });
  }


  openDialog(restaurante: RestauranteResponse): void {
    this.reservarDialog.open(ReservarDialogComponent, {
      width: '600px',
      data: restaurante
    });
  }

}

@Component({
  selector: 'app-reservar-dialog',
  templateUrl: './home.component-registrar-dialog.html',
  styleUrl: './home.component-registrar-dialog.css',
  standalone: true,
  imports: [MatButtonModule, 
            MatDialogTitle, 
            MatDialogContent, 
            MatFormFieldModule, 
            MatInputModule, 
            ReactiveFormsModule, 
            CommonModule, 
            NgxMaskDirective, 
            MatDatepickerModule,
            NgxMaterialTimepickerModule]
})

export class ReservarDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public restaurante: RestauranteResponse, 
              private formBuilder: FormBuilder, 
              private router: Router, 
              private dialogRef: MatDialogRef<ReservarDialogComponent>,
              private reservaService: ReservaService) {}

  formReservar: FormGroup = this.formBuilder.group({
    data: [new Date(), [Validators.required]],
    hora: ['', [Validators.required]],
    nome: ['', [Validators.required]],
    telefone: ['', [Validators.required,Validators.pattern(/^[1-9]{2}[0-9]{9}$/)]],
    email: ['', [Validators.required, Validators.email]],
    quantidadePessoas:['', [Validators.required, Validators.pattern(/^(0?[1-9]|[1-9][0-9])$/)]]
  });
  
  onSubmit(){
    var reserva = this.formReservar.value;
    var data = reserva.data.toISOString().split('T')[0];
    var dataHora = new Date(data+'T'+reserva.hora+':00');
    reserva.dataReserva = dataHora;
    reserva.idRestaurante = this.restaurante.id;
    this.reservaService.reservar(reserva).subscribe(reserva =>{
      this.router.navigate(['/reserva',reserva.id]);
      this.dialogRef.close();
      console.log(reserva);
    });
  }
}
