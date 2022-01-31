using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GateDrawer : MonoBehaviour, IUIElement
{
	public GateSelector gateSelector;
	public GameObject gateButtonPrefab;
	public GameObject gateButtonContainer;
	public event Action<BaseGate> gateTypeChanged;

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
		gateTypeChanged?.Invoke(baseGate);
	}

	public virtual void OnOpen()
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

	protected virtual Button GenerateGateButton(string gateName)
	{
		GameObject gateButton = Instantiate(gateButtonPrefab, gateButtonContainer.transform);
		Button button = gateButton.GetComponent<Button>();
		button.onClick.AddListener(() => OnGateSelected(gateName + "Gate"));
		gateButton.transform.GetChild(0).GetComponent<TMP_Text>().text = gateName;
		return button;
	}
}
