using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using IvisMaui.Data;
using IvisMaui.Models;

namespace IvisMaui.ViewModel;

public partial class StudentsViewModel : BaseUIViewModel
//ObservableObject, IRecipient<SelectBusMessage>
{
    public StudentsViewModel(ISqliteService sqliteservice) : base(sqliteservice)
    {
        base.LoadStudents(Global.Bus);
        Busnumber = Global.BusNumber;
    }

    [RelayCommand]
    async Task GoToStudentDetails(Student student)
    {
        if (student == null)
        return;

        await Shell.Current.GoToAsync(nameof(StudentDetailsPage), true, new Dictionary<string, object>
        {
            {"Student", student }
        });
    }

}
