using Xunit;

namespace Cybo.Tests;

public class Vec2Tests
{
    [Fact]
    public void EqualsReturnsFalseIfXNotSame()
    {
        var a = new Vec2(0, 234);
        var b = new Vec2(123, 234);
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsReturnsFalseIfYNotSame()
    {
        var a = new Vec2(0, 3);
        var b = new Vec2(0, 234);
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsReturnsTrueifSame()
    {
        var a = new Vec2(123, 234);
        var b = new Vec2(123, 234);
        Assert.True(a.Equals(b));
    }
}