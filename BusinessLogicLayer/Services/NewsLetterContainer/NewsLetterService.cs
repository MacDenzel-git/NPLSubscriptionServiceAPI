using BusinessLogicLayer.Services.NewsLetterContainer;
using Microsoft.EntityFrameworkCore;
using NPLDataAccessLayer.DataTransferObjects;
using NPLDataAccessLayer.GeneralHelpers;
using NPLDataAccessLayer.GenericRepositoryContainer;
using NPLDataAccessLayer.Models;
using NPLReusableResourcesPackage.AutoMapperContainer;
using NPLReusableResourcesPackage.ErrorHandlingContainer;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSBusinessLogicLayer;

namespace BusinessLogicLayer.Services.SubscriptionServiceContainer
{
    public class NewsLetterService : INewsLetterService
    {

        private readonly GenericRepository<NewsLetter> _service;
        private readonly GenericRepository<Publication> _publicationService;
        private readonly NPLSubsctiptionServiceDBContext _context;
        private readonly GenericRepository<Subscription> _subscriptionService;
        private readonly IMailService _mailService;
        public NewsLetterService(GenericRepository<Publication>  publicationService,NPLSubsctiptionServiceDBContext context, IMailService mailService, GenericRepository<NewsLetter> service, GenericRepository<Subscription> subscriptionService)
        {
            _publicationService = publicationService;
            _subscriptionService = subscriptionService;
            _service = service;
            _mailService = mailService;
            _context = context;
        }
        public async Task<OutputHandler> Create(NewsLetterDTO newsLetter)
        {
            try
            {
                string filename = newsLetter.FileLocation;
                //check if record with same parameters already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.PublicationId == newsLetter.PublicationId && newsLetter.PaperDated.Date == newsLetter.PaperDated.Date);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage("Publication with these Details")

                    };
                }

                var publication = await _publicationService.GetSingleItem(x=>x.PublicationId == newsLetter.PublicationId);

                string path = Path.Combine("NewsLetters", $"{publication.PublicationTitle}-{newsLetter.PaperDated.Day}-{newsLetter.PaperDated.Month}-{newsLetter.PaperDated.Year}{Path.GetExtension(filename)}" );
            

                var outputHandler = FileHandler.SaveFileFromByteByPath(newsLetter.File , path);

                if (outputHandler.IsErrorOccured)
                {
                    return outputHandler;
                }

                newsLetter.FileLocation = outputHandler.Result.ToString();
                newsLetter.Status = "Created";
                newsLetter.IsSubmittedSuccessfully = false;

                var mapped = new AutoMapper<NewsLetterDTO, NewsLetter>().MapToObject(newsLetter);
                var result = await _service.Create(mapped);
                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int paymentTypeId)
        {

            try
            {
                await _service.Delete(x => x.NewsLetterId == paymentTypeId);
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = StandardMessages.GetSuccessfulMessage()
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<NewsLetterDTO> GetNewsLetter(int newsLetterId)
        {
            var output = await _service.GetSingleItem(x => x.NewsLetterId == newsLetterId);
            return new AutoMapper<NewsLetter, NewsLetterDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<NewsLetterDTO>> GetAllNewsLetters()
        {
            IEnumerable<NewsLetterDTO> newsLetters = await (from nl in _context.NewsLetters
                                                            join c in _context.Publications on nl.PublicationId equals c.PublicationId
                                                            select new NewsLetterDTO
                                                            {
                                                                NewsLetterId = nl.NewsLetterId,
                                                                PublicationTitle = c.PublicationTitle,
                                                                Status = nl.Status,
                                                                PaperDated = nl.PaperDated,
                                                                IsSubmittedSuccessfully = nl.IsSubmittedSuccessfully
                                                            }).ToListAsync();
            return newsLetters;

        }

        public async Task<OutputHandler> Update(NewsLetterDTO newsLetter)
        {
            try
            {
                //check record already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.PublicationId == newsLetter.PublicationId && newsLetter.PaperDated == newsLetter.PaperDated);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage("Paper with these Details")

                    };
                }
                var mapped = new AutoMapper<NewsLetterDTO, NewsLetter>().MapToObject(newsLetter);
                var result = await _service.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<OutputHandler> SendSoftCopyNewsLetter(int newsLetterId)
        {
            var newsletterDetails = await _service.GetSingleItem(x=> x.NewsLetterId == newsLetterId);

            var newsLetter = await _service.GetSingleItem(x => x.NewsLetterId.Equals(newsLetterId));
             var subs = (from sub in _context.Subscriptions.Where(x => x.ExpiryDate.Date > DateTime.UtcNow.Date 
                         && x.PublicationId == newsletterDetails.PublicationId && x.SubscriptionStatusId == 2 && x.TypeOfDeliveryId == 3 )
                        join clients in _context.Clients on sub.ClientId equals clients.ClientId
                        select new ClientDTO
                        {
                            Email = clients.Email
                        }).ToList();
  
            var emailDetails = new EmailProperties
            {
                EmailBody = string.Format($"Dear Customer,Thank you for Subscribing with us, please find the attached Publication <br /> <br /> Kind Regards, <br /> Nation Publications Limited")
                ,
                AttachementLocation = newsLetter.FileLocation
            };

            if (subs.Count() == 0)
            {
                return new OutputHandler
                {

                    IsErrorOccured = true,
                    Message = "This publication does not have subscribers at the moment, no email was sent out"
                };
            }
            else
            {
                var output = await _mailService.EmailNewsLetter(subs, emailDetails);
                if (!output.IsErrorOccured)
                {
                    newsLetter.SubmissionCount = subs.Count();
                    newsLetter.IsSubmittedSuccessfully = true;
                    newsLetter.SubmittedBy = "SysAdmin";
                    newsLetter.SubmittedDate = DateTime.Now.AddHours(2);
                    output = await _service.Update(newsLetter);
                    return output;
                }
                else
                {
                    return output;
                }
            }
            


        }
    }
}