//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System;
using Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01;
using Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01.Consumer.Enums;
using Tutor2019.Core.Base;
using Tutor2019.Root.Console;

namespace Tutor2019.Apps.MessageBrokerKafka.Root
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
            : base("Apache Karma tutorial")
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
                new RootSample01Consumer("sample-01-consumer", CreateLogger<RootSample01Consumer>()),
                new RootSample01Producer("sample-01-producer", CreateLogger<RootSample01Producer>())
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
            var actionString = context?.Action?.Value;

            if (!string.IsNullOrWhiteSpace(actionString))
            {
                if (!int.TryParse(actionString, out int actionValue))
                {
                    actionValue = 0;
                }

                if (context.Sample01Consumer != null)
                {
                    RootSample01ConsumerActions action;

                    if (actionValue >= 1 && actionValue <= 2)
                    {
                        action = (RootSample01ConsumerActions)actionValue;
                    }
                    else
                    {
                        throw new Exception($"The action '{actionString}' is unknown");
                    }

                    context.Sample01Consumer.Action = action;
                }
            }

            context.Sample.Run(CancellationToken);

            return 0;
        }

        #endregion Private methods
    }
}
