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

namespace ApplicationStorage.InsertLogic
{
    class InsertProduct
    {
        public InsertProduct(ProductEntity productEntity, SqlConnection sqlConnection)
        {
            try
            {
                SqlCommand commandProduct = new SqlCommand("INSERT INTO [Product] (article, name, id_provider) VALUES (@article, @name, @id_provider)", sqlConnection);
                commandProduct.Parameters.AddWithValue("@article", productEntity.article);
                commandProduct.Parameters.AddWithValue("@name", productEntity.name);
                commandProduct.Parameters.AddWithValue("@id_provider", productEntity.id_provider);
                commandProduct.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateIdSupply(int id_supply, int id_product, SqlConnection sqlConnection)
        {
            try
            {
                SqlCommand commandProduct1 = new SqlCommand($"UPDATE Product SET id_supply={id_supply} WHERE IdProduct={id_product}", sqlConnection);
                commandProduct1.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
