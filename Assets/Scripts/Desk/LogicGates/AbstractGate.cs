using System;
using System.Collections.Generic;
using System.Linq;
using cakeslice;
using UnityEngine;
using UnityEngine.Assertions;

public enum GateType
{
	Default,
	And,
	Or,
	Not,
	Nand,
	Nor,
	Xor,
	Xnor,
	Source,
	Sink,
	Adder,
	HalfAdder
}

public abstract class AbstractGate : MonoBehaviour
{
	public GateType GateType { get; protected set; }

	public List<Pin> inputs;
	public List<Pin> outputs;

	public List<Outline> outlines;

	void Awake()
	{
		foreach (Pin input in inputs)
		{
			input.valueChanged += OnInputValueChanged;
			input.lineChanged += OnInputLineChanged;
		}

		foreach (Pin output in outputs)
		{
			output.valueChanged += OnOutputValueChanged;
		}
	}

	void OnDestroy()
	{
		foreach (Pin input in inputs)
		{
			input.valueChanged -= OnInputValueChanged;
			input.lineChanged -= OnInputLineChanged;
		}

		foreach (Pin output in outputs)
		{
			output.valueChanged -= OnOutputValueChanged;
		}
	}

	public void SetOutline(bool outlineEnabled)
	{
		foreach (Outline outline in outlines)
		{
			outline.eraseRenderer = !outlineEnabled;
		}
	}

	protected void Refresh()
	{
		bool[] outputValues = EvaluateSelf();

		Assert.IsNotNull(outputValues);

		for (int i = 0; i < outputs.Count; ++i)
		{
			outputs[i].State = outputValues[i];
		}
	}

	protected bool[] EvaluateSelf()
	{
		return Evaluate(inputs.Select(pin => pin.State).ToArray());
	}

	public abstract bool[] Evaluate(bool[] values);

	public abstract void InitGateType();

	void OnInputValueChanged(Pin pin, bool value)
	{
		Refresh();
	}

	static void OnInputLineChanged(Pin pin, Line line)
	{
		pin.State = line && line.LineStart.GetComponent<Pin>().State;
	}

	static void OnOutputValueChanged(Pin pin, bool value)
	{
		if (pin.Line)
			pin.Line.LineEnd.GetComponent<Pin>().State = value;
	}
}
