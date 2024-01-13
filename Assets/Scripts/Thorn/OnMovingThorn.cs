using Assets.Scripts.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMovingThorn : OnMovingFlatform
{
    private bool m_IsMoving = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetPlayer(collision.GetComponent<Player>()) && m_IsMoving)
        {
            Activate();
            m_IsMoving = false;
        }
    }
    private bool GetPlayer(Player player)
    {
        if(player == null) return false;
        return player is Player ? true : false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<IDie>();
        if (GetPlayer(player as Player))
        {
            player.Die();
        }
    }
}
