using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDirector : MonoBehaviour
{
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(PlayerPrefs.GetFloat("Score"));
        score.text = "" + PlayerPrefs.GetFloat("Score");
    }
    
    // Update is called once per frame
   
}
