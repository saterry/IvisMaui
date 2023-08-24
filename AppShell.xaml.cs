using IvisMaui.View;

namespace IvisMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(BusStopDetailsPage), typeof(BusStopDetailsPage));

        Routing.RegisterRoute(nameof(BusStopPage), typeof(BusStopPage));

        //Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));

        Routing.RegisterRoute(nameof(MessageDetailsPage), typeof(MessageDetailsPage));

        //Routing.RegisterRoute(nameof(SelectBusDetailsPage), typeof(SelectBusDetailsPage));

        Routing.RegisterRoute(nameof(StudentDetailsPage), typeof(StudentDetailsPage));

        Routing.RegisterRoute(nameof(SelectBusPage), typeof(SelectBusPage));

        Routing.RegisterRoute(nameof(RoutePage), typeof(RoutePage));

        Routing.RegisterRoute(nameof(StudentPage), typeof(StudentPage));

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }

    //protected override async void OnNavigating(ShellNavigatingEventArgs args)
    //{
    //    base.OnNavigating(args);

    //    ShellNavigatingDeferral token = args.GetDeferral();

    //var result = await DisplayActionSheet("Navigate?", "Cancel", "Yes", "No");
    //if (result != "Yes")
    //{
    //    args.Cancel();
    //}
    //token.Complete();
    //    }
}
