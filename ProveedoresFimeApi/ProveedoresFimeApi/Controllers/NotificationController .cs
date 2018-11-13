using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

namespace SendgridMailApp.Controllers {
    [Route("api/[controller]")]
    public class NotificationController : Controller {
        private readonly IConfiguration _configuration;

        public NotificationController(IConfiguration configuration) {
            _configuration=configuration;
        }

        [Route("SendNotification/{id}/{to}")]
        public async Task PostMessage([FromRoute] int id, [FromRoute] string to) {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("proveedoresfime@no-reply.com", "ProveedoresFime");
            List<EmailAddress> tos = new List<EmailAddress>
            {
              new EmailAddress(to)
          };

            var subject = "Solicitud Cotización ";
            var htmlContent = "<strong>Se solicito una cotización con el número"+id.ToString()+"</strong>\n";
            htmlContent+="<a href=http://labdispweb.azurewebsites.net/id/"+id.ToString()+">Haz Click Aqui!</a>.<br> ";
            var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, false);
            var response = await client.SendEmailAsync(msg);
        }
    }
}