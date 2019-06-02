//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample01;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample02;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample03;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample04;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample04.EmitLogDirect;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample05;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample06;
using Tutor2019.Core.Base;
using Tutor2019.Root.Console;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root
{
    /// <summary>
    /// Корень. Приложение.
    /// </summary>
    public class RootApp : RootConsoleApp
    {
        #region Fields

        private static readonly Lazy<RootApp> lazy = new Lazy<RootApp>(() => new RootApp());

        #endregion Fields

        #region Properties

        /// <summary>
        /// Экземпляр.
        /// </summary>
        public static RootApp Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        #endregion Properties

        #region Constructors

        private RootApp()
            : base("RabbitMQ tutorial")
        {
        }

        #endregion Constructors

        #region Protected methods

        /// <inheritdoc/>
        protected sealed override ILogger CreateAppLogger()
        {
            return CreateLogger<RootApp>();
        }

        /// <inheritdoc/>
        protected sealed override void OnStarted()
        {
            var samples = new CoreBaseSample[]
            {
                new RootSample01Receive(
                    "sample-01-receive",
                    CreateLogger<RootSample01Receive>()
                    ),
                new RootSample01Send(
                    "sample-01-send",
                    CreateLogger<RootSample01Send>()
                    ),
                new RootSample02NewTask(
                    "sample-02-new-task",
                    CreateLogger<RootSample02NewTask>()
                    ),
                new RootSample02Worker(
                    "sample-02-worker",
                    CreateLogger<RootSample02Worker>()
                    ),
                new RootSample03EmitLogFanout(
                    "sample-03-emit-log-fanout",
                    CreateLogger<RootSample03EmitLogFanout>()
                    ),
                new RootSample03ReceiveLogsFanout(
                    "sample-03-receive-logs-fanout",
                    CreateLogger<RootSample03ReceiveLogsFanout>()
                    ),
                new RootSample04EmitLogDirect(
                    "sample-04-emit-log-direct",
                    CreateLogger<RootSample04EmitLogDirect>()
                    ),
                new RootSample04ReceiveLogsDirect(
                    "sample-04-receive-logs-direct",
                    CreateLogger<RootSample04ReceiveLogsDirect>()
                    ),
                new RootSample05EmitLogTopic(
                    "sample-05-emit-log-topic",
                    CreateLogger<RootSample05EmitLogTopic>()
                    ),
                new RootSample05ReceiveLogsTopic(
                    "sample-05-receive-logs-topic",
                    CreateLogger<RootSample05ReceiveLogsTopic>()
                    ),
                new RootSample06Client(
                    "sample-06-client",
                    CreateLogger<RootSample06Client>()
                    ),
                new RootSample06Server(
                    "sample-06-server",
                    CreateLogger<RootSample06Server>()
                    )
            };

            foreach (var sample in samples)
            {
                CommandLine.Command(sample.Name, config => Configure(config, sample));
            }
        }

        #endregion Protected methods

        #region Private methods

        private void Configure(CommandLineApplication commandLine, CoreBaseSample sample)
        {
            AddHelpOption(commandLine);

            var context = new RootContext(sample, commandLine);

            commandLine.OnExecute(() => Execute(context));
        }

        private int Execute(RootContext context)
        {
            var message = context.Message?.Value;

            if (!string.IsNullOrEmpty(message))
            {
                if (context.Sample02NewTask != null)
                {
                    context.Sample02NewTask.Message = message;
                }

                if (context.Sample03EmitLogFanout != null)
                {
                    context.Sample03EmitLogFanout.Message = message;
                }

                if (context.Sample04EmitLogDirect != null)
                {
                    context.Sample04EmitLogDirect.Message = message;
                }

                if (context.Sample05EmitLogTopic != null)
                {
                    context.Sample05EmitLogTopic.Message = message;
                }

                if (context.Sample06Client != null)
                {
                    context.Sample06Client.Message = message;
                }
            }

            if (context.Severity != null && context.Severity.HasValue())
            {
                var severityString = context.Severity.Value();

                if (!int.TryParse(severityString, out int severityValue))
                {
                    severityValue = 0;
                }

                if (context.Sample04EmitLogDirect != null)
                {
                    RootSample04EmitLogDirectSeverities severity;

                    if (severityValue >= 1 && severityValue <= 3)
                    {
                        severity = (RootSample04EmitLogDirectSeverities)severityValue;
                    }
                    else
                    {
                        throw new Exception($"The severity '{severityString}' is unknown");
                    }

                    context.Sample04EmitLogDirect.Severity = severity;
                }
            }

            if (context.Severities != null && context.Severities.HasValue())
            {
                var severities = new List<RootSample04EmitLogDirectSeverities>(context.Severities.Values.Count);

                foreach (var severityString in context.Severities.Values)
                {
                    if (!int.TryParse(severityString, out int severityValue))
                    {
                        severityValue = 0;
                    }

                    if (severityValue >= 1 && severityValue <= 3)
                    {
                        severities.Add((RootSample04EmitLogDirectSeverities)severityValue);
                    }
                    else
                    {
                        throw new Exception($"The severity '{severityString}' is unknown");
                    }
                }

                if (context.Sample04ReceiveLogsDirect != null)
                {
                    context.Sample04ReceiveLogsDirect.Severities = severities;
                }
            }

            if (context.Route != null && context.Route.HasValue())
            {
                var severity = context.Route.Value();

                if (context.Sample05EmitLogTopic != null)
                {
                    context.Sample05EmitLogTopic.Route = severity;
                }
            }

            if (context.Routes != null && context.Routes.HasValue())
            {
                var routes = new List<string>(context.Routes.Values.Count);

                foreach (var route in context.Routes.Values)
                {
                    routes.Add(route);
                }

                if (context.Sample05ReceiveLogsTopic != null)
                {
                    context.Sample05ReceiveLogsTopic.Routes = routes;
                }
            }

            context.Sample.Run(CancellationToken);

            return 0;
        }

        #endregion Private methods
    }
}
