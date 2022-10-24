using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CantAfford : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI textMesh;

    void Update(){
        if(image.color.a >= 0.0f) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime * 1.2f);
            if(textMesh != null){
                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - Time.deltaTime * 1.2f);
            }
        }
    }

    public void Trigger(){
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 16);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f);
    }      
}
