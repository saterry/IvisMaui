using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IvisMaui.Data;
using IvisMaui.Models;
using System.Diagnostics;

namespace IvisMaui.ViewModel;

[QueryProperty(nameof(Student), "Student")]
public partial class StudentDetailsViewModel : BaseUIViewModel
{
    ISqliteService sqliteservice;
    public StudentDetailsViewModel
        (ISqliteService sqliteservice) : base(sqliteservice)
    {
        this.sqliteservice = sqliteservice;
    }

    [ObservableProperty]
    Student student;

    //[ObservableProperty]
    //bool isRefreshing;

    [RelayCommand]
    async Task GetStudentAsync(Student student)
    {
        if (IsBusy)
            return;

        try
        {

            IsBusy = true;
            this.student = student;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get student: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            //IsRefreshing = false;
        }
    }
}
