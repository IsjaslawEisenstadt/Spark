using cakeslice;
using UnityEngine;

public class BaseGate : MonoBehaviour
{
	public GameObject ActiveGate { get; private set; }
	public AbstractGate ActiveGateScript { get; private set; }
	bool selected;

	public GateInformation gateInformation;

	public bool Selected
	{
		get => selected;
		set
		{
			selected = value;
			SetOutline(value);
			ShowGateInformation(value, ActiveGate.GetComponent<AbstractGate>());
		}
	}

	void Awake() => ActiveGate = transform.GetChild(0).gameObject;

	void Start()
	{
		foreach (object child in gameObject.transform)
			((Transform)child).gameObject.GetComponent<AbstractGate>()?.InitGateTyp();
	}

	public void SetGateType(string gateName)
	{
		SetOutline(false);
		ActiveGate.SetActive(false);
		ActiveGate = gameObject.transform.Find(gateName).gameObject;
		ActiveGateScript = ActiveGate.GetComponent<AbstractGate>();
		ActiveGate.SetActive(true);
		SetOutline(true);

		ShowGateInformation(true, ActiveGate.GetComponent<AbstractGate>());
		PinSettings.Instance.SetVisualizationState(ActiveGate);
	}

	void SetOutline(bool outlineEnabled) => ActiveGate.GetComponent<Outline>().eraseRenderer = !outlineEnabled;

	void ShowGateInformation(bool show, AbstractGate gateObject) =>
		gateInformation.ShowGateInformation(show, gateObject);
}
