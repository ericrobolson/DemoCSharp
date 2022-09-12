namespace Cybo.Ecs.Components;

/// <summary>
/// A sprite that can be drawn.
/// </summary>
public class Sprite
{
    public Aabb? SubImage = null;
    public Color Color = Color.White();
    public string ImageFile = "";
    public Vec2 Position = new Vec2();
}