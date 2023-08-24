using IvisMaui.Data;
using IvisMaui.ViewModel;

namespace IvisMaui.View;

public partial class StudentPage : ContentPage
{
	public StudentPage(StudentsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {

        base.OnAppearing();
        Global.Form = "Student";
    }

}