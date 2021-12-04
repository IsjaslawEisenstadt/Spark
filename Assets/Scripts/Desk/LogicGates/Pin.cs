using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Pin : MonoBehaviour
{
	public event Action<Pin, bool> valueChanged;
	public event Action<Pin, Line> lineChanged;

	public Material enabledMaterial;
	public Material disabledMaterial;

	bool state;

	public bool State
	{
		get => state;
		set
		{
			state = value;
			meshRenderer.material = value ? enabledMaterial : disabledMaterial;
			valueChanged?.Invoke(this, value);
		}
	}

	Line line;

	public Line Line
	{
		get => line;
		set
		{
			line = value;
			lineChanged?.Invoke(this, value);
			if (value)
				State = value.LineStart.gameObject.GetComponent<Pin>().State;
		}
	}

	MeshRenderer meshRenderer;

	void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.material = disabledMaterial;
	}
}
