using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagTrack : MonoBehaviour
{
    public Vector3 lastClamp;
    public bool upsideFl = false;
    public Vector3Int lastFlag;
    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player1.activeSelf && !player2.activeSelf)
        {
            loadFlag();
        }
    }
    public void loadFlag()
    {
        //player1.GetComponent<checkTriggerTile>().myp.Clear();
        //player1.GetComponent<checkTriggerTile>().myl.Clear();
        //player2.GetComponent<checkTriggerTile>().myp.Clear();
        //player2.GetComponent<checkTriggerTile>().myl.Clear();

        player1.SetActive(true);
        player2.SetActive(true);

        if (upsideFl == true)
        {
            if (Vector3.Distance(player1.transform.position, lastFlag) > 4 || Vector3.Distance(player2.transform.position, lastFlag) > 4)
            {
                player1.transform.position = new Vector3(lastFlag.x, lastFlag.y, player1.transform.position.z);
                player2.transform.position = new Vector3(lastFlag.x, lastFlag.y - 1, player2.transform.position.z);
            }
        }
        else
        {
            if (Vector3.Distance(player1.transform.position, lastFlag) > 4 || Vector3.Distance(player2.transform.position, lastFlag) > 4)
            {
                player2.transform.position = new Vector3(lastFlag.x, lastFlag.y + 1, player2.transform.position.z);
                player1.transform.position = new Vector3(lastFlag.x, lastFlag.y + 2, player1.transform.position.z);

            }
        }
        GetComponent<camFollow>().changeClamp2(lastClamp);
    }
    public void updateFlag(Vector3Int currFlag,bool dir)
    {
        if (!player1.activeSelf)
        {
            if (upsideFl == true)
            {
                player1.transform.position = new Vector3(lastFlag.x, lastFlag.y - 1, player1.transform.position.z);
            }
            else
            {
                player1.transform.position = new Vector3(lastFlag.x, lastFlag.y + 1, player1.transform.position.z);
            }

        }
        if (!player2.activeSelf)
        {
            if (upsideFl == true)
            {
                player2.transform.position = new Vector3(lastFlag.x, lastFlag.y + 1, player2.transform.position.z);
            }
            else
            {
                player2.transform.position = new Vector3(lastFlag.x, lastFlag.y -1, player2.transform.position.z);
            }

        }
        upsideFl = dir;
        lastFlag = currFlag;
        lastClamp = GetComponent<camFollow>().myclamp;
    }
}
