using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceDemo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceOrders
    {
        [OperationContract]
        List<StockOnHand> GetStockOnHand(int siteId);

        [OperationContract]
        ReservedItem ReserveStock(int siteId, int productSku);

        [OperationContract]
        int UnReserveStock(ReservedItem ri);

        [OperationContract]
        string CreateOrder(Order order);
    }

    [DataContract]
    public class ReservedItem
    {
        [DataMember]
        public int SiteId { get; set; }
        [DataMember]
        public int ProductSku { get; set; }
        [DataMember]
        public int ReserveId { get; set; }
        [DataMember]
        public string SerialId { get; set; }
    }

    [DataContract]
    public class Order
    {
        [DataMember]
        public int SiteId { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public List<OrderLine> OrderLines { get; set; }

    }

    [DataContract]
    public class OrderLine
    {
        [DataMember]
        public int ProductSku { get; set; }
        [DataMember]
        public int ReserveId { get; set; }
        [DataMember]
        public int SerialId { get; set; }
    }

    [DataContract]
    public class StockOnHand
    {
        [DataMember]
        public int Product { get; set; }
        [DataMember]
        public string ProductDescription { get; set; }
        [DataMember]
        public int Qunatity { get; set; }
    }


    
}
