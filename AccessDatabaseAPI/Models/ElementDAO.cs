using AccessDatabaseAPI.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccessDatabaseAPI.Models
{
    public class ElementDAO
    {
        private Database db;

        //public void SaveElement(Element element)
        //{
        //    var insertQuery = "";
        //    insertQuery += "insert into tbElement values";
        //    insertQuery += string.Format("({0}, '{1}', '{2}', '{3}', '{4}', {5}, {6}, '{7}', {8}, {9}, {10}, '{11}', '{12}', '{13}', {14}, {15}, {16}, '{17}', '{18}', '{19}', '{20}', {21}, {22}, default);",
        //        element.atomicNumber,            //0 
        //        element.symbol,                  //1 ''
        //        element.name,                    //2 ''
        //        element.atomicMass,              //3 ''
        //        element.electronicConfiguration, //4 ''
        //        element.electronegativity.ToString().Replace(',', '.'),       //5 
        //        element.atomicRadius,            //6
        //        element.ionRadius,               //7 ''
        //        element.vanDerWaalsRadius,       //8
        //        element.ionizationEnergy,        //9
        //        element.electronAffinity,        //10
        //        element.oxidationStates,         //11 ''
        //        element.standardState,           //12 ''
        //        element.bondingType,             //13 ''
        //        element.meltingPoint,            //14
        //        element.boilingPoint,            //15
        //        element.density.ToString().Replace(',', '.'),                 //16 
        //        element.groupBlock,              //17 ''
        //        element.yearDiscovered,          //18 ''
        //        element.block,                   //19 ''
        //        element.cpkHexColor,             //20 ''
        //        element.period,                  //21
        //        element.group);                  //22

        //    using (db = new Database())
        //    {
        //        db.CommandExecuter(insertQuery);
        //    }

        //}
        //public void UpdateElement(Element element)
        //{
        //    //var updateQuery = "";
        //    //updateQuery += "update tbElement set ";
        //    //updateQuery += string.Format("NomeUsu = '{0}',
        //    //                           Cargo = '{1}',
        //    //                           DataNasc = STR_TO_DATE('{2}', '%d/%m/%Y %T')
        //    //                         where IdUsu = { 3}; ",
        //    //                           element.NomeUsu,
        //    //                           element.Cargo,
        //    //                           element.DataNasc,
        //    //                           element.IdUsu);

        //    //using (db = new Database())
        //    //{
        //    //    db.CommandExecuter(updateQuery);
        //    //}


        //    //atomicNumber INT PRIMARY KEY,
        //    //symbol VARCHAR(2) NOT NULL UNIQUE,  
        //    //name VARCHAR(80) NOT NULL UNIQUE
        //    //atomicMass VARCHAR(80) NOT NULL,
        //    //electronicConfiguration VARCHAR(200) NOT NULL,
        //    //electronegativity FLOAT NOT NULL,  
        //    //atomicRadius INT NOT NULL,
        //    //ionRadius VARCHAR(80),  
        //    //vanDerWaalsRadius INT,
        //    //ionizationEnergy INT NOT NULL,  
        //    //electronAffinity INT NOT NULL,
        //    //oxidationStates VARCHAR(80) NOT NULL,
        //    //standardState VARCHAR(80) NOT NULL,
        //    //bondingType VARCHAR(80) NOT NULL,
        //    //melltingPoint INT NOT NULL,  
        //    //boilingPoint INT NOT NULL,
        //    //density FLOAT NOT NULL,  
        //    //groupBlock VARCHAR(80) NOT NULL,
        //    //yearDiscovered DATE NOT NULL,  
        //    //block VARCHAR(2) NOT NULL,
        //    //cpkHexColor VARCHAR(6) NOT NULL,
        //    //period INT NOT NULL,  
        //    //groupfamily INT NOT NULL,
        //    //favorited BOOLEAN DEFAULT(true) 
        //}
        public void DeleteElement(int id)
        {
            var deleteQuery = "";
            deleteQuery += string.Format(" delete from  tbelement where atomicNumber = {0};", id);

            using (db = new Database())
            {
                db.CommandExecuter(deleteQuery);
            }
        }

        public List<Element> SelectAllElements()
        {
            using (db = new Database())
            {
                string selectAllQuery = "select * from tbElement;";
                var reader = db.CommandRetuner(selectAllQuery);
                return ConvertingReaderToList(reader);
            }
        }

        public Element SelectElementById(int id)
        {
            using (db = new Database())
            {
                string selectByIdQuery = string.Format("select * from tbElement WHERE atomicNumber = {0};", id);
                var reader = db.CommandRetuner(selectByIdQuery);
                return ConvertingReaderToList(reader).FirstOrDefault();
            }
        }

        public List<Element> ConvertingReaderToList(MySqlDataReader reader)
        {
            var elements = new List<Element>();
            var groupblockDAO = new GroupBlockDAO();
            var standardStateDAO = new StandardStateDAO();
            while (reader.Read())
            {
                var tempElement = new Element()
                {
                    atomicNumber = int.Parse(reader["atomicNumber"].ToString()),
                    name = reader["name"].ToString(),
                    symbol = reader["symbol"].ToString(),
                    atomicMass = reader["atomicMass"].ToString(),
                    yearDiscovered = DateTime.Parse(reader["yearDiscovered"].ToString()),
                    cpkHexColor = reader["cpkHexColor"].ToString(),
                    period = int.Parse(reader["period"].ToString()),
                    groupfamily = int.Parse(reader["groupfamily"].ToString()),
                    groupBlock = groupblockDAO.SelectGroupBlockById(int.Parse(reader["FK_groupblock"].ToString())),
                    standardState = standardStateDAO.SelectStandardStateById(int.Parse(reader["FK_standardState"].ToString()))
                };
                elements.Add(tempElement);
            }
            reader.Close();
            return elements;
        }
    }
}