using ParkAndRide.App.ConfigureJSON;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace ParkAndRide.App.EMail
{
    public class MailModel:IMailModel
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public MailModel() { }

        public MailModel(MailModel mail)
        {
            this.Name = mail.Name;
            this.Surname = mail.Surname;
            this.Email = mail.Email;
            this.Message = mail.Message;
        }

        public void sendEmail()
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Daniel", ConfigureOption.ConfigSMTP.Value.senderLogin));
            message.To.Add(new MailboxAddress("Daniel", ConfigureOption.ConfigSMTP.Value.senderLogin));

            message.Subject = "Test";
            message.Body = new TextPart("plain") {
                Text = this.Message
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(ConfigureOption.ConfigSMTP.Value.Server, ConfigureOption.ConfigSMTP.Value.port, false);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(ConfigureOption.ConfigSMTP.Value.senderLogin, ConfigureOption.ConfigSMTP.Value.senderPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }catch(Exception ex)
                {
                    Console.WriteLine("MailModel.sendEmail: " + ex.Message);
                }
            }

        }

    }
}
