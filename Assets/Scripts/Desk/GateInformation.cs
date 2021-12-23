using TMPro;
using UnityEngine;

public class GateInformation : MonoBehaviour
{
	public float offsetValue;
	public RectTransform truthTableContainer;

	Camera mainCamera;
	TMP_Text title;

	AbstractGate currentGate;
	bool isInitialized = false;

	public void OpenGateInformation(bool show, AbstractGate gate)
	{
		if (!show || gate is SourceGate || gate is SinkGate || gate is DefaultGate || gate == null)
		{
			currentGate = null;
			gameObject.SetActive(false);
			return;
		}

		currentGate = gate;

		InitGateInformation();

		title.SetText(currentGate.name);

		TruthTable.Instance.GenerateTruthTable(currentGate, truthTableContainer);

		UpdatePosition(true);

		gameObject.SetActive(true);
	}

	void Awake() => InitGateInformation();

	void Update() => UpdatePosition(true);

	void InitGateInformation()
	{
		if (isInitialized)
			return;

		title = GetComponentInChildren<TMP_Text>();
		mainCamera = Camera.main;
		isInitialized = true;
		UpdatePosition(false);
	}

	void UpdatePosition(bool lerpPosition)
	{
		Vector3 offset =
			Vector3.Cross(mainCamera.transform.right, mainCamera.transform.position - transform.position).normalized *
			offsetValue;

		transform.position = lerpPosition
			? Vector3.Lerp(transform.position, transform.parent.position + offset, 0.5f)
			: transform.parent.position + offset;

		transform.LookAt(transform.position + (transform.position - mainCamera.transform.position));
	}
}
