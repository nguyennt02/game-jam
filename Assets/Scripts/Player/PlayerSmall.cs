using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmall : Player
{
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
}
