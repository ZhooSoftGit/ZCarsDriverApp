using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using ZCarsDriver.CoreHelper;
using ZhooSoft.Auth;
using ZhooSoft.Auth.ViewModel;
using ZhooSoft.Auth.Views;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZhooSoft.Core.NavigationBase;

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
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<OTPVerificationViewModel>();
            return services;
        }

        private static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddSingleton<LoginPage>();
            services.AddSingleton<OTPVerificationPage>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();

            return services;
        }
    }
}
