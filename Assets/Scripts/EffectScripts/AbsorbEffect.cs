using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbEffect : MonoBehaviour
{
    public bool isActive = false;

    public Transform childTransform;

    SpriteRenderer sr;
    SpriteRenderer childSr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        childSr = childTransform.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(isActive) {
            if(transform.childCount > 1) {
                transform.GetChild(0).gameObject.SetActive(false);
                childTransform.parent = null;
            }
            
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime*0.5f);
            childSr.color = new Color(childSr.color.r, childSr.color.g, childSr.color.b, childSr.color.a - Time.deltaTime*0.5f);
            
            transform.Rotate(0, 0, 25*Time.deltaTime);
            childTransform.Rotate(0, 0, 50*Time.deltaTime);

            if(sr.color.a <= 0.0f) {
                Destroy(childTransform.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
