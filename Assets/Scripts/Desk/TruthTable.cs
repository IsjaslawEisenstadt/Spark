using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TruthTable : MonoBehaviour
{
    public static TruthTable Instance { get; private set; }
    public GameObject cellPrefab;
    public GameObject tableHeadingPrefab;

    void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

    public void GenerateTruthTable(AbstractGate currentGate, RectTransform container)
    {
        foreach (Transform child in container)
		{
			Destroy(child.gameObject);
		}

        TruthTableRow[] truthTable = GenerateTruthTableRows(currentGate);
        container.gameObject.GetComponent<GridLayoutGroup>().constraintCount = truthTable[0].Inputs.Length + truthTable[0].Outputs.Length;

		for (int i = 0; i < truthTable[0].Inputs.Length; ++i)
		{
			CreateHeadingCell(container, (currentGate is AdderGate || currentGate is HalfAdderGate) && i < truthTable[0].Inputs.Length - 1
				? "Carry In" : $"Input {i}");
		}

		for (int i = 0; i < truthTable[0].Outputs.Length; ++i)
		{
			CreateHeadingCell(container, (currentGate is AdderGate || currentGate is HalfAdderGate) && i < truthTable[0].Inputs.Length - 1
				? "Carry Out"
				: $"Output {i}");
		}

		foreach (TruthTableRow row in truthTable)
		{
			foreach (bool value in row.Inputs)
			{
				CreateCell(container, value ? "1" : "0");
			}

			foreach (bool value in row.Outputs)
			{
				CreateCell(container, value ? "1" : "0");
			}
		}
    }

    TruthTableRow[] GenerateTruthTableRows(AbstractGate currentGate)
    {
        TruthTableRow[] rows = new TruthTableRow[(int)Math.Pow(2, currentGate.inputs.Count)];
		for (int i = 0; i < rows.Length; ++i)
		{
			bool[] inp = new bool[currentGate.inputs.Count];
			for (int j = 0; j < currentGate.inputs.Count; ++j)
			{
				inp[j] = (i & (1 << j)) != 0;
			}

			rows[i] = new TruthTableRow(inp, currentGate.Evaluate(inp));
		}

        return rows;
    }

    void CreateCell(RectTransform container, string text)
	{
		Instantiate(cellPrefab, container).GetComponentInChildren<TMP_Text>().SetText(text);
	}


	void CreateHeadingCell(RectTransform container,string columnHeading)
	{
		TMP_Text cell = Instantiate(tableHeadingPrefab, container).GetComponentInChildren<TMP_Text>();
		cell.SetText(columnHeading);
	}
}

public readonly struct TruthTableRow
{
	public readonly bool[] Inputs;
	public readonly bool[] Outputs;

	public TruthTableRow(bool[] inputs, bool[] outputs) => (Inputs, Outputs) = (inputs, outputs);

	public override string ToString()
	{
		string ret = "TruthTableRow: Inputs = [";

		foreach (bool value in Inputs)
		{
			ret += value;
		}

		ret += "], Outputs = [";

		foreach (bool value in Outputs)
		{
			ret += value;
		}

		return ret + "]";
	}
}