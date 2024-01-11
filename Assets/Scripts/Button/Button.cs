using Assets.Scripts.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private string m_TypeUse;
    [SerializeReference] private IActivated[] lst_Iactivated;
    private Animator anim;
    private bool isTurnOn = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(m_TypeUse) && !isTurnOn)
        {
            anim.enabled = true;
            TurnOn();
            isTurnOn = true;
        }
    }
    private void TurnOn()
    {
        foreach(var obj in lst_Iactivated)
        {
            obj.Activate();
        }
    }
}
