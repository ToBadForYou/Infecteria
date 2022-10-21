using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI progressMesh;
    [SerializeField] Organ organ;

    [SerializeField] int maxAmount;

    public bool isFinished;

    void Start(){
        isFinished = true;
        StartCoroutine(LateStart(0.1f));
    }

    IEnumerator LateStart(float waitTime){
        yield return new WaitForSeconds(waitTime);
        isFinished = false;
    }

    void Update()
    {
        if(!isFinished) {
            progressMesh.text = (maxAmount - organ.cells.Count) + "/" + maxAmount;
            if(progressMesh.text == maxAmount + "/" + maxAmount)
                isFinished = true;
        }
    }
}
