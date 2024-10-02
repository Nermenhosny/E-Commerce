using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.OrderEntity
{
    public enum OrderPaymentStatus
    { 
        Pending,
        Received,
        Failed
    }

    public class Order : BaseEntity<Guid>
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }   =  DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; } //object not Entity 
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }   
        public OrderPaymentStatus OrderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public IReadOnlyList<OrderItem>OrderItems { get; set; }
        public decimal SubTotal { get; set; }//without the price of shipping
        public decimal GetTotal
            => SubTotal + DeliveryMethod.Price;
        public string? PaymentIntentId { get; set; }

        public string? BasketId { get; set; }


    }
}
