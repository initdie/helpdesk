import { TestBed } from '@angular/core/testing';

import { TicketApi } from './ticket-api';

describe('TicketApi', () => {
  let service: TicketApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TicketApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
