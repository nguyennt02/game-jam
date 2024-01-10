using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{
    public float speed;
    private float distance;
    public GameObject start_1;
    public GameObject start_2;
    Vector2 movement;
    public Rigidbody2D rb;
    public bool start1;
    public bool onTheBoat;

    // Start is called before the first frame update
    void Start()
    {
        start1 = true;
        onTheBoat = false;
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (onTheBoat == true)
        {
            moveBoat();
            speed = 1f;
        }
        if (onTheBoat == false)
        {
            speed = 0f;
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "playerSmall")
        {         
            onTheBoat = true;
        }
        else
        {
            onTheBoat = false;
        }

        if (collision.tag == "Player")
        {
            flipBoat();
        }

        if (collision.CompareTag("start1"))
        {
            start1 = true;
        }

        if (collision.CompareTag("start2"))
        {
            start1 = false;
        }
    }

    private void moveBoat()
    {
        if(start1 == true)
        {
            distance = Vector2.Distance(transform.position, start_2.transform.position);
            Vector2 direction = start_2.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(this.transform.position, start_2.transform.position, speed * Time.deltaTime);
        }
        else if (start1 == false)
        {
            distance = Vector2.Distance(transform.position, start_1.transform.position);
            Vector2 direction = start_1.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(this.transform.position, start_1.transform.position, speed * Time.deltaTime);
        }
    }

    private void flipBoat()
    {
        Destroy(this.gameObject);
    }
}
