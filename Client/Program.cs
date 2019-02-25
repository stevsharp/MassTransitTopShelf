using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using MassTransit.Util;
using Sample.MessageTypes;

namespace Client
{
    class Program
    {
        static void Main()
        {
            ConfigureLogger();

            Log4NetLogger.Use();

            IBusControl busControl = CreateBus();

            try
            {
                TaskUtil.Await(() => busControl.StartAsync());

                IRequestClient<ISimpleRequest, ISimpleResponse> client = busControl.CreateRequestClient();

                for (;;)
                {
                    Console.Write("Enter customer id (quit exits): ");
                    string customerId = Console.ReadLine();
                    if (customerId == "quit")
                        break;

                    Task.Run(async () =>
                    {
                        var response = await client.Request(new SimpleRequest(customerId));

                        Console.WriteLine("Customer Name: {0}", response.CusomerName);

                    }).Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                busControl.Stop();
            }

            Console.ReadLine();
        }

        public static IBusControl CreateBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(x => x.Host(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"]), h =>
            {
                h.Username("test");
                h.Password("test");
            }));
        }

        static void ConfigureLogger()
        {
            const string logConfig = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
            <log4net>
              <root>
                <level value=""INFO"" />
                <appender-ref ref=""console"" />
              </root>
              <appender name=""console"" type=""log4net.Appender.ColoredConsoleAppender"">
                <layout type=""log4net.Layout.PatternLayout"">
                  <conversionPattern value=""%m%n"" />
                </layout>
              </appender>
            </log4net>";

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(logConfig)))
            {
                XmlConfigurator.Configure(stream);
            }
        }
       
    }
}