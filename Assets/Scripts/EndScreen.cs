using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
        ScoreUI.score = 0;
    }
        public void OnQuit()
    {
        Application.Quit();
    }
}
