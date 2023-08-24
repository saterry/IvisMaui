using IvisMaui.ViewModel;
using System.Diagnostics;
using System.Reflection;

namespace IvisMaui.View;

public partial class SelectBusPage : ContentPage
{
    public SelectBusPage()
    {
    }

    public SelectBusPage(SelectBusViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        //this.Loaded += SelectBusPage_Loaded;
    }
}

