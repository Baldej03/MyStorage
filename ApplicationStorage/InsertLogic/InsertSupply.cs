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
    class InsertSupply
    {
        public InsertSupply(SupplyEntity supplyEntity, SqlConnection sqlConnection)
        {
            SqlCommand commandSupply = new SqlCommand("INSERT INTO [Supply] (id_product, id_storage, date, count, price, sumPrice)" +
                " VALUES (@id_product, @id_storage, @date, @count, @price, @sumPrice)", sqlConnection);
            commandSupply.Parameters.AddWithValue("@id_product", supplyEntity.id_product);
            commandSupply.Parameters.AddWithValue("@id_storage", supplyEntity.id_storage);
            commandSupply.Parameters.AddWithValue("@date", supplyEntity.date);
            commandSupply.Parameters.AddWithValue("@count", supplyEntity.count);
            commandSupply.Parameters.AddWithValue("@price", supplyEntity.price);
            commandSupply.Parameters.AddWithValue("@sumPrice", supplyEntity.sumPrice);
            commandSupply.ExecuteNonQuery().ToString();
        }
    }
}
