using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private LevelManager theLevelManger;

    public int damageToGive;

    void Start()
    {
        theLevelManger = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            //theLevelManger.Respawn();
            theLevelManger.HurtPlayer(damageToGive);
        }
    }

}
