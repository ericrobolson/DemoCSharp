using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Engine;
using App.Ecs;
using App.Ecs.Components;

public class EngineRenderSystem
{

	static int RENDER_CAPACITY = 1000;
	private List<GameObject> _renderables = new List<GameObject>(RENDER_CAPACITY);
	private List<GameObject> _aabbRenderables = new List<GameObject>(RENDER_CAPACITY);
	private bool _renderAabbs = true;
	private UnityEngine.Camera _camera;

	private Query<Vec2, Renderable> _renderQuery = new Query<Vec2, Renderable>();

	// Start is called before the first frame update
	public void Start(UnityEngine.Camera camera, Engine engine)
	{
		_camera = camera;
		_camera = UnityEngine.Camera.main;
		_camera.enabled = true;

		// Setup renderables
		var aabbMat = Resources.Load("Materials/DebugAabb", typeof(Material)) as Material;
		aabbMat.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f, 0.5f));

		for (int i = 0; i < RENDER_CAPACITY; i++)
		{
			// Setup regular renderables
			{
				var renderable = GameObject.CreatePrimitive(PrimitiveType.Cube);
				renderable.transform.position = new Vector3(0, 0.5f, 0);
				renderable.SetActive(false);
				_renderables.Add(renderable);
			}
			// Setup aabbs
			{
				var renderable = GameObject.CreatePrimitive(PrimitiveType.Cube);
				renderable.transform.position = new Vector3(0, 0.5f, 0);
				renderable.GetComponent<Renderer>().material = aabbMat;

				renderable.SetActive(false);
				_aabbRenderables.Add(renderable);
			}
		}
	}

	private void RenderRenderables(Engine engine)
	{
		_renderQuery.Reset();
		int i = 0;
		while (_renderQuery.Join(engine.world.position, engine.world.renderable))
		{
			var e = _renderQuery.Result();

			var x = engine.world.position.components[e.leftComponentIndex].x;
			var y = engine.world.position.components[e.leftComponentIndex].y;
			var z = engine.world.renderable.components[e.rightComponentIndex].z;

			// If entity is a camera, then use the camera
			if (engine.world.camera.Has(e.entity, out var componentIdx))
			{
				_camera.transform.position = new Vector3(x, y, z);
				_camera.orthographic = engine.world.camera.components[componentIdx].orthographic;

				var xrot = engine.world.camera.components[componentIdx].xRotationDegrees;
				var yrot = engine.world.camera.components[componentIdx].yRotationDegrees;
				var zrot = engine.world.camera.components[componentIdx].zRotationDegrees;

				var rot = new Quaternion();
				rot.eulerAngles = new Vector3(xrot, yrot, zrot);
				_camera.transform.rotation = rot;
			}
			else
			{
				_renderables[i].SetActive(true);
				_renderables[i].transform.position = new Vector3(x, y, z);
			}

			i += 1;
		}

		// Mark other things as inactive
		// TODO: could potentially optimize this in the future
		for (i = engine.world.renderable.activeComponents; i < _renderables.Count; i++)
		{
			_renderables[i].SetActive(false);
		}
	}

	private void RenderAabbs(Engine engine)
	{
		_renderQuery.Reset();
		int i = 0;
		int aabbIdx = 0;
		while (_renderQuery.Join(engine.world.position, engine.world.renderable))
		{
			var e = _renderQuery.Result();

			if (engine.world.aabb.Has(e.entity, out aabbIdx))
			{
				// Note: Unity treats Y as vertically up/down, but in the engine that is z.
				var x = engine.world.position.components[e.leftComponentIndex].x;
				var y = engine.world.position.components[e.leftComponentIndex].y;
				var z = engine.world.renderable.components[e.rightComponentIndex].z;

				var xScale = engine.world.aabb.components[aabbIdx].size.x;
				var yScale = 1;
				var zScale = engine.world.aabb.components[aabbIdx].size.y;

				_aabbRenderables[i].SetActive(true);
				_aabbRenderables[i].transform.position = new Vector3(x, y, z);
				_aabbRenderables[i].transform.localScale = new Vector3(xScale, yScale, zScale);
				i += 1;
			}
		}

		// Mark other things as inactive
		// TODO: could potentially optimize this in the future
		for (i = engine.world.aabb.activeComponents; i < _aabbRenderables.Count; i++)
		{
			_aabbRenderables[i].SetActive(false);
		}
	}

	// Update is called once per frame
	public void Render(float deltaT, Engine engine)
	{
		// Do render
		engine.Render(deltaT);
		RenderRenderables(engine);
		RenderAabbs(engine);
	}
}
