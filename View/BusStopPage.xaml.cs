using IvisMaui.ViewModel;

namespace IvisMaui.View;

public partial class BusStopPage : ContentPage
{
    public BusStopPage(BusStopsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}