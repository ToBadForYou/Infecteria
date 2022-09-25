using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject commandTooltip;

    public void OnPointerEnter(PointerEventData eventData){
        commandTooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        commandTooltip.SetActive(false);
    }
}
