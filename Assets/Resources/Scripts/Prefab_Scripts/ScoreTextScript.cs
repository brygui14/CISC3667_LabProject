using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreTextScript : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText(PersistentData.Instance.GetName() + "'s Score: " + PersistentData.Instance.GetScore()); 
    }
}
