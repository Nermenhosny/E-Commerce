using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.Order
{
    public class OrderWithItemsSpecification :BaseSpecification<Data.Entities.OrderEntity.Order>
    {
        public OrderWithItemsSpecification(Guid id ,String buyerEmail)
            :base(order => order.BuyerEmail == buyerEmail && order.Id == id ) 
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDescending(order => order.OrderDate);

        }
        public OrderWithItemsSpecification( String buyerEmail)
         : base(order => order.BuyerEmail == buyerEmail )
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);

        }
    }
}
