using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.NewsLetterContainer
{
    public interface INewsLetterService
    {
        Task<OutputHandler> Create(NewsLetterDTO newsLetter);
        Task<OutputHandler> Update(NewsLetterDTO newsLetter);
        Task<OutputHandler> Delete(int newsLetterId);
        Task<OutputHandler> SendSoftCopyNewsLetter(int newsLetterId);
        Task<IEnumerable<NewsLetterDTO>> GetAllNewsLetters();
        Task<NewsLetterDTO> GetNewsLetter(int newsLetterId); 
     }
}
