using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLIfe : MonoBehaviour
{
    // Start is called before the first frame update
    public int livesToGive;

    private LevelManager theLevelManager;
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            //theLevelManager.AddLives(livesToGive);
            gameObject.SetActive(false);
        }
    }
}
