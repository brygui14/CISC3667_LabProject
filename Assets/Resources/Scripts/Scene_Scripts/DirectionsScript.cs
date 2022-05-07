using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectionsScript : MonoBehaviour
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


    void Start() {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.one, 0f);
        LeanTween.scale(fader, Vector3.zero, 1f);
        
    }

    void Update()
    {
        if (Input.GetKeyDown("space")){
            fader.gameObject.SetActive(true);
            LeanTween.scale(fader, Vector3.zero, 0f);
            LeanTween.scale(fader, Vector3.one, 1f).setOnComplete(() => {
                SceneManager.LoadSceneAsync(EASYINDEX, LoadSceneMode.Single);
            });
            
        }
    }
}
