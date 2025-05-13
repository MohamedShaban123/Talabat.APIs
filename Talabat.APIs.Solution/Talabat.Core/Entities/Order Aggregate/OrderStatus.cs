using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public  enum OrderStatus
    {
        // we use this Data Notations to store enum as a string in database

        [EnumMember(Value ="Pending")]
        Pending ,
        [EnumMember(Value = "Payment Received")]
        PaymentReceived,
        [EnumMember(Value ="Payment Failed")]
        PaymentFailed
    }
}
