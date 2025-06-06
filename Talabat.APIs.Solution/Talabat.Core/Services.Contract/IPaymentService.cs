﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Services.Contract
{
    public interface IPaymentService
    {
        // First Create Payment Intent
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);

    }
}
