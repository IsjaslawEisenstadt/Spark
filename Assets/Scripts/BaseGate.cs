using UnityEngine;

public class BaseGate : MonoBehaviour
{
    GameObject currentSelectedGate;

    public void SetGateActive(string gateName)
    {
        if (currentSelectedGate != null)
        {
            currentSelectedGate.SetActive(false);
        }

        currentSelectedGate = gameObject.transform.Find(gateName).gameObject;
        currentSelectedGate.SetActive(true);

        Debug.Log("CurrentSelectedGAte: " + currentSelectedGate);
    }
}
