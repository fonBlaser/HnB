namespace HolesNBalls;

public class BoardPlane
{
    public int Width { get; }
    public int Height { get; }
    public IReadOnlyList<Hole> Holes { get; }

    public BoardPlane(int width, int height, IReadOnlyList<Hole> holes)
    {
        if (width < 2 || height < 2)
            throw new ArgumentOutOfRangeException(nameof(width), "Width and height must be at least 2.");

        if (holes == null || holes.Count == 0)
            throw new ArgumentException("Board must have at least one hole.", nameof(holes));
        
        if (holes.Count >= width * height)
            throw new ArgumentException("Board must have at least one free slot.", nameof(holes));
        
        List<Hole> holesOutside = holes.Where(h => h.X < 0 || h.Y < 0 || h.X >= width || h.Y >= height).ToList();  
        if(holesOutside.Any())
            throw new ArgumentException($"Some holes are outside: {string.Join(';', holesOutside)}", nameof(holes));
        
        Width = width;
        Height = height;
        Holes = holes;
    }
}