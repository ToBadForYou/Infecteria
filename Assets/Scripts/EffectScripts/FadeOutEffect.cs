using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeOutEffect : MonoBehaviour
{
    Image image;
    TextMeshProUGUI text;

    void Start(){
        image = GetComponent<Image>();
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        if(image.color.a >= 0.0f) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime * 0.5f);
            if(text != null){
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime * 0.5f);
            }
        }
        else {
            Destroy(gameObject);
        }
    }
}
