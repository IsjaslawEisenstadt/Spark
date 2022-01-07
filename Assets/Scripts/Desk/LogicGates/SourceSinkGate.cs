using System.Collections.Generic;
using UnityEngine;

public abstract class SourceSinkGate : AbstractGate
{
	[SerializeField] GameObject pinPrefab;

	[SerializeField] GameObject box;
	[SerializeField] float boxWidth = 0.02f;

	public void CreatePin(PinType pinType)
	{
		Pin newPin = Instantiate(pinPrefab, transform).GetComponent<Pin>();

		if (pinType == PinType.Input)
		{
			newPin.valueChanged += OnInputValueChanged;
			newPin.lineAdded += OnInputLineAdded;
			newPin.lineRemoved += OnInputLineRemoved;
			newPin.tag = "LogicGateInput";
			inputs.Add(newPin);
		}
		else
		{
			newPin.valueChanged += OnOutputValueChanged;
			newPin.tag = "LogicGateOutput";
			outputs.Add(newPin);
		}
	}

	public void UpdatePinPositions()
	{
		List<Pin> pins = GetPins();
		float xPos = pins[0].transform.localPosition.x;
		for (int i = 0; i < pins.Count; ++i)
		{
			pins[i].transform.localPosition = new Vector3(xPos, 0.0f, (2.0f * i + 1.0f - pins.Count) * boxWidth / 2);
		}

		Vector3 scale = box.transform.localScale;
		box.transform.localScale = new Vector3(scale.x, scale.y, pins.Count);
	}

	public abstract List<Pin> GetPins();
}
