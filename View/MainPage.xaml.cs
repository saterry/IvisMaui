using IvisMaui.Data;
//using IvisMaui.Models;
//using IvisMaui.Services;
using IvisMaui.ViewModel;

namespace IvisMaui.View;

public partial class MainPage : ContentPage
{
    //MessagesViewModel mainPage;
    //public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();
	public MainPage(MessagesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {

        base.OnAppearing();
        Global.Form = "Message";
    }

    //protected override void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    base.OnNavigatedTo(args);
    //    //this does not work 
    //    //viewModel.GetFieldPerformanceCommand.Execute(null);
    //}

    //protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    //{
    //    base.OnNavigatedFrom(args);
    //    //this does not work 
    //    //viewModel.GetFieldPerformanceCommand.Execute(null);
    //}
}

