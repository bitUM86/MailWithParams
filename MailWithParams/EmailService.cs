using MimeKit;
using System.Threading.Tasks;
using System.IO;
using System;
using MailKit.Security;
using System.Collections.Generic;
using System.Linq;

namespace MailWithParams
{
    internal class EmailService
    {
        Logger logService = new Logger(new FileLogger());
        public async Task SendEmailAsync(string smtp, int port, bool ssl, string emailFromPassword, MimeMessage emailMessage)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(smtp, port, ssl);
                try
                {
                    await client.AuthenticateAsync(emailMessage.From.ToString(), emailFromPassword);
                    var response = await client.SendAsync(emailMessage);
                    string attachments = ShowAttachments(emailMessage.Attachments.ToList());
                    logService.Log($"\r\nСтатус: \r\n{response}", emailMessage.Subject);
                    await client.DisconnectAsync(true);
                }
                catch (AuthenticationException ex)
                {
                    logService.Log($"Ошибка авторизации: \r\n {ex.Message}", emailMessage.Subject);
                }
                catch (Exception ex)
                {
                    logService.Log($"Ошибка: \r\n {ex.Message}", emailMessage.Subject);
                }
            }

        }

        public MimeMessage CreateMessage(string emailFromName, string emailFrom, string emailTo, string subject, string message, string attachmentFileName)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailFromName, emailFrom));
            emailMessage.To.Add(new MailboxAddress("", emailTo));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            var builder = new BodyBuilder();
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string directoryName = System.IO.Path.GetDirectoryName(path);
            try
            {
                builder.Attachments.Add($"{directoryName}\\{attachmentFileName}");
            }
            catch (FileNotFoundException ex)
            {
                logService.Log($"Файл не найден. Детали ошибки:\r\n {ex.Message}", subject);
            }
            catch (Exception ex)
            {
                logService.Log($"Ошибка обработки файла:\r\n {ex.Message} ", subject);
            }
            emailMessage.Body = builder.ToMessageBody();
            return emailMessage;
        }

        private string ShowAttachments(List<MimeEntity> attachments)
        {
            string attachmentsInfo = string.Empty;
            foreach (MimeEntity entity in attachments)
            {
                attachmentsInfo += entity.ToString();
            }
            return attachmentsInfo;
        }

    }
}
