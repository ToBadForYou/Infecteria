using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbEffect : MonoBehaviour
{
    public bool isActive = false;

    public Transform childTransform;

    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    void Update()
    {
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if(isActive) {
                if(transform.childCount > 1) {
                    transform.GetChild(0).gameObject.SetActive(false);
                    childTransform.parent = null;
                }
                
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime*0.5f);
                for(int i = 0; i < spriteRenderers.Count; i++) {
                    spriteRenderers[i].color = new Color(spriteRenderers[i].color.r, spriteRenderers[i].color.g, spriteRenderers[i].color.b, spriteRenderers[i].color.a - Time.deltaTime*0.5f);
                }
                
                transform.Rotate(0, 0, 25*Time.deltaTime);
                childTransform.Rotate(0, 0, 50*Time.deltaTime);

                if(sr.color.a <= 0.0f) {
                    Destroy(childTransform.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
