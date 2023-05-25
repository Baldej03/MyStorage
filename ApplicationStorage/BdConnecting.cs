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

namespace ApplicationStorage
{
    public class BdConnecting
    {
        private SqlConnection sqlConnection = null;

        private DataSet dataSet = null;

        public BdConnecting()
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\THUNDEROBOT\source\repos\ApplicationStorage\ApplicationStorage\bin\Debug\Warehouse.mdf;Integrated Security=True");
            dataSet = new DataSet();
        }

        public SqlConnection getSqlConnect()
        {
            return sqlConnection;
        }

        public DataSet GetDataSet()
        {
            return dataSet;
        }


        public SqlDataAdapter getDataAdapter(string command, string nameTable)
        {
            return new SqlDataAdapter(command + " " + nameTable.ToString(), sqlConnection);
        }
    }
}
