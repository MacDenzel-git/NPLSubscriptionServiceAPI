﻿using BusinessLogicLayer.Services.ClientServiceContainer;
using BusinessLogicLayer.Services.ClientTypeContainer;
using BusinessLogicLayer.Services.DistrictServiceContainer;
using BusinessLogicLayer.Services.PaymentServiceContainer;
using BusinessLogicLayer.Services.PaymentTypeContainer;
using BusinessLogicLayer.Services.PromotionServiceContainer;
using BusinessLogicLayer.Services.RegionContainer;
using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using BusinessLogicLayer.Services.SubscriptionStatusServiceContainer;
using BusinessLogicLayer.Services.SubscriptionTypeServiceContainer;
using NPLDataAccessLayer.GenericRepositoryContainer;
using NPLDataAccessLayer.Models;

namespace NPLSubscriptionServiceAPI
{
    public static class Registration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<GenericRepository<Client>>();
            serviceCollection.AddScoped<GenericRepository<ClientType>>();
            serviceCollection.AddScoped<GenericRepository<Country>>();
            serviceCollection.AddScoped<GenericRepository<District>>();
            serviceCollection.AddScoped<GenericRepository<Payment>>();
            serviceCollection.AddScoped<GenericRepository<PaymentType>>();
            serviceCollection.AddScoped<GenericRepository<Promotion>>();
            serviceCollection.AddScoped<GenericRepository<Region>>();
            serviceCollection.AddScoped<GenericRepository<Subscription>>();
            serviceCollection.AddScoped<GenericRepository<SubscriptionStatus>>();
            serviceCollection.AddScoped<GenericRepository<SubscriptionType>>();
            serviceCollection.AddScoped<GenericRepository<Region>>();
            return serviceCollection.AddScoped<GenericRepository<SubscriptionStatus>>();

          
        }

        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<IClientTypeService, ClientTypeService>();
            service.AddScoped<IPaymentTypeService, PaymentTypeService>();
            service.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
            service.AddScoped<IDistrictService, DistrictService>();
            service.AddScoped<IClientService, ClientService>();
            service.AddScoped<IPaymentService, PaymentService>();
            service.AddScoped<IPromotionService, PromotionService>();
            service.AddScoped<ISubscriptionService, SubscriptionService>();
            service.AddScoped<ISubscriptionStatusService, SubscriptionStatusService>();
             return service.AddScoped<IRegionService, RegionService>();
        
        }
    }
}
