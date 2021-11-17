using System;
using System.Linq;
using UnityEngine;

public abstract class AbstractGate : MonoBehaviour
{
	public Pin[] inputs;
	public Pin[] outputs;
	
	void Awake()
	{
		foreach (Pin input in inputs)
		{
			print("Adding input pin");
			
			input.valueChanged += Refresh;
			input.lineChanged += (pin, line) => pin.State = line && line.LineStart.GetComponent<Pin>().State;
		}

		foreach (Pin output in outputs)
		{
			print("Adding output pin");
			
			output.valueChanged += () =>
			{
				if (output.Line)
					output.Line.LineEnd.GetComponent<Pin>().State = output.State;
			};
		}
	}

	void OnDestroy()
	{
		// TODO: sign off event functions (extract lambdas into functions)
	}

	protected void Refresh()
	{
		bool[] outputValues = EvaluateSelf();

		foreach (var value in outputValues.Select((value, i) => new { value, i }))
		{
			outputs[value.i].State = value.value;
		}
	}

	protected bool[] EvaluateSelf()
	{
		return Evaluate(inputs.Select(pin => pin.State).ToArray());
	}
	
	protected abstract bool[] Evaluate(bool[] values);
}
