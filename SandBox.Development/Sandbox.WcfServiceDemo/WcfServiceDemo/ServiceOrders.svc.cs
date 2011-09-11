using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceDemo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class ServiceOrders : IServiceOrders
    {

        public List<StockOnHand> GetStockOnHand(int siteId)
        {
            List<StockOnHand> lSoh = new List<StockOnHand>();
            lSoh.Add(new StockOnHand(){Product = 1, ProductDescription = "iPhone", Qunatity = 10});
            lSoh.Add(new StockOnHand() { Product = 2, ProductDescription = "HTC Desire", Qunatity = 5 });
            lSoh.Add(new StockOnHand() { Product = 3, ProductDescription = "HTC WildFire", Qunatity = 20 });
            lSoh.Add(new StockOnHand() { Product = 4, ProductDescription = "Samsung Galaxy", Qunatity = 3 });
            lSoh.Add(new StockOnHand() { Product = 5, ProductDescription = "Sony Erricson", Qunatity = 1 });

            return lSoh;
        }

        public ReservedItem ReserveStock(int siteId, int productSku)
        {
            ReservedItem ri = new ReservedItem(){ProductSku = productSku, ReserveId = 101, SerialId = "23123234234", SiteId = siteId};
            return ri;
        }

        public int UnReserveStock(ReservedItem ri)
        {
            return 0;
        }

        public string CreateOrder(Order order)
        {
            return "BSB0001";
        }
    }
}
