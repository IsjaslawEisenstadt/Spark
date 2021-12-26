using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
	[SerializeField] Material enabledMaterial;
	[SerializeField] Material disabledMaterial;

	Pin lineStart;

	public Pin LineStart
	{
		get => lineStart;
		set
		{
			lineStart = value;
			lineRenderer.material = LineStart.State ? enabledMaterial : disabledMaterial;
			LineStart.valueChanged += OnLineStartValueChanged;
		}
	}

	public Pin LineEnd { get; private set; }

	LineRenderer lineRenderer;

	void Awake() => lineRenderer = GetComponent<LineRenderer>();

	void Start() => enabled = false;

	void Update() => SetPositions(new[] { LineStart.transform.position, LineEnd.transform.position });

	void OnDestroy()
	{
		if (LineStart)
		{
			LineStart.valueChanged -= OnLineStartValueChanged;
		}
	}

	public void SetPins(Pin start, Pin end)
	{
		LineStart = start;
		LineEnd = end;
		LineStart.AddLine(this);
		LineEnd.AddLine(this);
		enabled = true;
	}

	void OnLineStartValueChanged(Pin _, bool value)
	{
		if (lineRenderer)
		{
			lineRenderer.material = value ? enabledMaterial : disabledMaterial;
		}
	}

	public void SetPositions(Vector3[] positions) => lineRenderer.SetPositions(positions);

	public void Disconnect()
	{
		enabled = false;
		LineEnd = null;
	}
}
