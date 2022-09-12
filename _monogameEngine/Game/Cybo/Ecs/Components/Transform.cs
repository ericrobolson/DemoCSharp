namespace Cybo.Ecs.Components;

/// <summary>
/// A transform for an entity.
/// </summary>
public class Transform
{
    public Vec2 Position = new Vec2();
    public Vec2 PreviousPosition = new Vec2();
}