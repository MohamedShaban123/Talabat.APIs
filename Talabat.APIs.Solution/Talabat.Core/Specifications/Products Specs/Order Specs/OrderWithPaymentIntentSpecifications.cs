﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.Products_Specs.Order_Specs
{
 public   class OrderWithPaymentIntentSpecifications : BaseSpecifications<Order>
    {

        public OrderWithPaymentIntentSpecifications(string paymentIntentId)
            : base(O => O.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
