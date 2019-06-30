using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject GodKimchi;
    float span = 1f;
    float delta = 0;
    public float spanspeed = 1;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void FixedUpdate()
    {
        this.delta += Time.deltaTime*spanspeed;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(GodKimchi) as GameObject;
            float px = Random.Range(-6f, 7f);
            go.transform.position = new Vector3(px, 7, 0);
        }
    }
}
