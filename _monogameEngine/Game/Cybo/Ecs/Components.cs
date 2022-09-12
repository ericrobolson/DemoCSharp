using System.Collections.Generic;
using Cybo.Ecs.Components;

namespace Cybo.Ecs;

/// <summary>
/// Simple wrapper for all stored components.
/// </summary>
public class ComponentStorage
{
    internal EntityStore<Transform> Transforms { get; private set; }
    internal EntityStore<EntityAssets> Assets { get; private set; }
    internal List<IEntityStorage> Storages { get; private set; }

    public ComponentStorage(int maxEntities)
    {
        Transforms = new EntityStore<Transform>(maxEntities);
        Assets = new EntityStore<EntityAssets>(maxEntities);

        // This enables us to delete all entities for each storage
        Storages = new List<IEntityStorage>{
            Transforms, Assets,
        };
    }

}