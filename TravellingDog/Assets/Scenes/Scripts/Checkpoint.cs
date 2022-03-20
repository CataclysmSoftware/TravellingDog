using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Checkpoint : MonoBehaviour
{
    public bool CheckpointActive;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("isOpen", true);
        }
    }
}
