using System.Collections.Generic;
using Cybo.Containers;
using Cybo.Ecs.Components;

namespace Cybo.Ecs;

public class World
{
    int _activeEntities;
    ulong _tick;
    EntityId _nextEntity = new EntityId();

    ComponentStorage _components;
    readonly int _maxEntities;

    StaticBuffer<Asset> _activeAssets;
    StaticBuffer<Sprite> _spriteDrawList;

    public World(int maxEntities)
    {
        _maxEntities = maxEntities;
        _components = new ComponentStorage(maxEntities);

        _activeAssets = new StaticBuffer<Asset>(maxEntities);
        _spriteDrawList = new StaticBuffer<Sprite>(maxEntities);

        for (var i = 0; i < 100; i++)
        {
            AddEntity().IsSome(id =>
            {
                _components.Assets.Add(id, out var idx);
                _components.Assets.Values[idx].Assets.Add(new Asset
                {
                    FilePath = "Icon.bmp",
                    Type = AssetType.Texture
                });
                _components.Transforms.Add(id, out idx);
                _components.Transforms.Values[idx].Position = new Vec2(i * 5, i * 2);
            });
        }
    }

    /// <summary>
    /// Attempts to add a new entity.
    /// </summary>
    /// <returns></returns>
    public Option<EntityId> AddEntity()
    {
        var id = _nextEntity;
        if (_activeEntities < _maxEntities)
        {
            _activeEntities += 1;
            _nextEntity = _nextEntity.Next();
            return Option<EntityId>.Some(id);
        }

        return Option<EntityId>.None;
    }

    /// <summary>
    /// Deletes the given entity;
    /// </summary>
    /// <param name="id"></param>
    public void DeleteEntity(EntityId id)
    {
        _components.Storages.ForEach(s => s.Delete(id));
    }

    /// <summary>
    /// Ticks the world.
    /// </summary>
    public void Tick()
    {
        _tick += 1;

        Systems.Gravity(_components.Transforms);
        Systems.CalculateActiveAssets(_components.Transforms, _components.Assets, _activeAssets);
        Systems.RenderSprites(_components.Transforms, _spriteDrawList);
    }

    /// <summary>
    /// The assets that are active in the scene.
    /// </summary>
    /// <value></value>
    public List<Asset> ActiveAssets
    {
        get => _activeAssets.Items;
    }

    /// <summary>
    /// The sprite draw list, in order of sprites to be drawn.
    /// </summary>
    /// <value></value>
    public List<Sprite> SpriteDrawList
    {
        get => _spriteDrawList.Items;
    }
}