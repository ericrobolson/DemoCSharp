namespace App.Ecs.Systems
{
	public interface ISystem
	{
		void Reset();
		void Tick();
	}
}
