using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PhasmophobiaSaveEditor.Logging.Layouts;

namespace PhasmophobiaSaveEditor.Logging
{
    public class LogFactory : ILogFactory
    {
        private static readonly object SyncObject = new object();
        private static ILogFactory defaultInstance;

        private readonly ConcurrentDictionary<LoggerCacheKey, ILogger> cache = new ConcurrentDictionary<LoggerCacheKey, ILogger>();
        private IDictionary<string, ILogLayout> logLayoutsCache;

        public static ILogFactory Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    lock (SyncObject)
                    {
                        if (defaultInstance == null)
                        {
                            defaultInstance = new LogFactory();
                        }
                    }
                }

                return defaultInstance;
            }
        }

        public LoggerConfig Config { get; set; }

        public ILogger GetLogger(string name) => this.GetLoggerThreadSafe(name, Logger.DefaultLoggerType);

        public IDictionary<string, ILogLayout> GetLogLayouts()
        {
            return this.logLayoutsCache ?? (this.logLayoutsCache = AppDomain.CurrentDomain.GetAssemblies()
                                                                            .SelectMany(x => x.GetTypes())
                                                                            .Where(x => typeof(ILogLayout).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                                                                            .Select(Activator.CreateInstance).OfType<ILogLayout>()
                                                                            .ToDictionary(x => (x.GetType().GetCustomAttribute(typeof(LogLayoutAttribute)) as LogLayoutAttribute)?.Name, x => x));
        }

        private ILogger GetLoggerThreadSafe(string name, Type loggerType)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Name of logger cannot be null");
            }

            var loggerCacheKey = new LoggerCacheKey(name, loggerType ?? typeof(Logger));
            if (this.cache.TryGetValue(loggerCacheKey, out var result))
            {
                return result;
            }

            loggerCacheKey = new LoggerCacheKey(loggerCacheKey.Name, typeof(Logger));
            var logger = new Logger();

            logger.Initialize(name, this);
            this.cache.TryAdd(loggerCacheKey, logger);
            return logger;
        }
    }
}