using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSkin : MonoBehaviour
{
    public GameObject skinPrefab;

    void Start()
    {
        StartCoroutine(LateStart(0.5f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        if(transform.localScale.x > transform.localScale.y) {
            for(int i = 0; i < (int)transform.localScale.x; i++) {
                for(int j = 0; j < 3; j++) {
                    Vector2 pos = new Vector2(transform.position.x - transform.localScale.x/2.0f + i, transform.position.y + Random.Range(-0.5f, 0.5f));
                    Instantiate(skinPrefab, pos, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                }
            }
        }
        else {
            for(int i = 0; i < (int)transform.localScale.y; i++) {
                for(int j = 0; j < 3; j++) {
                    Vector2 pos = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y - transform.localScale.y/2.0f + i);
                    Instantiate(skinPrefab, pos, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                }
            }
        }
    }
}
