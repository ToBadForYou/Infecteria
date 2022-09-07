using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector2 originPos;
    private Vector2 target;

    public GameObject factoryPrefab;
    public Transform infectionBarPivot;

    public float offset;
    public float speed;

    public SpriteRenderer insideRenderer;

    public float maxInfectionAmount;
    public float infectionAmount;
    public bool isInfected;

    public float scaleFactor;
    private float initialScale;
    private float targetScaleX;
    private float targetScaleY;

    void Start()
    {
        initialScale = transform.localScale.x;
        SetTargetScale();
        originPos = transform.position;
        target = GetNewTarget();
    }

    private void SetTargetScale() {
        targetScaleX = initialScale + Random.Range(-scaleFactor, scaleFactor);
        targetScaleY = initialScale + Random.Range(-scaleFactor, scaleFactor);
    }

    private void UpdateScale() {
        float step = 0.15f * Time.deltaTime;
        transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(targetScaleX, targetScaleY), step);
        if(Vector2.Distance(transform.localScale, new Vector2(targetScaleX, targetScaleY)) <= 0.01f) {
            SetTargetScale();
        }
    }

    public void GetAbsorbed() {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.IncreaseDNAPoints(100);
        gm.IncreaseAbsorbedCells();

        Destroy(gameObject);
    }

    public void TurnIntoFactory() {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.IncreaseFactoryAmount();

        Instantiate(factoryPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Infect(float amount) {
        if(!isInfected){
            infectionAmount += amount;
            if(infectionAmount >= maxInfectionAmount) {
                infectionAmount = maxInfectionAmount;
                isInfected = true;
                GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                gm.IncreaseInfectedCells();
            }
            insideRenderer.color = new Color(insideRenderer.color.r, insideRenderer.color.g + amount/10.0f, insideRenderer.color.b, insideRenderer.color.a);
            infectionBarPivot.localScale = new Vector2(infectionAmount/10.0f, 1.0f);
        }
    }

    Vector2 GetNewTarget() {
        return new Vector2(Random.Range(originPos.x-offset, originPos.x+offset), Random.Range(originPos.y-offset, originPos.y+offset));
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        
        float diff = Vector2.Distance(transform.position, target);
        if(diff <= 0.01f) {
            target = GetNewTarget();
        }
        transform.position = Vector2.MoveTowards(transform.position, target, step);

        UpdateScale();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerMovement>().SetSpeed(2.5f);
            col.gameObject.GetComponent<Player>().currentCell = this;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerMovement>().SetSpeed(5.0f);
            col.gameObject.GetComponent<Player>().currentCell = null;
        }
    }
}
