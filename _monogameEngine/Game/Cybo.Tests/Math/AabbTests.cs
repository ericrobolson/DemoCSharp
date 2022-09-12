using Xunit;

namespace Cybo.Tests;

public class AabbTests
{
    [Fact]
    public void EqualsReturnsTrueifSame()
    {
        var a = new Aabb(1, 2, 3, 4);
        var b = new Aabb(1, 2, 3, 4);
        Assert.True(a.Equals(b));
    }

    [Fact]
    public void EqualsReturnsFalseIfSizeDifferent()
    {

        var a = new Aabb(new Vec2(1, 2), new Vec2(3, 4));
        var b = new Aabb(new Vec2(1, 2), new Vec2(4, 4));
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsReturnsFalseIfPositionDifferent()
    {

        var a = new Aabb(new Vec2(1, 2), new Vec2(3, 4));
        var b = new Aabb(new Vec2(0, 2), new Vec2(3, 4));
        Assert.False(a.Equals(b));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, 10)]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    [InlineData(10, 0)]
    [InlineData(10, 10)]
    [InlineData(10, 7)]
    [InlineData(7, 10)]
    public void ContainsReturnsTrue(int x, int y)
    {
        var aabb = new Aabb(0, 0, 10, 10);
        var v = new Vec2(x, y);
        Assert.True(aabb.Contains(v));
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(-1, -1)]
    [InlineData(0, -1)]
    [InlineData(11, 0)]
    [InlineData(11, 11)]
    [InlineData(0, 11)]
    public void ContainsReturnsFalse(int x, int y)
    {
        var aabb = new Aabb(0, 0, 10, 10);
        var v = new Vec2(x, y);
        Assert.False(aabb.Contains(v));
    }

    [Fact]
    public void Overlaps_SameAabb_ReturnsTrue()
    {
        var a = new Aabb(0, 0, 10, 10);
        var b = new Aabb(0, 0, 10, 10);

        Assert.True(a.Overlaps(b));
    }

    [Fact]
    public void Overlaps_TooFarLeft_ReturnsFalse()
    {
        var a = new Aabb(0, 0, 10, 10);
        var b = new Aabb(-10, 0, 9, 10);

        Assert.False(a.Overlaps(b));
    }
}