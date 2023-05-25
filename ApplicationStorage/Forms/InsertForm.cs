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
using ApplicationStorage.InsertLogic;
using ApplicationStorage.Entityes;

namespace ApplicationStorage
{
    public partial class InsertForm : Form
    {
        private BdConnecting bd = null;
        private ProviderEntity providerEntity;
        private StorageEntity storageEntity;
        public InsertForm(BdConnecting bd, ProviderEntity providerEntity, StorageEntity storageEntity)
        {
            InitializeComponent();
            this.bd = bd;
            this.providerEntity = providerEntity;
            this.storageEntity = storageEntity;
        }

        private void InsertForm_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            foreach (string el in storageEntity.adress)
            {
                comboBox1.Items.Add(el);
            }
            foreach (string el in providerEntity.nameProvider)
            {
                comboBox2.Items.Add(el);
            }
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.SelectedItem != "" && comboBox2.SelectedItem != "")
                {
                    ProductEntity productEntity = new ProductEntity(
                        int.Parse(textBox2.Text),
                        textBox1.Text,
                        comboBox2.SelectedIndex + 1);
                    InsertProduct insertProduct = new InsertProduct(productEntity, bd.getSqlConnect());

                    SqlCommand cmdIdProduct = new SqlCommand($"SELECT IdProduct FROM Product WHERE article = {int.Parse(productEntity.article.ToString())}", bd.getSqlConnect());
                    SupplyEntity supplyEntity = new SupplyEntity(
                        int.Parse(cmdIdProduct.ExecuteScalar().ToString()),
                        comboBox1.SelectedIndex + 1,
                        dateTimePicker1.Value.ToShortDateString(),
                        float.Parse(textBox3.Text),
                        float.Parse(textBox4.Text),
                        float.Parse(textBox3.Text) * float.Parse(textBox4.Text));
                    InsertSupply insertSupply = new InsertSupply(supplyEntity, bd.getSqlConnect());

                    SqlCommand cmdIdSupply = new SqlCommand($"SELECT Id FROM Supply t1 Join Product t2 ON (t1.id_product = {int.Parse(cmdIdProduct.ExecuteScalar().ToString())} AND t2.IdProduct = {int.Parse(cmdIdProduct.ExecuteScalar().ToString())})", bd.getSqlConnect());
                    insertProduct.UpdateIdSupply(int.Parse(cmdIdSupply.ExecuteScalar().ToString()), int.Parse(cmdIdProduct.ExecuteScalar().ToString()), bd.getSqlConnect());
                    MessageBox.Show("Продукт:\n" + productEntity.name + "\nБыл добавлен в БД");
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxPress(sender, e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxPress(sender, e);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxPress(sender, e);
        }
        private void textBoxPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
