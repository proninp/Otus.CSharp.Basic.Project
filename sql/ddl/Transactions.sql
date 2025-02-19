CREATE TABLE public."Transactions" (
	"Id" uuid NOT NULL,
	"UserId" uuid NOT NULL,
	"AccountId" uuid NOT NULL,
	"CategoryId" uuid NULL,
	"Date" date NOT NULL,
	"Amount" numeric NOT NULL,
	"Description" text NULL,
	CONSTRAINT "PK_Transactions" PRIMARY KEY ("Id")
);
CREATE INDEX "IX_Transactions_AccountId" ON public."Transactions" USING btree ("AccountId");
CREATE INDEX "IX_Transactions_CategoryId" ON public."Transactions" USING btree ("CategoryId");
CREATE INDEX "IX_Transactions_UserId" ON public."Transactions" USING btree ("UserId");


ALTER TABLE public."Transactions" ADD CONSTRAINT "FK_Transactions_Accounts_AccountId" FOREIGN KEY ("AccountId") REFERENCES public."Accounts"("Id") ON DELETE CASCADE;
ALTER TABLE public."Transactions" ADD CONSTRAINT "FK_Transactions_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES public."Categories"("Id");
ALTER TABLE public."Transactions" ADD CONSTRAINT "FK_Transactions_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;