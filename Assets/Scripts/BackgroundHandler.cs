using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHandler : MonoBehaviour
{
    public Transform layer1;
    public Transform layer2;
    public Transform layer3;

    private Vector3 mousePosition;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) { // Holding left mouse button
            mousePosition = Input.mousePosition; // Get mouse position
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); // Convert to world units
            
            float step1 = 4.0f * Time.deltaTime;
            float step2 = 3.0f * Time.deltaTime;
            float step3 = 2.0f * Time.deltaTime;
            layer1.position = Vector2.MoveTowards(layer1.position, -mousePosition, step1); // Move towards
            layer2.position = Vector2.MoveTowards(layer2.position, -mousePosition, step2); // Move towards
            layer3.position = Vector2.MoveTowards(layer3.position, -mousePosition, step3); // Move towards
        }
    }
}
