namespace Models;

public record Geometry(double[] Coordinates, string Type)
{
    public double[] Coordinates { get; init; } = Coordinates;

    public string Type { get; init; } = Type;
}