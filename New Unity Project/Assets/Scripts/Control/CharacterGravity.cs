using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravity : MonoBehaviour
{
    Rigidbody2D rb;
    string inputJump;

    public bool turnongrav = true;
    public float grav = 1f;
    public float lowJumpMultiplier = 2f;
   // public spaceLock spaceLock;
    public float fallMultiplier = 2.5f;
    int player_multipler = 1;
    public bool upsideDown = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (upsideDown)
        {
            inputJump = "JumpUpsideDown";
            player_multipler = -1;


        }
        else
        {
            inputJump = "JumpRightsideUp";
            player_multipler = 1;



        }
    }
    private void Update()
    {


    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (turnongrav)
        {

            if (player_multipler * rb.velocity.y < 0)
            {
                rb.gravityScale = player_multipler * fallMultiplier;
            }
            else if (player_multipler * rb.velocity.y > 0 && !Input.GetButton(inputJump))
            {
                rb.gravityScale = player_multipler * fallMultiplier;
            }
            else
            {
                rb.gravityScale = player_multipler * grav;
            }


        }
        else
        {
            rb.gravityScale = 0;
        }
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -17, 17), Mathf.Clamp(rb.velocity.y, -17, 17));

    }
}
