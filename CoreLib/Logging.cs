using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace CoreLib
{
    public class Logging
    {
        public static void LogInfo(string str)
        {
            LogManager.GetCurrentClassLogger().Info(str);
        }

        public static void Configure()
        {
            var config = new LoggingConfiguration();

            var target =
                new FileTarget
                {
                    FileName = "${basedir}/"+typeof(Logging).FullName + ".log"
                };

            config.AddTarget("logfile", target);
            
            var rule = new LoggingRule("*",LogLevel.Info, target);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            LogManager.GetCurrentClassLogger().Debug("Using programmatic config");
        }
    }
}
