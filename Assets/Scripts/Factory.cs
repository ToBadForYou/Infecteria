using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Infectable
{
    public GameObject microBacteriaPrefab;
    public GameObject DNAProducerPrefab;
    public float startTime = 10.0f;
    public float time = 10.0f;
    
    private int usedSlots = 0;
    private int maxSlots = 1;

    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0.0f) {
            GameObject obj = Instantiate(microBacteriaPrefab, transform.position, Quaternion.identity);
            obj.GetComponent<MicroBacteria>().unitMovement.MoveToPosition(new Vector2(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f)));
            time = startTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerMovement>().SetSpeed(2.5f);
            col.gameObject.GetComponent<Player>().currentFactory = this;
            Player playerScript = col.gameObject.GetComponent<Player>();
            if(CanBuild()) {
                playerScript.MakeObjActive(playerScript.bObject);
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerMovement>().SetSpeed(5.0f);
            col.gameObject.GetComponent<Player>().currentFactory = null;
            Player playerScript = col.gameObject.GetComponent<Player>();
            playerScript.MakeObjDeactive(playerScript.bObject);
        }
    }

    public bool CanBuild() {
        return usedSlots < maxSlots;
    }

    public void Build() {
        GameObject temp = Instantiate(DNAProducerPrefab, new Vector3(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f), -2.0f), Quaternion.identity);
        temp.transform.parent = transform;
        usedSlots++;
    }
}
