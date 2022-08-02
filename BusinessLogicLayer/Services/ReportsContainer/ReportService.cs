using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NPLDataAccessLayer.DataTransferObjects;
using NPLDataAccessLayer.GeneralHelpers;
using NPLDataAccessLayer.GenericRepositoryContainer;
using NPLDataAccessLayer.Models;
using NPLReusableResourcesPackage.AutoMapperContainer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.ReportsContainer
{
    public class ReportService : IReportService
    {
        private readonly GenericRepository<NewsLetter> _service;
        private readonly GenericRepository<Publication> _publicationService;
        private readonly NPLSubsctiptionServiceDBContext _context;
        private readonly GenericRepository<Subscription> _subscriptionService;
        private readonly GenericRepository<Region> _regionService;
        private readonly GenericRepository<PaymentType> _paymentTypeService;
        public readonly IConfiguration _configuration;

        public ReportService(GenericRepository<Publication> publicationService,
            NPLSubsctiptionServiceDBContext context,
             GenericRepository<NewsLetter> service,
            GenericRepository<Subscription> subscriptionService, IConfiguration configuration,
            GenericRepository<Region> regionService,
        GenericRepository<PaymentType> paymentTypeService
)
        {
            _regionService = regionService;
            _paymentTypeService = paymentTypeService;
            _publicationService = publicationService;
            _subscriptionService = subscriptionService;
            _service = service;
            _context = context;
        }
        public async Task<SubscriptionDTO> UsersGroupedByPublications()
        {

            var subscription = new SubscriptionDTO
            {
                PublicationStats = await _publicationService.FromSprocAsync<PublicationStats>("SpPublicationStats")
            };

            return subscription;
        }

        public Task<ReceiptDTO> GetReceipt(int PaymentId)
        {
            var output = (from p in _context.Payments.Where(x => x.PaymentId == PaymentId)
                          join paytypes in _context.PaymentTypes on p.PaymentTypeId equals paytypes.PaymentTypeId
                          join sub in _context.Subscriptions on p.ClientId equals sub.ClientId
                          join st in _context.SubscriptionTypes on sub.SubscriptionTypeId equals st.SubscriptionTypeId
                          join cl in _context.Clients on sub.ClientId equals cl.ClientId
                          join pub in _context.Publications on sub.PublicationId equals pub.PublicationId

                          select new ReceiptDTO
                          {
                              ClientName = cl.ClientName,
                              TransactionId = p.TransactionId,
                              PublicationTitle = pub.PublicationTitle,
                              Amount = sub.ChargeInMwk,
                              Duration = st.Duration,
                              PaymentAccountDetails = $"{paytypes.Description}-{paytypes.AccountNumber}",
                              ExpiryDate = sub.ExpiryDate.Date,
                              StartDate = sub.DateOfSubscription.Date
                          }
                          ).FirstOrDefaultAsync();
            return output;
        }

        public async Task<IEnumerable<SubscriptionDTO>> ListActiveSubscribedUsers()
        {
            IEnumerable<SubscriptionDTO> output = await (from s in _context.Subscriptions.Where(x => x.ExpiryDate > DateTime.UtcNow.AddHours(2) && x.SubscriptionStatusId == 2)
                                                         join pub in _context.Publications on s.PublicationId equals pub.PublicationId
                                                         join c in _context.Clients on s.ClientId equals c.ClientId
                                                         join st in _context.SubscriptionTypes on s.SubscriptionTypeId equals st.SubscriptionTypeId
                                                         join p in _context.Promotions on s.PromotionId equals p.PromotionId
                                                         join ss in _context.SubscriptionStatuses on s.SubscriptionStatusId equals ss.SubscriptionStatusId
                                                         select new SubscriptionDTO
                                                         {
                                                             SubscriptionId = s.SubscriptionId,
                                                             ClientName = c.ClientName,
                                                             PublicationTitle = pub.PublicationTitle,
                                                             SubscriptionTypeDescription = st.SubscriptionName,
                                                             PromotionCode = p.PromotionCode,
                                                             ChargeInMwk = s.ChargeInMwk,
                                                             NumberOfCopies = s.NumberOfCopies,
                                                             ExpiryDate = s.ExpiryDate,
                                                             SubscriptionStatusDescription = ss.Description,
                                                             TotalSubscriptions = c.TotalSubscription

                                                         }).ToListAsync();
            return output;

        }

        public async Task<IEnumerable<ClientDTO>> ListClientsByRegion(int regionId)
        {
            var output = await (from c in _context.Clients.Where(x => x.RegionId == regionId)
                                join r in _context.Regions on c.RegionId equals r.RegionId
                                join d in _context.Districts on c.DistrictId equals d.DistrictId
                                select new ClientDTO
                                {
                                    ClientName = c.ClientName,
                                    RegionName = r.RegionName,
                                    DistrictName = d.DistrictName
                                }).ToListAsync();
            return output;
        }

        public async Task<IEnumerable<SubscriptionDTO>> ListExpiredSubscribedUsers()
        {
            IEnumerable<SubscriptionDTO> output = await (from s in _context.Subscriptions.Where(x => x.ExpiryDate < DateTime.UtcNow.AddHours(2) || x.SubscriptionStatusId == 3)
                                                         join pub in _context.Publications on s.PublicationId equals pub.PublicationId
                                                         join c in _context.Clients on s.ClientId equals c.ClientId
                                                         join st in _context.SubscriptionTypes on s.SubscriptionTypeId equals st.SubscriptionTypeId
                                                         join p in _context.Promotions on s.PromotionId equals p.PromotionId
                                                         join ss in _context.SubscriptionStatuses on s.SubscriptionStatusId equals ss.SubscriptionStatusId
                                                         select new SubscriptionDTO
                                                         {
                                                             SubscriptionId = s.SubscriptionId,
                                                             ClientName = c.ClientName,
                                                             PublicationTitle = pub.PublicationTitle,
                                                             SubscriptionTypeDescription = st.SubscriptionName,
                                                             PromotionCode = p.PromotionCode,
                                                             ChargeInMwk = s.ChargeInMwk,
                                                             NumberOfCopies = s.NumberOfCopies,
                                                             ExpiryDate = s.ExpiryDate,
                                                             SubscriptionStatusDescription = ss.Description,
                                                             TotalSubscriptions = c.TotalSubscription

                                                         }).ToListAsync();
            return output;
        }

        public async Task<SubscriptionDTO> ActiveUsersGroupedByPublications()
        {
            var subscription = new SubscriptionDTO
            {
                PublicationStats = await _publicationService.FromSprocAsync<PublicationStats>("SpActiveUsersByPublicationStats")
            };

            return subscription;

        }

        public async Task<ReportDashBoardDTO> SetupReportDashboard()
        {

            var subscribers = await _subscriptionService.GetAll();
            var newsLetter = await _service.GetAll();
            decimal amount = 0;

            foreach (var subscriber in subscribers)
            {
                amount = amount + subscriber.ChargeInMwk;
            }

            var dashboard = new ReportDashBoardDTO
            {
                NumberOfSubscribers = subscribers.Count(),
                NewsLettersSent = newsLetter.Count(),
                TotalAmountInSubscription = amount,
                NumberOfUsersRegisteredToday = subscribers.Where(x => x.CreatedDate.Date == DateTime.UtcNow.Date).Count(),
                ActiveSubscriptions = await ListActiveSubscribedUsers(),
                ExpiredSubscriptions = await ListExpiredSubscribedUsers(),
                Regions = new AutoMapper<Region,RegionDTO>().MapToList(await _regionService.GetAll()),
                PaymentTypes = new AutoMapper<PaymentType,PaymentTypeDTO>().MapToList(await _paymentTypeService.GetAll()),

                //ActiveUsersGroupedByPublications = await ActiveUsersGroupedByPublications(),
                //UsersGroupedByPublications = await UsersGroupedByPublications()
            };

            
            return dashboard;

        }
    }
}
