using System;
using UnityEngine;

public class GateDrawer : MonoBehaviour
{
	BaseGate baseGateScript;

	public void OnCloseGateDrawer()
	{
		if (baseGateScript)
		{
			baseGateScript.Selected = false;
		}

		gameObject.SetActive(false);
	}

	public void OnGateSelected(string gateName) => baseGateScript.SetGateType(gateName);

	public void SetCurrentGateObject(GameObject gateObject)
	{
		baseGateScript = gateObject.GetComponent<BaseGate>();
		baseGateScript.Selected = true;
	}
}
