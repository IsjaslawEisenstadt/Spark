using System;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
	[SerializeField] GameObject playModeGameObject;

	PlayMode activePlayMode;

	void Awake()
	{
		foreach (PlayMode playMode in playModeGameObject.GetComponents<PlayMode>())
		{
			if (playMode.enabled)
			{
				activePlayMode = playMode;
				return;
			}
		}
	}

	public void OnClick()
	{
		activePlayMode.Play();
	}
}
