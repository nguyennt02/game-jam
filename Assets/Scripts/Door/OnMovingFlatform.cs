using Assets.Scripts.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMovingFlatform : MonoBehaviour, IActivated
{
    [SerializeField] protected MovingFlatfrom m_Flatform;

    public virtual void Activate()
    {
        StartCoroutine(Enabled());
    }
    protected virtual IEnumerator Enabled()
    {
        yield return new WaitForSeconds(1f);
        m_Flatform.enabled = true;
    }
}
