CREATE TABLE public."Categories" (
	"Id" uuid NOT NULL,
	"UserId" uuid NOT NULL,
	"Title" text NULL,
	"ParentCategoryId" uuid NULL,
	"Emoji" text NULL,
	"CategoryType" int4 DEFAULT 0 NOT NULL,
	CONSTRAINT "PK_Categories" PRIMARY KEY ("Id")
);
CREATE INDEX "IX_Categories_ParentCategoryId" ON public."Categories" USING btree ("ParentCategoryId");
CREATE INDEX "IX_Categories_UserId" ON public."Categories" USING btree ("UserId");


ALTER TABLE public."Categories" ADD CONSTRAINT "FK_Categories_Categories_ParentCategoryId" FOREIGN KEY ("ParentCategoryId") REFERENCES public."Categories"("Id") ON DELETE RESTRICT;
ALTER TABLE public."Categories" ADD CONSTRAINT "FK_Categories_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;