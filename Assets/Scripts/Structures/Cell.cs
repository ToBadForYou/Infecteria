using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Infectable
{
    private Vector2 originPos;
    private Vector2 target;
    
    public GameObject factoryPrefab;
    public Transform infectionBarPivot;
    public GameObject warningCured;

    public float offset;
    public float speed;

    public SpriteRenderer insideRenderer;

    public AudioSource source;
    public AudioClip soundEffect;

    public List<SoundManager> soundManagers;

    GameManager gm;

    void Start() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        foreach (SoundManager sm in soundManagers) {
            sm.CreateAudioSrc();
        }

        originPos = transform.position;
        target = GetNewTarget();
    }

    public void GetAbsorbed(){
        gm.IncreaseDNAPoints(30);
        gm.IncreaseAbsorbedCells();
        gm.RemoveCell(gameObject);

        if(organ){
            organ.RemoveCell(this);
        }

        Destroy(this);
        GetComponent<AbsorbEffect>().isActive = true;
    }

    public void TurnIntoFactory(){
        gm.IncreaseFactoryAmount(1);
        GameObject factoryObj = Instantiate(factoryPrefab, transform.position, Quaternion.identity);
        Factory factory = factoryObj.GetComponent<Factory>();
        factory.organ = organ;
        if(organ != null)
            organ.ReplaceCell(this, factory);
        gm.ReplaceCell(gameObject, factoryObj);
        Destroy(gameObject);
    }

    Vector2 GetNewTarget(){
        return new Vector2(Random.Range(originPos.x-offset, originPos.x+offset), Random.Range(originPos.y-offset, originPos.y+offset));
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE){
            float step = speed * Time.deltaTime;
            
            float diff = Vector2.Distance(transform.position, target);
            if(diff <= 0.01f) {
                target = GetNewTarget();
            }
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
    }

    public override void OnInfect(){
        gm.IncreaseInfectedCells(1);
        if(organ != null){
            GameObject.Find("GameManager").GetComponent<GameManager>().infectedHeartCells += 1;
            organ.CheckVictory();
        }

        foreach(SoundManager sm in soundManagers) {
            if(sm.eventIdentifier == "Infected")
                sm.PlaySound();
        }

        infectionBarPivot.gameObject.SetActive(false); // hide bar when infected, this may lead to problems later, if so: TODO: Fix bug
        insideRenderer.color = new Color(0.0f, 1.0f, 0.0f);
    }

    public override void OnInfectUpdate(float amount){
        insideRenderer.color = new Color(insideRenderer.color.r, insideRenderer.color.g + amount/10.0f, insideRenderer.color.b, insideRenderer.color.a);
        infectionBarPivot.localScale = new Vector2(infectionAmount/10.0f, 1.0f);
    }

    public GameObject particleSystemObj;
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player") {
            gm.timesEnteredCell++;

            Instantiate(particleSystemObj, col.gameObject.transform.position, Quaternion.identity);

            foreach(SoundManager sm in soundManagers) {
                sm.PlaySound();
            }

            col.gameObject.GetComponent<PlayerMovement>().SetSpeedPenalty(1f);
            col.gameObject.GetComponent<Player>().currentCell = this;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Player") {
            Instantiate(particleSystemObj, col.gameObject.transform.position, Quaternion.identity);
            
            foreach(SoundManager sm in soundManagers) {
                if(sm.eventIdentifier == "Player Enter Exit")
                    sm.PlaySound();
                
                if(sm.eventIdentifier == "Player Inside")
                    sm.StopSound();
            }

            col.gameObject.GetComponent<PlayerMovement>().SetSpeedPenalty(0f);
            col.gameObject.GetComponent<Player>().currentCell = null;
        }
    }
}
