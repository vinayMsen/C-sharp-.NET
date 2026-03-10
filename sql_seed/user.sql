use DotNetCourseDatabase;
GO
Select * from Users;
SELECT DB_NAME();
GO
CREATE TABLE Users(
     UserId INT IDENTITY(1, 1) PRIMARY KEY
    , FirstName NVARCHAR(50)
    , LastName NVARCHAR(50)
    , Email NVARCHAR(50)
    , Gender NVARCHAR(50)
    , Active BIT
);
CREATE TABLE UserSalary(
    USER_ID INT ,
    Salary DECIMAL(18, 4),
    FOREIGN KEY (USER_ID) REFERENCES Users(UserId)
);
CREATE TABLE UserJobInfo
(
    USER_ID INT
    , JobTitle NVARCHAR(50)
    , Department NVARCHAR(50),
    FOREIGN KEY (USER_ID) REFERENCES Users(UserId)
);

--INSERT INTO Users 
INSERT INTO Users (FirstName, LastName, Email, Gender, Active) VALUES
('John','Smith','john.smith@email.com','Male',1),
('Emily','Johnson','emily.johnson@email.com','Female',1),
('Michael','Brown','michael.brown@email.com','Male',0),
('Sarah','Davis','sarah.davis@email.com','Female',1),
('David','Wilson','david.wilson@email.com','Male',1),
('Emma','Taylor','emma.taylor@email.com','Female',1),
('Daniel','Anderson','daniel.anderson@email.com','Male',0),
('Olivia','Thomas','olivia.thomas@email.com','Female',1),
('James','Moore','james.moore@email.com','Male',1),
('Sophia','Martin','sophia.martin@email.com','Female',1),
('William','Jackson','william.jackson@email.com','Male',0),
('Ava','White','ava.white@email.com','Female',1),
('Alexander','Harris','alex.harris@email.com','Male',0),
('Mia','Clark','mia.clark@email.com','Female',1),
('Benjamin','Lewis','benjamin.lewis@email.com','Male',1),
('Charlotte','Walker','charlotte.walker@email.com','Female',1),
('Lucas','Hall','lucas.hall@email.com','Male',1),
('Amelia','Allen','amelia.allen@email.com','Female',0),
('Henry','Young','henry.young@email.com','Male',1),
('Harper','King','harper.king@email.com','Female',1);

-- inserting to usersalary
INSERT INTO UserSalary (USER_ID, Salary) VALUES
(1,45000.0000),
(2,52000.0000),
(3,60000.0000),
(4,48000.0000),
(5,75000.0000),
(6,51000.0000),
(7,67000.0000),
(8,72000.0000),
(9,55000.0000),
(10,58000.0000),
(11,64000.0000),
(12,47000.0000),
(13,69000.0000),
(14,53000.0000),
(15,71000.0000),
(16,49500.0000),
(17,62000.0000),
(18,50500.0000),
(19,56000.0000),
(20,54000.0000);

-- inserting to userjobinfo
INSERT INTO UserJobInfo (USER_ID, JobTitle, Department) VALUES
(1,'Software Engineer','IT'),
(2,'HR Manager','Human Resources'),
(3,'Database Administrator','IT'),
(4,'Accountant','Finance'),
(5,'Project Manager','Operations'),
(6,'UI Designer','Design'),
(7,'Backend Developer','IT'),
(8,'Marketing Manager','Marketing'),
(9,'Sales Executive','Sales'),
(10,'Data Analyst','Analytics'),
(11,'System Administrator','IT'),
(12,'Recruiter','Human Resources'),
(13,'DevOps Engineer','IT'),
(14,'Content Writer','Marketing'),
(15,'Business Analyst','Operations'),
(16,'Graphic Designer','Design'),
(17,'QA Engineer','IT'),
(18,'Digital Marketer','Marketing'),
(19,'Support Engineer','IT'),
(20,'Product Manager','Operations');