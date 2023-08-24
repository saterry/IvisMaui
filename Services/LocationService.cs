using IvisMaui.Models;

namespace IvisMaui.Services;

public partial class LocationService
{
    public event EventHandler<LocationModel> LocationChanged;
    public event EventHandler<string> StatusChanged;

    public void Initialize()
    {
#if ANDROID
        AndroidInitialize();
#endif
    }

    public void Stop()
    {
#if ANDROID
        AndroidStop();
#endif
    }

    protected virtual void OnLocationChanged(LocationModel e)
    {
        LocationChanged?.Invoke(this, e);
    }

    protected virtual void OnStatusChanged(string e)
    {
        StatusChanged?.Invoke(this, e);
    }
}