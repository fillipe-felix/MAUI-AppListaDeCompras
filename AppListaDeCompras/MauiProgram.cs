using System.Net;
using System.Net.Mail;

using AppListaDeCompras.Libraries.Validations;

using CommunityToolkit.Maui;

using Microsoft.Extensions.Logging;

using Mopups.Hosting;

namespace AppListaDeCompras;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureMopups()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");//OpenSansRegular
                fonts.AddFont("Poppins-ExtraBold.ttf", "PoppinsExtraBold");//OpenSansSemibold
            });

        builder.Services.AddScoped<AddItemValidator>();
        builder.Services.AddScoped<SmtpClient>(options =>
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential("felipesoaares14@gmail.com", "cyui rhgu oxwp ssvu");
            client.EnableSsl = true;
            return client;
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
