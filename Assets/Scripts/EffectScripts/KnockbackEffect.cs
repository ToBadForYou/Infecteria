using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    Vector2 knockbackPosition;
    GameObject tempGameObj = null;

    public float knockbackSpeed = 18.0f;
    public float knockbackMultiplier = 2.5f; 

    void OnCollisionEnter2D(Collision2D col){
        PlayerMovement playerMov = col.gameObject.GetComponent<PlayerMovement>();
        if(col.gameObject.GetComponent<PlayerMovement>()) {
            tempGameObj = col.gameObject;
            knockbackPosition = tempGameObj.transform.position - playerMov.lastMoveDir * knockbackMultiplier;
            playerMov.knockbackObject = gameObject;
        }
    }

    void Update() {
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if(tempGameObj != null && gameObject == tempGameObj.GetComponent<PlayerMovement>().knockbackObject) {
                tempGameObj.transform.position = Vector2.MoveTowards(tempGameObj.transform.position, knockbackPosition, knockbackSpeed * Time.deltaTime);
                if(Vector2.Distance(tempGameObj.transform.position, knockbackPosition) < 0.01f)
                    tempGameObj.GetComponent<PlayerMovement>().knockbackObject = null;
            }
        }
    }
}
