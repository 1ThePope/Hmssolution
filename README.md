# Hostel Management System (Hmssolution)

A RESTful Web API for managing off-campus student hostels, built with ASP.NET Core. Landlords list hostels and rooms, students browse and book rooms, and admins oversee the whole system, with role-based access enforced through JWT authentication.

## Features

- **Authentication and authorization:** registration and login with ASP.NET Core Identity and JWT bearer tokens
- **Three roles:** `Admin`, `LandLord` and `Student`, each with its own permissions (roles and a default admin account are seeded on startup)
- **Hostel management:** landlords upload hostels with photo, supporting document, price, location and address; admins can update or remove them
- **Room management:** add rooms to a hostel with room type, photo and availability status
- **Room booking:** students book rooms; the API rejects a booking if the room is already booked and keeps each hostel's booked/available room count in sync
- **Verification status:** hostels and landlords carry a verification status for admin review
- **Profiles:** admins can view student and landlord profiles; landlords can list their own hostels
- **Swagger UI** with JWT authorization built in, served at the application root in development

## Tech Stack

| Area | Technology |
|------|------------|
| Language / Framework | C#, ASP.NET Core Web API (.NET 9) |
| Database | SQL Server |
| ORM | Entity Framework Core 9 (code-first migrations) |
| Auth | ASP.NET Core Identity, JWT Bearer |
| Mapping | AutoMapper |
| Docs / Testing | Swagger (Swashbuckle), Postman |

## Project Structure

```
Hmssolution/
├── Controllers/        # Auth, Hostel, Room, Booking, Student, LandLord
├── DBContext/          # EF Core DbContext
├── HmsModel/           # Entities: Hostel, Room, Student, LandLord, HostelRoomBooking, ...
├── HmsDTOs/            # Create/Read DTOs per entity
├── Mapping/            # AutoMapper profiles
├── Services/           # JwtService (token generation)
├── Migrations/         # EF Core migrations
├── SeedData.cs         # Seeds roles and the default admin
└── Program.cs          # App configuration and pipeline
```

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server (Express or LocalDB is fine)
- Visual Studio 2022 or later

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/1ThePope/Hmssolution.git
   cd Hmssolution
   ```

2. **Configure the app.** Set your SQL Server connection string in `appsettings.json`, then store secrets with [user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) rather than committing them:
   ```bash
   dotnet user-secrets set "JWT:Secret" "<a-long-random-string-of-32+-characters>"
   dotnet user-secrets set "Admin:Email" "<admin-email>"
   dotnet user-secrets set "Admin:Password" "<admin-password>"
   ```

3. **Create the database**
   ```bash
   dotnet ef database update
   ```

4. **Run the project** with `F5` in Visual Studio or `dotnet run`. Swagger UI opens at the root URL (for example `http://localhost:5103/`).

### Authenticating in Swagger

1. Register via `POST /api/Auth/register`, or log in as the seeded admin.
2. Call `POST /api/Auth/login` and copy the token.
3. Click **Authorize** in Swagger and enter `Bearer <token>`.

## API Endpoints

| Method | Endpoint | Description | Access |
|--------|----------|-------------|--------|
| POST | `/api/Auth/register` | Register a Student or LandLord account | Public |
| POST | `/api/Auth/login` | Log in and receive a JWT | Public |
| GET | `/api/Hostel/Profile` | List all hostels | Admin, Student |
| GET | `/api/Hostel/{id}` | Get a hostel by ID | Admin, LandLord, Student |
| POST | `/api/Hostel/Upload` | Upload a new hostel | LandLord, Admin |
| PUT | `/api/Hostel/{id}` | Update a hostel | LandLord, Admin |
| DELETE | `/api/Hostel/{id}` | Delete a hostel | Admin |
| GET | `/api/Room/{id}` | Get a room by ID | Public |
| POST | `/api/Room` | Add a room to a hostel | LandLord, Admin |
| DELETE | `/api/Room/{id}` | Delete a room | LandLord, Admin |
| GET | `/api/Booking` | List all bookings | Public |
| GET | `/api/Booking/{id}` | Get a booking by ID | Public |
| POST | `/api/Booking` | Book a room | Public |
| DELETE | `/api/Booking/{id}` | Cancel a booking | Public |
| GET | `/api/Student/{id}` | Get a student by ID | Student, Admin |
| GET | `/api/Student/profile` | List all students | Admin |
| GET | `/api/LandLord/Profile` | List all landlords | Admin |
| GET | `/api/LandLord/{id}` | Get a landlord by ID | Admin, LandLord |
| GET | `/api/LandLord/myhostels` | List the logged-in landlord's hostels | LandLord |
| DELETE | `/api/LandLord/{id}` | Delete a landlord | Admin |

## Data Model

`LandLord` 1 — * `Hostel` 1 — * `Room`, and `Student` 1 — * `HostelRoomBooking` * — 1 `Room`.

## Future Improvements

- Email OTP verification at registration
- Role-based protection on the Booking endpoints
- Unit and integration tests
- A frontend client (React or Blazor)

## Author

**Winjobi Samuel Kayode**, Computer Science, Kwara State University
GitHub: [@1ThePope](https://github.com/1ThePope)
