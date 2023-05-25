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
using ApplicationStorage.Entityes;

namespace ApplicationStorage
{
    public partial class Form1 : Form
    {

        private BdConnecting bdEntity = new BdConnecting();

        protected SqlCommandBuilder sqlBuilder = null;

        protected SqlDataAdapter sqlDataAdapter = null;

        protected bool newRowAddding = false;

        public Form1()
        {
            InitializeComponent();

        }

        protected void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            bdEntity.getSqlConnect().Open();
        }

        protected void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlDataAdapter = bdEntity.getDataAdapter("SELECT 'Delete' AS [Command], * FROM", toolStripComboBox1.SelectedItem.ToString());
            sqlDataAdapter.Fill(bdEntity.GetDataSet(), $"{toolStripComboBox1.SelectedItem.ToString()}");

            LoadData();
        }
        public void LoadData()
        {
            try
            {
                bdEntity.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"].Clear();

                sqlDataAdapter.Fill(bdEntity.GetDataSet(), $"{toolStripComboBox1.SelectedItem}");

                dataGridView1.DataSource = bdEntity.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"];

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[0, i] = linkCell;
                }
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    dataGridView1.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if ((dataGridView1.Columns[j].Name).ToString() == "Id" ||
                        (dataGridView1.Columns[j].Name).ToString() == "id_product" ||
                        (dataGridView1.Columns[j].Name).ToString() == "id_storage" ||
                        (dataGridView1.Columns[j].Name).ToString() == "IdProvider" ||
                        (dataGridView1.Columns[j].Name).ToString() == "IdProduct")
                    {
                        dataGridView1.Columns[j].ReadOnly = true;

                    }
                }
                dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                if (e.RowIndex < 0)
                    return;
                if (e.ColumnIndex == 0)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView1.Rows.RemoveAt(rowIndex);

                            bdEntity.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"].Rows[rowIndex].Delete();

                            sqlDataAdapter.Update(bdEntity.GetDataSet(), $"{toolStripComboBox1.SelectedItem}");
                        }
                    }
                    else if (task == "Update" && toolStripComboBox1.SelectedIndex == 0)
                    {
                        int r = e.RowIndex;
                        float price = float.Parse((dataGridView1.Rows[r].Cells["price"].Value).ToString());
                        float sumPrice = float.Parse((dataGridView1.Rows[r].Cells["count"].Value).ToString());
                        for (int i = 1; i <= dataGridView1.Columns.Count - 1; i++)
                        {
                            bdEntity.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"].Rows[r][$"{(dataGridView1.Columns[i].Name).ToString()}"] = dataGridView1.Rows[r].Cells[$"{(dataGridView1.Columns[i].Name).ToString()}"].Value;
                        }
                        bdEntity.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"].Rows[r]["sumPrice"] = price * sumPrice;

                        sqlDataAdapter.Update(bdEntity.GetDataSet(), $"{toolStripComboBox1.SelectedItem}");

                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = "Delete";
                    }
                    else if (task == "Update" && toolStripComboBox1.SelectedIndex != 0)
                    {
                        int r = e.RowIndex;
                        for (int i = 1; i <= dataGridView1.Columns.Count - 1; i++)
                        {
                            bdEntity.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"].Rows[r][$"{(dataGridView1.Columns[i].Name).ToString()}"] = dataGridView1.Rows[r].Cells[$"{(dataGridView1.Columns[i].Name).ToString()}"].Value;
                        }

                        sqlDataAdapter.Update(bdEntity.GetDataSet(), $"{toolStripComboBox1.SelectedItem}");

                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = "Delete";
                    }
                }       
                LoadData();              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        protected void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (newRowAddding == false)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

                    DataGridViewRow editingRow = dataGridView1.Rows[rowIndex];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[0, rowIndex] = linkCell;

                    editingRow.Cells["Command"].Value = "Update";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedItem != null)
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = null;
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите таблицу!");
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

        }

        protected void toolStripButton1_Click(object sender, EventArgs e)
        {
            ProviderEntity providerEntity = new ProviderEntity(bdEntity.getSqlConnect());
            StorageEntity storageEntity = new StorageEntity(bdEntity.getSqlConnect());
            var form = new InsertForm(bdEntity, providerEntity, storageEntity);
            form.Show();
        }

        protected void toolStripButton2_Click(object sender, EventArgs e)
        {
            var form = new SearchForm(bdEntity, this);
            form.Show();
        }
        protected void toolStripButton4_Click(object sender, EventArgs e)
        {
            var form = new RequestForm(bdEntity);
            form.Show();
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Colum_KeyPress);

            if (toolStripComboBox1.SelectedIndex == 0 && (dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6 || dataGridView1.CurrentCell.ColumnIndex == 7) ||
                ((toolStripComboBox1.SelectedIndex == 1) && dataGridView1.CurrentCell.ColumnIndex == 3) ||
                (toolStripComboBox1.SelectedIndex == 2) && dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(Colum_KeyPress);
                }
            }
        }
        private void Colum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}