using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IvisMaui.Data;
using IvisMaui.Models;
using System.Diagnostics;

namespace IvisMaui.ViewModel;

[QueryProperty(nameof(Busmessage), "Busmessage")]
public partial class MessageDetailsViewModel : BaseUIViewModel
{
    ISqliteService sqliteservice;
    public MessageDetailsViewModel
        (ISqliteService sqliteservice) : base(sqliteservice)
    {
        this.sqliteservice = sqliteservice;
    }

    [ObservableProperty]
    Busmessage busmessage;

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GetBusmessageAsync(Busmessage busmessage)
    {
        if (IsBusy)
            return;

        try
        {

            IsBusy = true;
            this.busmessage = busmessage;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get message: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            //IsRefreshing = false;
        }
    }
}
