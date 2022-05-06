using Xunit;
using App.Ecs;
using App.Ecs.ComponentStore;
using System.Collections.Generic;


namespace App.Ecs.ComponentStore.Tests
{
	public class ComponentStoreTests
	{
		static int capacity = 10;
		ComponentStore<int> store = new ComponentStore<int>(capacity);

		[Fact]
		public void GetOnEmptyStoreReturnsNone()
		{
			var e = new EntityId(0);
			var has = store.Has(e, out var idx);
			Assert.False(has);
			Assert.Equal(-1, idx);
		}

		[Fact]
		public void AddWorks()
		{
			var e = new EntityId(0);
			var canAdd = store.Add(e, out var idx);
			Assert.True(canAdd);
			Assert.Equal(0, idx);

			var has = store.Has(e, out idx);
			Assert.True(has);
			Assert.Equal(0, idx);

			// Add a dummy one
			store.Add(new EntityId(1), out _);

			has = store.Has(e, out idx);
			Assert.True(has);
			Assert.Equal(0, idx);

			// Ensure we reference the same one
			canAdd = store.Add(e, out idx);
			Assert.True(has);
			Assert.Equal(0, idx);
		}

		[Fact]
		public void RemoveWorks()
		{
			var e = new EntityId(0);
			store.Add(e, out var idx);

			var e2 = new EntityId(1);
			store.Add(e2, out var idx2);

			Assert.Equal(0, idx);
			Assert.Equal(1, idx2);

			// Remove the first
			store.Remove(e);
			store.Remove(e);

			// Check to see if we still have the first
			var has = store.Has(e, out idx);
			Assert.False(has);
			Assert.Equal(-1, idx);

			// Make sure second is shifted
			Assert.Equal(1, store.activeComponents);
			has = store.Has(e2, out idx);
			Assert.True(has);
			Assert.Equal(0, idx);

			// nuke second
			store.Remove(e2);
			store.Remove(e2);
			store.Remove(e2);

			Assert.Equal(0, store.activeComponents);
		}
	}
}
