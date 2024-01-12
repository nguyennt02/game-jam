using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatfrom : MonoBehaviour
{
    [SerializeField] private Transform[] arr_PoinMove;
    [SerializeField] private float m_Speed;
    [SerializeField] private bool isMove;
    private Vector2 m_PoinNext;
    private int direction = 1;
    private int index = 1;
    private void Start()
    {
        m_PoinNext = (Vector2)arr_PoinMove[index].position;
    }
    private void Update()
    {
        Debug.Log(m_PoinNext);
        Moving();
    }
    private void Moving()
    {
        transform.position = Vector2.Lerp(transform.position, m_PoinNext, m_Speed * Time.deltaTime);
        Navigation(m_PoinNext);
    }
    private void Navigation(Vector2 target)
    {
        float distance = (target - (Vector2)transform.position).magnitude;
        if (distance > 0.1f) return;
        if (index == 0 || index == arr_PoinMove.Length - 1)
        {
            if (isMove)
            {
                this.enabled = false;
            }
            direction *= -1;
        }
        PoinNext();
    }
    private void PoinNext()
    {
        if(direction == 1)
        {
            index++;
        }
        else if(direction == -1)
        {
            index--;
        }
        m_PoinNext = (Vector2)arr_PoinMove[index].position;
    }
}
