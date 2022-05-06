using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Engine;

public class EngineScaffolding : MonoBehaviour
{

	private Engine _engine = new Engine();
	private EngineRenderSystem _renderSystem = new EngineRenderSystem();
	private Camera _camera;

	// Start is called before the first frame update
	void Start()
	{
		App.Logger.SetLogger(log =>
		{
			print(log);
		});

		_renderSystem.Start(_camera, _engine);
	}

	private void SetInputKey(string key, App.Input.InputKey mapped)
	{
		if (Input.GetKeyDown(key))
		{
			_engine.Input().SetInputKey(mapped, true);
		}
		else if (Input.GetKeyUp(key))
		{
			_engine.Input().SetInputKey(mapped, false);
		}
	}

	private void SetInputs()
	{
		SetInputKey("w", App.Input.InputKey.KbW);
		SetInputKey("a", App.Input.InputKey.KbA);
		SetInputKey("s", App.Input.InputKey.KbS);
		SetInputKey("d", App.Input.InputKey.KbD);
	}

	// Update is called once per frame
	void Update()
	{
		// Do inputs
		SetInputs();

		// Do update
		_engine.Update(Time.deltaTime);

		// Do render
		_renderSystem.Render(Time.deltaTime, _engine);
	}
}
