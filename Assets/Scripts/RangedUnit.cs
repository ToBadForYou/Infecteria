using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnit : Unit
{
    public GameObject target;
    public SpriteRenderer spriteRenderer;
    public bool attacking = false;
    public Color detectedColor = new Color(1, 0, 0, 0.4f);
    public Color idleColor = new Color(1, 1, 1, 0.4f);

    // Update is called once per frame
    protected void Update()
    {  
        if(target != null){
            attacking = true;
            spriteRenderer.color = detectedColor;
            Attack();
        } else if (attacking){
            attacking = false;
            spriteRenderer.color = idleColor;
            StopAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        target = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject == target){
            target = null;
        }
    }    

    void OnTriggerStay2D(Collider2D col)
    {
        if(col == null){
            target = col.gameObject;
        }
    }

    virtual public void Attack(){

    }

    virtual public void StopAttack(){

    }    
}
