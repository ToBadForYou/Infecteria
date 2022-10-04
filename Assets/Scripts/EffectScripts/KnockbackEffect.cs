using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    bool isGettingKnocked = false;
    Vector2 knockbackPosition;
    GameObject tempGameObj = null;

    public float knockbackSpeed = 20.0f;
    public float knockbackMultiplier = 2.5f; 

    void OnCollisionEnter2D(Collision2D col)
    {   
        if(col.gameObject.GetComponent<PlayerMovement>()) {
            tempGameObj = col.gameObject;
            knockbackPosition = col.gameObject.transform.position - col.gameObject.GetComponent<PlayerMovement>().lastMoveDir * knockbackMultiplier;
            isGettingKnocked = true;
        }
    }

    void Update() {
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if(isGettingKnocked) {
                tempGameObj.transform.position = Vector2.MoveTowards(tempGameObj.transform.position, knockbackPosition, knockbackSpeed * Time.deltaTime);
                if(Vector2.Distance(tempGameObj.transform.position, knockbackPosition) < 0.01f)
                    isGettingKnocked = false;
            }
        }
    }
}
