using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MuteScript : MonoBehaviour
{
	[SerializeField] Image speakerIcon;
	[SerializeField] TextMeshProUGUI soundText;
	[SerializeField] Sprite speakerOn;
	[SerializeField] Sprite speakerOff;

	public void toggleSound()
	{
		if (AudioListener.volume != 0)
		{
			AudioListener.volume = 0;
			speakerIcon.sprite = speakerOff;
			soundText.text = "Sound Off";
		}
		else
		{
			AudioListener.volume = 1;
			speakerIcon.sprite = speakerOn;
			soundText.text = "Sound On";
		}
	}
}
