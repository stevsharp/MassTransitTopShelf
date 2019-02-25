using Sample.MessageTypes;
using System;

namespace Client
{
    class SimpleRequest : ISimpleRequest
    {
        readonly string _customerId;
        readonly DateTime _timestamp;

        public SimpleRequest(string customerId)
        {
            _customerId = customerId;
            _timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp => _timestamp;

        public string CustomerId => _customerId;
    }
}
