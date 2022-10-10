using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    
    private float multiplier = 1.0f;
    private float maxAlpha = 1.0f;
    private float minAlpha = 0.0f;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void InvertMultiplier() {
        multiplier *= -1;
    }

    void SetAlpha(float alpha) {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            float newAlpha = sr.color.a + Time.deltaTime * multiplier;
            SetAlpha(newAlpha);

            if(sr.color.a > maxAlpha) {
                SetAlpha(maxAlpha);
                InvertMultiplier();
            }
            else if(sr.color.a < minAlpha) {
                SetAlpha(minAlpha);
                InvertMultiplier();
            }
        }
    }
}
