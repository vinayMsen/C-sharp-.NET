    CREATE DATABASE DotNetCourseDatabase
    GO
     
    USE DotNetCourseDatabase
    GO
    DELETE FROM Computer WHERE Motherboard='Z568h';
    DELETE FROM Computer WHERE Motherboard='zqw68';
    DELETE FROM Computer WHERE Motherboard='z690';
    select * from sys.tables;

    -- CREATE TABLE Computer(
    --     ComputerId INT IDENTITY(1,1) PRIMARY KEY,
    --     Motherboard NVARCHAR(50),
    --     CPUCores INT,
    --     HasWifi BIT,
    --     HasLTE BIT,
    --     ReleaseDate DATE,
    --     Price DECIMAL(18,4),
    --     VideoCard NVARCHAR(50)
    -- );
    select * FROM Computer;
    GO
    UPDATE Computer SET Motherboard='Sample-Motherboard' where ComputerId=1;
    --inserting values
    INSERT INTO Computer (
                                          [Motherboard]
                                        , [CPUCores]
                                        , [HasWifi]
                                        , [HasLTE]
                                        , [ReleaseDate]
                                        , [Price]
                                        , [VideoCard])
    VALUES ('Sample-Motherboard'
            , 4
            , 1  -- true
            , 0  -- false
            , GETDATE ()
            , 1000.28
            , 'Sample-VideoCard');


SELECT * from Computer;
DELETE  FROM Computer WHERE  ComputerId = 3;

UPDATE  Computer SET  Motherboard = 'Obsolete' WHERE  HasWifi = 0;
SELECT  [ComputerId]
        , [Motherboard]
        , ISNULL ([CPUCores], 4) AS CPUCores
        , [HasWifi]
        , [HasLTE]
        , [ReleaseDate]
        , [Price]
        , [VideoCard]
  FROM  Computer;



-- remove duplicate records and keep one 
WITH duplicates AS (
    SELECT 
        Motherboard,
        ROW_NUMBER() OVER (
            PARTITION BY Motherboard,Price   -- columns to check duplicates
            ORDER BY ComputerId                   -- keep the smallest id
        ) AS row_num
    FROM Computer
)
DELETE FROM Computer
WHERE Motherboard IN (
    SELECT Motherboard
    FROM duplicates
    WHERE row_num > 1
);


-- UPDATE  Computer
--    SET  CPUCores = 4
--  WHERE  CPUCores IS NULL;

SELECT name FROM sys.databases;

--  use master;
--  GO
-- SELECT * FROM sys.databases;
-- alter DATABASE DotNetCourseDatabase set Single_user with ROLLBACK IMMEDIATE; 
-- GO
-- DROP DATABASE DotNetCourseDatabase; 
-- GO

