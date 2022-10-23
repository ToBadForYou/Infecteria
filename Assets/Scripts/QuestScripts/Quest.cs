using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour
{
    [SerializeField] Image img;

    [SerializeField] TextMeshProUGUI titleMesh;
    [SerializeField] TextMeshProUGUI descriptionMesh;
    [SerializeField] protected TextMeshProUGUI progressMesh;

    [SerializeField] protected GameManager gm;

    [SerializeField] protected int maxAmount;
    public bool isFinished;
    public bool isActive;

    void Start(){
        StartCoroutine(LateStart(0.1f));
    }

    IEnumerator LateStart(float waitTime){
        yield return new WaitForSeconds(waitTime);
    }

    public virtual void InitQuest(){

    }

    public virtual void CheckQuestProgress(){

    }

    bool GetIfQuestFinished(){
        return progressMesh.text == maxAmount + "/" + maxAmount;
    }

    void FadeIn(){
        titleMesh.color = new Color(titleMesh.color.r, titleMesh.color.g, titleMesh.color.b, titleMesh.color.a + Time.deltaTime);
        descriptionMesh.color = new Color(descriptionMesh.color.r, descriptionMesh.color.g, descriptionMesh.color.b, descriptionMesh.color.a + Time.deltaTime);
        progressMesh.color = new Color(progressMesh.color.r, progressMesh.color.g, progressMesh.color.b, progressMesh.color.a + Time.deltaTime);
        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + Time.deltaTime);
    }

    void Dissolve(){
        titleMesh.color = new Color(titleMesh.color.r, titleMesh.color.g, titleMesh.color.b, titleMesh.color.a - Time.deltaTime);
        descriptionMesh.color = new Color(descriptionMesh.color.r, descriptionMesh.color.g, descriptionMesh.color.b, descriptionMesh.color.a - Time.deltaTime);
        progressMesh.color = new Color(progressMesh.color.r, progressMesh.color.g, progressMesh.color.b, progressMesh.color.a - Time.deltaTime);
        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - Time.deltaTime);
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * 50.0f);
    }

    void Update(){
        if(isActive) {
            if(!isFinished) {
                if(img.color.a <= 1.0f)
                    FadeIn();
                else {
                    CheckQuestProgress();
                    if(GetIfQuestFinished())
                        isFinished = true;
                }
            }
            else {
                Dissolve();
                if(img.color.a <= 0.0f)
                    Destroy(gameObject);
            }
        }
    }
}
