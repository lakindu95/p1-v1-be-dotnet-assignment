CREATE TABLE public."Orders" (
	"Id" uuid NOT NULL,
	"Name" text NOT NULL,
	"Email" text NOT NULL,
	"FlightId" uuid NOT NULL,
	"FlightRateId" uuid NOT NULL,
	"NoOfSeats" int NOT null,
	"Price" numeric NOT NULL,
	"Currency" int4 NOT NULL,
	"Status" int4 NOT NULL,
	CONSTRAINT "PK_Orders" PRIMARY KEY ("Id"),
	CONSTRAINT "FK_Orders_FlightRates_FlightRatesId" FOREIGN KEY ("FlightRateId") REFERENCES public."FlightRates"("Id") ON DELETE cascade,
	CONSTRAINT "FK_Orders_Flights_FlightId" FOREIGN KEY ("FlightId") REFERENCES public."Flights"("Id") ON DELETE CASCADE
);
