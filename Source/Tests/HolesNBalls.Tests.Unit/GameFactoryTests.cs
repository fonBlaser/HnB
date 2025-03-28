namespace HolesNBalls.Tests.Unit;

[Trait("Category", "Unit")]
public class GameFactoryTests
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    public void BoardPlane_WithAnyAxisLengthLessThanTwo_CannotBeCreated(int width, int height)
    {
        GameFactory factory = new GameFactory(new GameRules());
        
        List<Hole> holes = [new(0,0,0)];
        
        Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateBoardPlane(width, height, holes));
    }

    [Fact]
    public void BoardPlane_WithoutHoles_CannotBeCreated()
    {
        GameFactory factory = new GameFactory(new GameRules());
        
        Assert.Throws<ArgumentException>(() => factory.CreateBoardPlane(2, 2, []));
    }

    [Fact]
    public void BoardPlane_WithoutFreeSlot_CannotBeCreated()
    {
        GameFactory factory = new GameFactory(new GameRules());
        
        List<Hole> holes = Enumerable.Range(0, 4)
            .Select(i => new Hole(i, i % 2, i / 2))
            .ToList();
        
        Assert.Throws<ArgumentException>(() => factory.CreateBoardPlane(2, 2, holes));
    }
    
    [Fact]
    public void BoardPlane_WithHoleOutOfBoundaries_CannotBeCreated()
    {
        GameFactory factory = new GameFactory(new GameRules());
        
        List<Hole> holes = [new(0,0,0), new(1,1,1), new(2,2,2)]; // The last one is outside of board plane

        Assert.Throws<ArgumentException>(() => factory.CreateBoardPlane(2, 2, holes));
    }
    
    [Fact]
    public void BoardPlane_WithSameHoleNumbersIfDisallowedByRules_CannotBeCreated()
    {
        GameFactory factory = new GameFactory(new GameRules(AllowSameHoleNumbers: false));
        
        List<Hole> holes = [new(0,0,0), new(0,1,1)]; // The last one has the same number as the first one

        Assert.Throws<ArgumentException>(() => factory.CreateBoardPlane(2, 2, holes));
    }

    [Fact]
    public void BoardPlane_WithUniqueHoleNumbers_CanBeCreated()
    {
        GameFactory factory = new GameFactory(new GameRules(AllowSameHoleNumbers: false));

        List<Hole> holes = [new(0, 0, 0), new(1, 1, 1)];

        Assert.NotNull(factory.CreateBoardPlane(2, 2, holes));
    }

    [Fact]
    public void BoardPlane_WithSameHoleNumbersIfAllowedByRules_CanBeCreated()
    {
        GameFactory factory = new GameFactory(new GameRules(AllowSameHoleNumbers: true));
        
        List<Hole> holes = [new(0,0,0), new(0,1,1)]; // The last one has the same number as the first one

        Assert.NotNull(factory.CreateBoardPlane(2, 2, holes));
    }
}