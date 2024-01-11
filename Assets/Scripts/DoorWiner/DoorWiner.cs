using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWiner : MonoBehaviour
{
    [SerializeField] private string m_TypeUse;
    [NonSerialized] public bool isOpenDoor = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(m_TypeUse))
        {
            SetOpenDoor(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(m_TypeUse))
        {
            SetOpenDoor(false);
        }
    }
    private void SetOpenDoor(bool isOpenDoor)
    {
        this.isOpenDoor = isOpenDoor;
    }
}
