using Cybo.Containers;
using Cybo.Ecs.Components;

namespace Cybo.Ecs;

internal static partial class Systems
{
    public static void RenderSprites(
        EntityStore<Transform> transforms
        , StaticBuffer<Sprite> spriteDrawList
        )
    {
        spriteDrawList.Clear();

        transforms.ForEach((entity, transform) =>
        {
            // TODO: add in camera and use there
            var isInView = true;
            if (isInView)
            {
                spriteDrawList.Add(sprite =>
                {
                    sprite.Position = transform.Position;
                    sprite.SubImage = null;
                    sprite.ImageFile = "Icon.bmp";
                    return sprite;
                });
            }
        });
    }
}