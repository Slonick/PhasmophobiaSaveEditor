using PhasmophobiaSaveEditor.Utils;

namespace PhasmophobiaSaveEditor.Logging
{
    public class LogManager
    {
        private static readonly object SyncObject = new object();

        private static LogManager instance;

        private readonly ILogFactory logFactory = LogFactory.Default;

        public LogManager(ILogFactory logFactory)
        {
            this.logFactory = logFactory;
        }

        private LogManager() { }

        public static LogManager Default
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncObject)
                    {
                        if (instance == null)
                        {
                            instance = new LogManager();
                        }
                    }
                }

                return instance;
            }
        }

        public ILogger GetCurrentClassLogger() => this.logFactory.GetLogger(StackTraceUtils.GetClassFullName());

        public void SetConfig(LoggerConfig loggerConfig)
        {
            this.logFactory.Config = loggerConfig;
        }
    }
}