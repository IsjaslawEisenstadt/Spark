using UnityEngine;

public class GateDrawer : MonoBehaviour
{
	BaseGate baseGate;

	public void OnGateSelected(string gateName)
	{
		baseGate.SetGateType(gateName);
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
