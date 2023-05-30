using ApplicationStorage.Entityes;
using ApplicationStorage.InsertLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationStorage.Forms
{
    public partial class InsertMore : Form
    {
        private BdConnecting bd = null;
        public InsertMore(BdConnecting bd)
        {
            InitializeComponent();
            this.bd = bd;
        }

        private void InsertMore_Load(object sender, EventArgs e)
        {
            label1.Text = "Выберите \'Поставщик\' или \'Склад\'";
            button1.Visible = false;
            visiableFalse();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            visiableFalse();
            if (radioButton1.Checked)
            {
                button1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;

                label1.Text = "Добавить поставщика";
                label2.Text = "Название компании"; //1
                label3.Text = "ИНН поставщика"; // 2
                label4.Text = "Адресс";

                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;

                textBox2.MaxLength = 12;
                textBox1.MaxLength = 100;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            visiableFalse();
            if (radioButton2.Checked)
            {
                button1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;

                textBox1.MaxLength = 11;
                textBox3.MaxLength = 100;

                label1.Text = "Добавить склад";
                label2.Text = "Телефон"; //1
                label3.Text = "Адресс"; // 2
                label4.Text = "Имя руководителя";
                label5.Text = "Фамлия руководителя";
                label6.Text = "Отчество руководителя";

                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ProviderEntity providerEntity = new ProviderEntity(
                    textBox2.Text,
                    textBox1.Text,
                    textBox3.Text
                    );

                InsertProvider insertProvider = new InsertProvider(providerEntity, bd.getSqlConnect());
                MessageBox.Show($"Компания {textBox1.Text} была добавлена!"); ;
            }
            else if (radioButton2.Checked)
            {
                StorageEntity storageEntity = new StorageEntity(
                    textBox2.Text,
                    textBox1.Text,
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text
                    );

                InsertStorage insertProvider = new InsertStorage(storageEntity, bd.getSqlConnect());
                MessageBox.Show($"Склад по адерсу {textBox2.Text} был добавлен!"); ;
            }
            else
            {
                MessageBox.Show("Выберите \'Поставщик\' или \'Склад\'");
            }
        }

        private void visiableFalse()
        {
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;

            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new RegForm(bd);
            form.Show();
        }
    }
}
