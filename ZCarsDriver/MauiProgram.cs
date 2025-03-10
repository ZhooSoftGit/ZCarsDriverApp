﻿using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using ZCarsDriver.CoreHelper;
using ZCarsDriver.Services.Contracts;
using ZCarsDriver.Services.Services;
using ZCarsDriver.ViewModel;
using ZCarsDriver.Views;
using ZhooCars.Services;
using ZhooSoft.Auth;
using ZhooSoft.Auth.ViewModel;
using ZhooSoft.Auth.Views;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZhooSoft.Core.NavigationBase;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionToolkit()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(CustomEntry), typeof(CustomEntryHandler));
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddServices();
            builder.Services.AddViewModel();
            builder.Services.AddPages();


            builder.Services.AddSingleton<IMainAppNavigation, MainAppNavigation>();

            var app = builder.Build();

            ServiceHelper.Initialize(app.Services);


            return app;
        }

        private static IServiceCollection AddViewModel(this IServiceCollection services)
        {
            services.AddTransient<LoginViewModel>();
            services.AddTransient<OTPVerificationViewModel>();
            services.AddTransient<HomeViewModel>();
            return services;
        }

        private static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddTransient<LoginPage>();
            services.AddTransient<OTPVerificationPage>();
            services.AddTransient<HomeViewPage>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();

#if ANDROID
            services.AddSingleton<IProgressService, ProgressService_Android>();
#endif

#if IOS
            services.AddSingleton<IProgressService, ProgressService_iOS>();
#endif

            services.AddSingleton<HttpClient>();

            services.AddSingleton<IApiService, ApiService>();

            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IDocumentService, DocumentService>();
            services.AddSingleton<IDriverService, DriverService>();
            services.AddSingleton<IDriverShiftLogService, DriverShiftLogService>();
            services.AddSingleton<IDriverVehicleLinkService, DriverVehicleLinkService>();
            services.AddSingleton<IPeakHoursService, PeakHoursService>();
            services.AddSingleton<IRideHistoryService, RideHistoryService>();
            services.AddSingleton<IRideTripService, RideTripService>();
            services.AddSingleton<IServiceProviderService, ServiceProviderService>();
            services.AddSingleton<IServiceRequestService, ServiceRequestService>();
            services.AddSingleton<ISparePartsProviderService, SparePartsProviderService>();
            services.AddSingleton<ITaxiBookingService, TaxiBookingService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IVehicleDetailsService, VehicleDetailsService>();
            services.AddSingleton<IVehicleLocationService, VehicleLocationService>();
            services.AddSingleton<IVehicleMetadataService, VehicleMetadataService>();
            services.AddSingleton<IVendorService, VendorService>();

            return services;
        }
    }
}
