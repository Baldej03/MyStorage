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
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ApplicationStorage.Forms
{
    public partial class AdminForm : Form
    {
        private BdConnecting bd = null;
        public AdminForm(BdConnecting bd)
        {
            InitializeComponent();
            this.bd = bd;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String loginAdmin = textBox1.Text;
            String pasAdmin = Security.hasPassword(textBox2.Text);
           
            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable table = new DataTable();

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Admins WHERE adLog LIKE N'{loginAdmin.ToString()}' AND adPas LIKE N'{pasAdmin.ToString()}'", bd.getSqlConnect());

            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                var form = new InsertMore(bd);
                form.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует!");
            }

        }
    }
}
