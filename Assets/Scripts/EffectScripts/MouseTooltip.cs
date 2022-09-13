using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTooltip : MonoBehaviour
{
    public SpriteRenderer sr;
    float multiplier = 1.0f;

    void Update()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + Time.deltaTime * multiplier);
        if(sr.color.a > 1.0f) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
            multiplier = -1.0f;
        }
        else if(sr.color.a < 0.0f) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.0f);
            multiplier = 1.0f;
        }
    }
}
