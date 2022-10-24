using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public List<GameObject> tooltips = new List<GameObject>();

    public void HideTooltips(){
        foreach (GameObject tooltip in tooltips){
            tooltip.SetActive(false);
        }
    }
}
