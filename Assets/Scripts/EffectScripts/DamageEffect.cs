using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    public SpriteRenderer sr;

    Color originColor;
    bool isHit = false;
    float timePassed = 0.0f;
    float totalDuration = 0.5f;

    void Start()
    {
        originColor = sr.color;
    }

    void Update()
    {
        if(isHit) {
            if (timePassed < totalDuration) {
                timePassed+=Time.deltaTime;
                GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1.0f, 0.0f, 0.0f, 1.0f), originColor, timePassed / totalDuration);
            }
            else {
                isHit = false;
                timePassed = 0.0f;
            }
        }
    }

    public void Activate() {
        sr.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        isHit = true;
    }
}
