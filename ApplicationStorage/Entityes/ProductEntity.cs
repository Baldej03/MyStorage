using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationStorage
{
    public class ProductEntity
    {
        public int article { get; private set; }
        public string name { get; private set; }
        public int id_provider { get; private set; }

        public ProductEntity(int article, string name, int id_provider)
        {
            this.article = article;
            this.name = name;
            this.id_provider = id_provider;
        }
    }
}
