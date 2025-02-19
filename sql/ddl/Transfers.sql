CREATE TABLE public."Transfers" (
	"Id" uuid NOT NULL,
	"UserId" uuid NOT NULL,
	"FromAccountId" uuid NOT NULL,
	"ToAccountId" uuid NOT NULL,
	"Date" date NOT NULL,
	"FromAmount" numeric NOT NULL,
	"ToAmount" numeric NOT NULL,
	"Description" text NULL,
	CONSTRAINT "PK_Transfers" PRIMARY KEY ("Id")
);
CREATE INDEX "IX_Transfers_FromAccountId" ON public."Transfers" USING btree ("FromAccountId");
CREATE INDEX "IX_Transfers_ToAccountId" ON public."Transfers" USING btree ("ToAccountId");
CREATE INDEX "IX_Transfers_UserId" ON public."Transfers" USING btree ("UserId");


ALTER TABLE public."Transfers" ADD CONSTRAINT "FK_Transfers_Accounts_FromAccountId" FOREIGN KEY ("FromAccountId") REFERENCES public."Accounts"("Id") ON DELETE CASCADE;
ALTER TABLE public."Transfers" ADD CONSTRAINT "FK_Transfers_Accounts_ToAccountId" FOREIGN KEY ("ToAccountId") REFERENCES public."Accounts"("Id") ON DELETE CASCADE;
ALTER TABLE public."Transfers" ADD CONSTRAINT "FK_Transfers_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;