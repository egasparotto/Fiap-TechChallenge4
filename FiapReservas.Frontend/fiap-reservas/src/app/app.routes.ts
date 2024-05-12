import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ReservaComponent } from './reserva/reserva.component';
import { ConfirmarReservaComponent } from './confirmar-reserva/confirmar-reserva.component';

export const routes: Routes = [
    {path: 'home', component: HomeComponent},
    {path: 'login', component: LoginComponent},
    {path: 'reserva/:id', component: ReservaComponent},
    {path: 'confirmar/:id', component: ConfirmarReservaComponent},
    {path: '', redirectTo: '/home', pathMatch: 'full'}
];
