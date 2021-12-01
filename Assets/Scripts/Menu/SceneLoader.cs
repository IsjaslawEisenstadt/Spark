using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public SceneBlender sceneBlender;

	public void SwitchScene(string scene)
	{
		if (sceneBlender.IsFinished())
		{
			sceneBlender.StartTransition(TransitionType.FadeOut, () => { SceneManager.LoadScene(scene); });
		}
	}
}