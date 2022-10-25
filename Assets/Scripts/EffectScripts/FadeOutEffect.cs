using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeOutEffect : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start(){
        sr = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE){
            if(sr.color.a >= 0.0f) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime * 0.5f);
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}
