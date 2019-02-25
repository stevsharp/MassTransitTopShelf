﻿using System.IO;
using System.Text;
using log4net.Config;
using MassTransit.Log4NetIntegration.Logging;
using Topshelf;
using Topshelf.Logging;

namespace RequestService
{
    class Program
    {
        static int Main(string[] args)
        {
            ConfigureLogger();

            Log4NetLogWriterFactory.Use();

            Log4NetLogger.Use();

            return (int)HostFactory.Run(x => x.Service<RequestService>());
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