using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Application.Services.Interfaces;

public interface IEmailService
{
    /// <summary>
    /// Metado responsavel por enviar o email
    /// </summary>
    /// <param name="to">Para quem o email vai ser enviado</param>
    /// <param name="subject">Tema do email</param>
    /// <param name="body">Corpo do email</param>
    /// <returns></returns>
    Task SendEmailAsync(string to, string subject, string body);
}
