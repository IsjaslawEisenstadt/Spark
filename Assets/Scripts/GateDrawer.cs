using UnityEngine;

public class GateDrawer : MonoBehaviour
{
    GameObject currentBaseGateObject;
    BaseGate baseGateScript;

    public void OnCloseGateDrawer()
    {
        gameObject.SetActive(false);
    }

    public void OnGateSelected(string gateName)
    {
        Debug.Log("GateSelected: " + gateName);
        baseGateScript.SetGateActive(gateName);
    }

    public void SetCurrentGateObject(GameObject gateObject)
    {
        currentBaseGateObject = gateObject;
        baseGateScript = currentBaseGateObject.GetComponent<BaseGate>();

        Debug.Log("currentGateObject: " + currentBaseGateObject.name);
        Debug.Log("BaseGate: " + (baseGateScript != null));
    }
}
