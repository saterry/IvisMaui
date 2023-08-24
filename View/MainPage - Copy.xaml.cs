using IvisMaui.ViewModel;

namespace IvisMaui.View;

public partial class MainPageCopy : ContentPage
{
	public MainPageCopy(SelectBusViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}

