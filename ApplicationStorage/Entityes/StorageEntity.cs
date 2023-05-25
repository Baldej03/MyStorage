using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ApplicationStorage.Entityes
{
    public class StorageEntity
    {
        public List<string> adress = new List<string>();

        public StorageEntity(SqlConnection sqlConnection)
        {
            string query = "SELECT adress FROM Storage";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string> data = new List<string>();
            while (reader.Read())
            {
                adress.Add(reader[0].ToString());
            }
            reader.Close();
        }
    }
}
