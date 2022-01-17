using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionAccomplished : MonoBehaviour, IUIElement
{
	[SerializeField] TextMeshProUGUI missionRewardText;
	[SerializeField] Image missionRewardImage;
	void Start()
	{
		if (CurrentMission.missions == null)
			return;

		Mission currentMission = CurrentMission.missions[CurrentMission.currentMissionIndex];

		missionRewardText.text = currentMission.rewardText;
		missionRewardImage.sprite = currentMission.rewardImage;
	}

	public void OnOpen()
	{
		gameObject.SetActive(true);
	}

	public void OnClose()
	{
		gameObject.SetActive(false);
	}
}
