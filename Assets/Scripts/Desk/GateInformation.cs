using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GateInformation : MonoBehaviour
{
	public float offsetValue;
	public RectTransform truthTableContainer;
	public GameObject cellPrefab;

	Camera mainCamera;
	TMP_Text title;
	GridLayoutGroup grid;

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
		foreach (Transform child in truthTableContainer)
		{
			Destroy(child.gameObject);
		}

		TruthTableRow[] truthTable = currentGate.GenerateTruthTable();
		grid.constraintCount = truthTable[0].Inputs.Length + truthTable[0].Outputs.Length;
		for (int i = 0; i < truthTable[0].Inputs.Length; i++)
		{
			string columnHeading;
			if (gate is AdderGate)
				columnHeading = i < truthTable[0].Inputs.Length - 1 ? $"Input {i}" : "Carry In";
			else
				columnHeading = $"Input {i}";
			CreateHeadingCell(columnHeading);
		}

		for (int i = 0; i < truthTable[0].Outputs.Length; i++)
		{
			string columnHeading;
			if (gate is AdderGate)
				columnHeading = i < truthTable[0].Outputs.Length - 1 ? $"Carry Out" : "Sum";
			else
				columnHeading = $"Output {i}";
			CreateHeadingCell(columnHeading);
		}

		foreach (TruthTableRow row in truthTable)
		{
			foreach (bool value in row.Inputs)
			{
				CreateCell(value ? "1" : "0");
			}

			foreach (bool value in row.Outputs)
			{
				CreateCell(value ? "1" : "0");
			}
		}

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
		grid = truthTableContainer.GetComponent<GridLayoutGroup>();
		isInitialized = true;
		UpdatePosition(false);
	}

	void CreateCell(string text)
	{
		Instantiate(cellPrefab, truthTableContainer.transform).GetComponentInChildren<TMP_Text>().SetText(text);
	}


	void CreateHeadingCell(string columnHeading)
	{
		TMP_Text cell = Instantiate(cellPrefab, truthTableContainer.transform).GetComponentInChildren<TMP_Text>();
		cell.SetText(columnHeading);
		cell.fontSize = 0.005f;
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
