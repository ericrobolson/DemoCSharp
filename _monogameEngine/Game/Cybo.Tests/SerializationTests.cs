using System.Text.Json;
using Xunit;

namespace Cybo.Tests;

public class UnitTest1
{
    [Fact]
    public void AssetTest()
    {
        var value = new Asset { FilePath = "asdf", Type = AssetType.Texture };
        var expected = "{\"FilePath\":\"asdf\",\"Type\":0}";

        Assert.Equal(expected, JsonSerializer.Serialize(value));
    }

    [Fact]
    public void Vec2Test()
    {
        var value = new Vec2(123, 456);
        var expected = "{\"X\":123,\"Y\":456}";

        Assert.Equal(expected, JsonSerializer.Serialize(value));
    }

    [Fact]
    public void AabbTest()
    {
        var value = new Aabb(1, 2, 3, 4);
        var expected = "{\"Position\":{\"X\":1,\"Y\":2},\"Size\":{\"X\":3,\"Y\":4}}";

        Assert.Equal(expected, JsonSerializer.Serialize(value));
    }
}