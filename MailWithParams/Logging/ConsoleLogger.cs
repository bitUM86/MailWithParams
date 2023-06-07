using System;


namespace MailWithParams
{
    internal class ConsoleLogger : ILogService
    {
        public void Write(string message, string symbol) => Console.WriteLine(message);
    }
}
