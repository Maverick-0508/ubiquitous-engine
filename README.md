# Ubiquitous Engine - Build Instructions

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)

## Build

To build the solution:

```bash
dotnet build
```

## Run

To run the API:

```bash
dotnet run --project src/UbiquitousEngine.Api
```

The API will be available at `https://localhost:5001` and `http://localhost:5000`.
Swagger documentation will be available at `/swagger/index.html`.

## Test

To run the tests:

```bash
dotnet test
```

## Project Structure

- `src/UbiquitousEngine.Api` - Main ASP.NET Core Web API
- `tests/UbiquitousEngine.Tests` - Unit tests using xUnit