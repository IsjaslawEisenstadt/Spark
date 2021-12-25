using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class MissionInfo : MonoBehaviour, IUIElement
{
    public TMP_Text description;

	void Start() => description.text = CurrentMission.missions[CurrentMission.currentMissionIndex].missionDescription;

    public void OnClose() => gameObject.SetActive(false);

	public void OnOpen() => gameObject.SetActive(true);
}
