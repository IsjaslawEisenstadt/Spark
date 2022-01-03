using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GateDrawer : MonoBehaviour, IUIElement
{
	public GateSelector gateSelector;
	public GameObject gateButtonPrefab;
	public GameObject gateButtonContainer;
	public event Action<BaseGate> onGateTypeChanged;

	BaseGate baseGate;

	void Awake() => GenerateButtons();

	protected virtual void GenerateButtons()
	{
		Enum.GetValues(typeof(GateType))
			.Cast<GateType>()
			.Where(entry => entry != GateType.Default)
			.ToList()
			.ForEach(entry => GenerateGateButton(entry.ToString()));
	}

	public void OnGateSelected(string gateName)
	{
		baseGate.SetGateType(gateName);
		onGateTypeChanged?.Invoke(baseGate);
	}

	public void OnOpen()
	{
		gameObject.SetActive(true);

		if (baseGate)
			baseGate.Selected = false;

		baseGate = gateSelector.CurrentSelectedObject.transform.parent.parent.gameObject.GetComponent<BaseGate>();
		baseGate.Selected = true;
	}

	public void OnClose()
	{
		if (baseGate)
			baseGate.Selected = false;

		gameObject.SetActive(false);
	}

	protected void GenerateGateButton(string gateName)
	{
		GameObject gateButton = Instantiate(gateButtonPrefab, gateButtonContainer.transform);
		gateButton.GetComponent<Button>().onClick.AddListener(() => OnGateSelected(gateName + "Gate"));
		gateButton.transform.GetChild(0).GetComponent<TMP_Text>().text = gateName;
	}
}
