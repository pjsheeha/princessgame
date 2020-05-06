using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagchange : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void meActivate()
    {
        GetComponent<Animator>().Play("down");
    }
    public void meDeactivate()
    {
        GetComponent<Animator>().Play("up");

    }
}
