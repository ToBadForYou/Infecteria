using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveTowards : MonoBehaviour
{
    private Vector2 originPos;
    private Vector2 target;
    
    public float offset;
    public float speed;

    void Start()
    {
        originPos = transform.position;
        target = GetNewTarget();
    }

    Vector2 GetNewTarget() {
        return new Vector2(Random.Range(originPos.x-offset, originPos.x+offset), Random.Range(originPos.y-offset, originPos.y+offset));
    }

    void Update()
    {
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            float step = speed * Time.deltaTime;
            float diff = Vector2.Distance(transform.position, target);
            if(diff <= 0.01f)
                target = GetNewTarget();
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
    }
}
