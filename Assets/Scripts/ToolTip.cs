using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{
    public List<KeyCode> keyCodes;
    
    int count;
    public int maxCount = 5;

    bool fadeOut;

    public TextMeshProUGUI mesh;
    public TextMeshProUGUI mesh2;
    public SpriteRenderer sr;
    public SpriteRenderer sr2;

    void Update()
    {
        foreach(KeyCode keyCode in keyCodes) {
            if(Input.GetKeyDown(keyCode)) {
                count++;
            }
        }
        if(count >= maxCount) {
            fadeOut = true;
        }
        if(fadeOut) {
            mesh.color = new Color(mesh.color.r, mesh.color.g, mesh.color.b, mesh.color.a - Time.deltaTime);
            if(mesh2) {
                mesh2.color = new Color(mesh2.color.r, mesh2.color.g, mesh2.color.b, mesh2.color.a - Time.deltaTime);
            }

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime);
            if(sr2) {
                sr2.color = new Color(sr2.color.r, sr2.color.g, sr2.color.b, sr2.color.a - Time.deltaTime);
            }

            if(sr.color.a <= 0.0f) {
                Destroy(gameObject);
            }
        }
    }
}
