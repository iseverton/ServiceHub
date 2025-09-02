using FluentEmail.Core;
using ServiceHub.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Services.Email;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;
    public EmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        await _fluentEmail
            .To(to)
            .Subject(subject)
            .Body(body, isHtml: true)
            .SendAsync();
    }
}
