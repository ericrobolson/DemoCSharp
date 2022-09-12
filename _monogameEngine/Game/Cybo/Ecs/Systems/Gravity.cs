using Cybo.Ecs.Components;

namespace Cybo.Ecs;

internal static partial class Systems
{
    public static void Gravity(EntityStore<Transform> transforms)
    {
        transforms.ForEachComponent(transform =>
        {
            transform.PreviousPosition = transform.Position;
            transform.Position.X += 1;
        });
    }
}