# 🍽️ LunchSync Backend

A modern ASP.NET Core backend application for collaborative group lunch decision-making. LunchSync helps groups make quick, fair decisions on where to eat through interactive voting sessions.

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Technology Stack](#technology-stack)
- [Features](#features)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Development](#development)
- [Contributing](#contributing)

## 🎯 Overview

LunchSync Backend is a REST API that powers the LunchSync application, enabling groups to:
- Create collaborative lunch decision sessions
- Invite group members via PIN-based joining
- Vote on restaurant and dish preferences
- Get intelligent results based on collective voting
- Support both authenticated users and anonymous guests

The application uses a clean architecture pattern with separated concerns across Core, Infrastructure, and API layers.

## 🏗️ Architecture

The project follows **Clean Architecture** principles with clear separation of concerns:

```
LunchSync.Api/          → API layer (Controllers, Request/Response models)
LunchSync.Core/         → Business logic and domain models
LunchSync.Infrastructure → Database access and external services
```

**Key Architectural Features:**
- Domain-Driven Design (DDD) principles
- JWT-based authentication with AWS Cognito integration
- Entity Framework Core for data access
- PostgreSQL for data persistence
- CORS support for frontend integration
- Rate limiting for public endpoints
- Comprehensive error handling and validation

## 🛠️ Technology Stack

| Component | Technology |
|-----------|------------|
| **Language** | C# (.NET 9.0) |
| **Web Framework** | ASP.NET Core |
| **Database** | PostgreSQL |
| **ORM** | Entity Framework Core 9.0 |
| **Authentication** | JWT + AWS Cognito |
| **API Documentation** | Swagger/OpenAPI |
| **Container** | Docker |
| **Code Quality** | .NET Format, EditorConfig |

## ✨ Features

### Authentication & Authorization
- Email-based registration with OTP verification
- JWT token-based authentication
- AWS Cognito integration for secure user management
- Role-based authorization policies
- Support for authenticated users and guests

### Session Management
- Create voting sessions with unique PIN codes
- Host-based session control (create, start, cancel)
- Anonymous guest participation
- Real-time session status tracking

### Restaurant & Dish Management
- Browse restaurant collections
- View dishes by category or search
- Detailed restaurant information with associated dishes
- Pre-configured dish database

### Voting & Results
- Democratic voting system
- Real-time results tracking
- Multiple result resolution strategies (Boom/Pick)
- Host-controlled result finalization

### API Features
- Comprehensive OpenAPI/Swagger documentation
- Rate limiting on public authentication endpoints (5 requests/minute)
- CORS support for frontend at `lunchsync.space`
- Structured error responses
- Full async/await support

## 📁 Project Structure

```
LunchSync/
├── LunchSync.Api/                    # API Layer
│   ├── Controllers/                  # API endpoints
│   │   ├── AuthController.cs
│   │   ├── SessionsController.cs
│   │   ├── VotingController.cs
│   │   ├── ResultsController.cs
│   │   ├── RestaurantsController.cs
│   │   ├── DishesController.cs
│   │   └── CollectionsController.cs
│   └── Program.cs                    # Application configuration
├── LunchSync.Core/                   # Domain & Business Logic
│   ├── Modules/
│   │   ├── Auth/                     # Authentication domain
│   │   ├── Sessions/                 # Session management domain
│   │   └── RestaurantsAndDishes/     # Restaurant/Dish domain
│   └── Common/                       # Shared utilities
├── LunchSync.Infrastructure/         # Data Access & External Services
│   ├── Database/                     # EF Core DbContext
│   └── Services/                     # External integrations
└── LunchSync.sln                     # Solution file
```

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- PostgreSQL 12+
- Docker (optional)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/markByOverflow/lunch-sync-backend.git
   cd lunch-sync-backend
   ```

2. **Navigate to project directory:**
   ```bash
   cd LunchSync
   ```

3. **Restore dependencies:**
   ```bash
   dotnet restore LunchSync.sln
   ```

4. **Configure database connection:**
   Create `appsettings.json` in `LunchSync.Api` with your PostgreSQL connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=lunchsync;Username=postgres;Password=your_password"
     }
   }
   ```

5. **Run database migrations:**
   ```bash
   dotnet ef database update --project LunchSync.Infrastructure --startup-project LunchSync.Api
   ```

6. **Build the solution:**
   ```bash
   dotnet build LunchSync.sln --configuration Release
   ```

7. **Run the API:**
   ```bash
   dotnet run --project LunchSync.Api/LunchSync.Api.csproj
   ```

The API will be available at `https://localhost:5001`. Swagger documentation at `http://localhost:5000`.

### Docker Setup

```bash
docker build -t lunchsync-backend .
docker run -p 5000:5000 -p 5001:5001 lunchsync-backend
```

## 🔌 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/verify-otp` - Verify OTP code
- `POST /api/auth/resend-otp` - Resend OTP
- `POST /api/auth/login` - Login user
- `GET /api/auth/me` - Get current user info (requires auth)

### Sessions
- `POST /api/sessions` - Create new session (host only)
- `POST /api/sessions/{pin}/join` - Join session by PIN
- `POST /api/sessions/{pin}/start` - Start voting (host only)
- `POST /api/sessions/{pin}/cancel` - Cancel session (host only)
- `GET /api/sessions/{pin}/status` - Get session status

### Results
- `GET /api/sessions/{pin}/results` - Get voting results
- `POST /api/sessions/{pin}/boom` - Boom result (host only)
- `POST /api/sessions/{pin}/pick` - Pick result (host only)

### Restaurants & Dishes
- `GET /api/restaurants/{id}` - Get restaurant details
- `GET /api/dishes` - List dishes (with search/category filters)
- `GET /api/dishes/{id}` - Get dish details
- `GET /api/collections` - List all collections
- `GET /api/collections/{id}` - Get collection details

## 💻 Development

### Code Quality
The project maintains high code quality standards using:

```bash
# Format code
dotnet format LunchSync.sln

# Verify formatting
dotnet format LunchSync.sln --verify-no-changes
```

### Running Tests
```bash
dotnet test LunchSync.sln --configuration Release
```

### Build & CI/CD
The repository includes GitHub Actions CI pipeline (`.github/workflows/ci.yml`) that:
- Detects changes in the LunchSync directory
- Runs .NET build and format checks
- Executes all unit tests
- Ensures clean code standards

## 📝 Contributing

Contributions are welcome! Please follow these guidelines:

1. **Fork the repository**
2. **Create a feature branch:** `git checkout -b feature/your-feature`
3. **Follow code style:** Use `dotnet format` before committing
4. **Write tests** for new functionality
5. **Ensure all tests pass:** `dotnet test`
6. **Commit with clear messages**
7. **Push and create a Pull Request**

## 📄 License

This project is open source and available under the MIT License.

## 🤝 Support

For issues or questions:
- Open an issue on [GitHub Issues](https://github.com/markByOverflow/lunch-sync-backend/issues)
- Check existing documentation in the repository
- Review API docs at `/swagger` endpoint

---

**Frontend Repository:** [LunchSync Frontend](https://github.com/markByOverflow/lunch-sync-frontend)  
**Live URL:** https://lunchsync.space
