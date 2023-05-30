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

namespace ApplicationStorage.InsertLogic
{
    class InsertProvider
    {
        public InsertProvider(ProviderEntity providerEntity, SqlConnection sqlConnection)
        {
            try
            {
                SqlCommand commandProvider = new SqlCommand("INSERT INTO [Provider] (inn, nameProvider, adress) VALUES (@inn, @nameProvider, @adress)", sqlConnection);
                commandProvider.Parameters.AddWithValue("@inn", providerEntity.inn);
                commandProvider.Parameters.AddWithValue("@nameProvider", providerEntity.nameProvider);
                commandProvider.Parameters.AddWithValue("@adress", providerEntity.adress);
                commandProvider.ExecuteNonQuery().ToString();
            }   
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
