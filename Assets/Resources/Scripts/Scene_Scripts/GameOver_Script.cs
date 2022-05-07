using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOver_Script : MonoBehaviour
{

    const int NUM_HIGH_SCORES = 5;
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
    string playerName;
    int playerScore;


    void Start(){
        fader.gameObject.SetActive(false);
        playerName = PersistentData.Instance.GetName();
        playerScore = PersistentData.Instance.GetScore();
        
    }

    void Update(){
        if (Input.GetKeyDown("space")){
            loadMenu();
        }
    }

    void loadMenu(){
        SaveScore();
        ShowHighScores();
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, Vector3.one, 1f).setOnComplete(() => {
            clearPersistent();
            SceneManager.LoadSceneAsync(MENUINDEX, LoadSceneMode.Single);
        });
        
    }

    void SaveScore()
    {
        for (int i = 1; i <= NUM_HIGH_SCORES; i++)
        {
            string currentNameKey = NAME_KEY + i;
            string currentScoreKey = SCORE_KEY + i;

            if (PlayerPrefs.HasKey(currentScoreKey))
            {
                int currentScore = PlayerPrefs.GetInt(currentScoreKey);
                if (playerScore > currentScore)
                {
                    int tempScore = currentScore;
                    string tempName = PlayerPrefs.GetString(currentNameKey);

                    PlayerPrefs.SetString(currentNameKey, playerName);
                    PlayerPrefs.SetInt(currentScoreKey, playerScore);

                    playerName = tempName;
                    playerScore = tempScore;
                }

            }
            else
            {
                PlayerPrefs.SetString(currentNameKey, playerName);
                PlayerPrefs.SetInt(currentScoreKey, playerScore);
                return;
            }
        }
    }

    void ShowHighScores()
    {
        for (int i = 0; i <  NUM_HIGH_SCORES; i++)
        {
            Debug.Log(PlayerPrefs.GetString(NAME_KEY + (i+1)));
            Debug.Log(PlayerPrefs.GetInt(SCORE_KEY + (i+1)).ToString());
        }

    }

    void clearPersistent(){
        PersistentData.Instance.SetName("");
        PersistentData.Instance.SetScore(0);
    }

}
