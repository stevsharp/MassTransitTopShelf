using MassTransit;
using Sample.MessageTypes;
using System;
using System.Configuration;

namespace Client
{
    public static class BusExtenstions
    {
        public static IRequestClient<ISimpleRequest, ISimpleResponse> CreateRequestClient(this IBusControl busControl)
        {
            var serviceAddress = new Uri(ConfigurationManager.AppSettings["ServiceAddress"]);
            var client = busControl.CreateRequestClient<ISimpleRequest, ISimpleResponse>(serviceAddress, TimeSpan.FromSeconds(10));
            return client;
        }

        public static IBusControl CreateBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(x => x.Host(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"]), h =>
            {
                h.Username("test");
                h.Password("test");
            }));
        }

    }
}
