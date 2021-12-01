using UnityEngine;

public class FadeIn : Transition
{
	static readonly int colorID = Shader.PropertyToID("_Color");

	[field: SerializeField, ColorUsageAttribute(showAlpha: false)] 
	Color Color { get; set; }
	
	public override void Restart()
	{
		base.Restart();
		Material.SetColor(colorID, new Color(Color.r, Color.g, Color.b, 1.0f));
	}

	public override void Step(float delta)
	{
		base.Step(delta);
		float newAlpha = easingFunction(0.0f, 1.0f, Time / Length);
		Material.SetColor(colorID, new Color(Color.r, Color.g, Color.b, newAlpha));
	}

	public override bool IsBlockingClicks() => Material.GetColor(colorID).a > 0.5f;
}