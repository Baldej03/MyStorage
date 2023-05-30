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
    class InsertStorage
    {
        public InsertStorage(StorageEntity storageEntity, SqlConnection sqlConnection)
        {
            try
            {
                SqlCommand commandStorage = new SqlCommand("INSERT INTO [Storage] (adress, telephone, name, surname, middlename) VALUES (@adress, @telephone, @name, @surname, @middlename)", sqlConnection);
                commandStorage.Parameters.AddWithValue("@adress", storageEntity.adress);
                commandStorage.Parameters.AddWithValue("@telephone", storageEntity.telephone);
                commandStorage.Parameters.AddWithValue("@name", storageEntity.name);
                commandStorage.Parameters.AddWithValue("@surname", storageEntity.surname);
                commandStorage.Parameters.AddWithValue("@middlename", storageEntity.middlename);
                commandStorage.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
