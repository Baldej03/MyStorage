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
    public partial class SearchForm : Form
    {
        private BdConnecting bd = null;

        private SqlDataAdapter sqlDataAdapter = null;

        private SqlCommandBuilder sqlBuilder = null;
        private Form1 f1;
        public SearchForm(BdConnecting bd, Form1 f1)
        {
            InitializeComponent();
            this.bd = bd;
            this.f1 = f1;
            f1.Visible = false;
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void toolStripComboBox1_SelectedIndexChanged_1(object sender, EventArgs e) 
        {
            sqlDataAdapter = bd.getDataAdapter("SELECT 'Delete' AS [Command], * FROM", toolStripComboBox1.SelectedItem.ToString());
            sqlDataAdapter.Fill(bd.GetDataSet(), $"{toolStripComboBox1.SelectedItem.ToString()}");
            LoadData();

            toolStripComboBox2.Items.Clear();
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                toolStripComboBox2.Items.Add($"{(dataGridView1.Columns[i].Name).ToString()}");
            }   
        }

        public void LoadData()
        {
            try
            {
                bd.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"].Clear();

                sqlDataAdapter.Fill(bd.GetDataSet(), $"{toolStripComboBox1.SelectedItem}");

                dataGridView1.DataSource = bd.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"];

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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

                            bd.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"].Rows[rowIndex].Delete();

                            sqlDataAdapter.Update(bd.GetDataSet(), $"{toolStripComboBox1.SelectedItem}");
                        }
                    }
                    else if (task == "Update" && toolStripComboBox1.SelectedIndex == 0)
                    {
                        int r = e.RowIndex;
                        float price = float.Parse((dataGridView1.Rows[r].Cells["price"].Value).ToString());
                        float sumPrice = float.Parse((dataGridView1.Rows[r].Cells["count"].Value).ToString());
                        for (int i = 1; i <= dataGridView1.Columns.Count - 1; i++)
                        {
                            bd.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"].Rows[r][$"{(dataGridView1.Columns[i].Name).ToString()}"] = dataGridView1.Rows[r].Cells[$"{(dataGridView1.Columns[i].Name).ToString()}"].Value;
                        }
                        bd.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"].Rows[r]["sumPrice"] = price * sumPrice;

                        sqlDataAdapter.Update(bd.GetDataSet(), $"{toolStripComboBox1.SelectedItem}");

                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = "Delete";
                    }
                    else if (task == "Update" && toolStripComboBox1.SelectedIndex != 0)
                    {
                        int r = e.RowIndex;
                        for (int i = 1; i <= dataGridView1.Columns.Count - 1; i++)
                        {
                            bd.GetDataSet().Tables[$"{toolStripComboBox1.SelectedItem}"].Rows[r][$"{(dataGridView1.Columns[i].Name).ToString()}"] = dataGridView1.Rows[r].Cells[$"{(dataGridView1.Columns[i].Name).ToString()}"].Value;
                        }

                        sqlDataAdapter.Update(bd.GetDataSet(), $"{toolStripComboBox1.SelectedItem}");

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

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox1.SelectedIndex)
            {
                case 0:
                    {
                        if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 0)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Id = {float.Parse(toolStripTextBox1.Text)}";
                        }
                        else if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 1)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"id_product = {float.Parse(toolStripTextBox1.Text)}";
                        }
                        else if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 2)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"id_storage = {float.Parse(toolStripTextBox1.Text)}";
                        }

                        else if (!IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 3)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"date LIKE '%{toolStripTextBox1.Text}%'";
                        }

                        else if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 4)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"count = {float.Parse(toolStripTextBox1.Text)}";
                        }
                        else if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 5)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"price = {float.Parse(toolStripTextBox1.Text)}";
                        }
                        else if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 6)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"sumPrice = {float.Parse(toolStripTextBox1.Text)}";
                        }
                        break;
                    }
                case 1:
                    {
                        if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 0)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Id = {float.Parse(toolStripTextBox1.Text)}";
                        }
                        else if (!IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 1)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"adress LIKE '%{toolStripTextBox1.Text}%'";
                        }
                        else if (toolStripComboBox2.SelectedIndex == 2)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"telephone LIKE '%{toolStripTextBox1.Text}%'";
                        }
                        else if (!IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 3)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"header LIKE '%{toolStripTextBox1.Text}%'";
                        }
                        break;
                    }
                case 2:
                    {
                        if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 0)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"IdProvider = {float.Parse(toolStripTextBox1.Text)}";
                        }
                        else if (toolStripComboBox2.SelectedIndex == 1)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"inn LIKE '%{toolStripTextBox1.Text}%'";
                        }
                        else if (!IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 2)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"nameProvider LIKE '%{toolStripTextBox1.Text}%'";
                        }
                        else if (!IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 3)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"adress LIKE '%{toolStripTextBox1.Text}%'";
                        }
                        break;
                    }
                case 3:
                    {
                        if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 0)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"IdProduct = {float.Parse(toolStripTextBox1.Text)}";
                        }
                        else if (toolStripComboBox2.SelectedIndex == 1)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"article = {int.Parse(toolStripTextBox1.Text)}";
                        }
                        else if (!IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 2)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"name LIKE '%{toolStripTextBox1.Text}%'";
                        }
                        else if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 3)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"id_provider = {float.Parse(toolStripTextBox1.Text)}";
                        }
                        else if (IsNumeric(toolStripTextBox1.Text) && toolStripComboBox2.SelectedIndex == 4)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"id_supply = {float.Parse(toolStripTextBox1.Text)}";
                        }
                        break;
                    }
            }
        }

        public static bool IsNumeric(object Expression)
        {
            float retNum;

            bool isNum = float.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedItem != null)
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = null;
                toolStripComboBox2.SelectedItem = null;
                toolStripTextBox1.Text = null;
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите таблицу!");
            }
        }

        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1.Visible = true;
        }
    }
}
