using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilitiPickup : MonoBehaviour
{
    // Start is called before the first frame update

    private LevelManager theLevelManager;
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theLevelManager.Invincible();
            gameObject.SetActive(false);
       }
        
    }
}
