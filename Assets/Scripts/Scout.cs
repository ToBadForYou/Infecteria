using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Tower
{
    public Transform parentTransform;
    public FollowTarget followTarget;
    public GameObject exclamationMark;
    public bool isAlerted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    override public void Attack(){
        if (!isAlerted){
            isAlerted = true;
            followTarget.target = parentTransform;
            exclamationMark.SetActive(true);
        }
    }

    public void SetTarget(Transform target){
        followTarget.target = target;
    }
}
