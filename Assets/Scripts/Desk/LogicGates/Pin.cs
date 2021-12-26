using System;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Pin : MonoBehaviour
{
	public event Action<Pin, bool> valueChanged;
	public event Action<Pin, Line> lineAdded;
	public event Action<Pin> lineRemoved;

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

	public List<Line> Lines { get; set; } = new List<Line>();

	Outline outline;
	MeshRenderer meshRenderer;
	AbstractGate gate;

	void Awake()
	{
		outline = GetComponent<Outline>();

		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.material = disabledMaterial;

		gate = transform.parent.GetComponent<AbstractGate>();
	}

	public void AddLine(Line line)
	{
		Lines.Add(line);
		lineAdded?.Invoke(this, line);
	}

	public void ClearLines()
	{
		Lines.Clear();
		lineRemoved?.Invoke(this);
	}

	public void Clear()
	{
		foreach (Line line in Lines)
		{
			Destroy(line.gameObject);
			Pin lineStart = line.LineStart;
			Pin lineEnd = line.LineEnd;
			if (lineStart != this)
				lineStart.ClearLines();
			if (lineEnd != this)
				lineEnd.ClearLines();
		}
		ClearLines();
	}

	public bool CanConnect(Pin other)
	{
		if (!other)
		{
			return true;
		}

		if (other.gate == gate)
		{
			return false;
		}

		foreach (Pin pin in other.gate.outputs)
		{
			foreach (Line line in pin.Lines)
			{
				if (!CanConnect(line.LineEnd))
				{
					return false;
				}
			}
		}
		return true;
	}

	public void SetOutline(bool outlineEnabled) => outline.eraseRenderer = !outlineEnabled;
}
