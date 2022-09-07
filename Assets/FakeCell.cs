using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCell : MonoBehaviour
{
    public Cell realCellReference;
    public SpriteRenderer sr;

    void OnMouseEnter() {
        sr.color = new Color(0.0f, 1.0f, 0.0f);
    }

    void OnMouseDown() {
        GameObject.Find("SelectionHandler").GetComponent<SelectionHandler>().SetTargetAndReset(realCellReference);
    }

    void OnMouseExit() {
        sr.color = new Color(1.0f, 1.0f, 1.0f);
    }
}
