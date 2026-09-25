import { Component, input, computed } from '@angular/core';
import { TicketStatus } from '../models/ticket';

const STATUS_LABELS: Record<TicketStatus, string> = {
  Open: 'Відкрито',
  InProgress: 'В роботі',
  Done: 'Виконано'
};

@Component({
  selector: 'app-ticket-card',
  imports: [],
  templateUrl: './ticket-card.html',
  styleUrl: './ticket-card.css',
})
export class TicketCard {
  title = input.required<string>();
  description = input.required<string>();
  status = input.required<TicketStatus>();

  statusLabel = computed(() => STATUS_LABELS[this.status()]);
}
