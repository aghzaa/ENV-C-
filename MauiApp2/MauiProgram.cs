using MauiApp2.ViewModels;
using MauiApp2.Views;
using Microsoft.Extensions.Logging;
using MauiApp2.Services;

namespace MauiApp2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();

            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<DashboardPage>();

            builder.Services.AddTransient<UserViewModel>();
            builder.Services.AddTransient<KelolaUser>();

            builder.Services.AddTransient<RoleViewModel>();
            builder.Services.AddTransient<RolePage>();

            builder.Services.AddTransient<ProdukViewModel>();
            builder.Services.AddTransient<Product>();

            builder.Services.AddTransient<KategoriViewModel>();
            builder.Services.AddTransient<KelolaKategori>();

            builder.Services.AddSingleton<ApiServices>();

            return builder.Build();
        }
    }
}
