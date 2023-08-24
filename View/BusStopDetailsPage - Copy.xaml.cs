using IvisMaui.ViewModel;

namespace IvisMaui.View;

public partial class BusStopDetailsPage1 : ContentPage
{
	public BusStopDetailsPage1(BusStopDetailsViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
	}
}