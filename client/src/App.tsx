import './App.css'
import './types.ts'

import type { Ticket, TicketStatus } from './types';
import { useEffect, useState } from 'react';

type TicketCardProps = {
  title: string
  description: string
}

function TicketCard({ title, description }: TicketCardProps) {
  return (
    <>
      <h3>Title: {title}</h3>
      <p>{description}</p>
    </>
  )
}

function App() {
  const [tickets, setTicket] = useState<Ticket[]>([]);

  useEffect(() => {
    async function load() {
      const res = await fetch('http://localhost:5279/api/ticket');
      const data: Ticket[] = await res.json();
      setTicket(data);
    }
    load()
  }, [])

  return (
    <>
      {
        tickets.map(t => (
          <TicketCard key={t.id} title={t.title} description={t.description}/>
        ))
      }
    </>
  )
}

export default App
