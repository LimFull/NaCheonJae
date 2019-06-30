using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGenerator : MonoBehaviour
{
    public GameObject Alien;
    float span = 5f;
    float delta = 0;
    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(Alien) as GameObject;
            float px = Random.Range(-6f, 7f);
            go.transform.position = new Vector3(px, 7, 0);
        }
    }
}
