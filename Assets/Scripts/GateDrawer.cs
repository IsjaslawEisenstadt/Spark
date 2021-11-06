using System;
using UnityEngine;

public class GateDrawer : MonoBehaviour
{
	BaseGate baseGateScript;

	public void OnCloseGateDrawer() => gameObject.SetActive(false);

	public void OnGateSelected(string gateName) => baseGateScript.SetGateType(gateName);

	public void SetCurrentGateObject(GameObject gateObject) => baseGateScript = gateObject.GetComponent<BaseGate>();
}