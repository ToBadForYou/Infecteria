using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sugarPrefab;

    float timePassed = 0.0f;
    float maxTime = 20.0f;

    float offset = 10.0f;

    void Update()
    {
        timePassed+=Time.deltaTime;
        if(timePassed >= maxTime) {
            timePassed = 0.0f;
            Vector2 pos = new Vector2(Random.Range(transform.position.x - offset, transform.position.x + offset), Random.Range(transform.position.y - offset, transform.position.y + offset));
            Instantiate(sugarPrefab, pos, Quaternion.identity);
        }
    }
}
