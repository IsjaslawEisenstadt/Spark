using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractGate : MonoBehaviour
{
    public List<Pin> inputs;
    public List<Pin> outputs;

    void Awake()
    {
        foreach (Pin input in inputs)
        {
            input.valueChanged += OnInputValueChanged;
            input.lineChanged += OnInputLineChanged;
        }

        foreach (Pin output in outputs)
        {
            output.valueChanged += OnOutputValueChanged;
        }
    }

    void OnDestroy()
    {
        foreach (Pin input in inputs)
        {
            input.valueChanged -= OnInputValueChanged;
            input.lineChanged -= OnInputLineChanged;
        }

        foreach (Pin output in outputs)
        {
            output.valueChanged -= OnOutputValueChanged;
        }
    }

    public TruthTableRow[] GenerateTruthTable()
    {
        TruthTableRow[] rows = new TruthTableRow[(int)Math.Pow(2, inputs.Count)];
        for (int i = 0; i < rows.Length; ++i)
        {
            bool[] inp = new bool[inputs.Count];
            for (int j = 0; j < inputs.Count; ++j)
            {
                inp[j] = (i & (1 << j)) != 0;
            }

            rows[i] = new TruthTableRow(inp, Evaluate(inp));
        }

        return rows;
    }

    protected void Refresh()
    {
        bool[] outputValues = EvaluateSelf();

        foreach (var value in outputValues.Select((value, i) => new { value, i }))
        {
            outputs[value.i].State = value.value;
        }
    }

    protected bool[] EvaluateSelf()
    {
        return Evaluate(inputs.Select(pin => pin.State).ToArray());
    }

    protected abstract bool[] Evaluate(bool[] values);

    void OnInputValueChanged(Pin pin, bool value)
    {
        Refresh();
    }

    static void OnInputLineChanged(Pin pin, Line line)
    {
        pin.State = line && line.LineStart.GetComponent<Pin>().State;
    }

    static void OnOutputValueChanged(Pin pin, bool value)
    {
        if (pin.Line)
            pin.Line.LineEnd.GetComponent<Pin>().State = value;
    }
}

public readonly struct TruthTableRow
{
    public readonly bool[] Inputs;
    public readonly bool[] Outputs;

    public TruthTableRow(bool[] inputs, bool[] outputs) => (Inputs, Outputs) = (inputs, outputs);
}