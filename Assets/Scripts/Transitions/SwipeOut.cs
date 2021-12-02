using UnityEngine;

public class SwipeOut : Transition
{
	static readonly int cutOff = Shader.PropertyToID("_CutOff");

	public override void Restart()
	{
		base.Restart();
		Material.SetFloat(cutOff, 0.0f);
	}

	public override void Step(float delta)
	{
		base.Step(delta);
		Material.SetFloat(cutOff, EasingFunc(0.0f, 1.0f,  Time / Length));
	}

	public override bool IsBlockingClicks() => true;
}
