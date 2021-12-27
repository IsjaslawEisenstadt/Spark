using System;
using UnityEngine;

public enum TransitionType
{
	FadeIn,
	FadeOut,
	SwipeIn,
	SwipeOut
}

public abstract class Transition : MonoBehaviour
{
	[SerializeField] Material materialTemplate;

	public Material Material { get; private set; }

	[field: SerializeField] public float Length { get; set; } = 1.0f;

	[SerializeField] EasingFunction.Ease easingType = EasingFunction.Ease.Linear;

	public EasingFunction.Ease EasingType
	{
		get => easingType;
		set
		{
			easingType = value;
			EasingFunc = EasingFunction.GetEasingFunction(easingType);
		}
	}

	protected EasingFunction.Function EasingFunc { get; private set; }

	protected float Time { get; private set; }

	void Awake() => Material = Instantiate(materialTemplate);

	public bool IsFinished() => Time <= 0.0f;
	public virtual void Restart() => Time = Length;
	public virtual void Step(float delta) => Time -= delta;
	public virtual bool IsBlockingClicks() => false;
}
