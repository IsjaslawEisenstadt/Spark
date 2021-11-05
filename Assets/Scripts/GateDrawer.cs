using UnityEngine;

public class GateDrawer : MonoBehaviour
{
    GameObject currentGateObject;
    BaseGate baseGateScript;

    public void OnCloseGateDrawer()
    {
        gameObject.SetActive(false);
    }

    public void OnGateSelected(string gateName)
    {
        baseGateScript.SetGateActive(gateName);
    }

    public void SetCurrentGateObject(GameObject gateObject)
    {
        currentGateObject = gateObject;
        baseGateScript = currentGateObject.GetComponent<BaseGate>();
    }
}
