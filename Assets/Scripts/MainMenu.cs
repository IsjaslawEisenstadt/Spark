using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartMissionMode()
    {
        SceneManager.LoadScene("Desk", LoadSceneMode.Additive);
    }
    
    public void StartStudyMode()
    {
        SceneManager.LoadScene("Desk");
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Desk");
    }
}