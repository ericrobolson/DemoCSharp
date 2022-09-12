using Cybo.Containers;
using Cybo.Ecs.Components;

namespace Cybo.Ecs;

internal static partial class Systems
{
    public static void CalculateActiveAssets(
        EntityStore<Transform> transforms
        , EntityStore<EntityAssets> assets
        , StaticBuffer<Asset> activeAssets
        )
    {
        activeAssets.Clear();

        transforms.ForEach((entity, transform) =>
        {
            // TODO: add in camera and use there
            var isInView = true;
            if (isInView)
            {
                if (assets.GetIdx(entity, out var assetIdx))
                {
                    assets.Values[assetIdx].Assets.ForEach(asset =>
                    {
                        activeAssets.Add(aa =>
                        {
                            aa.Type = asset.Type;
                            aa.FilePath = asset.FilePath;

                            return aa;
                        });
                    });
                }
            }
        });
    }
}