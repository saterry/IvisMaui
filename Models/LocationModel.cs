namespace IvisMaui.Models;

public record LocationModel(double Latitude, double Longitude, double Bearing, float Speed)
{
    public double Latitude { get; } = Latitude;
    public double Longitude { get; } = Longitude;
    public double Bearing { get; } = Bearing;
    public float Speed { get; } = Speed;
}