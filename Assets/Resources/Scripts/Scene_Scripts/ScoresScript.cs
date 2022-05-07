using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresScript : MonoBehaviour
{

    const string NAME_KEY = "Player";
    const string SCORE_KEY = "Score";
    const int MENUINDEX = 0;
    const int DIRECTIONSINDEX = 1;
    const int OPTIONSINDEX = 2;
    const int SCORESINDEX = 3;
    const int EASYINDEX = 4;
    const int MEDIUMINDEX = 5;
    const int HARDINDEX = 6;
    const int GAMEOVERINDEX = 7;

    [SerializeField] RectTransform fader;

    [SerializeField] Text[] players;
    [SerializeField] Text[] scores;

    

    void Start() {
        ShowHighScores();
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.one, 0f);
        LeanTween.scale(fader, Vector3.zero, 1f).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }
    public void loadMenu(){
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, Vector3.one, 1f).setOnComplete(() => {
            SceneManager.LoadSceneAsync(MENUINDEX, LoadSceneMode.Single);
        });
    }

    void ShowHighScores()
    {
        for (int i = 0; i < 5; i++)
        {
            players[i].text = PlayerPrefs.GetString(NAME_KEY + (i+1));
            scores[i].text = PlayerPrefs.GetInt(SCORE_KEY + (i+1)).ToString();
        }

    }

    
}
