using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    bool isActive = false;
    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!isActive) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                isActive = true;
            }
        }
        else {
            if(sr.color.a <= 1.0f) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a+Time.deltaTime);
            }
            else {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
