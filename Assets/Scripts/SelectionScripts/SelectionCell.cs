using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionCell : MonoBehaviour
{
    public Cell realCellReference;
    public TextMeshProUGUI infectText;

    public void SelectCell() {
        GameObject.Find("SelectCell").GetComponent<SelectCell>().SetTargetCell(realCellReference);
    }
}
