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
    public partial class RegForm : Form
    {
        private BdConnecting bd = null;
        public RegForm(BdConnecting bd)
        {
            InitializeComponent();
            this.bd = bd;
        }

        private void RegForm_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 10;
            textBox3.PasswordChar = '*';
            textBox3.MaxLength = 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == textBox3.Text)
                {
                    String loginAdmin = textBox1.Text;
                    String pasAdmin = Security.hasPassword(textBox2.Text);

                    SqlCommand commandStorage = new SqlCommand("INSERT INTO [Admins] (adLog, adPas) VALUES (@adLog, @adPas)", bd.getSqlConnect());
                    commandStorage.Parameters.AddWithValue("@adLog", loginAdmin);
                    commandStorage.Parameters.AddWithValue("@adPas", pasAdmin);
                    commandStorage.ExecuteNonQuery().ToString();
                    MessageBox.Show($"Администратор {loginAdmin} был добавлен!"); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
