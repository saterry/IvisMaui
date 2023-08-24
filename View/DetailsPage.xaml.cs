using IvisMaui.ViewModel;

namespace IvisMaui;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(StudentDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}