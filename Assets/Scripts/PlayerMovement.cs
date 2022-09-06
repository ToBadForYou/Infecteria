using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    public float speed = 5.0f;

    public void SetSpeed(float movementSpeed) {
        speed = movementSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) { // Holding left mouse button
            mousePosition = Input.mousePosition; // Get mouse position
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); // Convert to world units
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, mousePosition, step); // Move towards
        }
    }
}
