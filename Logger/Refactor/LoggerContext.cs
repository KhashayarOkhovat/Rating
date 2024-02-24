namespace Logger.Refactor
{
    //  https://www.dofactory.com/net/strategy-design-pattern
    public class LoggerContext
    {
        private ILogger _logger;

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Log(string logText)
        {
            _logger.Log(logText);
        }
    }
}