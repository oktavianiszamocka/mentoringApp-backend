﻿using MentorApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(WelcomeRequest request);
        Task SendResetPasswordEmailAsync(string email);
        Task ResetPasswordWithTokenAsync(string newPassword, string resetToken);
        Task InviteToProject(String userName, String email, String projectName);
    }
}
