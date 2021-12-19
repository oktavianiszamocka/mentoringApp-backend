﻿using MailKit.Net.Smtp;
using MailKit.Security;
using MentorApp.Helpers;
using MentorApp.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class MailService : IMailService
    {
        //private readonly UserManager<IdentityUser> userManager;
        //UserName, Password and Email
        private readonly IUserRepository _userRepository;
        private readonly Helpers.MailSettings _mailSettings;
        private readonly IConfiguration _configuration;
        public MailService(IOptions<Helpers.MailSettings> mailSettings, IConfiguration configuration, IUserRepository userRepository)
        {
            _mailSettings = mailSettings.Value;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if(mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailAsync(WelcomeRequest request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\ResetMailTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Welcome {request.UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendResetPasswordEmailAsync(string email)
        {
            //var email = "ochaco@gmail.com";
            var userName = "Okta";
            var token = Guid.NewGuid().ToString();
            var apiKey = _configuration["API_KEY"];
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("s16434@pjwstk.edu.pl", "PJATK Mentor");
            //"pjatk.mentoring@gmail.com"
            var to = new EmailAddress(email);
            var subject = "Reseting the password";
            var text = "Reset text";

            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\ResetMailTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("**resetTokenString**", token);
            MailText = MailText.Replace("**username**", userName);
            var html = MailText;

            var message = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                text,
                html
            );

            await _userRepository.SavePasswordResetToken(token, email);
            await client.SendEmailAsync(message);
        }

    }
}
