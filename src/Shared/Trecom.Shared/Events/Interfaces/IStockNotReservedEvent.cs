﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Trecom.Shared.Events.Interfaces
{
    public interface IStockNotReservedEvent:CorrelatedBy<Guid>
    {
        public Guid OrderId { get; set; }

    }
}
