using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDrawer : MonoBehaviour
{
    public void OnCloseGateDrawer()
    {
        gameObject.SetActive(false);
    }
}
