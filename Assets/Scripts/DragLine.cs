using cakeslice;
using UnityEngine;

public class DragLine : MonoBehaviour
{
	GameObject currentLine;
	LineRenderer currentLineRenderer;
	GameObject lineSource;
	GameObject lineSink;

	public GameObject line;

	void Update()
	{
		if (Input.touchCount <= 0)
		{
			if (!currentLine)
			{
				return;
			}
			
			if (lineSink)
			{
				//tell gates about new connection
				currentLine.GetComponent<LineUpdater>().SetLineSourceAndSink(lineSource.transform, lineSink.transform);
			}
			else
			{
				Destroy(currentLine);
			}

			SetOutline(true);
			currentLine = null;
			currentLineRenderer = null;
			lineSource = null;
			lineSink = null;
			return;
		}

		Vector2 touchPosition = Input.GetTouch(0).position;

		Ray ray = Camera.main.ScreenPointToRay(touchPosition);

		if (!currentLine)
		{
			if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.CompareTag("LogicGateContactPoint"))
			{
				lineSource = hit.collider.gameObject;
				currentLine = Instantiate(line, lineSource.transform);
				currentLineRenderer = currentLine.GetComponent<LineRenderer>();
			}
		}
		else
		{
			Vector3 projectedTouchPosition;

			if (Physics.Raycast(ray, out RaycastHit hit) &&
			    hit.collider.gameObject.CompareTag("LogicGateContactPoint") && hit.collider.gameObject != lineSource)
			{
				lineSink = hit.collider.gameObject;
				projectedTouchPosition = lineSink.transform.position;
			}
			else
			{
				projectedTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y,
					(Camera.main.transform.position - gameObject.transform.position).magnitude));
				lineSink = null;
			}

			Vector3[] points = { lineSource.transform.position, projectedTouchPosition };

			currentLineRenderer.SetPositions(points);

			SetOutline(true);
		}
	}

	void SetOutline(bool outlineEnabled)
	{
		if (lineSource)
			lineSource.GetComponent<Outline>().eraseRenderer = !outlineEnabled;

		if (lineSink)
			lineSink.GetComponent<Outline>().eraseRenderer = !outlineEnabled;
	}
}