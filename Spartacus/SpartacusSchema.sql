BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "MilestoneRewards" (
	"Id"	TEXT NOT NULL,
	"CivId"	INTEGER NOT NULL,
	"TechId"	TEXT,
	"DisplayId"	INTEGER,
	"RolloverTextId"	INTEGER,
	"LevelRequirement"	INTEGER,
	"Icon"	TEXT,
	PRIMARY KEY("Id")
);
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
CREATE TABLE IF NOT EXISTS "MilestoneTiers" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"CivId"	INTEGER NOT NULL,
	"TechName"	TEXT NOT NULL
);
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
CREATE TABLE IF NOT EXISTS "StartingResources" (
	"CivId"	INTEGER NOT NULL,
	"Food"	INTEGER,
	"Wood"	INTEGER,
	"Gold"	INTEGER,
	"Stone"	INTEGER,
	PRIMARY KEY("CivId")
);
CREATE TABLE IF NOT EXISTS "FileChecksum" (
	"Fullname"	TEXT,
	"Filename"	TEXT NOT NULL,
	"Checksum"	TEXT NOT NULL,
	"LastWriteTime"	INTEGER NOT NULL,
	PRIMARY KEY("Fullname")
);
CREATE TABLE IF NOT EXISTS "StringTableLocations" (
	"TableName"	TEXT,
	"LocStart"	INTEGER,
	"LocEnd"	INTEGER,
	PRIMARY KEY("TableName")
);
CREATE TABLE IF NOT EXISTS "Languages" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Locale"	INTEGER NOT NULL,
	"Filename"	TEXT NOT NULL,
	"LocId"	INT NOT NULL,
	"Symbol"	TEXT,
	"Text"	TEXT
);
COMMIT;
