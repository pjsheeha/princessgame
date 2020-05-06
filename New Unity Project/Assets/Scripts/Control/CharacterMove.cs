// Note that the code in this script is a fairly naive/quick movement test (can implement better jumping later if there's time).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    LayerMask layer_mask;

    public bool upsideDown = false;
    string inputHorizontal;
    public float horizontalMove = 0f;
    public bool allowedMv = false;
    public float characterMoveSpeed;
    public Camera thisOne;
    public GameObject myShadow;
    public GameObject myBody;
    //public TouchInput joystick;

    // Start is called before the first frame update
    void Start()
    {

        thisOne = Camera.main;
        if (upsideDown)
        {
            inputHorizontal = "HorizontalUpsideDown";
            layer_mask = LayerMask.GetMask("rightsideUp", "Platform");

        }
        else
        {
            inputHorizontal = "HorizontalRightsideUp";
            layer_mask = LayerMask.GetMask("upsideDown", "Platform");


        }
    }



    private void Update()
    {
        horizontalMove = Input.GetAxisRaw(inputHorizontal);
    }
    private void FixedUpdate()
    {
        if (allowedMv)
        {




            bool hitSomethingL = false;
            bool hitSomethingR = false;
            Vector2 m_GroundCheck = new Vector2(transform.position.x - .5f, transform.position.y);
            Vector2 k_GroundedRadius = new Vector2(.01f, .5f);
            if (Physics2D.OverlapBox(m_GroundCheck, k_GroundedRadius, 0, layer_mask))
            {
                hitSomethingL = true;
              //  print("hitsomething");

            }
            else
            {
                hitSomethingL = false;
              //  print("nope");

            }
            m_GroundCheck = new Vector2(transform.position.x + .5f, transform.position.y);
            k_GroundedRadius = new Vector2(.01f, .5f);
            if (Physics2D.OverlapBox(m_GroundCheck, k_GroundedRadius, 0, layer_mask))
            {
              //  print("hitsomething");

                hitSomethingR = true;
            }
            else
            {
                hitSomethingR = false;
              //  print("nope");

            }

              //  print("why?");
                //GetComponent<Animator>().Play("run");


                //myShadow.transform.localPosition = new Vector3(myBody.transform.localPosition.x, myBody.transform.localPosition.y, myBody.transform.localPosition.z);

                if (hitSomethingL == false && horizontalMove<0)
                {
                // transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * horizontalMove, transform.localScale.y, transform.localScale.z);
                myShadow.GetComponent<SpriteRenderer>().flipX = true;
                    transform.Translate(Vector2.right * Time.deltaTime * characterMoveSpeed * horizontalMove);
                }
                if (hitSomethingR == false && horizontalMove > 0)
                {
                //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * horizontalMove, transform.localScale.y, transform.localScale.z);
                myShadow.GetComponent<SpriteRenderer>().flipX = false;

                transform.Translate(Vector2.right * Time.deltaTime * characterMoveSpeed * horizontalMove);

                }


        }
    }


        /*
        public bool TouchRightU()
        {
            float horizontalMove = joystick.Horizontal2;
            if (horizontalMove < 0)
            {
                return true;
            }
            return false;
        }

        public bool TouchLeftU()
        {
            float horizontalMove = joystick.Horizontal2;
            if (horizontalMove > 0)
            {
                return true;
            }
            return false;
        }

        public bool TouchRightN()
        {
            float horizontalMove = joystick.Horizontal;
            if (horizontalMove < 0)
            {
                return true;
            }
            return false;
        }

        public bool TouchLeftN()
        {
            float horizontalMove = joystick.Horizontal;
            if (horizontalMove > 0)
            {
                return true;
            }
            return false;
        }
        */

        // Update is called once per frame
        /*
        void FixedUpdate()
        {
            if (allowedMv)
            {
                RaycastHit2D hitL = Physics2D.Raycast(gameObject.transform.position, Vector2.left, .53f);   // Should I be trying to raycast from the exact bottom of the gameobject?
                RaycastHit2D hitR = Physics2D.Raycast(gameObject.transform.position, Vector2.right, .53f);   // Should I be trying to raycast from the exact bottom of the gameobject?
                bool hitL1 = false;
                bool hitR1 = false;
                if (hitL.collider != null)
                {
                    if (hitL.collider.tag == "Platform" || (hitL.collider.tag == "UpsideDownCharacter" ||  hitL.collider.tag == "RightsideUpCharacter"))
                    {
                        //print("LLLL");
                        hitL1 = true;
                    }
                    else
                    {
                        hitL1 = false;
                    }
                }
                else
                {
                    hitL1 = false;
                }
                if (hitR.collider != null)
                {
                    if (hitR.collider.tag == "Platform"  || (hitR.collider.tag == "UpsideDownCharacter" ||  hitR.collider.tag == "RightsideUpCharacter"))
                    {
                        hitR1 = true;
                    }
                    else
                    {
                        hitR1 = false;
                    }
                }
                else
                {
                    hitR1 = false;
                }

                    // If player uses the A or D keys, they move the upside down character.
                    if (gameObject.tag == UPSIDE_DOWN_CHARACTER_TAG)
                    {
                        if (Input.GetKey(KeyCode.A) || TouchLeftU() )
                        {
                            GetComponent<Animator>().Play("run");

                            if (transform.localScale.x < 0)
                            {
                                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                                myShadow.transform.localPosition = new Vector3(myBody.transform.localPosition.x - .35f, myBody.transform.localPosition.y, myBody.transform.localPosition.z);
                            }

                            if (canMove == true)
                            {

                                if (hitL1 == false)
                                {
                                    transform.Translate(Vector2.left * Time.deltaTime * characterMoveSpeed);
                                }

                            }
                            else
                            {


                                if (transform.position.x > thisOne.ScreenToWorldPoint(new Vector3(200, 0, 0)).x)
                                {
                                    if (hitL1 == false)
                                    {
                                        transform.Translate(Vector2.left * Time.deltaTime * characterMoveSpeed);
                                    }

                                }
                            }
                        }
                        else if (Input.GetKey(KeyCode.D) || TouchRightU())
                        {
                            GetComponent<Animator>().Play("run");

                            if (transform.localScale.x > 0)
                            {
                                myShadow.transform.localPosition = new Vector3(myBody.transform.localPosition.x + .3f, myBody.transform.localPosition.y, myBody.transform.localPosition.z);

                                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                            }
                            if (canMove == true)
                            {
                                if (hitR1 == false)
                                {
                                    transform.Translate(Vector2.right * Time.deltaTime * characterMoveSpeed);
                                }

                            }
                            else
                            {

                                if (transform.position.x < thisOne.ScreenToWorldPoint(new Vector3(Screen.width - 200, 0, 0)).x)
                                {
                                    if (hitR1 == false)
                                    {
                                        transform.Translate(Vector2.right * Time.deltaTime * characterMoveSpeed);
                                    }

                                }
                            }
                        }
                        else
                        {
                            GetComponent<Animator>().Play("idle");

                        }
                    }


                // If player uses the left arrow or right arrow keys, they move the rightside up character.
                if (gameObject.tag == RIGHTSIDE_UP_CHARACTER_TAG)
                {

                    if (Input.GetKey(KeyCode.LeftArrow) || TouchLeftN())
                    {
                        GetComponent<Animator>().Play("runr");


                        if (transform.localScale.x > 0)
                        {
                            myShadow.transform.localPosition = new Vector3(myBody.transform.localPosition.x + .3f, myBody.transform.localPosition.y, myBody.transform.localPosition.z);

                            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        }
                        if (canMove == true)
                        {
                            if (hitL1 == false)
                            {
                                transform.Translate(Vector2.left * Time.deltaTime * characterMoveSpeed);
                            }

                        }
                        else
                        {
                            if (transform.position.x > thisOne.ScreenToWorldPoint(new Vector3(200, 0, 0)).x)
                            {
                                if (hitL1 == false)
                                {
                                    transform.Translate(Vector2.left * Time.deltaTime * characterMoveSpeed);
                                }

                            }
                        }
                    }
                    else if (Input.GetKey(KeyCode.RightArrow) || TouchRightN())
                    {
                        GetComponent<Animator>().Play("runr");

                        if (transform.localScale.x < 0)
                        {
                            myShadow.transform.localPosition = new Vector3(myBody.transform.localPosition.x - .35f, myBody.transform.localPosition.y, myBody.transform.localPosition.z);

                            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        }
                        if (canMove == true)
                        {
                            if (hitR1 == false)
                            {
                                transform.Translate(Vector2.right * Time.deltaTime * characterMoveSpeed);
                            }

                        }
                        else
                        {
                            if (transform.position.x < thisOne.ScreenToWorldPoint(new Vector3(Screen.width - 200, 0, 0)).x)
                            {
                                if (hitR1 == false)
                                {
                                    transform.Translate(Vector2.right * Time.deltaTime * characterMoveSpeed);
                                }
                            }
                        }
                    }
                    else
                    {
                        GetComponent<Animator>().Play("idler");
                    }
                }
            }
        }
        */
        public void allowedMvTrue()
    {
        allowedMv = true;
    }
    public void allowedMvFalse()
    {
        allowedMv = false;
    }
}
