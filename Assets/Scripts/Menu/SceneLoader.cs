using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(SceneBlender))]
public class SceneLoader : MonoBehaviour
{
	SceneBlender sceneBlender;

	public TransitionType startTransitionType = TransitionType.FadeIn;
	public TransitionType endTransitionType = TransitionType.FadeOut;

	ARCameraManager arCameraManager;

	void Awake() => sceneBlender = GetComponent<SceneBlender>();
	void Start() 
	{
		switch (SceneManager.GetActiveScene().name)
		{
			case ("MissionMode"):
			case ("TutorialMode"):
			case ("StudyMode"):
			{
					arCameraManager = GameObject.Find("AR Camera").GetComponent<ARCameraManager>();
					arCameraManager.frameReceived += StartARTransition;
					break;
			}
			default: 
			{
				sceneBlender.StartTransition(startTransitionType);
				break;
			}
		}
	}

	public void SwitchScene(string scene)
	{
		if (sceneBlender.IsFinished())
		{
			sceneBlender.StartTransition(endTransitionType, () => SceneManager.LoadScene(scene));
		}
	}

	//currently not working as intended
	void StartARTransition(ARCameraFrameEventArgs args)
	{
		arCameraManager.frameReceived -= StartARTransition;
		sceneBlender.StartTransition(startTransitionType);
	}
}
