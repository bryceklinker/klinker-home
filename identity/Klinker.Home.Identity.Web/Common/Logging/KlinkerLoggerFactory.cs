using System.Collections;
using System.Collections.Immutable;
using Serilog;
using Serilog.Formatting.Compact;
using ILogger = Serilog.ILogger;

namespace Klinker.Home.Identity.Web.Common.Logging;

public record LoggerSettings : IEnumerable<KeyValuePair<string, object>>
{
    private readonly ImmutableDictionary<string, object> _settings;

    public LoggerSettings(string appName)
    {
        _settings = ImmutableDictionary<string, object>.Empty.Add("AppName", appName);
    }

    private LoggerSettings(ImmutableDictionary<string, object> settings)
    {
        _settings = settings;
    }

    public LoggerSettings With(string key, object value)
    {
        return new LoggerSettings(_settings.Add(key, value));
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        return _settings.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static class KlinkerLoggerFactory
{
    public static LoggerConfiguration CreateConfig(LoggerSettings settings)
    {
        var config = new LoggerConfiguration().Enrich
            .FromLogContext()
            .MinimumLevel.Information()
            .WriteTo.Console(new RenderedCompactJsonFormatter());

        foreach (var (key, value) in settings)
            config.Enrich.WithProperty(key, value);
        return config;
    }

    public static ILogger CreateLogger(LoggerSettings settings)
    {
        return CreateConfig(settings).CreateLogger();
    }
}
