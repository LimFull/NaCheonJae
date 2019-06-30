using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1024, 768, true);
        StartCoroutine("nextScene");
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("SampleScene");

    }
}
