using Store.Data.Entities.IdentittyEntities;

namespace Store.Data.Entities.OrderEntity
{
    public class ShippingAddress
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}