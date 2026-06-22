
export type TicketStatus = 'Open' | 'InProgress' | 'Done'

export type Ticket = {
    id: number,
    title: string,
    description: string,
    status: TicketStatus,
    assignedAgentId: number | null
}
