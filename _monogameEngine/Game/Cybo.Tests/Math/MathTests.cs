using Xunit;

namespace Cybo.Tests;

public class MathTests
{
    [Fact]
    public void ULongWraps()
    {
        ulong a = ulong.MaxValue;
        ulong expected = 0;
        Assert.Equal(expected, a + 1);
    }
}