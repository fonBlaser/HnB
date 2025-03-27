namespace HolesNBalls;

public class GameFactory
{
    private readonly GameRules _rules;

    public GameFactory(GameRules rules)
    {
        _rules = rules;
    }
    public BoardPlane CreateBoardPlane(int width, int height, List<Hole> holes)
    {
        if (width < 2 || height < 2)
            throw new ArgumentOutOfRangeException(nameof(width), "Width and height must be at least 2.");

        if (holes == null || holes.Count == 0)
            throw new ArgumentException("Board must have at least one hole.", nameof(holes));
        
        if (holes.Count >= width * height)
            throw new ArgumentException("Board must have at least one free slot.", nameof(holes));
        
        List<Hole> holesOutside = holes.Where(h => h.X < 0 || h.Y < 0 || h.X >= width || h.Y >= height).ToList();  
        if (holesOutside.Any())
            throw new ArgumentException($"Some holes are outside: {string.Join(';', holesOutside)}", nameof(holes));
        
        if (!_rules.AllowSameHoleNumbers && holes.GroupBy(h => h.Number).Any(g => g.Count() > 1))
            throw new ArgumentException("Holes must have unique numbers.", nameof(holes));
        
        return new BoardPlane(width, height, holes);
    }
}