﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "user" (
    id serial4 GENERATED BY DEFAULT AS IDENTITY,
    email varchar(100) NOT NULL,
    password varchar(200) NOT NULL,
    full_name varchar(200) NOT NULL,
    phone varchar(20) NOT NULL,
    avatar varchar(200) NOT NULL,
    date_of_birth timestamp NOT NULL,
    created_user varchar(100) NOT NULL,
    created_date timestamp NOT NULL,
    updated_user varchar(100),
    last_updated timestamp,
    is_deleted boolean NOT NULL,
    CONSTRAINT "PK_user" PRIMARY KEY (id)
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231216041523_Initial', '8.0.0');

COMMIT;
