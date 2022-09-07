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

        public void SaveElement(Element element)
        {
            var insertQuery = "";
            insertQuery += "insert into tbElement values";
            insertQuery += string.Format("({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', {8}, {9}, {10});",
                element.atomicNumber,                            //0 
                element.symbol,                                  //1 ''
                element.name,                                    //2 ''
                element.atomicMass,                              //3 ''
                element.yearDiscovered.ToString("yyyy-MM-dd"),   //4 ''
                element.cpkHexColor,                             //5 ''
                element.period,                                  //6
                element.group,                                   //7
                element.favorited,                               //8
                element.groupBlock.groupblockID,                 //9
                element.standardState.standardStateID);          //10 

            using (db = new Database())
            {
                db.CommandExecuter(insertQuery);
            }
        }

        public void UpdateElement(Element element)
        {
            var updateQuery = "";
            updateQuery += "update tbElement SET ";
            updateQuery += string.Format("symbol = '{1}', " +
                                         "name = '{2}', " +
                                         "atomicMass = '{3}', " +
                                         "yearDiscovered = '{4}', " +
                                         "cpkHexColor = '{5}', " +
                                         "period = {6}, " +
                                         "groupfamily = {7}, " +
                                         "favorited = {8}, " +
                                         "FK_groupblock = {9}, " +
                                         "FK_standardState = {10} " +
                                         "WHERE atomicNumber = {0};",
                element.atomicNumber,                            //0 
                element.symbol,                                  //1 ''
                element.name,                                    //2 ''
                element.atomicMass,                              //3 ''
                element.yearDiscovered.ToString("yyyy-MM-dd"),   //4 ''
                element.cpkHexColor,                             //5 ''
                element.period,                                  //6
                element.group,                                   //7
                element.favorited,                               //8
                element.groupBlock.groupblockID,                 //9
                element.standardState.standardStateID);          //10 

            using (db = new Database())
            {
                db.CommandExecuter(updateQuery);
            }
        }


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
                    group = int.Parse(reader["groupfamily"].ToString()),
                    favorited = bool.Parse(reader["favorited"].ToString()),
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