using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGate : MonoBehaviour
{
    private GameObject currentSelectedGate;

    public void SetGateActive(string GateName)
    {
        currentSelectedGate.SetActive(false);
        currentSelectedGate = gameObject.transform.Find(GateName).gameObject;
        currentSelectedGate.SetActive(true);
    }
}
