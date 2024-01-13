using Assets.Scripts.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Player : MonoBehaviour, IDie, ISpeed
{
    [SerializeField] protected string m_Tag;
    [SerializeField] protected float m_Speed;
    [SerializeField] protected float m_JumpForce;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Vector2 boxEdge;
    [SerializeField] protected GameObject[] lstParent;
    [Range(0, .3f)]
    [SerializeField] protected float movementSmoothing = .05f;

    protected Rigidbody2D rg2D;
    protected Animator anim;

    protected bool facingRight = true;
    protected Vector3 velocity = Vector3.zero;

    protected bool m_canMove = true;
    protected bool m_Grounded;


    protected virtual void Awake()
    {
        rg2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameObject.tag = m_Tag;
    }
    protected virtual void FixedUpdate()
    {
        CheckGrounded();
        Move();
    }

    protected virtual void Move()
    {
        if (m_canMove)
        {
            bool jump = GetJumping();
            float move = GetHorizontalMove() * m_Speed * Time.fixedDeltaTime;
            Moving(move);
            if(jump && m_Grounded)
            {
                Jumping();
            }
        }
    }
    protected virtual void CheckGrounded()
    {
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, boxEdge, 0, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        SetPlayerParent(collision.gameObject);
    }
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        SetPlayer(collision.gameObject);
    }
    protected virtual void SetPlayerParent(GameObject objectParent) {
        foreach (var obj in lstParent)
        {
            if (objectParent == obj)
            {
                transform.SetParent(obj.transform);
            }
        }
    }

    protected virtual void SetPlayer(GameObject objectParent)
    {
        foreach (var obj in lstParent)
        {
            if (objectParent == obj)
            {
                transform.SetParent(null);
            }
        }
    }

    protected virtual void Moving(float move)
    {
        anim.SetFloat("Move", Mathf.Abs(move));
        Vector3 targetVelocity = new Vector2(move * 10f, rg2D.velocity.y);
        rg2D.velocity = Vector3.SmoothDamp(rg2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }
    protected virtual void Jumping()
    {
        anim.SetTrigger("Jump");
        rg2D.AddForce(new Vector2(0f, m_JumpForce));
    }
    protected virtual void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    protected abstract float GetHorizontalMove();
    protected abstract bool GetJumping();

    public virtual void Die()
    {
        anim.SetTrigger("Dead");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public virtual void SetSpeed(float speed)
    {
        m_Speed *= speed;
    }
}
