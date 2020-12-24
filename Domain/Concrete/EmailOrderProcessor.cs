using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "serhiydutchyn@gmail.com";
        public string MailFromAddress = "wearstore@example.com";
        public bool UseSsl = true;
        public string Username = "serhii.dutchyn.knm.2018@lpnu.ua";
        public string Password = "21.10.2000";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"C:\Users\serhi\OneDrive\Робочий стіл\курсова код\eShop_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Entities.Cart cart, Entities.ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = true;
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("Новий заказ зроблено")
                .AppendLine("---")
                .AppendLine("Товари:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Wear.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (всього: {2} uan)", line.Quantity, line.Wear.Name, subtotal)
                        .AppendLine();
                    
                }

                body.AppendFormat("Загальна вартість: {0} uan", cart.ComputeTotalValue())
                    .AppendLine();
                body.AppendFormat("Телефон:")
                    .AppendLine(shippingDetails.Phone);
                body.AppendFormat("Ім'я:")
                    .AppendLine(shippingDetails.Name);
                body.AppendFormat("Доставка:")
                    .AppendLine("Вулиця:")
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine("Місто:")
                    .AppendLine(shippingDetails.City)
                    .AppendLine("Країна:")
                    .AppendLine(shippingDetails.Country)
                    .AppendLine("---")
                    .AppendFormat("Подарунковий пакет: {0}", shippingDetails.GiftWrap ? "Так" : "Ні");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Новий заказ зроблено",
                    body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            }
        }
    }
}
