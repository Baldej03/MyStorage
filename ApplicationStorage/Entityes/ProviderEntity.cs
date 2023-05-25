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
    public class ProviderEntity
    {
        public List<string> nameProvider = new List<string>();

        public ProviderEntity(SqlConnection sqlConnection)
        {
            string query = "SELECT nameProvider FROM Provider";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string> data = new List<string>();
            while (reader.Read())
            {
                nameProvider.Add(reader[0].ToString());
            }
            reader.Close();
        }
    }
}
