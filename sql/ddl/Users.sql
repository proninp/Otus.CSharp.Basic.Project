CREATE TABLE public."Users" (
	"Id" uuid NOT NULL,
	"TelegramId" int8 NOT NULL,
	"Username" text NULL,
	"Firstname" text NULL,
	"Lastname" text NULL,
	CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);