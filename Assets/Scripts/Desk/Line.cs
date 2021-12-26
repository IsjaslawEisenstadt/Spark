using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
	public Pin LineStart { get; private set; }
	public Pin LineEnd { get; private set; }

	LineRenderer lineRenderer;

	void Awake() => lineRenderer = GetComponent<LineRenderer>();

	void Start() => enabled = false;

	void Update() => SetPositions(new[] { LineStart.transform.position, LineEnd.transform.position });

	public void SetPins(Pin start, Pin end)
	{
		LineStart = start;
		LineEnd = end;
		LineStart.AddLine(this);
		LineEnd.AddLine(this);
		enabled = true;
	}

	public void SetPositions(Vector3[] positions) => lineRenderer.SetPositions(positions);

	public void Disconnect()
	{
		enabled = false;
		LineEnd = null;
	}
}
