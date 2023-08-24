using IvisMaui.Data;
using IvisMaui.Models;
using IvisMaui.ViewModel;
using System.Runtime.Intrinsics.X86;
using System.Windows.Input;
//using static Android.InputMethodServices.Keyboard;

namespace IvisMaui.View;

public partial class BusStopDetailsPage : ContentPage
{

    //public Studentstopdetail SelectedItem { get; set; }

    public BusStopDetailsPage(BusStopDetailsViewModel viewModel)
	{
        InitializeComponent();

        BindingContext = viewModel;
	}

    public ICommand ToggleItem => new Command<Studentstopdetail>(ToggleItemControl);

    public void ToggleItemControl(Studentstopdetail o)
    {
        Console.WriteLine($"toggle {o}");
    }

    //void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    Studentstopdetail SelectedItem = e.CurrentSelection[0] as Studentstopdetail;
    //    var vm = (BusStopDetailsViewModel)BindingContext;
    //    vm.SelectedItem = SelectedItem;

    //}

    //private void Button_Clicked(object sender, EventArgs e)
    //{
    //    Button button = sender as Button;
    //    var vm = (BusStopDetailsViewModel)BindingContext;
    //    var student = vm.SelectedItem;
    //    var studentnumber = student.StudentNumber;
    //    Console.WriteLine(studentnumber);


    //}
    //public ICommand SelectionChangedCommand => new Command<Object>((Object e) =>
    //{
    //    var vm = (BusStopDetailsViewModel)BindingContext;

    //    Console.WriteLine($"selection made {vm.SelectedItem.FullName}");
    //});

    //public void OnSelectionChanged(Object sender, SelectionChangedEventArgs e)
    //{
    //    Console.WriteLine("Selection changed click");
    //    Studentstopdetail ssd = e.CurrentSelection[0] as Studentstopdetail;
    //    Console.WriteLine(ssd.FullName);
    //}
    public void XAMLSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        //var vm = (BusStopDetailsViewModel)BindingContext;
        //vm.Toggled = !vm.Toggled;
        //var student = vm.SelectedItem;
        //var studentnumber = student.StudentNumber;
        //Console.WriteLine(studentnumber);

        string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        //Console.WriteLine("Status: " + row.Cells["Status"].Value.ToString());
        int Status = 0;
        //if (Int32.Parse(row.Cells["Status"].Value.ToString()) != 1)
        //{
        //    Status = 1;
        //}
        //else
        //{
        //    Status = 0;
        //}

        //string StudentNumber = row.Cells["StudentNumber"].Value.ToString();
        //UpdateStatus(StudentNumber, Global.StopId, Global.RouteId, Global.AmPm, Status);


        string stateName = e.Value ? "ON" : "OFF";
        //switchStateLabel.Text = $"The switch is {stateName}";
    }

    async void OnUnloadButtonClicked(object sender, EventArgs args)
    {
        var vm = (BusStopDetailsViewModel)BindingContext;
        await vm.UnloadBus();
    }

    //protected override void OnAppearing()
    //{

    //    base.OnAppearing();

    //    if (Global.Form != "Busstopdetails")
    //    {
    //        Shell.Current.GoToAsync("..");
    //    }

    //}
}