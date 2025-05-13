using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.Products_Specs.Order_Specs
{
    public class OrderSpecifications  :BaseSpecifications<Order>
    {
        public OrderSpecifications( string buyereEmail) 
              : base(O=>O.BuyerEmail == buyereEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDesc(O => O.OrderDate);
        }

        public OrderSpecifications(int orderId , string buyereEmail)
          : base(O => O.BuyerEmail == buyereEmail && O.Id == orderId )
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }


    }
}
