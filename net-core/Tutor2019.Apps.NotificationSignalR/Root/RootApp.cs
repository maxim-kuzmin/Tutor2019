//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System;
using Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01;
using Tutor2019.Core.Base;
using Tutor2019.Root.Console;

namespace Tutor2019.Apps.NotificationSignalR.Root
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
            : base("SignalR tutorial")
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
                new RootSample01Producer("sample-01-producer", CreateLogger<RootSample01Producer>()),
                new RootSample01Server("sample-01-server", CreateLogger<RootSample01Server>())
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

            var model = new RootModel(sample, commandLine);

            commandLine.OnExecute(() => Execute(model));
        }

        private int Execute(RootModel model)
        {
            model.Sample.Run(CancellationToken);

            return 0;
        }

        #endregion Private methods
    }
}
