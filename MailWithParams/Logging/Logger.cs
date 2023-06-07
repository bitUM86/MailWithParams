using System;

namespace MailWithParams
{
    internal class Logger
    {
        ILogService _logService;
        public Logger(ILogService logService)
        {
            this._logService = logService;
        }
        public void Log(string message, string symbol)
        {
            _logService.Write($"{ DateTime.Now } \r\n{message}", symbol);
        }
    }
}
