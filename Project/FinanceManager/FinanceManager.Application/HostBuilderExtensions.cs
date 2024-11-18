using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FinanceManager.Application
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddLogging(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

            hostBuilder.UseSerilog(Log.Logger);

            return hostBuilder;
        }

        public static IHostBuilder AddGlobalExceptionHandler(this IHostBuilder hostBuilder)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
            {
                var exception = eventArgs.ExceptionObject as Exception;
                Log.Fatal(exception, "Было перехвачено необработанное исключение в AppDomain");
                Environment.Exit(1);
            };

            TaskScheduler.UnobservedTaskException += (sender, eventArgs) =>
            {
                Log.Fatal(eventArgs.Exception, "Было перехвачено UnobservedTaskException");
                eventArgs.SetObserved();
            };

            return hostBuilder;
        }
    }
}
