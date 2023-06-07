using System;
using System.Net.Mail;

namespace MailWithParams.Inputs
{
    internal class InputData
    {
        Logger logService = new Logger(new FileLogger());

        public string _smtp;
        public int _port;
        public bool _ssl;
        public string _emailFrom;
        public string _emailFromPassword;
        public string _emailTo;
        public string _subject;
        public string _message;
        public string _attachmentFileName;
        public bool _noErrors = true;

        public InputData(string[] commandParams)
        {
            if (commandParams.Length == 9)
            {
                _smtp = commandParams[0];

                if (!Int32.TryParse(commandParams[1], out _port))
                {
                    _noErrors = false;
                    logService.Log("Ошибка ввода Порта. (Только целое число)", commandParams[6]);
                }
                if (!Boolean.TryParse(commandParams[2], out _ssl))
                {
                    _noErrors = false;
                    logService.Log("Ошибка ввода SSL. (только true/false)", commandParams[6]);
                }

                _emailFrom = commandParams[3];
                _emailFromPassword = commandParams[4];
                _emailTo = commandParams[5];
                _subject = commandParams[6];
                _message = commandParams[7];
                _attachmentFileName = commandParams[8];
            }
            else
            {
                logService.Log($"Необходимо передать 9 аргументов через пробел без дефисов. \r\nВы передали {commandParams.Length}", commandParams[6]);
            }
        }
    }
}
