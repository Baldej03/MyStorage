using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationStorage.Entityes
{
    class ProviderEntity
    {
        public string inn { get; private set; }
        public string nameProvider { get; private set; }
        public string adress { get; private set; }

        public ProviderEntity(string inn, string nameProvider, string adress)
        {
            this.inn = inn;
            this.nameProvider = nameProvider;
            this.adress = adress;
        }
    }
}
