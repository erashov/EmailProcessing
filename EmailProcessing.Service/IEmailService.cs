using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailProcessing.Service
{
   public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task<int> PaerserEmailAsync(string imapserv, string email, string password, string subject);
    }
}
