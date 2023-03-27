<h1 align="center">
  <img src="./acmelogo.png">
</h1>

<h4 align="center">
A simple flight booking system for .NET
</h4>

<p align="center">
 <a href="https://github.com/lakindu95/p1-v1-be-dotnet-assignment"><img src="https://img.shields.io/badge/ACME-Flights-informational.svg"></a>
 <a><img src="https://img.shields.io/badge/.NET-6-brightgreen.svg"></a>
<img alt="GitHub forks" src="https://img.shields.io/github/forks/lakindu95/p1-v1-be-dotnet-assignment?style=social">
 <a href="https://saythanks.io/to/lakindu95"><img src="https://img.shields.io/badge/Say%20Thanks-!-1EAEDB.svg"></a>
</p>

Welcome to ACME Flights flights booking engine. This application is written in .NET 6. Feel free to contribute and maintain the codebase. 

## 🚀 Install

## Prerequisities

- Docker Desktop
- .NET 6 SDK
- Visual Studio

# 📚 Overview

_ACMEFlights_ spins up the following APIs currently. Also you can check the swagger documentation to try out the APIs once spins up. 

* **[Airports](https://github.com/lakindu95/p1-v1-be-dotnet-assignment#airports)**

* **[Flights](https://github.com/lakindu95/p1-v1-be-dotnet-assignment#flights)**

* **[Order](https://github.com/lakindu95/p1-v1-be-dotnet-assignment#order)**

**Note** - You can use the swagger documentation once the API is spin up to explore the latest changes to the API.
```
https://_yoururl_/swagger/index.html
```

## 📝 Getting started

- Start the Postgres database with `docker-compose up -d` (the application is already configured properly, but if you want to connect to the db directly you can see the credentials in the `docker-compose.yml` file)
- You can now run the API project and everything should work. Upon start the application will run the migrations and seed data to the database.

### Run from CLI 

To run it from CLI, you should install dotnet CLI to your local machine
```
dotnet run --project ./API/API.csproj

To Exit
Ctrl + C
```

### Run from Visual Studio

```
1. Load the solution file sln
2. Build the project in Visual studio
3. Make sure the database is running on the docker as above step
4. Select the **API** as the start up project
5. Run the project (first time when running the project, a migration script will run and create the data needed)

Enjoy!
```

## 📝 APIs 

### Airports

<hr>

- Register Airports
    - The API will register the airports and make sure that the code has atleast 3 characters to register.

**Request**
```
POST /Airports
{
  "code": "string",
  "name": "string"
}
```

**Response**
```
POST /Airports
{
  "id": "Guid"
  "code": "string",
  "name": "string"
}
```

### Flights

<hr>

- Search for flights by destination
    - The API will search the available flights based on the destination and return the details.

**Request**
```
GET /Flights/Search?destination="abc"
```

**Response**
```
[
  {
    "departureAirportCode": "Guid",
    "arrivalAirportCode": "Guid",
    "departure": "2021-08-01T07:55:00+05:30",
    "arrival": "2021-08-01T10:55:00+05:30",
    "priceFrom": 14233
  },
  {
    "departureAirportCode": "Guid",
    "arrivalAirportCode": "Guid",
    "departure": "2022-11-02T03:27:00+05:30",
    "arrival": "2022-11-02T15:27:00+05:30",
    "priceFrom": 10371
  } ...
]
```

### Order

<hr>

- Create a new order
    - Customers can book the flights by creating the order. Flight ID and Flight Rate ID is required. Number of seats must be more than 0.

**Request**
```
POST /Order
{
  "name": "string",
  "email": "string",
  "flightId": "string",
  "flightRateId": "string",
  "noOfSeats": int
}
```

**Response**
```
{
  "orderId": "Guid",
  "name": "test",
  "email": "test@gmail.com",
  "flightId": "Guid",
  "flightRateId": "Guid",
  "noOfSeats": 1,
  "price": 14254,
  "status": "Pending",
  "createdDate": "2023-03-27T12:27:41.3171033+00:00",
  "updatedDate": "2023-03-27T12:27:41.3171141+00:00"
}
```

<hr>

- Confirm the created order Id
    - Once the Order is placed, it will stay as a pending order until this API is run to confirm the order. 

**Request**
```
PUT /Confirm
{
  "id": "string"
}
```

**Response**
```
{
  "orderId": "Guid",
  "name": "test",
  "email": "test@gmail.com",
  "flightId": "Guid",
  "flightRateId": "Guid",
  "noOfSeats": 1,
  "price": 14254,
  "status": "Confirmed",
  "createdDate": "2023-03-27T12:27:41.3171033+00:00",
  "updatedDate": "2023-03-27T12:29:50.3171141+00:00"
}
```

## 📝 To Do List
- Dockerizing the entire project with HTTPS
- Adding unit tests and test coverage
- Pagination for GET requests like lists with Meta tags
- Authentication flow