using System.Collections.Generic;

namespace App.Ecs
{
	public interface IComponentStore
	{
		void Reset();
		void Remove(EntityId entity);
	}

	public class ComponentStore<T> : IComponentStore where T : new()
	{
		internal List<EntityId> entities;

		/// <summary>
		/// The list of components.
		/// Inactive components are shifted back.
		/// You should not modify this, only index into it with
		/// ids from Add() or Has()
		/// </summary>
		/// <value></value>
		public List<T> components { get; private set; }

		/// <summary>
		/// The number of active components.
		/// </summary>
		/// <value></value>
		public int activeComponents { get; private set; } = 0;


		private int _capacity;

		/// <summary>
		/// Creates a new component store with the given capacity.
		/// </summary>
		/// <param name="capacity"></param>
		public ComponentStore(int capacity)
		{
			// Create static list of components
			entities = new List<EntityId>(capacity);
			components = new List<T>(capacity);
			for (int i = 0; i < capacity; i++)
			{
				entities.Add(new EntityId(0));
				components.Add(new T());
			}

			_capacity = capacity;
		}

		/// <summary>
		/// Attempts to add a new component for the entity.
		/// Returns true if it could be added.
		/// Returns false if it could not.
		/// Returns the index for the component if it was added.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="componentIdx"></param>
		/// <returns></returns>
		public bool Add(EntityId entity, out int componentIdx)
		{
			if (Has(entity, out componentIdx))
			{
				return true;
			}

			componentIdx = -1;

			if (activeComponents >= _capacity)
			{
				return false;
			}

			componentIdx = activeComponents;
			entities[componentIdx] = entity;
			activeComponents += 1;

			return true;
		}


		/// <summary>
		/// Checks if there is a component for the given entity.
		/// Returns true if so.
		/// Returns false if not.
		/// Returns the index for the component if it exists.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="componentIdx"></param>
		/// <returns></returns>
		public bool Has(EntityId entity, out int componentIdx)
		{
			for (int idx = 0; idx < activeComponents; idx++)
			{
				if (entities[idx].Equals(entity))
				{
					componentIdx = idx;
					return true;
				}
			}

			componentIdx = -1;
			return false;
		}

		/// <summary>
		/// Removes the given component for the given entity.
		/// </summary>
		/// <param name="entity"></param>
		public void Remove(EntityId entity)
		{
			for (int idx = 0; idx < activeComponents; idx++)
			{
				if (entities[idx].Equals(entity))
				{
					// Swap components + entity
					if (activeComponents >= 1)
					{
						var swapIdx = activeComponents - 1;
						var swapEntity = entities[swapIdx];
						var swapComponent = components[swapIdx];

						entities[swapIdx] = entities[idx];
						components[swapIdx] = components[idx];

						entities[idx] = swapEntity;
						components[idx] = swapComponent;
					}

					activeComponents -= 1;
					return;
				}
			}
		}

		public void Reset()
		{
			activeComponents = 0;
		}
	}
}

