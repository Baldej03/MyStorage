using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationStorage.Entityes
{
    class StorageEntity
    {
        public string adress { get; private set; }
        public string telephone { get; private set; }
        public string name { get; private set; }
        public string surname { get; private set; }
        public string middlename { get; private set; }

        public StorageEntity(string adress, string telephone, string name, string surname, string middlename)
        {
            this.adress = adress;
            this.telephone = telephone;
            this.name = name;
            this.surname = surname;
            this.middlename = middlename;
        }
    }
}
