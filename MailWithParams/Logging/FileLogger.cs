
using System.IO;

namespace MailWithParams
{
    internal class FileLogger : ILogService
    {

        public void Write(string message, string symbol)
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string directoryName = System.IO.Path.GetDirectoryName(path);
            using (StreamWriter sw = File.AppendText($"{directoryName}\\{symbol}log.txt"))
            {
                sw.WriteLine(message);
                sw.WriteLine("__________________________________________________________________________________________");
            }
        }
    }
}
