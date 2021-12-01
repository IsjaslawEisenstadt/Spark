using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public SceneBlender sceneBlender;

	public TransitionType startTransitionType = TransitionType.FadeIn;
	public TransitionType endTransitionType = TransitionType.FadeOut;

	void Start() => sceneBlender.StartTransition(startTransitionType);

	public void SwitchScene(string scene)
	{
		if (sceneBlender.IsFinished())
		{
			sceneBlender.StartTransition(endTransitionType, () => { SceneManager.LoadScene(scene); });
		}
	}
}
