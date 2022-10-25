using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PingPongFadeEffect : MonoBehaviour
{
    bool fadeOut = true;
    TextMeshProUGUI textMesh;

    void Start(){
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        if(fadeOut) {
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - Time.deltaTime);
            if(textMesh.color.a <= 0.0f) {
                fadeOut = false;
            }
        }
        else {
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a + Time.deltaTime);
            if(textMesh.color.a >= 1.0f) {
                fadeOut = true;
            }
        }
    }
}
