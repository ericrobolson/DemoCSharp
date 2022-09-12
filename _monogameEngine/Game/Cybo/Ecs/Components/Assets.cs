using System.Collections.Generic;

namespace Cybo.Ecs.Components;

/// <summary>
/// A list of assets for an entity.
/// </summary>
public class EntityAssets
{
    public List<Asset> Assets = new List<Asset>();
}