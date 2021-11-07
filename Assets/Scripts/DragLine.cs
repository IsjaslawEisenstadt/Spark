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

			SetOutlineSink(false);
			SetOutlineSource(false);
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

				SetOutlineSource(true);
			}
		}
		
		if (currentLine)
		{
			Vector3 projectedTouchPosition;

			if (Physics.Raycast(ray, out RaycastHit hit) &&
			    hit.collider.gameObject.CompareTag("LogicGateContactPoint") && hit.collider.gameObject != lineSource)
			{
				if (hit.collider.gameObject != lineSink)
                {
					lineSink = hit.collider.gameObject;
					SetOutlineSink(true);
				}

				projectedTouchPosition = lineSink.transform.position;
			}
			else
			{
				projectedTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y,
					(Camera.main.transform.position - gameObject.transform.position).magnitude + Camera.main.nearClipPlane));

				SetOutlineSink(false);
				lineSink = null;
			}

			Vector3[] points = { lineSource.transform.position, projectedTouchPosition };

			currentLineRenderer.SetPositions(points);
		}
	}

	void SetOutlineSource(bool outlineEnabled)
	{
		if (lineSource)
			lineSource.GetComponent<Outline>().eraseRenderer = !outlineEnabled;
	}

	void SetOutlineSink(bool outlineEnabled)
	{
		if (lineSink)
			lineSink.GetComponent<Outline>().eraseRenderer = !outlineEnabled;
	}
}