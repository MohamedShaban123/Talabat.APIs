
# Talabat APIs

## Overview

The **Talabat APIs** is a RESTful backend service built with **ASP.NET Core** and **.NET 6**. It provides a scalable and maintainable solution for managing orders, products, baskets, payments, and user authentication.

The project follows modern software development best practices including separation of concerns, design patterns like Repository and Unit of Work, and tools like Swagger for API documentation.

---

## Features

- **Authentication & Authorization**: Secure user login/registration using **JWT** and **ASP.NET Identity**.
- **Role-Based Access Control**: Manage and restrict access based on user roles.
- **Order Management**: Endpoints to create, retrieve, and manage customer orders with support for delivery methods and payment intents.
- **Basket Management**: Manage user baskets with **Redis** for distributed caching and performance.
- **Product Management**: Browse, search, filter, and paginate products, brands, and categories.
- **Payment Integration**: Create and manage **payment intents** seamlessly.
- **Database Management**: Uses **Entity Framework Core** with migrations and data seeding.
- **Custom Middleware**: Centralized exception handling and custom error responses.
- **API Documentation**: Integrated **Swagger/OpenAPI** for interactive API exploration.

---

## Prerequisites

Make sure you have the following installed:

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Redis](https://redis.io/)
- [Postman](https://www.postman.com/) *(optional, for testing)*

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/MohamedShaban123/Talabat.APIs
cd talabat-apis
```

### 2. Configure the Database

Update `appsettings.json` with your database connection strings:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.; Database=TalabatDB; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=True;",
  "IdentityConnection": "Server=.; Database=TalabatIdentityDB; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=True;",
  "Redis": "localhost:6379"
}
```

### 3. Apply Migrations & Seed Data

Run the following commands to set up the databases:

```bash
dotnet ef database update
```

### 4. Run the API

```bash
dotnet run
```

---

## API Endpoints

### 🧑‍💼 AccountController

- `POST /api/account/register` — Register a new user.
- `POST /api/account/login` — Login and receive a JWT token.
- `GET /api/account` — Get the current logged-in user.

### 🛒 BasketController

- `GET /api/basket/{id}` — Retrieve a basket.
- `POST /api/basket` — Create or update a basket.
- `DELETE /api/basket/{id}` — Delete a basket.

### 📦 OrdersController

- `POST /api/orders` — Create a new order.
- `GET /api/orders` — Retrieve all orders for the current user.
- `GET /api/orders/{id}` — Get an order by ID.

### 💳 PaymentsController

- `POST /api/payments/{basketId}` — Create or update a payment intent.
- `POST /api/payments/webhook` — Handle webhook events from the payment provider.

### 🛍️ ProductsController

- `GET /api/products` — Get all products (supports filtering, sorting, pagination).
- `GET /api/products/{id}` — Get product details by ID.
- `GET /api/products/brands` — Get all product brands.
- `GET /api/products/categories` — Get all product categories.

### ⚠️ ErrorController

- `GET /errors/{code}` — Simulate an error response for testing.

### 📚 BaseApiController

- Acts as a base class for other controllers, adding shared functionality and consistent routing (`[api/[controller]]`).

---

## Running Tests

Run unit and integration tests with:

```bash
dotnet test
```

---

## Technologies Used

- **Framework**: ASP.NET Core, .NET 6
- **Database**: SQL Server, Entity Framework Core
- **Caching**: Redis (via `StackExchange.Redis`)
- **Authentication**: ASP.NET Identity, JWT Tokens
- **Design Patterns**: Repository, Unit of Work, Specification Pattern
- **API Docs**: Swagger / OpenAPI
- **Logging**: Built-in `ILogger` logging

---

## License

This project is open source and available under the [MIT License](LICENSE).

---

## Contact

For any inquiries or contributions, please reach out at [mohammedshaban1458@gmail.com]
