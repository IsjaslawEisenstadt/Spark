using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
	LineRenderer lineRenderer;

	Transform start;
	Transform end;

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
		lineRenderer.SetPositions(new[] { start.position, end.position });
	}

	public void SetLine(Transform lineStart, Transform lineEnd)
	{
		start = lineStart;
		end = lineEnd;
		enabled = true;
	}

	public void SetPositions(Vector3[] positions) => lineRenderer.SetPositions(positions);

	public GameObject GetStart() => start.gameObject;
}
