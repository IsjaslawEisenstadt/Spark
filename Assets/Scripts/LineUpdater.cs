using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineUpdater : MonoBehaviour
{
	LineRenderer lineRenderer;

	Transform start;
	Transform end;

	void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
		enabled = false;
	}

	void Update()
	{
		lineRenderer.SetPositions(new[] { start.position, end.position });
	}

	public void SetLineStartAndEnd(Transform lineStart, Transform lineEnd)
	{
		start = lineStart;
		end = lineEnd;
		enabled = true;
	}

	public void SetPositions(Vector3[] positions)
	{
		lineRenderer.SetPositions(positions);
	}
}
