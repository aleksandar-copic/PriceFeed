# PriceFeed API

This is a .NET 8-based API service for providing live financial instrument prices using Binance WebSocket streams.  
Built using **Clean Architecture** principles with CQRS, MediatR, and a modular structure.

---

## Features

- REST API for listing instruments and getting their latest price
- WebSocket endpoint that pushes real-time price updates to subscribed clients
- Background service subscribing to Binance WebSocket once, and broadcasting prices to all users
- Global exception handling and structured logging

---

## Tech Stack

- .NET 8
- MediatR
- CQRS + Clean Architecture
- ASP.NET Core Web API
- WebSockets
- Hosted BackgroundService
- Custom Middleware

---

## 💻 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/)
- [C# Extension for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
---

## Setup Instructions

1. **Clone the repository:**

```bash
git clone https://github.com/aleksandar-copic/PriceFeed.git
cd PriceFeed/PriceFeed/PriceFeed.API
```

2. **Restore NuGet Packages**
```bash
dotnet restore
```

3. **Run the Project**
```bash
dotnet run
```

4. **Open Swagger**
Open Swagger at http://localhost:5213/swagger/index.html
---
