using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationStorage
{
    public class SupplyEntity
    {
        public int id_product { get; private set; }
        public int id_storage { get; private set; }
        public string date { get; private set; }
        public float count { get; private set; }
        public float price { get; private set; }
        public float sumPrice { get; private set; }

        public SupplyEntity(int id_product, int id_storage, string date, float count, float price, float sumPrice)
        {
            this.id_product = id_product;
            this.id_storage = id_storage;
            this.date = date;
            this.count = count;
            this.price = price;
            this.sumPrice = sumPrice;
        }
    }
}
