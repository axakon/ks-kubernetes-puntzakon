using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using Shared;

namespace puntworker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Subscriber _sub;
        private Task t;

        public Worker(ILogger<Worker> logger, Subscriber sub)
        {
            _sub = sub;
            _logger = logger;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _sub.Dispose();
            await t;
            await base.StopAsync(cancellationToken);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Started listening to messages");
            t = Task.Run(() => {
                _sub.Listen(stoppingToken, message => {
                    
                    _logger.LogInformation($"Composing message..");
                    var mail = new MimeMessage();
                    mail.From.Add(new MailboxAddress("pun@untzakon.se"));
                    mail.Subject = "Untz untz untz PUN INC!";
                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = $"PUN:\n\n{message.Body}\n\nFattar du?";
                    mail.Body = bodyBuilder.ToMessageBody();
                    using (var client = new SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                        client.Connect("in-v3.mailjet.com", 587);
                        client.Authenticate("c96f2c384a3a2617e5689a5bb400ec5a", "76af85ff5df3f536ad94f268875cf7c5");
                        client.SendAsync(mail,
                            new MailboxAddress("pun@untzakon.se"), 
                            new List<MailboxAddress>() { new MailboxAddress("patrik.nyman@axakon.se") }).Wait();
                        client.DisconnectAsync(true).Wait();
                        _logger.LogInformation($"Successfully sent message..");
                    }
                });
            }, stoppingToken);

            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        
    }
}
