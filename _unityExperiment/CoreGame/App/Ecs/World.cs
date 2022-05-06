using System;
using System.Collections.Generic;
using App.Ecs.Assemblages;
using App.Ecs.Components;
using App.Ecs.Systems;

namespace App.Ecs
{
	public class World
	{
		//
		// Entity
		//
		private EntityId _nextEntity = new EntityId(0);

		//
		// Components
		//

		public ComponentStore<Aabb> aabb;
		public ComponentStore<Camera> camera;
		public ComponentStore<Collideable> collideable;
		public ComponentStore<Collision> collision;
		public ComponentStore<Controllable> controllable;
		public ComponentStore<Renderable> renderable;
		public ComponentStore<Vec2> position;
		public ComponentStore<Vec2> prevPosition;

		//
		// Book keeping
		//

		private List<ISystem> _systems;
		private List<EntityId> _deadEntities;
		private List<IComponentStore> _components;

		static int MAX_ENTITIES = 10000;

		private static ComponentStore<T> Store<T>() where T : new()
		{
			return new ComponentStore<T>(MAX_ENTITIES);
		}

		public World()
		{

			_deadEntities = new List<EntityId>(MAX_ENTITIES);

			aabb = Store<Aabb>();
			camera = new ComponentStore<Camera>(3);
			collideable = Store<Collideable>();
			collision = Store<Collision>();
			controllable = Store<Controllable>();
			position = Store<Vec2>();
			prevPosition = Store<Vec2>();
			renderable = Store<Renderable>();

			// TODO: add static uint as a type id for components. This may allow dynamic querying or
			// dynamic scheduling of jobs.
			_components = new List<IComponentStore>{
				position,
				prevPosition,
				aabb,
				camera,
				collideable,
				collision,
				controllable,
				renderable,
			};

			_systems = new List<ISystem> {
				new SysMovement(ref position, ref prevPosition),
				new SysControl(ref position, ref controllable),
				new SysRender(ref position, ref prevPosition, ref renderable, ref camera),
			 };

			// Ensure we store all reset logic in here.
			Reset();
			TestWorld();
		}

		private void TestWorld()
		{
			Assemblage.Controllable(this);
			Assemblage.CameraPlatformer(this);
		}

		/// <summary>
		/// Adds a new entity.
		/// </summary>
		/// <returns></returns>
		public EntityId AddEntity()
		{
			var e = _nextEntity;
			_nextEntity = new EntityId(e.id + 1);

			return e;
		}

		/// <summary>
		/// Removes the given entity.
		/// </summary>
		/// <param name="entity"></param>
		public void RemoveEntity(EntityId entity)
		{
			_deadEntities.Add(entity);
		}

		/// <summary>
		/// Resets the entire world.
		/// </summary>
		public void Reset()
		{
			// Rarely called, dgaf about gc
			_nextEntity = new EntityId(0);
			_deadEntities.Clear();

			foreach (var system in _systems)
			{
				system.Reset();
			}

			foreach (var component in _components)
			{
				component.Reset();
			}
		}

		private EntityId cam;

		/// <summary>
		///	Performs a single tick of the world.
		/// </summary>
		public void Tick()
		{


			//
			// Keep this as fast as possible w/ minimal allocations
			//

			int i, j = 0;


			// debug
			for (i = 0; i < position.activeComponents; i++)
			{
				App.Logger.Log($"active cams: {camera.activeComponents}");
				App.Logger.Log($"cam id: {cam.id}, e: ${position.entities[i].id}, x: {position.components[i].x}, y: {position.components[i].y}");
			}
			// end debug


			// Tick all systems
			for (i = 0; i < _systems.Count; i++)
			{
				_systems[i].Reset();
				_systems[i].Tick();
			}

			// Clean up dead entities
			for (i = 0; i < _components.Count; i++)
			{
				// Since _components[i] is in cache, do all entities then
				for (j = 0; j < _deadEntities.Count; j++)
				{
					_components[i].Remove(_deadEntities[j]);
				}
			}

			// TODO: replace with object pool for speed if needed
			_deadEntities.Clear();
		}
	}
}
