using UnityEngine;
using UnityEngine.EventSystems;

public class GateSelector : MonoBehaviour
{
	public GameObject CurrentSelectedObject { get; private set; }

	private bool touchStartOverUI;
	private bool isTouching;

	void Update()
	{
		if (Input.touchCount <= 0)
			return;

		Touch touchEvent = Input.GetTouch(0);

		if (Input.GetTouch(0).phase == TouchPhase.Began)
			return;

		if (!isTouching)
		{
			isTouching = true;
			touchStartOverUI = EventSystem.current.IsPointerOverGameObject(0);
		}

		if (touchEvent.phase == TouchPhase.Ended)
		{
			if (!(touchStartOverUI || EventSystem.current.IsPointerOverGameObject(0)))
			{
				(bool successful, GameObject hitObject) = RayCaster.Instance.GetHitObject();

				if (successful && (hitObject.CompareTag("LogicGate") || hitObject.CompareTag("Source") ||
									hitObject.CompareTag("Sink")))
				{
					CurrentSelectedObject = hitObject;
					UIManager.Instance.OnGateSelected();
				}
				else
					UIManager.Instance.OnNoGateSelected();
			}

			touchStartOverUI = isTouching = false;
		}
	}
}
