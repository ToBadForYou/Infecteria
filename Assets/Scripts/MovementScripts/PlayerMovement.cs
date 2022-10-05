using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    public float speed = 5.0f;
    public float additionalSpeed = 0.0f;
    public GameObject knockbackObject;
    public bool isPaused;

    public Vector3 lastMoveDir;

    public void SetSpeed(float movementSpeed) {
        speed = movementSpeed;
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if(!isPaused) {
                float step = speed * Time.deltaTime;

                if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) {
                    mousePosition = Input.mousePosition;
                    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    transform.position = Vector2.MoveTowards(transform.position, mousePosition, step);
                }

                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                Vector3 movementVector = new Vector3(horizontal * step, vertical * step, 0);
                if(movementVector.magnitude > 0){
                    lastMoveDir = movementVector.normalized;
                    transform.position += movementVector;
                }
            }
        }
    }
}
