using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCell : MonoBehaviour
{
    public Cell realCellReference;

    public void SelectCell() {
        GameObject.Find("SelectCell").GetComponent<SelectCell>().SetTargetCell(realCellReference);
    }
}
