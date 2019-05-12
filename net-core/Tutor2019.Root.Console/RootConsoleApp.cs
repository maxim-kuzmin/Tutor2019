//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading;

namespace Tutor2019.Root.Console
{
    public abstract class RootConsoleApp
    {
        #region Constants

        private const string LOG_OUTPUT_TEMPLATE = "{NewLine}[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}{NewLine}   {Message:lj}{NewLine}{Exception}";

        #endregion Constants

        #region Properties

        /// <summary>
        /// Токен отмены.
        /// </summary>
        protected CancellationToken CancellationToken { get; private set; }

        /// <summary>
        /// Командная строка.
        /// </summary>
        protected CommandLineApplication CommandLine { get; private set; }

        /// <summary>
        /// Регистратор.
        /// </summary>
        protected Microsoft.Extensions.Logging.ILogger Logger { get; private set; }

        /// <summary>
        /// Область применения сервисов.
        /// </summary>
        protected IServiceScope ServiceScope { get; private set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя приложения.</param>
        public RootConsoleApp(string name)
        {
            CommandLine = new CommandLineApplication
            {
                Name = name
            };

            AddHelpOption(CommandLine);

            CommandLine.OnExecute(
                () =>
                {
                    System.Console.WriteLine($"{CommandLine.Name} application.");

                    CommandLine.ShowHelp();

                    return 0;
                });

            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceScope = services.BuildServiceProvider().CreateScope();

            Logger = CreateAppLogger();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Запустить.
        /// </summary>
        /// <param name="args">Аргументы.</param>
        public void Run(string[] args)
        {
            OnStarted();

            var cts = new CancellationTokenSource();

            CancellationToken = cts.Token;

            System.Console.CancelKeyPress += (sender, eventArgs) =>
            {
                OnStopped();

                Logger.LogInformation("Application is shutting down...");

                cts.Cancel();

                ServiceScope.Dispose();

                eventArgs.Cancel = true;
            };

            var exitCode = 0;

            try
            {
                exitCode = CommandLine.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                ex.Command.ShowHelp();

                exitCode = 1;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(new EventId(), ex, "Unexpected error occured. See logs for details.");

                exitCode = -1;
            }
            finally
            {
                Logger.LogInformation("Application is shutting down with code {exitCode}.", exitCode);
            }

            Environment.Exit(exitCode);
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        /// Добавить возможность получения помощи.
        /// </summary>
        /// <param name="commandLine">Командная строка.</param>
        protected void AddHelpOption(CommandLineApplication commandLine)
        {
            commandLine.HelpOption("-?|-h|--help");
        }

        /// <summary>
        /// Конфигурировать сервисы.
        /// </summary>
        /// <param name="services">Сервисы.</param>
        protected virtual void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console(outputTemplate: LOG_OUTPUT_TEMPLATE)
                .CreateLogger();

            services.AddLogging(builder => builder.AddSerilog(Log.Logger, true));
        }

        /// <summary>
        /// Создать регистратор приложения.
        /// </summary>
        /// <returns>Регистратор.</returns>
        protected abstract Microsoft.Extensions.Logging.ILogger CreateAppLogger();

        /// <summary>
        /// Создать регистратор.
        /// </summary>
        /// <returns>Регистратор.</returns>
        /// <typeparam name="T">Тип регистратора.</typeparam>
        protected Microsoft.Extensions.Logging.ILogger CreateLogger<T>()
        {            
            return ServiceScope.ServiceProvider.GetRequiredService<ILogger<T>>();
        }

        /// <summary>
        /// Обработать событие запуска.
        /// </summary>
        protected virtual void OnStarted()
        {
        }

        /// <summary>
        /// Обработать событие остановки.
        /// </summary>
        protected virtual void OnStopped()
        {
        }

        #endregion Protected methods
    }
}
