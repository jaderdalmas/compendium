using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Compendium
{
  public static class Program
  {
    private const string AppName = "Compendium";
    private const string AppType = "Application";
    public static void Main(string[] args)
    {
      if (!EventLog.SourceExists(AppName))
        EventLog.CreateEventSource(AppName, AppType);

      string cnn = "Username=postgres;Password=;Host=localhost;Port=;Database=compendium";
      IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
      {
        { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
        { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
        { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
        { "raise_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
        { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
        { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
        { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
        { "machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
      };
      Log.Logger = new LoggerConfiguration() // Configure Serilog for logging
        .Enrich.FromLogContext()
        .WriteTo.PostgreSql(cnn, "tlog", columnWriters, period: new TimeSpan(0, 0, 30), schemaName: "public", needAutoCreateTable: true, restrictedToMinimumLevel: LogEventLevel.Warning)
        .CreateLogger();

      try { CreateHostBuilder(args).Build().Run(); }
      finally { Log.CloseAndFlush(); }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
      .ConfigureLogging((context, logging) =>
      {
        logging.AddEventLog(new EventLogSettings { SourceName = AppName, LogName = AppType });
      })
      .ConfigureWebHostDefaults(webBuilder =>
      {
        webBuilder.UseStartup<Startup>();
      })
      .UseSerilog();
  }
}
