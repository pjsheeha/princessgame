using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class checkTriggerTile : MonoBehaviour
{
    public LayerMask layer_mask;
    public LayerMask flag_layer_mask;
    public LayerMask death_layer_mask;
    public LayerMask treasure_layer_mask;
    public LayerMask switches_layer_mask;

    public GameObject otherPlayer;
    public Tilemap m;
    public Tilemap f;
    public Tilemap d;
    public Tilemap t;
    public Tilemap s;

    public List<TileBase> death = new List<TileBase>();
    public List<TileBase> flags = new List<TileBase>();
    public List<TileBase> treasures = new List<TileBase>();
    public List<TileBase> switches = new List<TileBase>();

    public List<TileBase> myList = new List<TileBase>();
    public List<Vector3Int> myl = new List<Vector3Int>();
    public List<float> myp = new List<float>();
    public float vSpeed = 0;
    public bool goingthru = false;
    public bool upside = false;
    public camFollow myCa;
    public flagTrack myFl;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void checkColDeath(Vector2 m_GroundCheck, Vector2 k_GroundedRadius)
    {
        if (Physics2D.OverlapBox(m_GroundCheck, k_GroundedRadius, 0, death_layer_mask.value))
        {
            GridLayout gridLayout = d.GetComponentInParent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
            if (d.GetTile(cellPosition) != null)
            {
                print(d.GetTile(cellPosition).name);
                
                if (death.Contains(d.GetTile(cellPosition))) {
                    print("DEATHDEATH");
                    myl.Clear();
                    myp.Clear();
                    gameObject.SetActive(false);
                    
                }
            }
        }

    }

    public void checkColFlag(Vector2 m_GroundCheck, Vector2 k_GroundedRadius)
    {
        if (Physics2D.OverlapBox(m_GroundCheck, k_GroundedRadius, 0, flag_layer_mask.value))
        {
            GridLayout gridLayout = f.GetComponentInParent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
            if (f.GetTile(cellPosition) != null)
            {
                print(f.GetTile(cellPosition).name);
                if (f.GetTile(cellPosition).name == flags[0].name)
                {
                    f.SetTile(cellPosition, flags[1]);
                    f.SetTileFlags(cellPosition, TileFlags.None);
                    if (myFl.lastFlag != Vector3Int.zero)
                    {
                        if (myFl.upsideFl == true)
                        {
                            f.SetTile(myFl.lastFlag, flags[2]);

                        }
                        else
                        {
                            f.SetTile(myFl.lastFlag, flags[0]);

                        }
                        f.SetTileFlags(myFl.lastFlag, TileFlags.None);

                    }
                    else
                    {
                        // f.SetTile(myFl.lastFlag, flags[0]);

                    }
                    myFl.updateFlag(cellPosition, false);

                }
                if (f.GetTile(cellPosition).name == flags[2].name)
                {
                    f.SetTile(cellPosition, flags[3]);
                    f.SetTileFlags(cellPosition, TileFlags.None);
                    if (myFl.lastFlag != Vector3Int.zero)
                    {
                        if (myFl.upsideFl == true)
                        {
                            f.SetTile(myFl.lastFlag, flags[2]);

                        }
                        else
                        {
                            f.SetTile(myFl.lastFlag, flags[0]);

                        }
                        //f.SetTileFlags(myFl.lastFlag, TileFlags.None);

                    }
                    myFl.updateFlag(cellPosition, true);

                }
                myFl.loadFlag();
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        vSpeed = GetComponent<Rigidbody2D>().velocity.y;
        if (upside == false)
        {
            if (vSpeed > 0)
            {
                vSpeed = 1;
            }
            if (vSpeed < 0)
            {
                vSpeed = -1;
            }
            if (vSpeed == 0)
            {
                vSpeed = 0;
            }
        }
        else
        {
            if (vSpeed > 0)
            {
                vSpeed = -1;
            }
            if (vSpeed < 0)
            {
                vSpeed = 1;
            }
            if (vSpeed == 0)
            {
                vSpeed = 0;
            }
        }
        Vector2 m_GroundCheck = new Vector2(transform.position.x - .5f, transform.position.y);
        Vector2 k_GroundedRadius = new Vector2(.01f, .5f);
        m_GroundCheck = new Vector2(transform.position.x, transform.position.y);
        k_GroundedRadius = new Vector2(.5f, .5f);
        checkColFlag(m_GroundCheck, k_GroundedRadius);
        checkColDeath(m_GroundCheck, k_GroundedRadius);
        if (Physics2D.OverlapBox(m_GroundCheck, k_GroundedRadius, 0, layer_mask.value))
        {
            //  print("hitsomething");

            GridLayout gridLayout = m.GetComponentInParent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
            //print(m.GetTile(cellPosition));
            if (m.GetTile(cellPosition) != null )
            {
                Vector3Int cellPositiona = new Vector3Int(cellPosition.x, 0, 0);
                List<Vector3Int> cellsa = new List<Vector3Int>();
                if (m.GetTile(cellPosition).name == myList[0].name && GetComponent<CharacterMove>().horizontalMove < 0 && (!myl.Contains(cellPositiona) || myp[myl.IndexOf(cellPositiona)] != GetComponent<CharacterMove>().horizontalMove) && (!otherPlayer.GetComponent<checkTriggerTile>().myl.Contains(cellPositiona) || otherPlayer.GetComponent<checkTriggerTile>().myp[otherPlayer.GetComponent<checkTriggerTile>().myl.IndexOf(cellPositiona)] != GetComponent<CharacterMove>().horizontalMove))
                {
                    print("4OK");

                    //if (otherPlayer.GetComponent<checkTriggerTile>().goingthru == false)
                  //  {
                        myl.Clear();
                        myp.Clear();
                        otherPlayer.GetComponent<checkTriggerTile>().myl.Clear();
                        otherPlayer.GetComponent<checkTriggerTile>().myp.Clear();

                        myCa.changeClamp(new Vector2Int(-1, 0));
                    
                        
                    if (Vector3.Distance(otherPlayer.transform.position, transform.position)> 5)
                    {
                        if (otherPlayer.GetComponent<checkTriggerTile>().upside == true)
                        {
                            otherPlayer.transform.position = new Vector3(transform.position.x - .5f, cellPosition.y-.5f, 0);
                        }
                        else if (otherPlayer.GetComponent<checkTriggerTile>().upside == false)
                        {
                            otherPlayer.transform.position = new Vector3(transform.position.x - .5f, cellPosition.y-.5f, 0);

                        }
                    }
                    goingthru = true;
                    myl.Add(cellPositiona);
                    myp.Add(GetComponent<CharacterMove>().horizontalMove);
                     otherPlayer.GetComponent<checkTriggerTile>().myl.Add(cellPositiona);
                    otherPlayer.GetComponent<checkTriggerTile>().myp.Add(GetComponent<CharacterMove>().horizontalMove);



                 //   }
                }
               // if (myl.Contains(cellPositiona))
               // {
                    if (m.GetTile(cellPosition).name == myList[0].name && GetComponent<CharacterMove>().horizontalMove > 0 && (!myl.Contains(cellPositiona) || myp[myl.IndexOf(cellPositiona)] != GetComponent<CharacterMove>().horizontalMove) && (!otherPlayer.GetComponent<checkTriggerTile>().myl.Contains(cellPositiona) || otherPlayer.GetComponent<checkTriggerTile>().myp[otherPlayer.GetComponent<checkTriggerTile>().myl.IndexOf(cellPositiona)] != GetComponent<CharacterMove>().horizontalMove))
                    {
                        print("1OK");
 
                      //  if (otherPlayer.GetComponent<checkTriggerTile>().goingthru == false)
                      //  {
                            myl.Clear();
                            myp.Clear();
                         otherPlayer.GetComponent<checkTriggerTile>().myl.Clear();
                         otherPlayer.GetComponent<checkTriggerTile>().myp.Clear();

                            myCa.changeClamp(new Vector2Int(1, 0));
                        if (Vector3.Distance(otherPlayer.transform.position, transform.position) > 5)
                        {
                            if (otherPlayer.GetComponent<checkTriggerTile>().upside == true)
                            {
                                otherPlayer.transform.position = new Vector3(transform.position.x + .5f, cellPosition.y+.5f, 0);
                            }
                            else if (otherPlayer.GetComponent<checkTriggerTile>().upside == false)
                            {
                                otherPlayer.transform.position = new Vector3(transform.position.x + .5f, cellPosition.y+.5f, 0);

                            }
                        }
                        goingthru = true;
                    myl.Add(cellPositiona);
                    myp.Add(GetComponent<CharacterMove>().horizontalMove);
                    otherPlayer.GetComponent<checkTriggerTile>().myl.Add(cellPositiona);
                        otherPlayer.GetComponent<checkTriggerTile>().myp.Add(GetComponent<CharacterMove>().horizontalMove);
                    }
               //. }
                if (m.GetTile(cellPosition).name == myList[2].name && GetComponent<CharacterMove>().horizontalMove > 0 && (!myl.Contains(cellPositiona) || myp[myl.IndexOf(cellPositiona)] != GetComponent<CharacterMove>().horizontalMove) && (!otherPlayer.GetComponent<checkTriggerTile>().myl.Contains(cellPositiona) || otherPlayer.GetComponent<checkTriggerTile>().myp[otherPlayer.GetComponent<checkTriggerTile>().myl.IndexOf(cellPositiona)] != GetComponent<CharacterMove>().horizontalMove))
                {
                    print("2OK");

                   // if (otherPlayer.GetComponent<checkTriggerTile>().goingthru == false)
                   // {
                        myl.Clear();
                        otherPlayer.GetComponent<checkTriggerTile>().myl.Clear();
                        otherPlayer.GetComponent<checkTriggerTile>().myp.Clear();
                    
                        myp.Clear();

                        myCa.changeClamp(new Vector2Int(1, 0));
                    if (Vector3.Distance(otherPlayer.transform.position, transform.position) > 5)
                    {
                        if (otherPlayer.GetComponent<checkTriggerTile>().upside == true)
                        {
                            otherPlayer.transform.position = new Vector3(transform.position.x + .5f, cellPosition.y+.5f, 0);
                        }
                        else if (otherPlayer.GetComponent<checkTriggerTile>().upside == false)
                        {
                            otherPlayer.transform.position = new Vector3(transform.position.x + .5f, cellPosition.y+.5f, 0);

                        }
                    }
                    myl.Add(cellPositiona);
                    goingthru = true;

                    myp.Add(GetComponent<CharacterMove>().horizontalMove);
                       otherPlayer.GetComponent<checkTriggerTile>().myl.Add(cellPositiona);
                       otherPlayer.GetComponent<checkTriggerTile>().myp.Add(GetComponent<CharacterMove>().horizontalMove);
                   // }
                    
                }
                //if (myl.Contains(cellPositiona))
                //{
                    if (m.GetTile(cellPosition).name == myList[2].name && GetComponent<CharacterMove>().horizontalMove < 0 && (!myl.Contains(cellPositiona) || myp[myl.IndexOf(cellPositiona)] != GetComponent<CharacterMove>().horizontalMove) && (!otherPlayer.GetComponent<checkTriggerTile>().myl.Contains(cellPositiona) || otherPlayer.GetComponent<checkTriggerTile>().myp[otherPlayer.GetComponent<checkTriggerTile>().myl.IndexOf(cellPositiona)] != GetComponent<CharacterMove>().horizontalMove))
                {
                        print("3OK");

                        //if (otherPlayer.GetComponent<checkTriggerTile>().goingthru == false)
                        //{
                            myl.Clear();
                            myp.Clear();
                         otherPlayer.GetComponent<checkTriggerTile>().myl.Clear();
                         otherPlayer.GetComponent<checkTriggerTile>().myp.Clear();

                            myCa.changeClamp(new Vector2Int(-1, 0));
                        if (Vector3.Distance(otherPlayer.transform.position, transform.position) > 5)
                        {
                            if (otherPlayer.GetComponent<checkTriggerTile>().upside == true)
                            {
                                otherPlayer.transform.position = new Vector3(transform.position.x -.5f, cellPosition.y-.5f, 0);
                            }
                            else if(otherPlayer.GetComponent<checkTriggerTile>().upside == false)
                            
                                {
                                otherPlayer.transform.position = new Vector3(transform.position.x-.5f, cellPosition.y-.5f, 0);

                            }
                        }

                        goingthru = true;
                    myl.Add(cellPositiona);
                    myp.Add(GetComponent<CharacterMove>().horizontalMove);
                    otherPlayer.GetComponent<checkTriggerTile>().myl.Add(cellPositiona);
                        otherPlayer.GetComponent<checkTriggerTile>().myp.Add(GetComponent<CharacterMove>().horizontalMove);
                        // }
                        //myl.Add(cellPositiona);
                        // myp.Add(GetComponent<CharacterMove>().horizontalMove);
                   // }
                }
                ///kn
                ///
                Vector3Int cellPositionb = new Vector3Int(0, cellPosition.y, 0);

                if (m.GetTile(cellPosition).name == myList[1].name && vSpeed > 0 && (!myl.Contains(cellPositionb) || myp[myl.IndexOf(cellPositionb)] != vSpeed) && (!otherPlayer.GetComponent<checkTriggerTile>().myl.Contains(cellPositionb) || otherPlayer.GetComponent<checkTriggerTile>().myp[otherPlayer.GetComponent<checkTriggerTile>().myl.IndexOf(cellPositionb)] != vSpeed))
                {
                    print("2OK");
                    myl.Clear();
                    myp.Clear();
                    otherPlayer.GetComponent<checkTriggerTile>().myl.Clear();
                    otherPlayer.GetComponent<checkTriggerTile>().myp.Clear();

                        myCa.changeClamp(new Vector2Int(0, 1));
                    if (Vector3.Distance(otherPlayer.transform.position, transform.position) > 1)
                    {
                        
                        if (otherPlayer.GetComponent<checkTriggerTile>().upside == true)
                        {
                            otherPlayer.transform.position = new Vector3(cellPosition.x, transform.position.y - 1f, 0);
                        }
                        else
                        {
                            otherPlayer.transform.position = new Vector3(cellPosition.x, transform.position.y+.5f, 0);

                        }
                        
                    }

                    myl.Add(cellPositionb);
                    myp.Add(vSpeed);
                  //  otherPlayer.GetComponent<checkTriggerTile>().myl.Add(cellPositionb);
                  //  otherPlayer.GetComponent<checkTriggerTile>().myp.Add(vSpeed);
                    goingthru = true;

                }
               // if (myl.Contains(cellPositionb))
               // {
                    if (m.GetTile(cellPosition).name == myList[1].name && vSpeed < 0 && (!myl.Contains(cellPositionb) || myp[myl.IndexOf(cellPositionb)] != vSpeed) && (!otherPlayer.GetComponent<checkTriggerTile>().myl.Contains(cellPositionb) || otherPlayer.GetComponent<checkTriggerTile>().myp[otherPlayer.GetComponent<checkTriggerTile>().myl.IndexOf(cellPositionb)] != vSpeed))
                    {
                        print("3OK");
                        myl.Clear();
                        myp.Clear();
                        otherPlayer.GetComponent<checkTriggerTile>().myl.Clear();
                        otherPlayer.GetComponent<checkTriggerTile>().myp.Clear();
                        if (Vector3.Distance(otherPlayer.transform.position, transform.position) > 1)
                        {
                        
                            if (otherPlayer.GetComponent<checkTriggerTile>().upside == true)
                            {
                                otherPlayer.transform.position = new Vector3(cellPosition.x, transform.position.y - 1f, 0);
                            }
                            else
                            {
                                otherPlayer.transform.position = new Vector3(cellPosition.x, transform.position.y + .5f, 0);

                            }
                            
                        }
                        myCa.changeClamp(new Vector2Int(0, -1));
                    myl.Add(cellPositionb);
                    myp.Add(vSpeed);
                    otherPlayer.GetComponent<checkTriggerTile>().myl.Add(cellPositionb);
                        otherPlayer.GetComponent<checkTriggerTile>().myp.Add(vSpeed);
                        goingthru = true;



                        //myl.Add(cellPosition);
                        //myp.Add(GetComponent<CharacterMove>().horizontalMove);
                    }
                //}
                ///

                ///kn
                ///
                
                ///kn
                ///
                if (m.GetTile(cellPosition).name == myList[3].name && vSpeed < 0 && (!myl.Contains(cellPositionb) || myp[myl.IndexOf(cellPositionb)] != vSpeed) && (!otherPlayer.GetComponent<checkTriggerTile>().myl.Contains(cellPositionb) || otherPlayer.GetComponent<checkTriggerTile>().myp[otherPlayer.GetComponent<checkTriggerTile>().myl.IndexOf(cellPositionb)] != vSpeed))
                {
                    print("1OK");
                    myl.Clear();
                    myp.Clear();
                    otherPlayer.GetComponent<checkTriggerTile>().myl.Clear();
                    otherPlayer.GetComponent<checkTriggerTile>().myp.Clear();
                    myCa.changeClamp(new Vector2Int(0, -1));
                        myl.Add(cellPositionb);
                        myp.Add(vSpeed);
                    otherPlayer.GetComponent<checkTriggerTile>().myl.Add(cellPositionb);
                    otherPlayer.GetComponent<checkTriggerTile>().myp.Add(vSpeed);
                    if (Vector3.Distance(otherPlayer.transform.position, transform.position) > 1)
                    {
                        
                        if (upside == true)
                        {
                            otherPlayer.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, 0);
                        }
                        else
                        {
                            otherPlayer.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0);

                        }


                    }
                    goingthru = true;


                }
              //  if (myl.Contains(cellPositionb))
               // {
                    if (m.GetTile(cellPosition).name == myList[3].name && vSpeed > 0 && (!(myl.Contains(cellPositionb)) || myp[myl.IndexOf(cellPositionb)] != vSpeed) && (!otherPlayer.GetComponent<checkTriggerTile>().myl.Contains(cellPositionb) || otherPlayer.GetComponent<checkTriggerTile>().myp[otherPlayer.GetComponent<checkTriggerTile>().myl.IndexOf(cellPositionb)] != vSpeed))
                    {
                        print("4OK");
                        myl.Clear();
                        myp.Clear();
                        otherPlayer.GetComponent<checkTriggerTile>().myl.Clear();
                        otherPlayer.GetComponent<checkTriggerTile>().myp.Clear();
 
                            myCa.changeClamp(new Vector2Int(0, 1));
                    myl.Add(cellPositionb);
                    myp.Add(vSpeed);
                    otherPlayer.GetComponent<checkTriggerTile>().myl.Add(cellPositionb);
                        otherPlayer.GetComponent<checkTriggerTile>().myp.Add(vSpeed);
                        if (Vector3.Distance(otherPlayer.transform.position, transform.position) >1)
                        {

                        if (upside == true)
                        {
                            otherPlayer.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, 0);
                        }
                        else
                        {
                            otherPlayer.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0);

                        }


                    }
                        //otherPlayer.transform.posit
                        //myl.Add(cellPosition);
                        //myp.Add(GetComponent<CharacterMove>().horizontalMove);
                        goingthru = true;

                    }
               // }
                ///

                //  print(m.GetTile(cellPosition));
            }
            else
            {
                //myl.Clear();
                //myp.Clear();
            }

        }
        else
        {
            goingthru = false;

            //  print("nope");

        }

    }

}
