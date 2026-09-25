import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateTicket, Ticket } from './models/ticket';

@Injectable({ providedIn: 'root' })
export class TicketApi {
    private http = inject(HttpClient);
    private apiUrl = 'https://localhost:7093/api/ticket';

    getTickets() : Observable<Ticket[]> {
        return this.http.get<Ticket[]>(this.apiUrl);
    }

    createTicket(ticket: CreateTicket) : Observable<void> {
        return this.http.post<void>(this.apiUrl, ticket);
    }
}
