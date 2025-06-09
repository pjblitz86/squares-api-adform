# 🟦 Squares API

The **Squares API** allows users to manage a list of 2D points and identify all possible squares that can be formed from them. Designed with enterprise usability in mind, it supports importing points, adding or deleting individual points, and retrieving all squares formed by the stored dataset.

---

## 🚀 Features

- ✅ Add, import, delete 2D points
- ✅ Identify all perfect squares from a list of points
- ✅ RESTful API with Swagger documentation
- ✅ PostgreSQL database with EF Core, doesn't allow duplicate points
- ✅ Manual request timing log (SLI logging)
- ✅ Unit tested with xUnit

---

## 🛠️ Tech Stack

- [.NET 8](https://dotnet.microsoft.com/en-us/)
- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/)
- [Entity Framework Core (EF Core)](https://learn.microsoft.com/en-us/ef/core/)
- [PostgreSQL](https://www.postgresql.org/)
- [Swagger / Swashbuckle](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle)
- [xUnit](https://xunit.net/) for unit testing

---

## 📦 Prerequisites

- [Visual studio community edition](https://visualstudio.microsoft.com/vs/community/)
- [PostgreSQL 15+](https://www.postgresql.org/download/)
- (Optional) [pgAdmin](https://www.pgadmin.org/) for database GUI

---

## ⚙️ Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/squares-api-adform.git
cd squares-api-adform/squares-api-adform
```

### 2. Create PostgreSQL Database

Open `psql` or pgAdmin and create a database:

```sql
CREATE DATABASE squares_db;
```

### 3. Configure Connection String

Edit `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=squares_db;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

Ensure this file is ignored by Git via `.gitignore`.

---

### 4. Apply Database Migrations

```bash
dotnet ef database update
```

> If EF CLI isn't installed, run:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

---

### 5. Run the API

```bash
dotnet run
```

Or press `F5` in Visual Studio.

---

## 📚 API Endpoints

Once running, open Swagger UI:

```
https://localhost:7295/swagger
```

### 🔹 Points

- `GET /api/points` — List all stored points
- `POST /api/points` — Add a single point
- `POST /api/points/import` — Import a list of points
- `DELETE /api/points?x=1&y=2` — Delete a point by coordinates

### 🔹 Squares

- `GET /api/squares` — Returns all valid squares formed by stored points

---

## ✅ Example Square Input

The following points form a square:

```json
[
  { "x": -1, "y": -1 },
  { "x": -1, "y": 1 },
  { "x": 1, "y": -1 },
  { "x": 1, "y": 1 }
]
```

Test it using `POST /api/points/import`, then call `GET /api/squares`.

---

## 🔍 Testing

Run the unit tests with:

```bash
dotnet test
```

Covered:
- SquareService unit tests (xUnit)
- Logic validation for multiple distinct squares
- Edge cases (non-square inputs)
- PointsController tests using in-memory EF Core

---

## 🐳 Docker Support (Optional - Not Yet Added)

Planned: Dockerfile and docker-compose support for API and PostgreSQL

---

## 🧠 Design & Scalability Considerations

- Started with InMemory DB for fast prototyping
- Switched to PostgreSQL for persistence
- Square detection uses diagonal math + hashing to avoid duplicates
- Layered architecture: separation of concerns via controllers/services
- Tested against 1000+ points — performance remains under 5s per request
- Scales horizontally: stateless controllers and async calls
- SLI logging via `ILogger` tracks duration of square detection

---

## 📞 Feedback

This project is designed for backend interview-style evaluation. Feedback and suggestions are welcome!
