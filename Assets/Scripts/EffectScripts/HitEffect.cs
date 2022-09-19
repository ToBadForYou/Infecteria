using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public float fadeSpeed = 0.02f;
    public float travelSpeed = 0.8f;

    public SpriteRenderer sr;

    void Update()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime * fadeSpeed);
        transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime * travelSpeed);
        if(sr.color.a <= 0.0f) {
            Destroy(gameObject);
        }
    }
}
