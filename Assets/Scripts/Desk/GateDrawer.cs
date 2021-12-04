using System;
using UnityEngine;
using UnityEngine.Events;

public class GateDrawer : MonoBehaviour
{
	public event Action<BaseGate> onGateTypeChanged;

	BaseGate baseGate;

	public void OnGateSelected(string gateName)
	{
		baseGate.SetGateType(gateName);
		onGateTypeChanged?.Invoke(baseGate);
	}

	public void Open(GameObject gateObject)
	{
		gameObject.SetActive(true);

		if (baseGate)
		{
			baseGate.Selected = false;
		}

		baseGate = gateObject.GetComponent<BaseGate>();
		baseGate.Selected = true;
	}

	public void Close()
	{
		if (baseGate)
		{
			baseGate.Selected = false;
		}

		PinSettings.Instance.Close();
		HiddenGateArrow.Instance.Deactivate();

		gameObject.SetActive(false);
	}
}
