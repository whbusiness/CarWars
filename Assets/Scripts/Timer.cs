using UnityEngine;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    [SerializeField] private float _MaxTime = 300f; //5 Minutes
    public float _TimePassed;

    void Update()
    {
        _TimePassed += Time.deltaTime;
        if(_TimePassed > _MaxTime)
        {
            //print("Show New Screen");
            //SceneManager.LoadScene(_sceneNameToLoad); //Type The scene name in the editor

            if (ScoreUI.score < 300)
            {
                SceneManager.LoadScene("1StarEndScreen");
            }
            if(ScoreUI.score > 300 && ScoreUI.score < 600)
            {
                SceneManager.LoadScene("2StarEndScreen");
            }
            if(ScoreUI.score > 600)
            {
                SceneManager.LoadScene("3StarEndScreen");
            }
            
        }
    }
}
