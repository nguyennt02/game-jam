using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class movement : MonoBehaviour
{
    public float speed;
    public float JumpForce = 50;
    public Vector2 Movement/*, poiterInput*/;
    public Rigidbody2D rb;
    public Animator anim;
    public bool isFacingRight;
    public bool jump;

    void Start()
    {
        speed = 5;
        JumpForce = 50;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jump = true;
    }

    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("speed", Movement.magnitude);

        if (isFacingRight == true && Movement.x == -1)
        {
            transform.localScale = new Vector3(-5f,5f,1f);
            isFacingRight = false;
        }

        if(isFacingRight == false && Movement.x == 1)
        {
            transform.localScale = new Vector3(5f,5f,1f);
            isFacingRight = true;
        }

        /*if (Input.GetKeyDown ("space") && jump == true)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        } */

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position+Movement*speed*Time.fixedDeltaTime); //movement
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water")
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0f;
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OnFloor")
        {
            jump = false;
        }
    }*/
}
