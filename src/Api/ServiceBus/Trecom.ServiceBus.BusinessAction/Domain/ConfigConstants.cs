using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trecom.ServiceBus.BusinessAction.Domain
{
    public class ConfigConstants
    {
        public const string Compensate = "compensate";
        public const string DefaultRetentionTime = "86400000";
        public const string DefaultCompensateRetentionTime = "86400000";
        public const int DefaultPartition = 2;
        public const int DefaultCompensatePartition = 2;
        public const int DefaultInSyncReplica = 1;
    }
}
