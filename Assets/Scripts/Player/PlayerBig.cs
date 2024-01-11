using Assets.Scripts.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBig : Player
{
   protected override float GetHorizontalMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            return 1;
        else
            return 0;
    }

    protected override bool GetJumping()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }
}
