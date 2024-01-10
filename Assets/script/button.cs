using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public Animator anim;
    public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        wall.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "playerSmall")
        {           
            IActivated();
        }
    }

    private void IActivated()
    {
        wall.SetActive(false);
        anim.SetTrigger("turn on");
    }
}
