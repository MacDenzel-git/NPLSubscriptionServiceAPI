using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.GeneralHelpers
{
    public interface IMailService
    {
        Task<OutputHandler> EmailMember(string actionType, EmailProperties emailProperties);
        Task<OutputHandler> EmailNewsLetter(List<ClientDTO> clients,EmailProperties emailProperties);
    }
}
