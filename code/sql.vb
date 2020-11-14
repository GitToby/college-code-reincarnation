SQL Code
USE
Backup and Restore .sql File
computing2
DROP TABLE IF EXISTS Contacts ;
CREATE TABLE "Contacts (
Contact ID'
MEDIUMINT AUTO INCREMENT NOT NULL,
FirstName " VARCHAR (20) NOT NULL
LastName VARCHAR (20) NOT NULL ,
DOB DATE NOT NULL ,
Email" VARCHAR (50) NOT NULL,
`Phone VARCHAR (12) NOT NULL,
PRIMARY KEY (ContactID)
) ;
INSERT INTO Contacts VALUES (1, 'John',
'Smith' ,
11995-06-24'
'john.smith@email.com' , '01299323145');
INSERT INTO Contacts VALUES (2, 'Herb',
Jones' ,
'1994-09-301
' herb. jones@email.com' , '01748932311' ) ;
INSERT INTO ~Contacts VALUES (3, 'Jackie', 'Chan',
1995-02-04'
'jackie. chan@email. com' , '01429323786') ;
INSERT INTO ~Contacts VALUES (4, 'Al',
'Baker' ,
11995-06-24
'al. baker@email.com'
, ' 01429932314 ' ) ;
INSERT INTO ~Contacts VALUES (5, 'Sue',
Booker', '1994-10-29'
' sue . booker@email.com' , '01425932762') ;
INSERT INTO Contacts VALUES (6, 'Rob',
Booker', '1994-11-29'
rb@email. net'
, 1 01425674962' ) ;
INSERT INTO Contacts VALUES (7, 'Jessica', 'Roberts', '1984-01-07'
' badger1010@msn.net'
, '077762549621 ) ;
INSERT INTO Contacts VALUES (8, 'Dasie',
Reton',
'1977-11-05'
'idonthaveemail@pleasestop.it'
, 1066625496221' ) ;
INSERT INTO Contacts VALUES (9, 'Jessica', 'Kaligri', '1997-04-24
' #modern@Kaligrii646. twit'
, ' 031415926535' ) ;
INSERT INTO Contacts VALUES
(10, 'Toby', 'Awesome' ,
11961-03-17'
' Ihatethis@comproject. meh'
, '098568459214' ) ;
DROP TABLE IF EXISTS Relations ;
CREATE TABLE Relations (
~Reference" MEDIUMINT (4) AUTO_INCREMENT NOT NULL ,
`ContactID1 MEDIUMINT (4) NOT NULL,
`ContactID2 MEDIUMINT (4) NOT NULL,
PRIMARY KEY (Reference)
)
INSERT INTO Relations VALUES
(1,1,3) ;
INSERT INTO Relations VALUES
(2,1,4) ;
INSERT INTO
"Relations VALUES
(3,2,4) ;
DROP TABLE IF EXISTS MemberTypes ;
CREATE TABLE "MemberTypes (
~TypeID" MEDIUMINT Primary KEY NOT NULL,`TypeDescription" VARCHAR (10) NOT NULL
) ;
INSERT INTO ~MemberTypes VALUES (1,' 'Junior' ) ;
INSERT INTO ~MemberTypes VALUES
(2,
' Adult ' ) ;
INSERT INTO
`MemberTypes VALUES (3, 'Club'
) ;
DROP TABLE IF EXISTS SessionTable ;
CREATE
TABLE SessionTable (
SessionID MEDIUMINT AUTO_INCREMENT NOT NULL,
DayOfWeek VARCHAR (10) NOT NULL,
`StartTime' TIME NOT NULL,
EndTime TIME NOT NULL,
`MemberTypeKey" MEDIUMINT NOT NULL,
PRIMARY KEY (SessionID)
) ;
INSERT INTO ~SessionTable VALUES
(1, 'Monday', '18: 00:00', '20:00:00' , 1) ;
INSERT INTO SessionTable VALUES
(2, 'Tuesday', '18: 00: 00', '20:00 : 00' , 2) ;
INSERT INTO SessionTable VALUES
(3, 'Tuesday', '19:00:00', '21:00 : 00 ' , 3) ;
DROP TABLE IF EXISTS ContactSessions ;
CREATE
TABLE ContactSessions (
`ContactIDRef MEDIUMINT NOT NULL,
~SessionIDRef MEDIUMINT NOT NULL
) ;
INSERT INTO ContactSessions VALUES (1,1) ;
INSERT INTO ContactSessions VALUES
(2, 1) ;
INSERT INTO ContactSessions"
VALUES
( 3 , 1 ) ;
INSERT INTO ContactSessions VALUES (4,1) ;
INSERT INTO ContactSessions"
VALUES (4,2) ;
INSERT INTO ContactSessions"
VALUES (5,1) ;
INSERT INTO ContactSessions"
VALUES (5,2) ;
INSERT INTO ContactSessions VALUES (6,2) ;
INSERT INTO ContactSessions VALUES (6,1) ;
INSERT INTO ContactSessions VALUES (6,3) ;
INSERT INTO ~ContactSessions"
VALUES (7,2) ;
INSERT INTO "ContactSessions"
VALUES (8,1) ;
INSERT INTO ContactSessions"
VALUES (8,3) ;
INSERT INTO ContactSessions" VALUES (9,2) ;
INSERT INTO ~ContactSessions"
VALUES
(9,3) ;
INSERT INTO ContactSessions VALUES
(10,1) ;Sqtup.sql file for user installs
// First off, creating the database. It won't have any information
// stored, it's just a blank canvas
CREATE DATABASE "Computing2 ;
// create the user at local host, with the password "chicken"
CREATE USER 'toby '@'localhost' IDENTIFIED BY 'chicken' ;
// Grant all permissions to all the tables in computing2 to the user
// just created.
GRANT ALL ON computing2. * TO 'toby'@'localhost' ;
// Again creating another user, this time however at % which allows
the users to access thedatabase
CREATE USER 'toby'@' %' IDENTIFIED BY 'chicken' ;
/ / This new user then must have all the permissions on the computing
// database also, this allows for the user at any location to access
// the DB
GRANT ALL ON computing2. * TO 'toby' @'localhost' ;
// selects the newly created computing database to use
USE computing2
// Starts creation of the tables to populate the database
DROP TABLE IF EXISTS Contacts ;
CREATE TABLE Contacts (
`ContactID" MEDIUMINT AUTO INCREMENT NOT NULL,
`FirstName VARCHAR (20) NOT NULL ,
`LastName VARCHAR ( 20) NOT NULL ,
DOB DATE NOT NULL
~Email VARCHAR (50) NOT NULL,
~Phone ~ VARCHAR (12) NOT NULL,
PRIMARY KEY (ContactID)
) ;
DROP TABLE IF EXISTS Relations ;
CREATE TABLE Relations (
`Reference MEDIUMINT (4) AUTO_INCREMENT NOT NULL ,
`ContactID1 MEDIUMINT (4) NOT NULL,
~ContactID2" MEDIUMINT (4) NOT NULL,
PRIMARY KEY (Reference)
) ;
DROP TABLE IF EXISTS MemberTypes ;
CREATE TABLE MemberTypes (
"TypeID" MEDIUMINT Primary KEY NOT NULL,
TypeDescription
VARCHAR (10) NOT NULL
) ;DROP TABLE IF EXISTS
`SessionTable ;
CREATE TABLE SessionTable (
`SessionID" MEDIUMINT AUTO_INCREMENT NOT NULL,
`DayOfWeek VARCHAR (10) NOT NULL,
StartTime TIME NOT NULL,
EndTime TIME NOT NULL,
`MemberTypeKey MEDIUMINT NOT NULL,
PRIMARY KEY (SessionID)
) ;
DROP TABLE IF EXISTS ContactSessions ;
CREATE
TABLE ContactSessions (
`ContactIDRef MEDIUMINT NOT NULL,
SessionIDRef MEDIUMINT NOT NULL
) ;