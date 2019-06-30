using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Director : MonoBehaviour
{
    public GameObject hpGage;
    public GameObject player;
    public Text score;
    public Text score2;
    // Start is called before the first frame update
    float time = 0;

    public void FixedUpdate()
    {
        time += Time.deltaTime;
        score.text = ""+System.Math.Truncate(time*100)/100f;
    }
    public void DecreaseHp()
    {
        hpGage.GetComponent<Image>().fillAmount -= 0.7f;
        if (hpGage.GetComponent<Image>().fillAmount <= 0)
        {
            player.GetComponent<PlayerController>().Die();
            PlayerPrefs.SetFloat("Score", (float)System.Math.Truncate(time * 100) / 100f);
            score2.text = "" + PlayerPrefs.GetFloat("Score");
            score.gameObject.active = false;
            //SceneManager.LoadScene("Result");
            StartCoroutine("ResultScene");
        }
        
    }
    
    IEnumerator ResultScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Result");
    }
}
