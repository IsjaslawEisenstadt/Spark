using UnityEngine;

public class SwipeIn : Transition
{
	static readonly int cutOff = Shader.PropertyToID("_CutOff");

	public override void Restart()
	{
		base.Restart();
		Material.SetFloat(cutOff, 1.0f);
	}

	public override void Step(float delta)
	{
		base.Step(delta);
		Material.SetFloat(cutOff, EasingFunc(0.0f, 1.0f, 1.0f - Time / Length));
	}

	public override bool IsBlockingClicks() => Material.GetFloat(cutOff) > 0.3f;
}
