using CommunityToolkit.Maui;
using IvisMaui.Data;
using IvisMaui.Services;
using IvisMaui.View;
using IvisMaui.ViewModel;
using System.Reflection;

namespace IvisMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()

            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                //fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialDesignIcons");
                fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIcons");
            });
			
    	//builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		//builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		//builder.Services.AddSingleton<IMap>(Map.Default);

		builder.Services.AddSingleton<StudentService>();

        builder.Services.AddTransient<BaseViewModel>();
        builder.Services.AddTransient<BaseUIViewModel>();

        builder.Services.AddTransient<RoutePage>();
        builder.Services.AddTransient<RoutesViewModel>();

        builder.Services.AddTransient<BusStopPage>();
        builder.Services.AddTransient<BusStopsViewModel>();

        builder.Services.AddTransient<MessageDetailsPage>();
        builder.Services.AddTransient<MessageDetailsViewModel>();

        builder.Services.AddTransient<StudentPage>();
        builder.Services.AddTransient<StudentsViewModel>();

        builder.Services.AddTransient<SelectBusPage>();
        builder.Services.AddTransient<SelectBusViewModel>();

        //builder.Services.AddTransient<SelectBusDetailsPage>();
        //builder.Services.AddTransient<SelectBusDetailsViewModel>();

        builder.Services.AddTransient<StudentDetailsPage>();
        builder.Services.AddTransient<StudentDetailsViewModel>();

        builder.Services.AddTransient<BusStopDetailsPage>();
        builder.Services.AddTransient<BusStopDetailsViewModel>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MessagesViewModel>();

        //Add ViewModel for Details page or remove if not needed.
        //builder.Services.AddTransient<DetailsPage>();

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "IvisMaui.ivis.db");

        ExtractSaveResource("IvisMaui.ivis.db");

        builder.Services.AddSingleton<ISqliteService, SqliteService>();

        builder.Services.AddSingleton<IRestService, RestService>();
        builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();
        builder.Services.AddSingleton<IStatusQueueService, StatusQueueService>();


        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<SqliteService>(s, dbPath));

        return builder.Build();
	}

    public static void ExtractSaveResource(String filename)
    {
        string location = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, filename);

        if (!File.Exists(location))
        {
            Assembly a = Assembly.GetExecutingAssembly();
            Stream resFilestream = a.GetManifestResourceStream(filename);
            if (resFilestream != null)
            {
                BinaryReader br = new BinaryReader(resFilestream);
                FileStream fs = new FileStream(location, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                bw.Write(ba);
                br.Close();
                bw.Close();
                resFilestream.Close();
            }
        }
    }
}
