using UnityEngine;
using UnityEngine.EventSystems;

public class GateSelector : MonoBehaviour
{
	public GateDrawer gateDrawer;

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

			if (rayCast.successful && (rayCast.hitObject.CompareTag("LogicGate") || rayCast.hitObject.CompareTag("Source")))
			{
				gateDrawer.Open(rayCast.hitObject.transform.parent.gameObject);
				PinSettings.Instance.CheckAndOpen(rayCast.hitObject);
			}
			else
			{
				gateDrawer.Close();
				PinSettings.Instance.Close();
			}
		}
	}
}
