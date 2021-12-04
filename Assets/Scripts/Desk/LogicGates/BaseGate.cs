using cakeslice;
using UnityEngine;

public class BaseGate : MonoBehaviour
{
	GameObject activeGate;
	bool selected;

	public GateInformation gateInformation;

	public bool Selected
	{
		get => selected;
		set
		{
			selected = value;
			SetOutline(value);
			ShowGateInformation(value, activeGate.GetComponent<AbstractGate>());
		}
	}

	void Awake()
	{
		// The gate that is active by default has to be found and saved in currentSelectedGate
		foreach (object child in gameObject.transform)
		{
			if (child is Transform childTransform && childTransform.gameObject.activeSelf)
			{
				activeGate = childTransform.gameObject;
				return;
			}
		}
	}

	public void SetGateType(string gateName)
	{
		SetOutline(false);
		activeGate.SetActive(false);
		activeGate = gameObject.transform.Find(gateName).gameObject;
		activeGate.SetActive(true);
		SetOutline(true);

		ShowGateInformation(true, activeGate.GetComponent<AbstractGate>());
		PinSettings.Instance.SetVisualizationState(activeGate);
	}

	void SetOutline(bool outlineEnabled) => activeGate.GetComponent<Outline>().eraseRenderer = !outlineEnabled;

	void ShowGateInformation(bool show, AbstractGate gateObject) =>
		gateInformation.ShowGateInformation(show, gateObject);
}
