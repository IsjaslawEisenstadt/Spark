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
	[SerializeField] TextMeshProUGUI playButtonLabel;

	protected SourceGate Source { get; private set; }
	protected SinkGate Sink { get; private set; }

	Coroutine playCoroutine;

	void Start()
	{
		gateDrawer.gateTypeChanged += GateTypeChanged;
		playErrorAnimationHash = Animator.StringToHash("playErrorAnimation");
	}

	void OnDestroy()
	{
		gateDrawer.gateTypeChanged -= GateTypeChanged;
	}

	public void Play()
	{
		if (Source == null)
		{
			errorPanelMessage.text = "No Source available.";
			errorPanelAnim.SetTrigger(playErrorAnimationHash);
		}
		else if (Sink == null)
		{
			errorPanelMessage.text = "No Sink available.";
			errorPanelAnim.SetTrigger(playErrorAnimationHash);
		}
		else if (playCoroutine != null)
		{
			StopCoroutine(playCoroutine);
			playCoroutine = null;
			playButtonLabel.text = "Play";
		}
		else
		{
			playCoroutine = StartCoroutine(PlayInputs());
		}
	}

	IEnumerator PlayInputs()
	{
		playButtonLabel.text = "Cancel";

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
		playButtonLabel.text = "Play";
		playCoroutine = null;
	}

	protected abstract void EvaluatePlay(TruthTableRow[] result);

	void GateTypeChanged(BaseGate baseGate)
	{
		if (Source && !Source.gameObject.activeSelf)
		{
			Source = null;
		}

		if (Sink && !Sink.gameObject.activeSelf)
		{
			Sink = null;
		}

		AbstractGate gateScript = baseGate.ActiveGate;
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
