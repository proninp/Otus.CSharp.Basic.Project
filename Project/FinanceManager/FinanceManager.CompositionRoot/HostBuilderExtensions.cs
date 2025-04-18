﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FinanceManager.CompositionRoot;
public static class HostBuilderExtensions
{
    public static IHostBuilder AddLogging(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        return hostBuilder.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));
    }

    public static IHostBuilder AddGlobalExceptionHandler(this IHostBuilder hostBuilder)
    {
        AppDomain.CurrentDomain.UnhandledException += (sendet, eventArgs) =>
        {
            var exception = eventArgs.ExceptionObject as Exception;
            Log.Fatal(exception, "An unhandled exception was caught in the AppDomain");
            Environment.Exit(1);
        };

        TaskScheduler.UnobservedTaskException += (sender, eventArgs) =>
        {
            Log.Fatal(eventArgs.Exception, "An UnobservedTaskException was intercepted");
            eventArgs.SetObserved();
        };

        return hostBuilder;
    }
}
