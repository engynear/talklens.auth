-- +goose Up
-- +goose StatementBegin

CREATE TABLE IF NOT EXISTS "Users" (
    "Id" text NOT NULL,
    "UserName" text NOT NULL,
    "PasswordHash" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "LastLoginAt" timestamp with time zone,
    "IsActive" boolean NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX IF NOT EXISTS "UserNameIndex" ON "Users" ("UserName");

-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin

DROP TABLE IF EXISTS "Users";

-- +goose StatementEnd 