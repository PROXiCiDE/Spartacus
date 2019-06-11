BEGIN TRANSACTION;
DROP TABLE IF EXISTS "Milestone";
CREATE TABLE IF NOT EXISTS "Milestone" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"CivId"	INTEGER,
	"TechId"	TEXT,
	"LevelRequirement"	INTEGER,
	"Icon"	TEXT
);
DROP TABLE IF EXISTS "ProtoAgeUnitType";
CREATE TABLE IF NOT EXISTS "ProtoAgeUnitType" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"ProtoNameId"	TEXT NOT NULL,
	"Type"	TEXT NOT NULL
);
DROP TABLE IF EXISTS "ProtoAgeUnitFlag";
CREATE TABLE IF NOT EXISTS "ProtoAgeUnitFlag" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"ProtoNameId"	TEXT NOT NULL,
	"Flag"	INTEGER NOT NULL
);
DROP TABLE IF EXISTS "ProtoAgeResourceCost";
CREATE TABLE IF NOT EXISTS "ProtoAgeResourceCost" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"ProtoNameId"	TEXT,
	"Food"	INTEGER,
	"Wood"	INTEGER,
	"Gold"	INTEGER,
	"Stone"	INTEGER
);
DROP TABLE IF EXISTS "TechTreeEffects";
CREATE TABLE IF NOT EXISTS "TechTreeEffects" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"TechNameId"	TEXT NOT NULL,
	"Type"	TEXT,
	"Amount"	INTEGER,
	"Scaling"	INTEGER,
	"SubType"	TEXT,
	"Relativity"	TEXT,
	"Target"	TEXT,
	"AllActions"	INTEGER,
	"Action"	TEXT,
	"UnitType"	TEXT,
	"Proto"	TEXT,
	"Culture"	TEXT,
	"Resource"	TEXT,
	"Component"	TEXT,
	"Status"	TEXT,
	"NewName"	TEXT,
	"DamageType"	TEXT
);
DROP TABLE IF EXISTS "TechTreeResource";
CREATE TABLE IF NOT EXISTS "TechTreeResource" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"TechNameId"	INTEGER,
	"ResourceTypeEnum"	INTEGER,
	"Value"	INTEGER
);
DROP TABLE IF EXISTS "TechTreeFlag";
CREATE TABLE IF NOT EXISTS "TechTreeFlag" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"TechNameId"	TEXT,
	"Flag"	TEXT
);
DROP TABLE IF EXISTS "TechTree";
CREATE TABLE IF NOT EXISTS "TechTree" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Name"	TEXT NOT NULL,
	"DisplayNameId"	INTEGER,
	"ResearchPoints"	INTEGER,
	"Status"	TEXT,
	"Icon"	TEXT,
	"ContentPack"	INTEGER
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
INSERT INTO "ProtoAgeUnitType" VALUES (3,'UnitName0','TechName0');
INSERT INTO "ProtoAgeUnitType" VALUES (4,'UnitName1','TechName1');
INSERT INTO "ProtoAgeUnitType" VALUES (5,'UnitName2','TechName2');
INSERT INTO "ProtoAgeUnitType" VALUES (6,'UnitName3','TechName3');
INSERT INTO "ProtoAgeUnitType" VALUES (7,'UnitName4','TechName4');
INSERT INTO "ProtoAgeUnitType" VALUES (8,'UnitName0','TechName011');
INSERT INTO "ProtoAgeUnitType" VALUES (9,'UnitName0','TechName022');
INSERT INTO "ProtoAgeUnitFlag" VALUES (45,'UnitName0',85);
INSERT INTO "ProtoAgeUnitFlag" VALUES (46,'UnitName1',86);
INSERT INTO "ProtoAgeUnitFlag" VALUES (47,'UnitName2',87);
INSERT INTO "ProtoAgeUnitFlag" VALUES (48,'UnitName3',88);
INSERT INTO "ProtoAgeUnitFlag" VALUES (49,'UnitName4',89);
INSERT INTO "ProtoAgeResourceCost" VALUES (6,'UnitName0',10,5,0,0);
INSERT INTO "ProtoAgeResourceCost" VALUES (7,'UnitName1',11,6,0,0);
INSERT INTO "ProtoAgeResourceCost" VALUES (8,'UnitName2',12,7,0,0);
INSERT INTO "ProtoAgeResourceCost" VALUES (9,'UnitName3',13,8,0,0);
INSERT INTO "ProtoAgeResourceCost" VALUES (10,'UnitName4',14,9,0,0);
INSERT INTO "ProtoAge" VALUES (1,'UnitName0',0,0,20,'AnimFile0','IconFile','PortraitFile',0,1024,8192,12288,10,0);
INSERT INTO "ProtoAge" VALUES (2,'UnitName1',200,400,21,'AnimFile1','IconFile','PortraitFile',1024,1025,8192,12288,10,1);
INSERT INTO "ProtoAge" VALUES (3,'UnitName2',400,800,22,'AnimFile2','IconFile','PortraitFile',2048,1026,8192,12288,10,2);
INSERT INTO "ProtoAge" VALUES (4,'UnitName3',600,1200,23,'AnimFile3','IconFile','PortraitFile',3072,1027,8192,12288,10,3);
INSERT INTO "ProtoAge" VALUES (5,'UnitName4',800,1600,24,'AnimFile4','IconFile','PortraitFile',4096,1028,8192,12288,10,4);
COMMIT;
