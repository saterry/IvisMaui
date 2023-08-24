using IvisMaui.Data;
using IvisMaui.ViewModel;
//using System.Diagnostics;
//using System.Reflection;

namespace IvisMaui.View;

public partial class RoutePage : ContentPage
{
    RoutesViewModel routepage;
    public RoutePage(RoutesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        routepage = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Global.Form = "Route";

        routepage.OnAppearing();
    }
}