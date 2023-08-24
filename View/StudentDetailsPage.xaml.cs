using IvisMaui.ViewModel;

namespace IvisMaui;

public partial class StudentDetailsPage : ContentPage
{
	public StudentDetailsPage(StudentDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}