using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenuScript : MonoBehaviour
{

    const int MENUINDEX = 0;
    const int DIRECTIONSINDEX = 1;
    const int OPTIONSINDEX = 2;
    const int SCORESINDEX = 3;
    const int EASYINDEX = 4;
    const int MEDIUMINDEX = 5;
    const int HARDINDEX = 6;
    const int GAMEOVERINDEX = 7;

    [SerializeField] RectTransform fader;
    public GameObject inputField;

    void Start(){
        fader.gameObject.SetActive(false);
    }

    public void loadDirections(){
        string name = inputField.GetComponent<TMP_InputField>().text;

        if (name.Equals("")){
            name = "Player";
        }

        PersistentData.Instance.SetName(name);
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, Vector3.one, 1f).setOnComplete(() => {
            SceneManager.LoadSceneAsync(DIRECTIONSINDEX, LoadSceneMode.Single);
        });

        
        

    }

    public void loadOptions(){
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, Vector3.one, 1f).setOnComplete(() => {
            SceneManager.LoadSceneAsync(OPTIONSINDEX, LoadSceneMode.Single);
        });
    }

    public void loadScores(){
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, Vector3.one, 1f).setOnComplete(() => {
            SceneManager.LoadSceneAsync(SCORESINDEX, LoadSceneMode.Single);
        });
    }

    
}
