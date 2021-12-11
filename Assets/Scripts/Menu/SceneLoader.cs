using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneBlender))]
public class SceneLoader : MonoBehaviour
{
	SceneBlender sceneBlender;

	public TransitionType startTransitionType = TransitionType.FadeIn;
	public TransitionType endTransitionType = TransitionType.FadeOut;

	void Awake() => sceneBlender = GetComponent<SceneBlender>();
	void Start() => sceneBlender.StartTransition(startTransitionType);

	public void SwitchScene(string scene)
	{
		if (sceneBlender.IsFinished())
		{
			sceneBlender.StartTransition(endTransitionType, () => { SceneManager.LoadScene(scene); });
		}
	}
}
