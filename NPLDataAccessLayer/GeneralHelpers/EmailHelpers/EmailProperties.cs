using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.GeneralHelpers
{
    public class EmailProperties
    {
        public string RecepientEmail { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
        public string AttachementLocation { get; set; }
        public string PasswordResetOTP { get; set; } = "";
        public string PasswordResetUrlLink { get; set; } = "";
    }
}
