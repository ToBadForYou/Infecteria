using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    public float speed = 5.0f;
    public bool isPaused;

    public void SetSpeed(float movementSpeed) {
        speed = movementSpeed;
    }

    void Update()
    {
        if(!isPaused) {
            float step = speed * Time.deltaTime;
            if (Input.GetMouseButton(0)) { // Holding left mouse button
                mousePosition = Input.mousePosition; // Get mouse position
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); // Convert to world units
                transform.position = Vector2.MoveTowards(transform.position, mousePosition, step); // Move towards
            }

            //Alternative player movement
            if(Input.GetKey(KeyCode.W)) {
                transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
            }
            if(Input.GetKey(KeyCode.A)) {
                transform.position = new Vector3(transform.position.x - step, transform.position.y, transform.position.z);
            }
            if(Input.GetKey(KeyCode.S)) {
                transform.position = new Vector3(transform.position.x, transform.position.y - step, transform.position.z);
            }
            if(Input.GetKey(KeyCode.D)) {
                transform.position = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
            }
        }
    }
}
