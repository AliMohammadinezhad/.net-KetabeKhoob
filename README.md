# KetabeKhoob

# Shop API

**Shop API** is a .NET-based backend for an e-commerce multivendor platform and database seeding.

## Getting Started

### Admin User Info

* **Email:** [admin@admin.com](mailto:admin@admin.com)
* **Phone Number:** 09121234567 
* **Password:** 123456

### Running Locally

1. Clone the repository:

```bash
git clone <repo-url>
cd src/Shop/EndPoints/Shop.Api
```

2. Build and run the API:

```bash
dotnet build
dotnet run
```

3. The API will automatically apply migrations and seed the database.

### Docker Setup

1. Make sure `docker-compose.yml` is present at `src/Shop/EndPoints/Shop.Api/docker-compose.yml`.
2. Run:

```bash
docker-compose -f src/Shop/EndPoints/Shop.Api/docker-compose.yml up --build
```

3. The API will connect to SQL Server and seed the default admin user and connect to redis Database for caching.

### Notes

* The database seeding ensures an initial admin user and role exist.
* No code changes needed to access the admin account; it is ready after running the API or Docker setup.
