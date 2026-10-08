CREATE DATABASE CollegeDB;
GO

USE CollegeDB;
GO

SET XACT_ABORT ON;
BEGIN TRANSACTION;

SELECT * INTO dbo.Groups FROM College.dbo.Groups;
SELECT * INTO dbo.Teachers FROM College.dbo.Teachers;
SELECT * INTO dbo.Subjects FROM College.dbo.Subjects;
SELECT * INTO dbo.Students FROM College.dbo.Students;
SELECT * INTO dbo.Grades FROM College.dbo.Grades;

ALTER TABLE dbo.Groups ADD CONSTRAINT PK_Groups PRIMARY KEY (GroupId);
ALTER TABLE dbo.Groups ADD CONSTRAINT UQ_Groups_GroupName UNIQUE (GroupName);
ALTER TABLE dbo.Teachers ADD CONSTRAINT PK_Teachers PRIMARY KEY (TeacherId);
ALTER TABLE dbo.Subjects ADD CONSTRAINT PK_Subjects PRIMARY KEY (SubjectId);
ALTER TABLE dbo.Subjects ADD CONSTRAINT UQ_Subjects_SubjectName UNIQUE (SubjectName);
ALTER TABLE dbo.Students ADD CONSTRAINT PK_Students PRIMARY KEY (StudentId);
ALTER TABLE dbo.Grades ADD CONSTRAINT PK_Grades PRIMARY KEY (GradeId);

ALTER TABLE dbo.Students ADD CONSTRAINT FK_Students_Groups
    FOREIGN KEY (GroupId) REFERENCES dbo.Groups (GroupId);
ALTER TABLE dbo.Subjects ADD CONSTRAINT FK_Subjects_Teachers
    FOREIGN KEY (TeacherId) REFERENCES dbo.Teachers (TeacherId);
ALTER TABLE dbo.Grades ADD CONSTRAINT FK_Grades_Students
    FOREIGN KEY (StudentId) REFERENCES dbo.Students (StudentId);
ALTER TABLE dbo.Grades ADD CONSTRAINT FK_Grades_Subjects
    FOREIGN KEY (SubjectId) REFERENCES dbo.Subjects (SubjectId);
ALTER TABLE dbo.Grades ADD CONSTRAINT CK_Grades_Score CHECK (Score >= 2 AND Score <= 5);
ALTER TABLE dbo.Grades ADD CONSTRAINT DF_Grades_GradeDate DEFAULT GETDATE() FOR GradeDate;

COMMIT TRANSACTION;
