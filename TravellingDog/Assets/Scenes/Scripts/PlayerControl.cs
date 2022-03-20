using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public Rigidbody2D myRigidbody;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatisGround;
    public bool isGround;
    private Animator myAnim;
    public Vector3 respawnPosition;
    public Vector3 respawnPositionCheckpoint;
    public LevelManager theLevelManager;
    public float knockbackForce;
    public float knockbackForceUP;
    public float knockbackLenght;
    private float knockbackCounter;
    public float inviniciblityLenght;
    private float inviniciblityCounter;
    public GameObject stompBox;
    public bool canMove;
    public bool moveright;
    public bool moveleft;
    public bool movejump;
    public ParticleSystem particle;

    public GameObject dustPuff;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        respawnPosition = transform.position;
        theLevelManager = FindObjectOfType<LevelManager>();
        canMove = true;

        GameObject dustObject = Instantiate(dustPuff, this.transform.position, this.transform.rotation) as GameObject;
        dustObject.SetActive(false);
        particle = dustPuff.GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    public void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatisGround);

        if (inviniciblityCounter > 0)
        {
            inviniciblityCounter -= Time.deltaTime;
        }
        if (knockbackCounter <= 0 && canMove)
        {
            if (moveright)
            {
                myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
                EmissionTrue();
            }
            else if (moveleft)
            {
                myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
                EmissionTrue();

            } else
            {
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
                var emission = particle.emission;
                emission.enabled = false;
            }
            if (isGround && movejump)
            {
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
                movejump = false;
                EmissionTrue();
                //theLevelManager.jumpSound.Play();
            }
            //theLevelManager.invincible = false;
        }
        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;
            if (transform.localScale.x > 0)
            {
                myRigidbody.velocity = new Vector3(knockbackForce, knockbackForceUP, 0f);
                //StartCoroutine("KnockEnd");
            }
            else if (transform.localScale.x < 0)
            {
                myRigidbody.velocity = new Vector3(-knockbackForce, knockbackForceUP, 0f);
                //StartCoroutine("KnockEnd");
            }
            else
            {
                myRigidbody.velocity = new Vector3(0f, knockbackForceUP, 0f);
            }
        }
        if (inviniciblityCounter <= 0)
        {
            //theLevelManager.invincible = false;
        }

        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetBool("Grounded", isGround);

        if (myRigidbody.velocity.y < 0)
        {
            stompBox.SetActive(true);
        } else
        {
            stompBox.SetActive(false);
        }
    }

    public void EmissionTrue()
    {
        var emission = particle.emission;
        emission.enabled = true;
        var d = particle.main;
        if (theLevelManager.invincible == true)
        {
            d.startColor = new Color(Random.Range(0f, 255f) / 255f, Random.Range(0f, 255f) / 255f, Random.Range(0f, 255f) / 255f);
            d.startSize = 0.065f;
        }
        else
        {
            d.startColor = new Color(178f / 255f, 147f / 255f, 107f / 255f);
            d.startSize = 0.05f;
        }
    }

    public void Jump() 
    {
        movejump = true;
    }
    public void JumpStop()
    {
        movejump = false;
    }
    public void RiMove() 
    {
        moveright= true;
    }
    public void RiStop()
    {
         moveright = false;
    }
    public void LeMove() 
    {
        moveleft = true;
    }
    public void LeStop()
    {
         moveleft = false;
    }
    
    /*public IEnumerator KnockEnd()
    {
        yield return new WaitForSeconds(0.85F);
        myRigidbody.velocity = Vector3.zero;
    }*/
    public void Knockback()
    {
        knockbackCounter = knockbackLenght;
        inviniciblityCounter = inviniciblityLenght;
        //theLevelManager.invincible = true;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "KillPlane")
       {
         theLevelManager.Retry();
       }

        if(other.tag == "Checkpoint")
        {
            respawnPositionCheckpoint = other.transform.position;
        }
    }

}
