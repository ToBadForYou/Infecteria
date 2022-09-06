using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject microBacteriaPrefab;
    public float startTime = 10.0f;
    public float time = 10.0f;

    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0.0f) {
            Instantiate(microBacteriaPrefab, transform.position, Quaternion.identity);
            time = startTime;
        }
    }
}
