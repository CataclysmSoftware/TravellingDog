using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private bool canMove;
    private Rigidbody2D myRigidbody;
    public Transform startPoint;
    public Transform endPoint;
    public bool movingRight;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight && transform.position.x > endPoint.position.x)
        {
            movingRight = false;
        }
        if (!movingRight && transform.position.x < startPoint.position.x)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void OnBecameVisible()
    {
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (other.tag == "KillPlane")
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
