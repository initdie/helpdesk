export type TicketStatus = 'Open' | 'InProgress' | 'Done'

export interface Ticket {
    id: number,
    title: string,
    description: string,
    status: TicketStatus,
    assignedAgentId: number | null
}

export interface CreateTicket {
    title: string,
    description: string
}