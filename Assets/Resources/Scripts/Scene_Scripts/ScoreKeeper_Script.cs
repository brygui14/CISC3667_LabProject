using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreKeeper_Script : MonoBehaviour
{

    const int MENUINDEX = 0;
    const int DIRECTIONSINDEX = 1;
    const int OPTIONSINDEX = 2;
    const int SCORESINDEX = 3;
    const int EASYINDEX = 4;
    const int MEDIUMINDEX = 5;
    const int HARDINDEX = 6;
    const int GAMEOVERINDEX = 7;

    private float score;
    private int scoreDeductionPerSecond;

    bool levelWon = false;

    TextMeshProUGUI text;

    void Start(){
        score = PersistentData.Instance.GetScore();
        if (score == 0){
            score = 100;
        }
        getDifficultyLevel();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText("Score: " + Mathf.Round(score));
        if (!levelWon){
            score -= scoreDeductionPerSecond * Time.deltaTime;
        }
    }

    private void getDifficultyLevel(){
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 4){
            scoreDeductionPerSecond = 10;
        }
        else if (index == 5){
            scoreDeductionPerSecond = 20;
        }
        else{
            scoreDeductionPerSecond = 30;
        }

    }

    void Won(int value){
        levelWon = true;
        score += value;
        PersistentData.Instance.SetScore(Mathf.RoundToInt(score));
    }

    void nextLevel(int levelIndex){
        if (levelIndex == 4){
            SceneManager.LoadSceneAsync(MEDIUMINDEX, LoadSceneMode.Single);
        }
        else if (levelIndex == 5){
            SceneManager.LoadSceneAsync(HARDINDEX, LoadSceneMode.Single);
        }
        else{
            SceneManager.LoadSceneAsync(GAMEOVERINDEX, LoadSceneMode.Single);
        }
    }
}
