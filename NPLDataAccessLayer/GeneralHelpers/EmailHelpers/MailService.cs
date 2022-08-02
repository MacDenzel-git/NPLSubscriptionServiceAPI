using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
 

namespace NPLDataAccessLayer.GeneralHelpers
{
    public class MailService : IMailService
    {

        //Action - three types of actions that can be sent (Recover = Password recovery, Activation = Account Activation, SpecialMail = To send a particularEmail
        //Send to Multiple emails = add multiple emails to RecepientEmail separated by a comma
        //Copy Multiple emails = add multiple emails to CC separated by a comma
         //OTP is used in cases of password recovery to hold the otp for reset 
        public async Task<OutputHandler> EmailMember(string actionType, 
         EmailProperties emailProperties
          )
        {
            try
            {
                var message = new MailMessage();
                if (actionType.Equals("Recover"))
                {
                    var code = HttpUtility.UrlEncode(emailProperties.PasswordResetOTP); //the token is not url friend it changes, so encode it first 
                    //var qoute = @""";
                    string url = $"href=\"{emailProperties.PasswordResetUrlLink}?Token={code}\""; //resetLinkUrl is the url to the application sending the reset request
                    message.Subject = emailProperties.Subject;
                    message.Body = String.Format($"Click <a {url}> HERE </a> to reset your password. Thank you");
                }
                else if (actionType.Equals("NewsLetter"))
                {
                    message.Subject = emailProperties.Subject;
                    message.Body = string.Format(emailProperties.EmailBody);
                    message.IsBodyHtml = true;
                }
                else if (actionType == "SubscriptionPaymentSuccess")
                {

                    message.Subject = emailProperties.Subject;
                    message.Body = string.Format(emailProperties.EmailBody);
                    message.IsBodyHtml = true;
                }
                

                message.From = new MailAddress("denzelmac8@gmail.com");
                message.CC.Add(new MailAddress("DenzelMachowa@gmail.com"));
                message.To.Add(emailProperties.RecepientEmail);
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "denzelmac8@gmail.com",  // replace with valid value
                        Password = "ccbsxapqvtmobxnl"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);

                    return new OutputHandler
                    {
                        IsErrorOccured = false,
                        Message = "Email Sent Succesfully"
                    };
                }
            }
            catch (Exception ex)
            {
                return new OutputHandler { IsErrorOccured = true, Message = "Something went wrong, Please Ensure that your email is valid and try gain" };
            }
        }

        public async Task<OutputHandler> EmailNewsLetter(List<ClientDTO> clients, 
        EmailProperties emailProperties
         )
        {
            try
            {
                var message = new MailMessage();
               
                    message.Subject = emailProperties.Subject;
                    message.Body = string.Format(emailProperties.EmailBody);
                    message.IsBodyHtml = true;
               

                message.From = new MailAddress("denzelmac8@gmail.com");

                

                foreach (var recepientAddress in clients)
                {
                    message.To.Add(new MailAddress(recepientAddress.Email));
                }
                message.CC.Add(new MailAddress("DenzelMachowa@gmail.com"));


                Attachment attachment;
                attachment = new Attachment(emailProperties.AttachementLocation);
                message.Attachments.Add(attachment);
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "denzelmac8@gmail.com",  // replace with valid value
                        Password = "ccbsxapqvtmobxnl"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);

                    return new OutputHandler
                    {
                        IsErrorOccured = false,
                        Message = "Email Sent Succesfully"
                    };
                }
            }
            catch (Exception ex)
            {
                return new OutputHandler { IsErrorOccured = true, Message = "Something went wrong, Please Ensure that your email is valid and try gain" };
            }
        }
    }
}
