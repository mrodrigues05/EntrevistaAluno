CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Student" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Student" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Email" TEXT NOT NULL
);

CREATE TABLE "Premium" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Premium" PRIMARY KEY AUTOINCREMENT,
    "Título" TEXT NOT NULL,
    "StartDate" TEXT NOT NULL,
    "EndDate" TEXT NOT NULL,
    "StudentId" INTEGER NOT NULL,
    CONSTRAINT "FK_Premium_Student_StudentId" FOREIGN KEY ("StudentId") REFERENCES "Student" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Premium_StudentId" ON "Premium" ("StudentId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240225234815_Initial', '7.0.16');

COMMIT;

