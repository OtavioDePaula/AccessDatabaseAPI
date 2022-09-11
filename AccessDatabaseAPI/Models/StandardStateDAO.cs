using AccessDatabaseAPI.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace AccessDatabaseAPI.Models
{
    public class StandardStateDAO
    {
        private Database db;
        public List<StandardState> SelectAllStandardState()
        {
            using (db = new Database())
            {
                string selectAllQuery = "select * from tbStandardState;";
                var reader = db.CommandRetuner(selectAllQuery);
                return ConvertingReaderToList(reader);
            }
        }

        public StandardState SelectStandardStateById(int id)
        {
            using (db = new Database())
            {
                string selectByIdQuery = string.Format("select * from tbStandardState WHERE StandardStateID = {0};", id);
                var reader = db.CommandRetuner(selectByIdQuery);
                return ConvertingReaderToList(reader).FirstOrDefault();
            }
        }

        public StandardState SelectStandardStateByName(string name)
        {
            using (db = new Database())
            {
                string selectByNameQuery = string.Format("call spSelectStandardState('{0}');", name);
                var reader = db.CommandRetuner(selectByNameQuery);
                return ConvertingReaderToList(reader).FirstOrDefault();
            }
        }

        public List<StandardState> ConvertingReaderToList(MySqlDataReader reader)
        {
            var standardStateList = new List<StandardState>();

            while (reader.Read())
            {
                var tempStandardState = new StandardState()
                {
                    standardStateID = int.Parse(reader["standardStateID"].ToString()),
                    standardState = reader["standardState"].ToString()
                };
                standardStateList.Add(tempStandardState);
            }
            reader.Close();
            return standardStateList;
        }
    }
}