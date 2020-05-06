// Note that the code in this script is a fairly naive/quick jumping test (can implement better jumping later if there's time).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    bool playerlastground = false;
    bool getButton = false;
    bool getButtonUp = false;
    // Need to check make sure jumpRequest can't be set to true if the character is still jumping.
    string inputJump;
    LayerMask layer_mask;
    LayerMask other_player_mask;
    int player_multipler = 1;
    bool allowedMv = false;
    public bool upsideDown = false;
    public Rigidbody2D rb2;
    public float jumpForce;
    public AudioClip myJump;
    public float startTime = 0;
    public bool decidewhatToDo = false;
    public float jumpRaycastDistance;
    bool madeSound = false;
    private bool jumpRequest;
    //public TouchInput joystick;

    public bool decide = true;
    private bool isJumping;
    private bool collidedWithSomething = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionStay2D(Collision2D collision)   // If characters begin level with edges directly on a platform, will this still work?
    {
        // Does this fix the sticky platform bug? Or will there now be a touch of jumping lag once a character lands on a platform?
        // Does this create weird problems when jumping on/off edges?

        // Debug.Log("Now calling CheckIfGrounded");

    }
    void Start()
    {
        allowedMv = GetComponent<CharacterMove>().allowedMv;
        //jumpRaycastDistance = 0.5f;
        isJumping = false;   // I'm assuming that this is the case?
        if (upsideDown)
        {
            inputJump = "JumpUpsideDown";
            player_multipler = -1;
            layer_mask = LayerMask.GetMask( "Platform");
            other_player_mask = LayerMask.GetMask("rightsideUp");

        }
        else
        {
            inputJump = "JumpRightsideUp";
            player_multipler = 1;

            layer_mask = LayerMask.GetMask( "Platform");
            other_player_mask = LayerMask.GetMask("upsideDown");
           // print(layer_mask);

        }
    }
    void CheckIfTouchCeiling()
    {
        Vector2 m_CeilCheck = new Vector2(transform.position.x, transform.position.y + (player_multipler * .5f));
        Vector2 k_CeilRadius = new Vector2(.45f, .01f);
        Collider2D hit = Physics2D.OverlapBox(m_CeilCheck, k_CeilRadius, layer_mask);

        if (Physics2D.OverlapBox(m_CeilCheck, k_CeilRadius, 0, layer_mask))
        {
           //print("BBBBBBBB");

            decidewhatToDo = false;
            rb.velocity = new Vector2(0, 0);


        }
        if (Physics2D.OverlapBox(m_CeilCheck, k_CeilRadius, 0, other_player_mask))
        {
            
        }

    }

    void CheckIfGrounded()
    {
        
        Vector2 m_GroundCheck = new Vector2(transform.position.x, transform.position.y - (player_multipler * .5f));
        Vector2 k_GroundedRadius = new Vector2(.5f, .5f);
        Vector2 k_GroundedPLayerRadius = new Vector2(.5f, .08f);
        if (playerlastground == false)
        {
             k_GroundedRadius = new Vector2(.63f, .563f);
             k_GroundedPLayerRadius = new Vector2(.63f, .08f);
        }
        else
        {
             k_GroundedRadius = new Vector2(.63f, .63f);
             k_GroundedPLayerRadius = new Vector2(.7f, .08f);
        }

        Collider2D hit = Physics2D.OverlapBox(m_GroundCheck, k_GroundedPLayerRadius,0, other_player_mask);
        if (hit == null)
        {
            playerlastground = false;
            rb.isKinematic = false;
        }
        if (Physics2D.OverlapBox(m_GroundCheck, k_GroundedRadius, 0, layer_mask))
        {
            playerlastground = false;
            isJumping = false;
            GetComponent<CharacterGravity>().turnongrav = true;


        }
        if (Physics2D.OverlapBox(m_GroundCheck, k_GroundedPLayerRadius, 0, other_player_mask))
        {
            playerlastground = true;

            isJumping = false;
            GetComponent<CharacterGravity>().turnongrav = true;
            rb.isKinematic = true;
            
            GetComponent<CharacterGravity>().turnongrav = false;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            collidedWithSomething = true;
        }


    }
    private void Update()
    {
        bool frigate = false;

        if (collidedWithSomething)
        {
            GetComponent<CharacterGravity>().turnongrav = true;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        allowedMv = GetComponent<CharacterMove>().allowedMv;
        if (allowedMv) { 
        if (Input.GetButtonDown(inputJump) && !isJumping)
        {

            startTime = 0;
            decidewhatToDo = true;
            rb.isKinematic = false;

            decide = true;
        }
            if (!isJumping)
            {
                if (decide == true)
                {
                    //  print("hm");

                    if ((Input.GetButton(inputJump)) && startTime < .15f)
                    {
                        getButton = true;

                        // print("hm");
                    }
                    if ((Input.GetButton(inputJump)) && startTime >= .15f)
                    {
                        startTime = 0.15f;
                        decidewhatToDo = false;
                        getButton = false;
                        decide = true;
                        frigate = true;
                        jumpRequest = true;
                        isJumping = true;
                        rb.isKinematic = false;
                        rb.constraints = RigidbodyConstraints2D.None;
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }
                    if ((Input.GetButtonUp(inputJump)) && frigate == false)
                    {
                        getButton = false;
                        decide = true;
                        decidewhatToDo = false;
                        startTime = Mathf.Ceil(startTime * 100) / 100;

                        jumpRequest = true;
                        isJumping = true;
                        rb.isKinematic = false;
                        rb.constraints = RigidbodyConstraints2D.None;
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }
                }
            }
        }

    }
    private void FixedUpdate()
    {
       CheckIfTouchCeiling();
        CheckIfGrounded();

        if (getButton)
        {
            startTime += Time.fixedDeltaTime;

        }
        if (jumpRequest)
        {
            GetComponent<AudioSource>().PlayOneShot(myJump);

            if (decide == true)
            {
                startTime = 0;
            }
            startTime = 0;
            decide = false;


            jumpRequest = false;
        }


        if (decidewhatToDo)
        {

            rb.velocity = new Vector3(0, (player_multipler * jumpForce * startTime * 2.5f) + (player_multipler * 5),0);
            //rb.AddForce(new Vector3(0, (player_multipler * jumpForce * startTime * 2.5f), 0), ForceMode2D.);

        }


    }
}
