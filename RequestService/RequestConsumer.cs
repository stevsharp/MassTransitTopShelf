﻿using System.Threading.Tasks;
using MassTransit;
using MassTransit.Logging;
using Sample.MessageTypes;

namespace RequestService
{
    public partial class RequestConsumer : IConsumer<ISimpleRequest>
    {
        readonly ILog _log = Logger.Get<RequestConsumer>();

        public async Task Consume(ConsumeContext<ISimpleRequest> context)
        {
            _log.InfoFormat("Returning name for {0}", context.Message.CustomerId);

            context.Respond(new SimpleResponse
            {
                CusomerName = string.Format("Customer Number {0}", context.Message.CustomerId)
            });
        }
    }
}