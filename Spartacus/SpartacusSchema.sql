BEGIN TRANSACTION;
DROP TABLE IF EXISTS "MilestoneRewards";
CREATE TABLE IF NOT EXISTS "MilestoneRewards" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"CivId"	INTEGER,
	"TechId"	TEXT,
	"DisplayId"	INTEGER,
	"RolloverTextId"	INTEGER,
	"LevelRequirement"	INTEGER,
	"Icon"	TEXT
);
DROP TABLE IF EXISTS "MilestoneTiers";
CREATE TABLE IF NOT EXISTS "MilestoneTiers" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"CivId"	INTEGER,
	"TechName"	TEXT
);
DROP TABLE IF EXISTS "ProtoAgeUnitType";
CREATE TABLE IF NOT EXISTS "ProtoAgeUnitType" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"Name"	TEXT NOT NULL,
	"Type"	TEXT NOT NULL
);
DROP TABLE IF EXISTS "ProtoAgeUnitFlag";
CREATE TABLE IF NOT EXISTS "ProtoAgeUnitFlag" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"Name"	TEXT NOT NULL,
	"Flag"	INTEGER NOT NULL
);
DROP TABLE IF EXISTS "ProtoAge";
CREATE TABLE IF NOT EXISTS "ProtoAge" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"Name"	TEXT,
	"DisplayNameId"	INTEGER,
	"EditorNameId"	INTEGER,
	"PopulationCount"	INTEGER,
	"AnimFile"	TEXT,
	"Icon"	TEXT,
	"PortraitIcon"	TEXT,
	"RolloverTextId"	INTEGER,
	"ShortRolloverTextId"	INTEGER,
	"InitialHitpoints"	INTEGER,
	"MaxHitpoints"	INTEGER,
	"LOS"	INTEGER,
	"AllowedAge"	INTEGER
);
DROP TABLE IF EXISTS "ProtoAgeResourceCost";
CREATE TABLE IF NOT EXISTS "ProtoAgeResourceCost" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"Name"	TEXT,
	"Food"	INTEGER,
	"Wood"	INTEGER,
	"Gold"	INTEGER,
	"Stone"	INTEGER
);
DROP TABLE IF EXISTS "Advisors";
CREATE TABLE IF NOT EXISTS "Advisors" (
	"Name"	TEXT NOT NULL,
	"CivId"	INTEGER,
	"Age"	INTEGER,
	"Icon"	TEXT,
	"Rarirty"	INTEGER,
	"RollverTextId"	INTEGER,
	"DisplayDescriptionId"	INTEGER,
	"DisplayNameId"	INTEGER,
	"MinLevel"	INTEGER,
	"ItemLevel"	INTEGER,
	"TechId"	TEXT,
	PRIMARY KEY("Name")
);
DROP TABLE IF EXISTS "Civilizations";
CREATE TABLE IF NOT EXISTS "Civilizations" (
	"CivilizationId"	INTEGER NOT NULL,
	"DisplayNameId"	INTEGER NOT NULL,
	"RolloverNameId"	INTEGER NOT NULL,
	"ShieldTexture"	TEXT NOT NULL,
	"ShieldGreyTexture"	TEXT NOT NULL,
	"Age0"	TEXT NOT NULL,
	"Age1"	TEXT NOT NULL,
	"Age2"	TEXT NOT NULL,
	"Age3"	TEXT NOT NULL,
	"StoreHouseTechId"	INTEGER,
	PRIMARY KEY("CivilizationId")
);
DROP TABLE IF EXISTS "CivilizationsStartingResources";
CREATE TABLE IF NOT EXISTS "CivilizationsStartingResources" (
	"CivId"	INTEGER NOT NULL,
	"Food"	INTEGER,
	"Wood"	INTEGER,
	"Gold"	INTEGER,
	"Stone"	INTEGER,
	PRIMARY KEY("CivId")
);
DROP TABLE IF EXISTS "FileChecksum";
CREATE TABLE IF NOT EXISTS "FileChecksum" (
	"Fullname"	TEXT,
	"Filename"	TEXT NOT NULL,
	"Checksum"	TEXT NOT NULL,
	"LastWriteTime"	INTEGER NOT NULL,
	PRIMARY KEY("Fullname")
);
DROP TABLE IF EXISTS "StringTableLocations";
CREATE TABLE IF NOT EXISTS "StringTableLocations" (
	"TableName"	TEXT,
	"LocStart"	INTEGER,
	"LocEnd"	INTEGER,
	PRIMARY KEY("TableName")
);
DROP TABLE IF EXISTS "Languages";
CREATE TABLE IF NOT EXISTS "Languages" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Locale"	INTEGER NOT NULL,
	"Filename"	TEXT NOT NULL,
	"LocId"	INT NOT NULL,
	"Symbol"	TEXT,
	"Text"	TEXT
);
COMMIT;
