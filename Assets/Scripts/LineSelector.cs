using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineSelector : MonoBehaviour
{
	GameObject currentSelectedLine;

	void Update()
	{
		if (Input.touchCount <= 0 || EventSystem.current.IsPointerOverGameObject(0))
		{
			return;
		}

		Touch touchEvent = Input.GetTouch(0);

		if (touchEvent.phase == TouchPhase.Ended)
		{
			var rayCast = RayCaster.Instance.GetHitObject();

			if (!rayCast.successful || rayCast.hitObject == currentSelectedLine)
				return;

			if (currentSelectedLine)
            {
				currentSelectedLine.GetComponent<Line>().ActivateDeletionOption(false);
				currentSelectedLine = null;
			}

			if (rayCast.hitObject.CompareTag("Line"))
            {
				currentSelectedLine = rayCast.hitObject;
				currentSelectedLine.GetComponent<Line>().ActivateDeletionOption(true);
			}
		}
	}
}
