using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public abstract class PlayMode : MonoBehaviour
{
	[SerializeField] GateDrawer gateDrawer;
	[SerializeField] Animator errorPanelAnim;
	[SerializeField] TextMeshProUGUI errorPanelMessage;
	[SerializeField] int playErrorAnimationHash;
	protected SourceGate Source { get; private set; }
	protected SinkGate Sink { get; private set; }

	void Awake()
	{
		errorPanelAnim = GameObject.Find("ErrorPanel").GetComponent<Animator>();
		errorPanelMessage = GameObject.Find("ErrorPanelText").GetComponent<TextMeshProUGUI>();
	}

	void Start()
	{
		gateDrawer.onGateTypeChanged += OnGateTypeChanged;
		playErrorAnimationHash = Animator.StringToHash("playErrorAnimation");
	}

	void OnDestroy()
	{
		gateDrawer.onGateTypeChanged -= OnGateTypeChanged;
	}

	public void Play()
	{
		if (Source == null)
		{
			errorPanelMessage.text = "No Source available.";
			errorPanelAnim.SetTrigger(playErrorAnimationHash);
		}

		if (Sink == null)
		{
			errorPanelMessage.text = "No Sink available.";
			errorPanelAnim.SetTrigger(playErrorAnimationHash);
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
