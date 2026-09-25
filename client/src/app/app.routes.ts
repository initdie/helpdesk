import { Routes } from '@angular/router';
import { TicketList } from './ticket-list/ticket-list'
import { TicketCreate } from './ticket-create/ticket-create';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import {authGuard} from './guard-guard';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    {path: 'tickets', component: TicketList, canActivate: [authGuard]},
    {path: 'tickets/new', component: TicketCreate, canActivate: [authGuard]},
    {path: '', redirectTo: 'tickets', pathMatch: 'full'}
];
