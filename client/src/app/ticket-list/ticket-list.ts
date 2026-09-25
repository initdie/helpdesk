import { Component, signal, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TicketCard } from '../ticket-card/ticket-card';
import { TicketApi } from '../ticket-api';
import { Ticket } from '../models/ticket';

@Component({
  selector: 'app-ticket-list',
  imports: [TicketCard, RouterLink],
  templateUrl: './ticket-list.html',
  styleUrl: './ticket-list.css'
})
export class TicketList implements OnInit {
  private api = inject(TicketApi);
  tickets = signal<Ticket[]>([]);

  ngOnInit() {
    this.api.getTickets().subscribe(data => this.tickets.set(data));
  }
}
