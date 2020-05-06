using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    public GameObject play1;
    public GameObject play2;
    public float smoothTime = 0.3F;
    public Vector3 myclamp;
    public Vector2Int lim;
    bool hu = false;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
       myclamp =  transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ch;
        //  if (Vector3.Distance(play1.transform.position, play2.transform.position) < 10)
        // {
        if (hu == false)
        {
            ch = Vector3.SmoothDamp(transform.position, new Vector3(((play2.transform.position.x + play1.transform.position.x) / 2), ((play2.transform.position.y + play1.transform.position.y) / 2), -10), ref velocity, smoothTime);
            if (ch.x < myclamp.x + lim.x && ch.x > myclamp.x - lim.x)
            {
                transform.position = new Vector3(ch.x, transform.position.y, transform.position.z);
            }
            if (ch.y < myclamp.y + lim.y && ch.y > myclamp.y - lim.y)
            {
                transform.position = new Vector3(transform.position.x, ch.y, transform.position.z);

            }

        }
        else
        {

             if (Vector3.Distance(transform.position, myclamp) > .1f)
            {
               transform.position =  Vector3.SmoothDamp(transform.position, myclamp, ref velocity, smoothTime/2);
            }
            else
            {
                hu = false;
            }

        }
        // }
        //  else
        //  {
        //ch = Vector3.SmoothDamp(transform.position, new Vector3(play1.transform.position.x ,play1.transform.position.y, -10), ref velocity, smoothTime);
        //  }


    }
    public void changeClamp(Vector2Int myC)
    {
//        print("W");
        myclamp.x += myC.x * 20;
        myclamp.y += myC.y * 20;
        hu = true;
        //transform.position = myclamp;
    }
    public void changeClamp2(Vector3 myC)
    {
        print("W");
        myclamp.x = myC.x ;
        myclamp.y = myC.y ;
        hu = true;
        //transform.position = myclamp;
    }
}
