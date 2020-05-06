using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlScript : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;
    string inputHorizontal;
    string inputJump;
    public bool upsidedown = false;
    float horizontalMove = 0f;
    
    bool jump = false;
    bool crouch = false;
    // Start is called before the first frame update
    void Start()
    {
        if (upsidedown)
        {
            inputHorizontal = "HorizontalUpsideDown";
            inputJump = "JumpUpsideDown";
        }
        else
        {
            inputHorizontal = "HorizontalRightsideUp";
            inputJump = "JumpRightsideUp";


        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw(inputHorizontal) * runSpeed;

        if (Input.GetButtonDown(inputJump))
        {
            print("wha");
            jump = true;
        }

    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void Jump()
    {

    }
}
