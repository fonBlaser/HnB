namespace HolesNBalls.Tests.Unit;

[Trait("Unit", "Board")]
public class BoardPlaneTests
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    public void BoardPlane_WithAnyAxisLengthLessThanTwo_CannotBeCreated(int width, int height)
    {
        List<Hole> holes = [new(0,0,0)];
        
        Assert.Throws<ArgumentOutOfRangeException>(() => new BoardPlane(width, height, holes));
    }

    [Fact]
    public void BoardPlane_WithoutHoles_CannotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new BoardPlane(2, 2, []));
    }

    [Fact]
    public void BoardPlane_WithoutFreeSlot_CannotBeCreated()
    {
        List<Hole> holes = Enumerable.Range(0, 4)
                                     .Select(i => new Hole(i, i % 2, i / 2))
                                     .ToList();
        
        Assert.Throws<ArgumentException>(() => new BoardPlane(2, 2, holes));
    }
    
    [Fact]
    public void BoardPlane_WithHoleOutOfBoundaries_CannotBeCreated()
    {
        List<Hole> holes = [new(0,0,0), new(1,1,1), new(2,2,2)]; // The last one is outside of board plane

        Assert.Throws<ArgumentException>(() => new BoardPlane(2, 2, holes));
    }
}