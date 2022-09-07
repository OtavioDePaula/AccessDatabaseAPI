using AccessDatabaseAPI.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace AccessDatabaseAPI.Models
{
    public class GroupBlockDAO
    {
        private Database db;
        public List<GroupBlock> SelectAllGroupBlock()
        {
            using (db = new Database())
            {
                string selectAllQuery = "select * from tbElement;";
                var reader = db.CommandRetuner(selectAllQuery);
                return ConvertingReaderToList(reader);
            }
        }

        public GroupBlock SelectGroupBlockById(int id)
        {
            using (db = new Database())
            {
                string selectByIdQuery = string.Format("select * from tbGroupBlock WHERE GroupBlockID = {0};", id);
                var reader = db.CommandRetuner(selectByIdQuery);
                return ConvertingReaderToList(reader).FirstOrDefault();
            }
        }

        public GroupBlock SelectGroupBlockByName(string name)
        {
            using (db = new Database())
            {
                string selectByNameQuery = string.Format("select * from tbGroupBlock WHERE GroupBlock LIKE '%{0}%' LIMIT 1;", name);
                var reader = db.CommandRetuner(selectByNameQuery);
                return ConvertingReaderToList(reader).FirstOrDefault();
            }
        }

        public List<GroupBlock> ConvertingReaderToList(MySqlDataReader reader)
        {
            var groupBlockList = new List<GroupBlock>();

            while (reader.Read())
            {
                var tempGroupBlock = new GroupBlock()
                {
                    groupblockID = int.Parse(reader["groupBlockID"].ToString()),
                    groupblock = reader["groupBlock"].ToString()
                };
                groupBlockList.Add(tempGroupBlock);
            }
            reader.Close();
            return groupBlockList;
        }
    }
}
