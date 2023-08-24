using IvisMaui.ViewModel;

namespace IvisMaui.View;

public partial class MessagePage : ContentPage
{
	public MessagePage(MessagesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}

