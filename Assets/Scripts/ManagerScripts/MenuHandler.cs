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
        GameObject.Find("GameManager").GetComponent<GameManager>().SetWinStats();
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
                GameObject temp = GameObject.Find("GameManager");
                if(temp) {
                    Destroy(temp);
                }
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
