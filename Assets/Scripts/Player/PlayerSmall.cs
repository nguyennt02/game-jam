using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmall : Player
{
    [SerializeField] private GameObject[] lstParent;
    protected override float GetHorizontalMove()
    {
        if (Input.GetKey(KeyCode.A))
            return -1;
        else if (Input.GetKey(KeyCode.D))
            return 1;
        else
            return 0;
    }

    protected override bool GetJumping()
    {
        return Input.GetKeyDown(KeyCode.W);
    }
    protected override void SetPlayerSmall(GameObject objectParent)
    {
        foreach (var obj in lstParent)
        {
            if (objectParent == obj)
            {
                transform.SetParent(obj.transform);
            }
            else
            {
                transform.SetParent(null);
            }
        }
    }
}
