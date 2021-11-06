using UnityEngine;

public class BaseGate : MonoBehaviour
{
	GameObject currentSelectedGate;

	void Awake()
	{
		// The gate that is active by default has to be found and saved in currentSelectedGate
		foreach (object child in gameObject.transform)
		{
			if (child is Transform childTransform && childTransform.gameObject.activeSelf)
			{
				currentSelectedGate = childTransform.gameObject;
				return;
			}
		}
	}

	public void SetGateType(string gateName)
	{
		currentSelectedGate.SetActive(false);
		currentSelectedGate = gameObject.transform.Find(gateName).gameObject;
		currentSelectedGate.SetActive(true);
	}
}
