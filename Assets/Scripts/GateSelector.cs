using UnityEngine;

public class GateSelector : MonoBehaviour
{
	public GameObject gateDrawer;

	GateDrawer gateDrawerScript;

	void Awake()
    {
		gateDrawerScript = gateDrawer.GetComponent<GateDrawer>();
    }

    void Update()
	{
		if (Input.touchCount <= 0)
		{
			return;
		}
		
		Vector2 touchPosition = Input.GetTouch(0).position;

		Ray ray = Camera.main.ScreenPointToRay(touchPosition);

		if (Physics.Raycast(ray, out RaycastHit hit) && hit.rigidbody.gameObject.CompareTag("LogicGate"))
		{
			gateDrawerScript.SetCurrentGateObject(gameObject);
			gateDrawer.SetActive(true);
		}
	}
}
