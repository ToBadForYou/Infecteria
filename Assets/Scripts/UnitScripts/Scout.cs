using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : DetectorUnit
{
    public GameObject parent;
    public GameObject exclamationMark;
    public GameObject targetCell = null;
    public Vector2 alertPosition;
    public bool finished;
    public bool isAlerted;

    public AudioSource audioSrc;
    public AudioClip soundEffect;

    new void Start() {
        base.Start();
        SetUnitStats(5, 5, 0, 1, 1.0f, 0.2f, false);
    }

    public override void Attack(){
        if (!isAlerted){
            SetAlerted(target);
        }
    }

    public void SetAlerted(GameObject triggerObject){
        isAlerted = true;
        exclamationMark.SetActive(true);
        FollowTarget(parent);
        finished = true; 
        alertPosition = triggerObject.transform.position;
        audioSrc = GameObject.Find("Sound Effect Player").GetComponent<AudioSource>();
        audioSrc.clip = soundEffect;
        audioSrc.Play();
    }

    public void SetTarget(GameObject target){
        FollowTarget(target);
        targetCell = target;
    }

    // TODO replace with task
    public override void OnReachedDestination(GameObject target){
        if(targetCell != null && target == targetCell){
            Infectable cell = targetCell.GetComponent<Infectable>();
            if (cell != null && (cell.isInfected || cell.infectionAmount/cell.maxInfectionAmount > 0.5)){
                // TODO heal the cell
                SetAlerted(targetCell);
            }
            else {
                FollowTarget(parent);
                finished = true;
            }
        }

        if(finished && target == parent) {
            if(isAlerted){
                parent.GetComponent<Heart>().OnReport(alertPosition);
            }
            OnDeath();
            Destroy(transform.root.gameObject); 
        }      
    }
}
