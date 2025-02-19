CREATE TABLE public."Currencies" (
	"Id" uuid NOT NULL,
	"Title" text NOT NULL,
	"CurrencyCode" text NOT NULL,
	"CurrencySign" text NOT NULL,
	"Emoji" text NULL,
	CONSTRAINT "PK_Currencies" PRIMARY KEY ("Id")
);