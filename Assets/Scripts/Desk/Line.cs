using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
	LineRenderer lineRenderer;

	public Transform LineStart { get; private set; }
	public Transform LineEnd { get; private set; }

	void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Start()
	{
		enabled = false;
	}

	void Update()
	{
		lineRenderer.SetPositions(new[] { LineStart.position, LineEnd.position });
	}

	public void SetLine(Transform start, Transform end)
	{
		LineStart = start;
		LineEnd = end;
		enabled = true;
	}

	public void SetPositions(Vector3[] positions) => lineRenderer.SetPositions(positions);

	public GameObject GetStart() => LineStart.gameObject;

	public void Disconnect()
    {
		enabled = false;
	}
}