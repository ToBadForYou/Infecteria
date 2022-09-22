using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayer : MonoBehaviour
{
    public SpriteRenderer sr;

    void OnMouseEnter() {
        sr.color = new Color(1.0f, 1.0f, 1.0f);
    }

    void OnMouseDown() {
        //GameObject.Find("SelectionHandler").GetComponent<SelectionHandler>().SetFollowPlayerAndReset();
    }

    void OnMouseExit() {
        sr.color = new Color(0.0f, 1.0f, 0.0f);
    }
}
