using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoveEffect : MonoBehaviour
{
    private Vector3 mousePosition;
    private float speed;

    public float divider;
    public bool isPaused;

    public void UpdateSpeed() {
        speed = GameObject.Find("Player").GetComponent<PlayerMovement>().speed / divider;
    }

    public void Start() {
        UpdateSpeed();
    }

    void Update(){
        if(!isPaused) {
            float step = speed * Time.deltaTime;

            if (Input.GetMouseButton(0)) {
                mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.position = Vector2.MoveTowards(transform.position, -mousePosition, step);
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movementVector = new Vector3(horizontal * step, vertical * step, 0);
            transform.position -= movementVector;
        }
    }
}
