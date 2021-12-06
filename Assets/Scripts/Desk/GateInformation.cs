using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GateInformation : MonoBehaviour
{
	Camera mainCamera;

	TMP_Text title;
	public float offsetValue;
	public RectTransform truthTableContainer;
	public GameObject cellPrefab;
	TruthTableRow[] truthTable;

	AbstractGate currentGate;
	bool isInitialized = false;

	void Awake()
	{
		InitGateInformation();
	}

	void InitGateInformation()
	{
		if (isInitialized)
			return;

		title = GetComponentInChildren<TMP_Text>();
		mainCamera = Camera.main;
		isInitialized = true;
		UpdatePosition(false);
	}

	void PopulateTruthTable()
	{
		foreach (TruthTableRow row in truthTable)
		{
			foreach (bool value in row.Inputs)
			{
				CreateCell(value);
			}

			foreach (bool value in row.Outputs)
			{
				CreateCell(value);
			}
		}
	}

	void CreateCell(bool value)
	{
		Instantiate(cellPrefab, truthTableContainer.transform).GetComponentInChildren<TMP_Text>()
			.SetText(value ? "1" : "0");
	}

	void Update() => UpdatePosition(true);

	void OnEnable()
	{
		InitGateInformation();
		SetupGateInformation();
		UpdatePosition(true);
	}

	public void OpenGateInformation(bool show, AbstractGate gate)
	{
		if (gate is SourceGate || gate is SinkGate || gate == null)
		{
			gameObject.SetActive(false);
			return;
		}

		currentGate = gate;
		gameObject.SetActive(show);
	}

	public void SetupGateInformation()
	{
		title.SetText(currentGate.name);
		foreach (Transform child in truthTableContainer)
		{
			Destroy(child.gameObject);
		}

		truthTable = currentGate.GenerateTruthTable();
		SetGridLayoutConstraintCount(truthTable[0].Inputs.Length + truthTable[0].Outputs.Length);
		PopulateTruthTable();
	}

	void SetGridLayoutConstraintCount(int size)
	{
		truthTableContainer.GetComponent<GridLayoutGroup>().constraintCount = size;
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
