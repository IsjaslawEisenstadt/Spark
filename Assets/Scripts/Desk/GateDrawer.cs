using System;
using UnityEngine;

public class GateDrawer : MonoBehaviour, IUIElement
{
	public GateSelector gateSelector;
	public event Action<BaseGate> onGateTypeChanged;

	BaseGate baseGate;


	public void OnGateSelected(string gateName)
	{
		baseGate.SetGateType(gateName);
		onGateTypeChanged?.Invoke(baseGate);
	}

	public void OnOpen()
	{
		gameObject.SetActive(true);

		if (baseGate)
		{
			baseGate.Selected = false;
		}

		baseGate = gateSelector.CurrentSelectedObject.transform.parent.parent.gameObject.GetComponent<BaseGate>();
		baseGate.Selected = true;
	}

	public void OnClose()
	{
		if (baseGate)
		{
			baseGate.Selected = false;
		}

		gameObject.SetActive(false);
	}
}
