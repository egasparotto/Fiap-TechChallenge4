import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReservaService } from '../services/reserva.service';
import { ReservaResponse } from '../entities/reserva/reserva-response';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { NgxMaskDirective } from 'ngx-mask';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-reserva',
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
            NgxMaterialTimepickerModule,
            MatSelectModule],
  templateUrl: './reserva.component.html',
  styleUrl: './reserva.component.css'
})
export class ReservaComponent implements OnInit {

  public reserva : ReservaResponse = new ReservaResponse();

  formReservar: FormGroup = this.formBuilder.group({
    data: new FormControl({value: '', disabled: true}),
    hora: new FormControl({value: '', disabled: true}),
    nome: new FormControl({value: '', disabled: true}),
    telefone: new FormControl({value: '', disabled: true}),
    email: new FormControl({value: '', disabled: true}),
    quantidadePessoas: new FormControl({value: '', disabled: true}),
    status: new FormControl({value: '', disabled: true})
  });

  constructor(private route: ActivatedRoute,
              private reservaService: ReservaService,
              private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params =>{
      this.reservaService.obterReserva(params['id']).subscribe(reserva =>{
        var form = reserva as any;
        form.data = reserva.dataReserva;
        var tzoffset = (new Date()).getTimezoneOffset() * 60000; //offset in milliseconds
        var localISOTime = (new Date(new Date(reserva.dataReserva).getTime() - tzoffset)).toISOString();
        form.hora = localISOTime.split('T')[1].substring(0,5);

        this.reserva = reserva;
        this.formReservar.patchValue(form);
      });
    });
  }
}
