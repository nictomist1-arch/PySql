-- Run as an administrator of COMP11A1\SQLEXPRESS.
-- The application uses Data_Base_8, matching Data_Base_8_Filegroups.sql.
USE [master];
GO
IF SUSER_ID(N'COMP11A1\necto') IS NULL
    CREATE LOGIN [COMP11A1\necto] FROM WINDOWS;
GO
USE [Data_Base_8];
GO
IF USER_ID(N'COMP11A1\necto') IS NULL
    CREATE USER [COMP11A1\necto] FOR LOGIN [COMP11A1\necto];
GO
GRANT CONNECT TO [COMP11A1\necto];
GRANT SELECT, INSERT ON OBJECT::[dbo].[products] TO [COMP11A1\necto];
GO
