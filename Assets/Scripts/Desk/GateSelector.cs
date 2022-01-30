using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GateSelector : MonoBehaviour
{
	public GameObject CurrentSelectedObject { get; private set; }

	bool touchStartOverUI;
	bool isTouching;

	void Update()
	{
		if (SparkInput.GetTouchCount() <= 0)
		{
			return;
		}

		TouchPhase touchPhase = SparkInput.GetTouchPhase();

		if (touchPhase == TouchPhase.Began)
		{
			return;
		}

		if (!isTouching)
		{
			isTouching = true;
			touchStartOverUI = SparkInput.IsPointerOverGameObject();
		}

		if (touchPhase == TouchPhase.Ended)
		{
			if (!(touchStartOverUI || SparkInput.IsPointerOverGameObject()))
			{
				(bool successful, GameObject hitObject) = RayCaster.Instance.GetHitObject();

				if (successful && (hitObject.CompareTag("LogicGate") || hitObject.CompareTag("Source") ||
				                   hitObject.CompareTag("Sink")))
				{
					CurrentSelectedObject = hitObject;
					UIManager.Instance.OnGateSelected();
				}
				else
				{
					UIManager.Instance.OnNoGateSelected();
				}
			}

			touchStartOverUI = isTouching = false;
		}
	}
}

public static class SparkInput
{
	public static int GetTouchCount()
	{
		return Input.touchCount +
		       (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0) ? 1 : 0);
	}

	public static Vector2 GetTouchPosition()
	{
		return Input.touchCount > 0
			? Input.GetTouch(0).position
			: new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

	public static TouchPhase GetTouchPhase()
	{
		if (Input.touchCount > 0)
		{
			return Input.GetTouch(0).phase;
		}

		return (Input.GetMouseButtonDown(0), Input.GetMouseButton(0)) switch
		{
			(true, _) => TouchPhase.Began,
			(false, true) => TouchPhase.Moved,
			_ => TouchPhase.Ended
		};
	}

	public static bool IsPointerOverGameObject()
	{
		return Input.touchCount > 0
			? EventSystem.current.IsPointerOverGameObject(0)
			: EventSystem.current.IsPointerOverGameObject();
	}
}
