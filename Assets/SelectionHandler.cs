using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    private bool isDown;
    //public Transform selectionTransform;

    private Vector2 topLeft;
    private Vector2 bottomRight;

    private List<MicroBacteria> selectedMicroBacterias;

    List<MicroBacteria> GetSelectedBacterias() {
        List<MicroBacteria> bacterias = new List<MicroBacteria>();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Bacteria");
        for(int i = 0; i < objs.Length; i++) {
            if(objs[i].transform.position.x >= topLeft.x && objs[i].transform.position.x <= bottomRight.x) {
                if(objs[i].transform.position.y <= topLeft.y && objs[i].transform.position.y >= bottomRight.y) {
                    bacterias.Add(objs[i].GetComponent<MicroBacteria>());
                }
            }
        }

        return bacterias;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1)) {
            Debug.Log("Down");
            //selectionTransform.gameObject.SetActive(true);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(worldPosition.x, worldPosition.y);
            topLeft = mousePosition;
            
            isDown = true;
        }
        else if(Input.GetMouseButtonUp(1)) {
            Debug.Log("Up");
            //selectionTransform.gameObject.SetActive(false);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(worldPosition.x, worldPosition.y);
            bottomRight = mousePosition;

            selectedMicroBacterias = GetSelectedBacterias();
            foreach (MicroBacteria bacteria in selectedMicroBacterias) {
                bacteria.ToggleSelection();
            }

            isDown = false;
        }
    }
}
