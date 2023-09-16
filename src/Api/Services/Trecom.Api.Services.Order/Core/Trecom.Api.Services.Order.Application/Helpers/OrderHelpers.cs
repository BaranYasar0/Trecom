using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trecom.Api.Services.Order.Application.Helpers;

public static class OrderHelpers
{
    public static int GenerateOrderId()
    {
        return int.Parse(String.Join(DateTime.Today.ToString(), "123"));
    }

    public static int GenerateDeliveryNumber()
    {
        return int.Parse(String.Join(DateTime.Today.ToString(), "456"));
    }
}