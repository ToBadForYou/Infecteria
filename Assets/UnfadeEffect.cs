using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfadeEffect : MonoBehaviour
{
    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(sr.color.a > 0.0f) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a-Time.deltaTime);
        }
        else {
            Destroy(gameObject);
        }
    }
}
