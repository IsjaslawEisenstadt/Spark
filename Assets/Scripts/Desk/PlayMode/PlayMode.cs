using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PlayMode : MonoBehaviour
{
	[SerializeField] GateDrawer gateDrawer;

	protected SourceGate Source { get; private set; }
	protected SinkGate Sink { get; private set; }

	void Start()
	{
		gateDrawer.onGateTypeChanged += OnGateTypeChanged;
	}

	void OnDestroy()
	{
		gateDrawer.onGateTypeChanged -= OnGateTypeChanged;
	}

	public void Play()
	{
		if (Source == null)
		{
			Animator errorPanelAnim = GameObject.Find("ErrorPlane").GetComponent<Animator>();
			TextMeshProUGUI errorPanelMessage = GameObject.Find("ErrorPanelText").GetComponent<TextMeshProUGUI>();

			errorPanelMessage.text = "No Source available.";
			errorPanelAnim.SetBool("open", true);
		}

		if (Sink == null)
		{
			Debug.Log("No Sink!");
			// TODO: add error message display
		}
		else
		{
			StartCoroutine(PlayInputs());
		}
	}

	IEnumerator PlayInputs()
	{
		TruthTableRow[] result = new TruthTableRow[(int)Math.Pow(2, Source.outputs.Count)];

		for (int i = 0; i < result.Length; ++i)
		{
			for (int j = 0; j < Source.outputs.Count; ++j)
			{
				Source.outputs[j].State = (i & (1 << j)) != 0;
			}

			result[i] = new TruthTableRow(Source.outputs.Select(pin => pin.State).ToArray(),
				Sink.inputs.Select(pin => pin.State).ToArray());

			yield return new WaitForSeconds(3.0f);
		}

		EvaluatePlay(result);
	}

	protected abstract void EvaluatePlay(TruthTableRow[] result);

	public void OnGateTypeChanged(BaseGate baseGate)
	{
		AbstractGate gateScript = baseGate.ActiveGateScript;

		if (Source == gateScript)
		{
			Source = null;
		}
		else if (Sink == gateScript)
		{
			Sink = null;
		}

		if (gateScript is SourceGate sourceGate)
		{
			Source = sourceGate;
		}
		else if (gateScript is SinkGate sinkGate)
		{
			Sink = sinkGate;
		}
	}
}
