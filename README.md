# Helpdesk API

A ticketing / helpdesk REST API built with ASP.NET Core. Supports the full ticket lifecycle (create → assign → change status → close), user registration/login with JWT authentication, and role-based access (`User` / `Agent`).

## Tech stack

- **ASP.NET Core** Web API (.NET 10)
- **Entity Framework Core** + **PostgreSQL** (Npgsql)
- **JWT** authentication (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- **BCrypt.Net-Next** for password hashing
- **xUnit** + EF Core InMemory for unit tests

## Features

- CRUD for tickets
- Ticket status lifecycle: `Open → InProgress → Done`
- Assign a ticket to an agent (Agent role only)
- Filter tickets by status
- Register / login, JWT issued on login
- Role-based authorization

## Getting started

### Prerequisites
- .NET 10 SDK
- PostgreSQL running locally

### Configuration
Set the connection string and JWT options in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=helpdesk;Username=postgres;Password=postgres"
},
"Jwt": {
  "Key": "your-secret-key-min-32-chars",
  "Issuer": "helpdesk",
  "Audience": "helpdesk-users"
}
```

### Run

```bash
dotnet ef database update   # apply migrations / create tables
dotnet run                  # start the API
```

### Tests

```bash
dotnet test
```

## Authentication

Most ticket endpoints require a JWT. Flow:

1. `POST /api/auth/register` to create a user.
2. `POST /api/auth/login` to receive a token.
3. Send the token on protected requests:

```
Authorization: Bearer <token>
```

## Enums

| TicketStatus | value | | Role  | value |
|---|---|---|---|---|
| Open         | 0 | | User  | 0 |
| InProgress   | 1 | | Agent | 1 |
| Done         | 2 | | | |

> Note: enums are serialized as their integer value unless `JsonStringEnumConverter` is configured, in which case names (`"Open"`) are accepted.

## API endpoints

### Auth

| Method | Route | Auth | Body | Responses |
|---|---|---|---|---|
| POST | `/api/auth/register` | — | `{ "email", "password", "role" }` | `200` OK · `400` email taken |
| POST | `/api/auth/login` | — | `{ "email", "password" }` | `200` `{ "token" }` · `401` invalid credentials |

### Tickets  *(require `Authorization: Bearer <token>`)*

| Method | Route | Auth | Body | Responses |
|---|---|---|---|---|
| GET | `/api/Ticket?status={0\|1\|2}` | any user | — | `200` list (filter optional) |
| GET | `/api/Ticket/{id}` | any user | — | `200` ticket · `404` |
| POST | `/api/Ticket` | any user | `{ "title", "description" }` | `200` OK |
| PUT | `/api/Ticket/{id}` | any user | `{ "title", "description" }` | `204` · `404` |
| DELETE | `/api/Ticket/{id}` | any user | — | `204` · `404` |
| PATCH | `/api/Ticket/{ticketId}/assign?agentId={id}` | **Agent** | — | `204` · `404` · `403` non-agent |
| PATCH | `/api/Ticket/{ticketId}/status` | any user | `{ "status": 2 }` | `204` · `404` |

## Example requests

```http
### Register an agent
POST http://localhost:5279/api/auth/register
Content-Type: application/json

{ "email": "agent@helpdesk.com", "password": "Pass123!", "role": 1 }

### Login
POST http://localhost:5279/api/auth/login
Content-Type: application/json

{ "email": "agent@helpdesk.com", "password": "Pass123!" }

### Create a ticket
POST http://localhost:5279/api/Ticket
Content-Type: application/json
Authorization: Bearer <token>

{ "title": "Printer is down", "description": "3rd floor, room 12" }

### List open tickets
GET http://localhost:5279/api/Ticket?status=0
Authorization: Bearer <token>

### Assign ticket #5 to agent (Agent role required)
PATCH http://localhost:5279/api/Ticket/5/assign?agentId=2
Authorization: Bearer <token>

### Change status of ticket #5 to Done
PATCH http://localhost:5279/api/Ticket/5/status
Content-Type: application/json
Authorization: Bearer <token>

{ "status": 2 }
```
