using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using IvisMaui.Data;
using IvisMaui.Models;
//using IvisMaui.View;

namespace IvisMaui.ViewModel;

public partial class MessagesViewModel : BaseUIViewModel// : ObservableObject, IRecipient<SelectBusMessage>
{

    public MessagesViewModel(ISqliteService sqliteservice) : base(sqliteservice)
    {
        Task.Run(async () => { await InitAsync(); });

        //base.LoadMessages(Global.Bus);
        //Busnumber = Global.BusNumber;

    }

    private async Task InitAsync()
    {
        await base.LoadMessages(Global.Bus);
        Busnumber = Global.BusNumber;
    }


    [RelayCommand]
    async Task GoToMessageDetails(Busmessage busmessage)
    {
        if (busmessage == null)
            return;

        await Shell.Current.GoToAsync(nameof(MessageDetailsPage), true, new Dictionary<string, object>
        {
            {"Busmessage", busmessage }
        });
    }

    //public void Receive(SelectBusMessage message)
    //{
    //    //Task.Run(async () => await FindAudioFiles(progress1));
    //    //Task.Run(() =>
    //    if (!MainThread.IsMainThread)
    //    {
    //        Console.WriteLine("Not main thread");
    //    }
    //    else
    //    {
    //        Console.WriteLine("Main thread");
    //    }

    //    //MainThread.BeginInvokeOnMainThread(async () =>
    //    //{
    //    //    //Add form specific functions
    //    //    //if (Global.Form == "Student")
    //    //    //{
    //    //    await LoadMessages(Global.Bus);
    //    //    await LoadRoutes(message.Value);
    //    //    await LoadStudents(message.Value);
    //    //    //Busnumber = Global.BusNumber;
    //    //    Busnumber = message.Value.Number;
    //    //    //}
    //    //});
    //}


}