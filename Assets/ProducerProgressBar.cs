using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerProgressBar : MonoBehaviour
{
    public float time;
    float timePassed;
    
    void Start()
    {
        transform.localScale = new Vector2(0.0f, 1.0f);
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        transform.localScale = new Vector2(timePassed/time, 1.0f);
        if(timePassed >= time) {
            timePassed = 0.0f;
        }
    }
}
