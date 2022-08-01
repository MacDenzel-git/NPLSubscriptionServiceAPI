using Microsoft.EntityFrameworkCore;
using NPLDataAccessLayer.DataTransferObjects;
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

namespace BusinessLogicLayer.Services.SubscriptionServiceContainer
{
    public class SubscriptionService : ISubscriptionService
    {

        private readonly GenericRepository<Subscription> _subscriptionService;
        private readonly GenericRepository<SubscriptionType> _subscriptionTypeService;
        private readonly GenericRepository<Promotion> _promotionService;
        private readonly GenericRepository<Payment> _paymentService;
        private readonly NPLSubsctiptionServiceDBContext _context;
        public SubscriptionService(GenericRepository<Subscription> subscriptionService,
            GenericRepository<SubscriptionType> subscriptionTypeService,
            GenericRepository<Payment> paymentService,
        GenericRepository<Promotion> promotionService,NPLSubsctiptionServiceDBContext context)
        {
            _promotionService = promotionService;
            _subscriptionTypeService = subscriptionTypeService;
            _context = context;
            _subscriptionService = subscriptionService;
            _paymentService = paymentService;
        }
        public async Task<OutputHandler> Create(SubscriptionDTO subscription)
        {
            try
            {
                //Validate Subscription
                var output = await ValidateSubscription(subscription);
                if (output.IsErrorOccured)
                {
                    return output;
                }
                subscription = (SubscriptionDTO)output.Result;

                var mapped = new AutoMapper<SubscriptionDTO, Subscription>().MapToObject(subscription);
                var result = await _subscriptionService.Create(mapped);

                var payment = await _paymentService.GetSingleItem(x =>x.PaymentId == subscription.PaymentId);
                payment.IsUsed = true;

                await _paymentService.Update(payment);
                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int subscriptionId)
        {

            try
            {
                await _subscriptionService.Delete(x => x.SubscriptionId == subscriptionId);
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

        public async Task<SubscriptionDTO> GetSubscription(int subscriptionId)
        {
            var output = await _subscriptionService.GetSingleItem(x => x.SubscriptionId == subscriptionId);
            return new AutoMapper<Subscription, SubscriptionDTO>().MapToObject(output);
        }

        public async Task<List<SubscriptionDTO>> GetAllSubscriptions()
        {


            var output = (from s in _context.Subscriptions
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
                              SubscriptionStatusDescription = ss.Description

                          }).ToList();
            return output;
        }

        public async Task<OutputHandler> Update(SubscriptionDTO subscription)
        {
            try
            {
                var output = await ValidateSubscription(subscription);
                subscription = (SubscriptionDTO)output.Result;
                var mapped = new AutoMapper<SubscriptionDTO, Subscription>().MapToObject(subscription);
                var result = await _subscriptionService.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<OutputHandler> ValidateSubscription(SubscriptionDTO subscription)
        {
            Subscription output = await _subscriptionService.GetSingleItem(x => x.ClientId == subscription.ClientId && x.ExpiryDate < DateTime.UtcNow.AddHours(2));
            if (output != null)
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = $"This Client already has an active subscription scheduled to expire on {output.ExpiryDate.ToShortDateString()}"
                };
            }
            //Verify Date
            if (subscription.DateOfSubscription < DateTime.UtcNow.AddHours(2))
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "Subscription Start date has to be days after today, please choose a different date"
                };
            }

            //Make sure there is a valid number of copies selected
            if (subscription.NumberOfCopies <= 0 || subscription.NumberOfCopies == null)
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "0 Number of Copies is not valid, please choose any number from 1"
                };
            }
            
            //Verify Payment 
            var paymentDetails = await _paymentService.GetSingleItem(x => x.PaymentId == subscription.PaymentId);
            if (paymentDetails != null)
            {
                if (paymentDetails.IsUsed)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"Payment has already been used"
                    };
                }

            }
            else
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = $"Payment does not exist, please check and enter choose the correct details"
                };

            }
            var subscriptionType = await _subscriptionTypeService.GetSingleItem(x => x.SubscriptionTypeId == subscription.SubscriptionTypeId);
          
            //Calculate date of expiry
            if (subscriptionType.IsCalculationTypeMonths)
            {
                subscription.ExpiryDate = subscription.DateOfSubscription.AddMonths(subscriptionType.Duration);
            }
            else
            {
                int days = subscriptionType.Duration * 7; //convert it to days to pass it to AddDays function
                subscription.ExpiryDate = subscription.DateOfSubscription.AddDays(days);

            }


            int durationInDays = (subscription.ExpiryDate - subscription.DateOfSubscription ).Days;
            
            //calculate price according to number of copies
            decimal totalFee = Convert.ToDecimal(subscriptionType.SubscriptionFee * subscription.NumberOfCopies) * durationInDays;
           
            //Calculate Subscription FEE
            if (subscription.PromotionId == 1)
            {
                subscription.ChargeInMwk = totalFee;
            }
            else //There's a promition attacheed
            {
                var promotion = await _promotionService.GetSingleItem(x => x.PromotionId == subscription.PromotionId);
                if (promotion != null) //not deactivating in incase we would reuse
                {
                    //check if promotionCode is Valid
                    return new OutputHandler { IsErrorOccured = true, Message = $"Promotion Code:{promotion.PromotionCode} Does not exist, please verify and try again" };
                }

                decimal percentage = (decimal)promotion.DiscountPercentage / 100;//convert number to decimal for calculations
                totalFee = totalFee * percentage; //find the amount to deduct per the percentage from the total fee
                subscription.ChargeInMwk = subscriptionType.SubscriptionFee - totalFee; //the charge will be the fee minus the amount subtracted due to promotion


            }

            //TODO:Check Active Subscriptions : if exist keep record in subscriptions?copy to Active Subscription


            //TODO: check if the client already has a running subscription

            return new OutputHandler
            {
                IsErrorOccured = false,
                Message = "Validation Succeded",
                Result = subscription
            };
        }
    }
}
