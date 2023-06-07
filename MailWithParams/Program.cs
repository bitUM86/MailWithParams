using MailWithParams.Inputs;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace MailWithParams
{
    internal class Program
    {
         static async Task Main(string[] args)
        {
            InputData inputData = new InputData(args);
            EmailService  emailService = new EmailService();
            var message = emailService.CreateMessage(String.Empty, inputData._emailFrom, inputData._emailTo, inputData._subject, inputData._message, inputData._attachmentFileName);
            await emailService.SendEmailAsync(inputData._smtp, inputData._port, inputData._ssl, inputData._emailFromPassword, message);
        }
    }
}
