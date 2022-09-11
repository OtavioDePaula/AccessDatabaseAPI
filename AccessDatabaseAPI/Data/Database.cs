using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace AccessDatabaseAPI.Data
{
    public class Database : IDisposable
    {
        private readonly MySqlConnection connection;

        public Database()
        {
            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
            connection.Open();
        }

        public void CommandExecuter(string queryString)
        {
            var command = new MySqlCommand
            {
                CommandText = queryString,
                CommandType = CommandType.Text,
                Connection = connection
            };
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MySqlDataReader CommandRetuner(string queryString)
        {
            var command = new MySqlCommand(queryString, connection);
            try
            {
                return command.ExecuteReader();
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}

/*
DROP DATABASE IF EXISTS db_periodicTable;
CREATE DATABASE IF NOT EXISTS db_periodicTable;
USE db_periodicTable; 

CREATE TABLE IF NOT EXISTS tbstandardState (
	standardStateID INT PRIMARY KEY auto_increment,
    standardState VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS tbGroupblock (
	groupblockID INT PRIMARY KEY auto_increment,
    groupblock VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS tbElement (
	atomicNumber INT PRIMARY KEY, 
    symbol VARCHAR(2) NOT NULL UNIQUE, 
    name VARCHAR(80) NOT NULL UNIQUE,
    atomicMass VARCHAR(80) NOT NULL, 
    yearDiscovered SMALLINT NOT NULL, 
    cpkHexColor VARCHAR(6) NOT NULL, 
    period INT NOT NULL, 
    groupfamily INT NOT NULL,
    favorited BOOLEAN DEFAULT(true),
    FK_standardState INT NOT NULL,
    FK_groupblock INT NOT NULL,
    FOREIGN KEY (FK_standardState) REFERENCES tbstandardState(standardStateID),
    FOREIGN KEY (FK_groupblock) REFERENCES tbGroupblock(groupblockID)
); 
                             
INSERT INTO tbstandardState VALUES (default, "liquid"), 
								   (default, "solid"),
                                   (default, "gas");
                                   
INSERT INTO tbGroupBlock VALUES (default, "halogenios"), 
							    (default, "nobre gas"),
                                (default, "trogenios");
INSERT INTO tbElement VALUES (
                                1,
                                'A',
                                'Argon',
                                '98.90',
                                1765,
                                'FF0000',
                                1,
                                13,
                                default,
                                1,
                                1
                             ),(
                                2,
                                'Z',
                                'Zrgon',
                                '98.90',
                                2012,
                                'FF0000',
                                4,
                                5,
                                default,
                                2,
                                2 
                             ),(
                             	3,
                                'X',
                                'Xrgon',
                                '98.90',
                                0001,
                                'FF0000',
                                9,
                                2,
                                default,
                                3,
                                3
                             );                                   

SELECT * from tbStandardState;
SELECT * from tbGroupBlock;
SELECT * FROM tbElement;

USE db_periodictable;
DROP PROCEDURE IF EXISTS spSelectGroupBlock;
DROP PROCEDURE IF EXISTS spSelectStandardState;

DELIMITER //
CREATE PROCEDURE IF NOT EXISTS spSelectGroupBlock(vGroupBlock VARCHAR(80))
BEGIN
	IF EXISTS (SELECT * FROM tbgroupblock WHERE groupBlock = vGroupBlock) THEN
    	SELECT * FROM tbgroupblock WHERE groupBlock = vGroupBlock;
    ELSE
    	INSERT INTO tbgroupblock VALUES (default, vGroupBlock);
        SELECT * FROM tbgroupblock WHERE groupBlock = vGroupBlock;
    END IF;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE IF NOT EXISTS spSelectStandardState(vStandardState VARCHAR(80))
BEGIN
	IF EXISTS (SELECT * FROM tbStandardState WHERE StandardState = vStandardState) THEN
    	SELECT * FROM tbStandardState WHERE StandardState = vStandardState;
    ELSE
    	INSERT INTO tbStandardState VALUES (default, vStandardState);
        SELECT * FROM tbStandardState WHERE StandardState = vStandardState;
    END IF; 
END //
DELIMITER ;

call SPSelectStandardState('test3');
 */