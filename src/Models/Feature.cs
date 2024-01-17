namespace Models;

public record Feature(string Type, Geometry Geometry, Properties Properties)
{
    public string Type { get; init; } = Type;

    public Geometry Geometry { get; init; } = Geometry;

    public Properties Properties { get; init; } = Properties;
}