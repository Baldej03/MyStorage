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
    public partial class RequestForm : Form
    {
        private BdConnecting bd = null;
        private SqlDataAdapter sqlDataAdapter = null;
        public RequestForm(BdConnecting bd)
        {
            InitializeComponent();
            this.bd = bd;
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(textBox1.Text, bd.getSqlConnect());

                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch
            {
                MessageBox.Show("Некорректный запрос!");
            }
        }
    }

}
