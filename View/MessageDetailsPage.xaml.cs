using IvisMaui.ViewModel;

namespace IvisMaui;

public partial class MessageDetailsPage : ContentPage
{
	public MessageDetailsPage(MessageDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}