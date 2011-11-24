using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Dominion
{
    public interface INotification
    {
        [OperationContract(IsOneWay = true)]
        void OnNotification(string message);
    }
}