using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE)
            if(target)
                transform.position = Vector3.Lerp(transform.position, target.position, 0.1f) + new Vector3 (0.0f, 0.0f, -10.0f);
    }
}
