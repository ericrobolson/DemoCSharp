using Xunit;
using App.Ecs;
using App.Ecs.ComponentStore;
using System.Collections.Generic;


namespace App.Ecs.Query.Tests
{
	public class QueryTests
	{
		static int capacity = 10;
		ComponentStore<int> storeA = new ComponentStore<int>(capacity);
		ComponentStore<bool> storeB = new ComponentStore<bool>(capacity);

		Query<int, bool> query = new Query<int, bool>();

		[Fact]
		public void NoActiveReturnsFalse()
		{
			Assert.False(query.Join(storeA, storeB));
		}

		[Fact]
		public void OneActiveReturnsFalse()
		{
			storeA.Add(new EntityId(1), out var idx);

			Assert.False(query.Join(storeA, storeB));
		}

		[Fact]
		public void OneJoinedReturnsTrueWhenSameIdx()
		{
			var e1 = new EntityId(0);
			var idx = 0;
			storeA.Add(e1, out idx);
			storeB.Add(e1, out idx);

			Assert.True(query.Join(storeA, storeB));
			Assert.Equal(e1, query.Result().entity);
			Assert.Equal(0, query.Result().leftComponentIndex);
			Assert.Equal(0, query.Result().rightComponentIndex);

			Assert.False(query.Join(storeA, storeB));
		}

		[Fact]
		public void OneJoinedReturnsTrueWhenoffset()
		{
			var e1 = new EntityId(0);
			var e2 = new EntityId(1);
			var idx = 0;
			storeA.Add(e2, out idx);
			storeA.Add(e1, out idx);
			storeB.Add(e1, out idx);

			Assert.True(query.Join(storeA, storeB));
			Assert.Equal(e1, query.Result().entity);
			Assert.Equal(1, query.Result().leftComponentIndex);
			Assert.Equal(0, query.Result().rightComponentIndex);

			Assert.False(query.Join(storeA, storeB));
		}

		[Fact]
		public void OneJoinedReturnsTrueWhenDifferentIdx()
		{
			var e1 = new EntityId(0);
			var e2 = new EntityId(1);
			var idx = 0;
			storeA.Add(e2, out idx);
			storeA.Add(e1, out idx);
			storeB.Add(e1, out idx);

			Assert.True(query.Join(storeA, storeB));
			Assert.Equal(e1, query.Result().entity);
			Assert.Equal(1, query.Result().leftComponentIndex);
			Assert.Equal(0, query.Result().rightComponentIndex);

			Assert.False(query.Join(storeA, storeB));
		}
	}
}
