using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    public float scaleFactor;
    public float additionalScale = 0.0f;

    private float initialScale;
    private float targetScaleX;
    private float targetScaleY;

    void Start()
    {
        initialScale = transform.localScale.x;
        SetTargetScale();
    }

    private void SetTargetScale() {
        targetScaleX = initialScale + Random.Range(-scaleFactor, scaleFactor);
        targetScaleY = initialScale + Random.Range(-scaleFactor, scaleFactor);
    }

    private void FixFactoryProducers() {
        List<Transform> transforms = new List<Transform>();
        if (transform.childCount > 0)
            for(int i = 0; i < transform.childCount; i++)
                if(transform.GetChild(i).gameObject.name.Contains("Producer"))
                    transforms.Add(transform.GetChild(i));
        for(int i = 0; i < transforms.Count; i++) {
            float xScaleDiff = transform.localScale.x - 1;
            float yScaleDiff = transform.localScale.y - 1;
            transforms[i].localScale = new Vector2(1.0f-xScaleDiff, 1.0f-yScaleDiff);
            Debug.Log("GOING IN");
        }
    }

    private void UpdateScale() {
        float step = 0.15f * Time.deltaTime;
        transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(targetScaleX, targetScaleY), step);
        
        float xScaleDiff = 1 - transform.localScale.x;
        float yScaleDiff = 1 - transform.localScale.y;

        if(transform.childCount > 1) {
            transform.GetChild(1).localScale = new Vector2(1.0f + additionalScale + xScaleDiff, 1.0f + additionalScale + yScaleDiff);
        }

        if(Vector2.Distance(transform.localScale, new Vector2(targetScaleX, targetScaleY)) <= 0.01f) {
            SetTargetScale();
        }

        FixFactoryProducers();
    }

    void Update()
    {
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE)
            UpdateScale();
    }
}
