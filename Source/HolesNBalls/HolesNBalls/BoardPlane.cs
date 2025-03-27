namespace HolesNBalls;

public class BoardPlane
{
    public int Width { get; }
    public int Height { get; }
    public IReadOnlyList<Hole> Holes { get; }

    internal BoardPlane(int width, int height, IReadOnlyList<Hole> holes)
    {
        Width = width;
        Height = height;
        Holes = holes;
    }
}