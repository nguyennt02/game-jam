using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMovingBoat : OnMovingFlatform
{
    [SerializeField] private bool m_PlayerSmall;
    [SerializeField] private bool m_PlayerBig;
    private bool m_IsFall = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_PlayerBig && m_PlayerSmall)
        {
            if (collision.collider.CompareTag("PlayerSmall") || collision.collider.CompareTag("PlayerBig"))
            {
                m_IsFall = true;
                Activate();
                m_IsFall = false;
            }
        }
        else if (m_PlayerBig)
        {
            if (collision.collider.CompareTag("PlayerBig"))
            {
                m_IsFall = true;
                Activate();
                m_IsFall = false;
            }
            else if (collision.collider.CompareTag("PlayerSmall"))
            {
                StartCoroutine(Fall());
            }
        }
        else if(m_PlayerSmall)
        {
            if (collision.collider.CompareTag("PlayerSmall"))
            {
                m_IsFall = true;
                Activate();
                m_IsFall = false;
            }
            else if (collision.collider.CompareTag("PlayerBig"))
            {
                StartCoroutine(Fall());
            }
        }
    }

    public override void Activate()
    {
        base.Activate();
        if (!m_IsFall)
        {
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        m_IsFall = false;
        yield return new WaitForSeconds(0.2f);

        float elapsed = 0f;
        float duration = 1f;

        Vector3 from = transform.position;
        Vector3 to = new Vector3(transform.position.x, transform.position.y - 2);

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        StopCoroutine(Fall());
    }
}
