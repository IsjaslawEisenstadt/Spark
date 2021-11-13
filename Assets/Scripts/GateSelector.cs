using UnityEngine;
using UnityEngine.EventSystems;

public class GateSelector : MonoBehaviour
{
	public GameObject gateDrawer;

	GateDrawer gateDrawerScript;

	void Awake() => gateDrawerScript = gateDrawer.GetComponent<GateDrawer>();

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

			if (rayCast.successful && rayCast.hitObject.CompareTag("LogicGate"))
			{
				gateDrawerScript.Open(rayCast.hitObject.transform.parent.gameObject);
			}
			else
			{
				gateDrawerScript.Close();
			}
		}
	}
}
