IF DB_ID(N'SchoolLab04') IS NULL EXEC(N'CREATE DATABASE SchoolLab04');
GO
USE SchoolLab04;
GO
SET XACT_ABORT ON;
BEGIN TRANSACTION;
CREATE TABLE tblDepartments(DepartmentID int IDENTITY PRIMARY KEY, DepartmentName nvarchar(100) NOT NULL);
CREATE TABLE tblInstructors(InstructorID int IDENTITY PRIMARY KEY, InstructorName nvarchar(100) NOT NULL, DepartmentID int NOT NULL REFERENCES tblDepartments);
CREATE TABLE tblStudents(StudentID int IDENTITY PRIMARY KEY,StudentName nvarchar(100) NOT NULL,DateOfBirth date NOT NULL,City nvarchar(50) NOT NULL,Age int NOT NULL CHECK(Age>=0),YearOfEnroll int NOT NULL, Major nvarchar(50), GPA decimal(3,2));
CREATE TABLE tblCourses(CourseID int IDENTITY PRIMARY KEY,CourseName nvarchar(100) NOT NULL,Credits int NOT NULL CHECK(Credits>0), DepartmentID int NOT NULL REFERENCES tblDepartments, InstructorID int NOT NULL REFERENCES tblInstructors);
CREATE TABLE tblStudentCourses(StudentID int REFERENCES tblStudents, CourseID int REFERENCES tblCourses, PRIMARY KEY(StudentID,CourseID));
CREATE TABLE Suppliers(SupplierID int IDENTITY PRIMARY KEY,Name nvarchar(100) NOT NULL);
CREATE TABLE Products(ProductID int IDENTITY PRIMARY KEY,Name nvarchar(100) NOT NULL,SupplierID int REFERENCES Suppliers);
CREATE TABLE Customers(Id int IDENTITY PRIMARY KEY, Name nvarchar(100) NOT NULL, Email nvarchar(150) NOT NULL);
CREATE TABLE tblUsers(UserID int IDENTITY PRIMARY KEY,Username nvarchar(50) NOT NULL UNIQUE,PasswordHash varbinary(32) NOT NULL);
INSERT tblDepartments VALUES(N'Computer Science'),(N'Mathematics');
INSERT tblInstructors VALUES(N'Mai Anh',1),(N'Thu Hà',2);
INSERT tblStudents VALUES(N'Nguyễn Gia Bình','20050301',N'TP.HCM',21,2024,N'IT',3.65),(N'Lan Anh','20040612',N'Đà Nẵng',22,2024,N'Math',3.40);
INSERT tblCourses VALUES(N'Software Engineering',3,1,1),(N'Calculus',4,2,2);
INSERT tblStudentCourses VALUES(1,1),(2,2);
INSERT Customers VALUES(N'Minh An','an@example.org');
INSERT tblUsers VALUES(N'admin',HASHBYTES('SHA2_256',CONVERT(nvarchar(50),N'SchoolDemo26!')));

COMMIT;

-- Additional sign-ins from the handout
INSERT tblUsers VALUES(N'user1',HASHBYTES('SHA2_256',CONVERT(nvarchar(50),N'password1'))),(N'user2',HASHBYTES('SHA2_256',CONVERT(nvarchar(50),N'password2')));
