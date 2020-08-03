<Query Kind="SQL" />

BEGIN TRAN

--Create Table
IF OBJECT_ID('dbo.TestTable', 'U') IS NOT NULL
	DROP TABLE dbo.TestTable
GO
CREATE TABLE dbo.TestTable (
	c1 BIGINT IDENTITY(1,1)
	, c2 CHAR(200) NOT NULL
	, CONSTRAINT PK_TestTable PRIMARY KEY (c1)
)
GO
--Load data
;WITH
   X1 AS ( SELECT name AS C FROM sys.objects)
  , X2 AS ( SELECT DISTINCT name AS C FROM sys.columns)
  , X3 AS ( SELECT REVERSE(name) AS C FROM sys.objects)
INSERT dbo.TestTable (c2)
SELECT TOP 50000
   X1.C + ' ' + X2.C + ' ' + X3.C AS C1
FROM X1 CROSS JOIN X2 CROSS JOIN X3
GO



UPDATE dbo.TestTable WITH (ROWLOCK)
	SET c2 = c1
	WHERE c1 <= 7000

SELECT resource_type, request_mode, COUNT(1) AS [This Many Locks]
FROM sys.dm_tran_locks WHERE request_session_id = @@SPID
GROUP BY resource_type, request_mode

SELECT  COUNT(1) ,
        resource_type ,
        resource_subtype ,
        resource_lock_partition ,
        request_mode ,
        request_type, 
		request_status,
		resource_associated_entity_id
FROM    sys.dm_tran_locks
WHERE request_session_id = @@SPID
GROUP BY resource_type ,
        resource_subtype ,
        resource_lock_partition ,
        request_mode ,
        request_type , 
		request_status,
		resource_associated_entity_id;



ROLLBACK TRAN



