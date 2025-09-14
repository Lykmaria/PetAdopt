# PetAdopt 🐾

Simple **ASP.NET Core Razor Pages** app (.NET 9) for pet adoption.

## Features
- Browse pets and shelters
- Apply to adopt a pet
- Leave reviews for shelters
- User dashboard (my applications)
- Admin dashboard (approve/reject applications)

---

## Prerequisites
- Windows 11
- .NET 9 SDK (`dotnet --info`)
- Visual Studio 2022 (ASP.NET workload)
- SQL Server LocalDB (or SQL Express)

---

## Getting Started

```powershell
# Go to the Web project
cd src\PetAdopt.Web

# Install EF tools (once)
dotnet tool install --global dotnet-ef

# Create database
dotnet ef database update --project ..\PetAdopt.Data --startup-project .\

# Run the app
dotnet run
```

Browse to the URL shown (e.g., https://localhost:7043/).

---

## Default Admin
- Email: `admin@petadopt.local`
- Password: `Admin!234`

(Admin user and role are created at startup.)

---

## Solution Structure
```
src/
  PetAdopt.Domain/         # Entities
  PetAdopt.Data/           # DbContext, EF migrations, repos
  PetAdopt.Application/    # Services, interfaces
  PetAdopt.Web/            # Razor Pages UI + Identity

tests/
  PetAdopt.Tests/          # xUnit tests (placeholder)
```

---

## Common Commands
```powershell
# Add a new migration
dotnet ef migrations add <Name> --project ..\PetAdopt.Data --startup-project .\

# Update DB
dotnet ef database update --project ..\PetAdopt.Data --startup-project .\

# Drop DB (careful!)
dotnet ef database drop --force --project ..\PetAdopt.Data --startup-project .\
```

---

## Troubleshooting
- **Login page error about _LoginPartial** → ensure `_LoginPartial.cshtml` exists under `Pages/Shared/`.
- **Invalid object name (e.g. Pets)** → run `dotnet ef database update`.
- **Migration mismatch** → delete `Migrations/` folder, add fresh `InitialCreate`, then run update.

---

## License
Educational use (Coding Factory).

