CREATE TABLE public."Accounts" (
	"Id" uuid NOT NULL,
	"UserId" uuid NOT NULL,
	"CurrencyId" uuid NOT NULL,
	"Title" text NULL,
	"IsDefault" bool NOT NULL,
	"IsArchived" bool NOT NULL,
	CONSTRAINT "PK_Accounts" PRIMARY KEY ("Id")
);
CREATE INDEX "IX_Accounts_CurrencyId" ON public."Accounts" USING btree ("CurrencyId");
CREATE INDEX "IX_Accounts_UserId" ON public."Accounts" USING btree ("UserId");


ALTER TABLE public."Accounts" ADD CONSTRAINT "FK_Accounts_Currencies_CurrencyId" FOREIGN KEY ("CurrencyId") REFERENCES public."Currencies"("Id") ON DELETE CASCADE;
ALTER TABLE public."Accounts" ADD CONSTRAINT "FK_Accounts_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;