using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCell : MonoBehaviour
{
    public Cell realCellReference;
    public SpriteRenderer sr;

    void OnMouseEnter() {
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE)
            sr.color = new Color(0.0f, 1.0f, 0.0f);
    }

    void OnMouseDown() {
        GameObject.Find("SelectCell").GetComponent<SelectCell>().SetTargetCell(realCellReference);
    }

    void OnMouseExit() {
        sr.color = new Color(1.0f, 1.0f, 1.0f);
    }
}
