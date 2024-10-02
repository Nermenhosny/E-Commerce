namespace Store.Data.Entities.OrderEntity
{
    public class OrderItem : BaseEntity<Guid>
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductItemOrdered ItemOrdered { get; set; }//object not Entity
        public Guid OrderId { get; set; }   
    }
}