﻿using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Abstractions
{
    public interface ICustomerService : IBaseService<Customer>
    {
    }
}
