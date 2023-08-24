using IvisMaui.Data;
using IvisMaui.ViewModel;

namespace IvisMaui;

public partial class SelectBusDetailsPage : ContentPage
{
	public SelectBusDetailsPage(SelectBusDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        //this.Loaded += SelectPage_Loaded;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        //Shell.Current.Navigation.PopToRootAsync();
        //this does not work 
        //viewModel.GetFieldPerformanceCommand.Execute(null);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        //this does not work 
        //viewModel.GetFieldPerformanceCommand.Execute(null);
        //Shell.Current.Navigation.PopToRootAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //SelectBusDetailsViewModel viewModel = new SelectBusDetailsViewModel();
        //Navigation.RemovePage(new SelectBusDetailsPage(viewModel));
        //string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ivis.db");

        //SqliteService sqliteservice = new SqliteService(dbPath);
        //var viewModel = new SelectBusDetailsViewModel(sqliteservice);
        //await Navigation.PopModalAsync();
        //await Navigation.PopModalAsync(new SelectBusDetailsPage(viewModel), false);


        //var state = Shell.Current.CurrentState.Location;
        //var navfrom Shell.Current.NavigatedFrom();
        //await Navigation.PushModalAsync(new SelectBusPage(), false);
    }

    //protected async void SelectPage_Loaded(object sender, EventArgs e)
    //{
    //    //await Navigation.//.PopAsync();//.PushModalAsync(new OnBoardingPage(), false);
    //    var existingPages = Navigation.NavigationStack.ToList();
    //    foreach (var page in existingPages)
    //    {
    //        if (page != null)
    //            Navigation.RemovePage(page);
    //    }
    //}
}