using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverVisibility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject target;

    public void OnPointerEnter(PointerEventData eventData){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE)
            target.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        target.SetActive(false);
    }
}
