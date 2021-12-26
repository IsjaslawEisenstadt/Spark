using UnityEngine;

public class BaseGate : MonoBehaviour
{
	public GateInformation gateInformation;

	[field: SerializeField] public AbstractGate ActiveGate { get; private set; }

	bool selected;

	public bool Selected
	{
		get => selected;
		set
		{
			selected = value;
			SetOutline(value);
			ShowGateInformation(value, ActiveGate);
		}
	}

	void Start()
	{
		foreach (Transform child in gameObject.transform)
		{
			child.gameObject.GetComponent<AbstractGate>()?.InitGateType();
		}
	}

	public void SetGateType(string gateName)
	{
		SetOutline(false);
		ActiveGate.Clear();
		ActiveGate.gameObject.SetActive(false);
		ActiveGate = gameObject.transform.Find(gateName).GetComponent<AbstractGate>();
		ActiveGate.gameObject.SetActive(true);
		SetOutline(true);

		ShowGateInformation(true, ActiveGate);
		UIManager.Instance.OnGateSelected();
	}

	void SetOutline(bool outlineEnabled) => ActiveGate.SetOutline(outlineEnabled);

	void ShowGateInformation(bool show, AbstractGate gateObject) =>
		gateInformation.OpenGateInformation(show, gateObject);
}
