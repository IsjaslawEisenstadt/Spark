using UnityEngine;

public class BaseGate : MonoBehaviour
{
	public GateInformation gateInformation;

	public AbstractGate ActiveGateScript { get; private set; }

	GameObject activeGate;

	public GameObject ActiveGate
	{
		get => activeGate;
		private set
		{
			activeGate = value;
			ActiveGateScript = ActiveGate.GetComponent<AbstractGate>();
		}
	}

	bool selected;

	public bool Selected
	{
		get => selected;
		set
		{
			selected = value;
			SetOutline(value);
			ShowGateInformation(value, ActiveGateScript);
		}
	}

	void Awake() => ActiveGate = transform.GetChild(0).gameObject;

	void Start()
	{
		foreach (object child in gameObject.transform)
			((Transform)child).gameObject.GetComponent<AbstractGate>()?.InitGateType();
	}

	public void SetGateType(string gateName)
	{
		SetOutline(false);
		ActiveGateScript.Clear();
		ActiveGate.SetActive(false);
		ActiveGate = gameObject.transform.Find(gateName).gameObject;
		ActiveGate.SetActive(true);
		SetOutline(true);

		ShowGateInformation(true, ActiveGateScript);
		UIManager.Instance.OnGateSelected();
	}

	void SetOutline(bool outlineEnabled) => ActiveGateScript.SetOutline(outlineEnabled);

	void ShowGateInformation(bool show, AbstractGate gateObject) =>
		gateInformation.OpenGateInformation(show, gateObject);
}
