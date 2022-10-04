using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnfadeEffect : MonoBehaviour
{
    Image image;

    void Start() {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(image.color.a > 0.0f)
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a-Time.deltaTime);
        else
            Destroy(gameObject);
    }
}
