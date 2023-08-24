using IvisMaui.Models;
using System.Net.Http.Json;

namespace IvisMaui.Services;

public class StudentService
{
    HttpClient httpClient;
    public StudentService()
    {
        this.httpClient = new HttpClient();
    }

    List<Student> studentList;
    public async Task<List<Student>> GetStudents()
    {
        if (studentList?.Count > 0)
            return studentList;

        // Online
        var response = await httpClient.GetAsync("https://www.montemagno.com/students.json");
        if (response.IsSuccessStatusCode)
        {
            studentList = await response.Content.ReadFromJsonAsync<List<Student>>();
        }
        // Offline
        /*using var stream = await FileSystem.OpenAppPackageFileAsync("studentdata.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        studentList = JsonSerializer.Deserialize<List<Student>>(contents);
        */
        return studentList;
    }
}
