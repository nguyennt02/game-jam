using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMovingBoat : OnMovingFlatform
{
    [SerializeField] private string m_TypeUse;
    [SerializeField] private string m_TypeNotUse;
    private bool m_IsFall = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(m_TypeUse))
        {
            Activate();
        }
        else if (collision.collider.CompareTag(m_TypeNotUse) && m_IsFall)
        {
            m_IsFall = false;
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(1f);

        float elapsed = 0f;
        float duration = 2f;

        Vector3 from = transform.position;
        Vector3 to = new Vector3(transform.position.x, transform.position.y - 1);

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
